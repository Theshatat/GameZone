namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel: GameFormViewModel
    {

        [AllowedExtension(FileSettings.AllowedExtensions), MaximumFileSize(FileSettings.MaxFileSizeInMB)]
        public IFormFile Cover { get; set; }
    }
}
