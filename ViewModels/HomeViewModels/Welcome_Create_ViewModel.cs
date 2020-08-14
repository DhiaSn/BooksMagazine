namespace BooksMagazine.ViewModels.HomeViewModels
{
    public class Welcome_Create_ViewModel : BooksMagazine.Models.HomeModels.Welcome
    {
        public Microsoft.AspNetCore.Http.IFormFile Image { get; set; }
        // This To Check Over Posting Atack
        public string Type { get; set; }
    }
}
