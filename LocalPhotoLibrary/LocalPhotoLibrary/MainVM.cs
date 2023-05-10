using MyPhoto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace LocalPhotoLibrary
{
    public class MainVM : INotifyPropertyChanged
    {
        #region INotify
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region Properties
        private int _selectedPhotoIndex;
        public int SelectedPhotoIndex
        {
            get { return _selectedPhotoIndex; }
            set
            {
                _selectedPhotoIndex = value;
                NotifyPropertyChanged();
            }
        }
        private string photoPath;
        public string LocalPhotoPath
        {
            get { return photoPath; }
            set
            {
                photoPath = value;
                NotifyPropertyChanged();
            }
        }
        #endregion
        MainModel model { get; set; }
        public ObservableCollection<Photo> Photos { get; set; }
        public ICommand LoadPCPhotos { get; private set; }
        public ICommand SelectFolderCommand { get; private set; }

        private void UpdatePhotosFromPC()
        {
            Photos.Clear();
            List<Photo> modelPhotos = model.GetPhotos(LocalPhotoPath);
            modelPhotos.ForEach(x => Photos.Add(x));

            if (Photos.Count > 0) SelectedPhotoIndex = 0;
        }
        private void SelectFolder()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    LocalPhotoPath = dialog.SelectedPath;
                    UpdatePhotosFromPC();
                }
            }
        }
        public MainVM()
        {
            Photos = new ObservableCollection<Photo>();
            model = new MainModel();
            LoadPCPhotos = new RelayCommand(UpdatePhotosFromPC);
            SelectFolderCommand = new RelayCommand(SelectFolder);
            LocalPhotoPath = "C:\\Users\\matth\\Pictures\\Dragon Wallpaper";
            UpdatePhotosFromPC();
        }
    }
}
