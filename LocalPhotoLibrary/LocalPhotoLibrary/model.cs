using LocalPhotosLib;
using MyPhoto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPhotoLibrary
{
    public class MainModel
    {
        public List<Photo> GetPhotos(string path)
        {
            List<Photo> photos = PhotoLoader.GetAllPhotos(path);
            return photos;
        }
    }
}
