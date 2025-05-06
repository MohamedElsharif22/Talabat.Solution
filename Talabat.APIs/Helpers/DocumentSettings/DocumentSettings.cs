namespace Talabat.APIs.Helpers.DocumentSettings
{
    public static class DocumentSettings
    {
        // function to upload a file into the given folderName
        public async static Task<string?> UploadFile(IFormFile file, string folderName)
        {
            if (file is null || file.Length == 0) return null;

            // check folder exists or create it
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // generate a unique file name
            string fileName = $"{Guid.NewGuid()}_{file.FileName}";

            string filePath = Path.Combine(folderPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            // copy the file to the new path
            await file.CopyToAsync(stream);

            return fileName;

        }

        // function to delete a file from the given folderName
        public static void DeleteFile(string fileName, string folderName)
        {
            if (string.IsNullOrEmpty(fileName)) return;
            // check if the file is a default file
            if (DefaultAppFiles.IsDefaultFile(fileName)) return;
            // check if the file is the default profile picture
            string filePath = DefaultAppFiles.GetFilePath(fileName, folderName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }



    }
}
