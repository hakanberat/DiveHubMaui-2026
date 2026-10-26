namespace DiveHubMauiApp.Components;
public partial class ContentCard:ContentView
{
 public event EventHandler? Invoked;
 public ContentCard(){InitializeComponent();}
 private void ExploreClicked(object? sender,EventArgs e)=>Invoked?.Invoke(this,EventArgs.Empty);
}
