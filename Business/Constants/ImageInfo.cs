using System.IO;

namespace Business.Constants
{
    public static class ImageInfo
    {
        public static string DefaultImageFolder = $"{Directory.GetParent(Directory.GetCurrentDirectory())}\\WebAPI\\wwwroot\\images\\";
        public static string DefaultImage = $"{DefaultImageFolder}defaultLogo.jpg";
    }
}
