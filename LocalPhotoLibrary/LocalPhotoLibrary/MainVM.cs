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

        private int selectedPhotoIndex;
        public int SelectedPhotoIndex
        {
            get { return selectedPhotoIndex; }
            set
            {
                selectedPhotoIndex = value;
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
        PhotoLoader model { get; set; }
        public ObservableCollection<Photo> Photos { get; set; }
        public ICommand LoadPCPhotos { get; private set; }
        public ICommand SelectFolderCommand { get; private set; }

        private async void StartConsumer()
        {
            Photos.Clear();
            await Task.Run(() =>
            {
                foreach (var path in model.photoPathsQueue.GetConsumingEnumerable())
                {
                    var photo = new Photo { URL = path };
                    model.LoadMetaData(photo);
                    System.Windows.Application.Current.Dispatcher.Invoke(() => Photos.Add(photo));
                }
            });
        }

        private void UpdatePhotosFromPC()
        {
            model.ProducePhotoPaths(LocalPhotoPath);
            StartConsumer();
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
            model = new PhotoLoader();
            LoadPCPhotos = new RelayCommand(UpdatePhotosFromPC);
            SelectFolderCommand = new RelayCommand(SelectFolder);
            LocalPhotoPath = "C:\\Users\\matth\\Pictures\\Dragon Wallpaper";
            UpdatePhotosFromPC();
        }
    }
}
