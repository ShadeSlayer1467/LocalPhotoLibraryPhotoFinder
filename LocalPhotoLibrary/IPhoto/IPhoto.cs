using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhoto
{
    public interface IPhotoAPI
    {
        string PluginName { get; }
        string IconPath { get; }
        List<Photo> GetPhotos();
    }
}
