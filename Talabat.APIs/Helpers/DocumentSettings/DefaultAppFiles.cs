namespace Talabat.APIs.Helpers.DocumentSettings
{
    public static class DefaultAppFiles
    {
        private const string defaultProfilePicture = "defaultPP.png";
        public static string DefaultProfilePicture => defaultProfilePicture;

        public static string GetFilePath(string fileName, string folderName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, fileName);
        }
        // function to check if the file is a default file
        public static bool IsDefaultFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return false;

            if (fileName is defaultProfilePicture)
            {
                return true;
            }

            return false;

        }
    }
}
