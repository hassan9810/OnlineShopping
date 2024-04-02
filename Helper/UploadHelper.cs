namespace Store.Helper
{
    public static class UploadHelper
    {
        public static async Task<string> UploadImage(IFormFile file, IWebHostEnvironment hosting, string userId)
        {

            string path = Path.Combine(hosting.WebRootPath, "Images", userId.ToString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var filesNames = Directory.GetFiles(path);
            var imageName = file.FileName;
            if (filesNames.Contains(Path.Combine(path, imageName)))
            {
                throw new ApplicationException("Image Name Exists");
            }

            using (FileStream stream = new FileStream(Path.Combine(path, imageName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return ("wwwroot/" + "Images/" + userId + "/" + imageName);
        }
    }
}
