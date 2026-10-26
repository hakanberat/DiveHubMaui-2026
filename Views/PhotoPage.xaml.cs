using DiveHubMauiApp.ViewModels;
namespace DiveHubMauiApp.Views;
public partial class PhotoPage:ContentPage,IQueryAttributable
{
 private readonly PhotoViewModel vm;
 private string photoId="";
 public PhotoPage(PhotoViewModel vm){InitializeComponent();BindingContext=this.vm=vm;vm.PropertyChanged+=async(_,e)=>{if(e.PropertyName==nameof(vm.Photo)){PhotoImage.Opacity=0.4;await PhotoImage.FadeToAsync(1,160);}};}
 public void ApplyQueryAttributes(IDictionary<string,object> query){if(query.TryGetValue("photoId",out var id))photoId=id.ToString()??"";}
 protected override async void OnAppearing(){base.OnAppearing();await vm.LoadAsync(photoId);}
 private async void CloseClicked(object? sender,EventArgs e){try{await Shell.Current.GoToAsync("..");}catch{vm.Error="Unable to close the viewer. Please try the back button.";}}
 private void NextSwiped(object? sender,SwipedEventArgs e)=>vm.Next.Execute(null);
 private void PreviousSwiped(object? sender,SwipedEventArgs e)=>vm.Previous.Execute(null);
}