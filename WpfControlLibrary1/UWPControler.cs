using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace UniversalPictureViewer
{
    public class UPWControler
    {
        #region events
        public event ViewerClosingEventHandler ViewerClosingEvent;
        public delegate void ViewerClosingEventHandler(object o, EventArgs args);
        #endregion

        #region attributes
        private UPWWindow _window;
        private UPWWindow.EMode _mode;

        private FileStream _currentImageStream;
        private BitmapImage _currentImage;

        private ObservableCollection<string> _filteredImagePaths;
        private int _pos;
        private string _imageDirectory;
        private List<string> _allImagePaths;
        #endregion

        #region constructor
        public UPWControler(UPWWindow window, UPWWindow.EMode mode)
        {
            _window = window;
            _mode = mode;

            if (_mode == UPWWindow.EMode.directory_mode)
            {
                _imageDirectory = Settings.PictureDirectory;
            }

            if (Initialize(out string errorMessage))
            {
                var success = LoadImage(out ImageInformation ii, out errorMessage);
            }
            else
            {
                // to do
            }


        }
        #endregion

        #region methods
        internal void ApplySetting()
        {
            _window.Title = Settings.WindowTitle;
            _window.ViewerGroupBox.Header = Settings.ViewerGroupBoxHeader;
        }

        internal void HandleClose()
        {
            // raise event
            OnViewerClosing(this, new EventArgs());

            // close window
            _window.Close();
        }

        protected virtual void OnViewerClosing(object o, EventArgs args)
        {
            ViewerClosingEventHandler handler = ViewerClosingEvent;

            if (handler != null)
            {
                ViewerClosingEvent(this, args);
            }
        }

        public bool Initialize(out string errorMessage)
        {
            // image directory containing the viewed images
            //_imageDirectory = FileSystemHelper.GetActorDirectoryPath(CurrentActor.Firstname, CurrentActor.Lastname, CurrentActor.YearOfBirth);

            // load favourites of all actors (not only the current one) from disk
            // bool success = _favmanager.LoadAllFavourites(out errorMessage);

            // loading sucessfull
            // if (success)
            //{
                // get all image from current directory
                _allImagePaths = GetAllImagesPathInDirectory(_imageDirectory, out errorMessage);

            // sort all image files by source
            // UpdatePathBySource(_allImagePaths);

            /*
             // there are images
             if (_allImagePaths != null)
             {
                 // get the directory with the photos of the actor
                 var imageDirectory = FileSystemHelper.GetActorDirectoryPath(CurrentActor.Firstname, CurrentActor.Lastname, CurrentActor.YearOfBirth);

                 // get the favourites of the current actor by comparing the loaded list of all actors
                 //CurrentImageFavouriteList = new ObservableCollection<string>(GetSavedFavouriteImagesOfCurrentActor(imageDirectory));
                 _favmanager.Initialize();

                 // add entries to the combo box with all download sources
                 FillDownloadSourceComboBox();
             }*/
            //}

            //return success;
            return true;
        }

        private bool LoadImage(out ImageInformation ii, out string errorMessage)
        {
            var success = true;
            errorMessage = string.Empty;
            ii = null;

            // There are image in the current actor directory
            if (_filteredImagePaths.Count > _pos)

                try
                {
                    // close all stream if it was opened before (from previous image)
                    if (_currentImageStream != null) _currentImageStream.Close();

                    // load file
                    _currentImageStream = new FileStream(_filteredImagePaths[_pos], FileMode.Open, FileAccess.Read);
                    _currentImage = new BitmapImage();
                    _currentImage.BeginInit();
                    _currentImage.StreamSource = _currentImageStream;
                    _currentImage.EndInit();

                    // set source of image control
                    _window.imgViewer.Source = _currentImage;

                    // get the image information
                    ii = ReadImageInformation(_filteredImagePaths[_pos]);
                }
                catch (Exception ex)
                {
                    success = false;
                    errorMessage = ex.Message;
                }

            return success;
        }

        public static ImageInformation ReadImageInformation(string path)
        {
            try
            {
                string filename = Path.GetFileName(path);
                long fileSize = new System.IO.FileInfo(path).Length;

                var img = Image.FromFile(path);

                var ii = new ImageInformation()
                {
                    FileName = filename,
                    FilePath = path,
                    FileSize = fileSize,
                    XResolution = img.Width,
                    YResolution = img.Height
                };

                img.Dispose();

                return ii;
            }
            catch (Exception)
            {
                return new ImageInformation()
                {
                    FileName = path,
                    FilePath = path,
                    FileSize = 0,
                    XResolution = 0,
                    YResolution = 0
                };
            }
        }

        public static List<string> GetAllImagesPathInDirectory(string path, out string errorMessage)
        {
            List<string> extentions = new List<string> { ".JPG", ".JPE", ".BMP", ".GIF", ".PNG" };

            return GetAllFilesInDirectory(path, extentions, out errorMessage);
        }

        public static List<string> GetAllFilesInDirectory(string path, List<string> extentions, out string errorMessage)
        {
            List<string> found = new List<string>();
            errorMessage = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    errorMessage = "Invalid Path";
                    return null;
                }

                string[] files = Directory.GetFiles(path);

                foreach (var f in files)
                {
                    if (extentions != null)
                    {
                        if (extentions.Contains(Path.GetExtension(f).ToUpperInvariant()))
                        {
                            found.Add(f);
                        }
                    }
                    else
                    {
                        found.Add(f);
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return null;
            }

            return found;
        }
        #endregion
    }
}
