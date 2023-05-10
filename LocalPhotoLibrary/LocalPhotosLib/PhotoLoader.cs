using MyPhoto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LocalPhotosLib
{
    public static class PhotoLoader
    {
        public static List<Photo> GetAllPhotos(string directoryPath)
        {
            List<Photo> photos = new List<Photo>();
            GetPhotosRecursive(directoryPath, photos);
            return photos;
        }
        private static async void GetPhotosRecursive(string directoryPath, List<Photo> photos)
        {
            // Supported photo file extensions
            string[] supportedExtensions = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff" };

            // Get all files in the current directory
            string[] allFiles = Directory.GetFiles(directoryPath);

            // Filter files by supported extensions
            string[] photoFiles = allFiles
                .Where(file => supportedExtensions.Contains(Path.GetExtension(file).ToLower()))
                .ToArray();

            // Iterate through the photo files and create a Photo object for each
            foreach (string photoFile in photoFiles)
            {
                Photo photo = new Photo();
                photo.URL = photoFile;
                // Load and set the metadata for the photo
                LoadMetaData(photo);
                photos.Add(photo);
            }

            // Recursively search through subdirectories
            string[] subdirectories = Directory.GetDirectories(directoryPath);
            foreach (string subdirectory in subdirectories)
            {
                GetPhotosRecursive(subdirectory, photos);
            }
        }
        private static void LoadMetaData(Photo photo)
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
    }
}
