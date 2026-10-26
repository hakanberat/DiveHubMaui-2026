using DiveHubMauiApp.Models;
using SQLite;
namespace DiveHubMauiApp.Data;
public interface IDiveRepository
{
    Task<List<DiveRecord>> GetAllAsync();
    Task<DiveRecord?> GetAsync(int id);
    Task SaveAsync(DiveRecord dive);
    Task DeleteAsync(int id);
}
public sealed class DiveRepository(string path) : IDiveRepository, IAsyncDisposable
{
    private readonly SemaphoreSlim gate = new(1, 1);
    private SQLiteAsyncConnection? database;
    private async Task<SQLiteAsyncConnection> OpenAsync()
    {
        if (database is not null) return database;
        var connection = new SQLiteAsyncConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex, storeDateTimeAsTicks: true);
        await connection.ExecuteScalarAsync<int>("PRAGMA busy_timeout = 5000");
        await connection.ExecuteScalarAsync<string>("PRAGMA journal_mode = WAL");
        var version = await connection.ExecuteScalarAsync<int>("PRAGMA user_version");
        if (version > 1) { await connection.CloseAsync(); throw new InvalidOperationException("This logbook was created by a newer app. Update Dive Hub before opening it."); }
        await connection.CreateTableAsync<DiveRecord>();
        foreach (var operation in new[] { "INSERT", "UPDATE" })
            await connection.ExecuteAsync($"""
                CREATE TRIGGER IF NOT EXISTS validate_dive_{operation.ToLowerInvariant()} BEFORE {operation} ON Dives
                WHEN NEW.DiveNumber < 1 OR NEW.DiveNumber > 999999 OR length(trim(NEW.DiveSite)) = 0
                  OR NEW.MaxDepth <= 0 OR NEW.MaxDepth > 400 OR NEW.Duration < 1 OR NEW.Duration > 1440
                  OR NEW.StartingPressure < 1 OR NEW.StartingPressure > 400
                  OR NEW.EndingPressure < 0 OR NEW.EndingPressure > NEW.StartingPressure
                  OR NEW.Weight < 0 OR NEW.Weight > 100
                BEGIN SELECT RAISE(ABORT, 'Invalid dive record'); END
                """);
        await connection.ExecuteAsync("PRAGMA user_version = 1");
        database = connection;
        return connection;
    }
    private async Task<T> UseAsync<T>(Func<SQLiteAsyncConnection, Task<T>> operation)
    {
        await gate.WaitAsync();
        try { return await operation(await OpenAsync()); }
        finally { gate.Release(); }
    }
    public Task<List<DiveRecord>> GetAllAsync() => UseAsync(db => db.Table<DiveRecord>().OrderByDescending(d => d.Date).ThenByDescending(d => d.DiveNumber).ToListAsync());
    public Task<DiveRecord?> GetAsync(int id) => UseAsync<DiveRecord?>(async db => await db.FindAsync<DiveRecord>(id));
    public async Task SaveAsync(DiveRecord dive)
    {
        DiveValidation.Validate(dive);
        dive.DiveSite = dive.DiveSite.Trim();
        dive.Date = dive.Date.Date;
        await UseAsync(async db =>
        {
            var duplicate = await db.Table<DiveRecord>().Where(d => d.DiveNumber == dive.DiveNumber && d.Id != dive.Id).CountAsync();
            if (duplicate != 0) throw new ArgumentException($"Dive number {dive.DiveNumber} already exists. Choose a different number.");
            try
            {
                if (dive.Id == 0) return await db.InsertAsync(dive);
                if (await db.UpdateAsync(dive) == 0) throw new InvalidOperationException("This dive no longer exists. Return to the logbook and add it again.");
                return 1;
            }
            catch (SQLiteException ex) when (ex.Result == SQLite3.Result.Constraint)
            { throw new ArgumentException("The dive could not be saved. Check the number and field values.", ex); }
        });
    }
    public async Task DeleteAsync(int id) => await UseAsync(db => db.DeleteAsync<DiveRecord>(id));
    public async ValueTask DisposeAsync()
    {
        await gate.WaitAsync();
        try { if (database is not null) { await database.CloseAsync(); database = null; } }
        finally { gate.Release(); }
    }
}


