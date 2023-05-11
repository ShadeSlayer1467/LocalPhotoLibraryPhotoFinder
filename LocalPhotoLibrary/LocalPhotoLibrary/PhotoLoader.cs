using MyPhoto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LocalPhotoLibrary
{
    public class PhotoLoader
    {
        public List<string> GetPhotoPaths(string directoryPath)
        {
            List<string> photoPaths = new List<string>();
            if (Directory.Exists(directoryPath))
                GetPhotoPathsRecursive(directoryPath, photoPaths);
            return photoPaths;
        }
        private void GetPhotoPathsRecursive(string directoryPath, List<string> photoPaths)
        {
            // Supported photo file extensions
            string[] supportedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff" };

            try
            {
                // Get all files in the current directory
                string[] allFiles = Directory.GetFiles(directoryPath);
                // Filter files by supported extensions
                string[] photoFiles = allFiles
                    .Where(file => supportedExtensions.Contains(Path.GetExtension(file).ToLower()))
                    .ToArray();

                // Iterate through the photo files and add their paths to the list
                foreach (string photoFile in photoFiles)
                {
                    photoPaths.Add(photoFile);
                }

                // Recursively search through subdirectories
                string[] subdirectories = Directory.GetDirectories(directoryPath);
                foreach (string subdirectory in subdirectories)
                {
                    GetPhotoPathsRecursive(subdirectory, photoPaths);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void LoadMetaData(Photo photo)
        {
            // Check if file exists
            if (!File.Exists(photo.URL))
                return;
            try
            {
                // Load and set the metadata for the photo
                // Replace this with actual metadata loading logic
                FileInfo fileInfo = new FileInfo(photo.URL);

                BitmapImage bitmap = new BitmapImage(new Uri(fileInfo.FullName));


                PhotoMetaData Path = new PhotoMetaData { Key = "Path", Value = fileInfo.FullName };
                PhotoMetaData Width = new PhotoMetaData { Key = "Width", Value = bitmap.PixelWidth.ToString() };
                PhotoMetaData Height = new PhotoMetaData { Key = "Height", Value = bitmap.PixelHeight.ToString() };
                PhotoMetaData Filename = new PhotoMetaData { Key = "Filename", Value = fileInfo.Name };
                PhotoMetaData Date_Created = new PhotoMetaData { Key = "Date Created", Value = fileInfo.CreationTime.ToShortDateString() };
                PhotoMetaData Time_Created = new PhotoMetaData { Key = "Time Created", Value = fileInfo.CreationTime.ToShortDateString() };

                photo.MetaData.Add(Height);
                photo.MetaData.Add(Width);
                photo.MetaData.Add(Filename);
                photo.MetaData.Add(Date_Created);
                photo.MetaData.Add(Time_Created);
                photo.MetaData.Add(Path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load metadata for photo: {ex.ToString()}");
            }
        }
    }
}
