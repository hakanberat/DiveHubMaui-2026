namespace DiveHubMauiApp.Models;
public sealed record DiveStatistics(int Count,long Minutes,double MaximumDepth);
public static class LogbookQuery
{
 public static List<DiveRecord> Apply(IEnumerable<DiveRecord> dives,string? search,string sort)
 {
  var term=search?.Trim()??"";
  var query=dives.Where(x=>term.Length==0||$"{x.DiveNumber} {x.DiveSite} {x.DiveBuddy} {x.DiveLeader} {x.Notes} {x.DateLabel}".Contains(term,StringComparison.OrdinalIgnoreCase));
  return (sort switch{
   "Oldest first"=>query.OrderBy(x=>x.Date).ThenBy(x=>x.DiveNumber),
   "Dive number ↓"=>query.OrderByDescending(x=>x.DiveNumber),
   "Dive number ↑"=>query.OrderBy(x=>x.DiveNumber),
   _=>query.OrderByDescending(x=>x.Date).ThenByDescending(x=>x.DiveNumber)
  }).ToList();
 }
 public static DiveStatistics Statistics(IReadOnlyCollection<DiveRecord> dives)=>new(dives.Count,dives.Sum(x=>(long)x.Duration),dives.Count>0?dives.Max(x=>x.MaxDepth):0);
}