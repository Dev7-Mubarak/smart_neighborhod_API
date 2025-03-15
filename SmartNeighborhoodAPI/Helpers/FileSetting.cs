namespace SmartNeighborhoodAPI.Helpers
{
    public class FileSetting
    {
        public const string UsersImagesPath = "/Assets/Users";
        public const string AllowedExtenstions = ".jpg,.jpeg,.png";
        public const int MaxFileSizeInMB = 1;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    }
}
