namespace BooksMagazine.ViewModels
{
    public class CreateTopic_ViewModel : BooksMagazine.Models.HomeModels.Topic
    {
        public Microsoft.AspNetCore.Http.IFormFile Image { get; set; }
        // This To Check Over Posting Atack
        public string Type { get; set; }
    }
}
