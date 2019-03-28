using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalPictureViewer
{
    public class ImageInformation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        bool _markedForDelete;

        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public int XResolution { get; set; }
        public int YResolution { get; set; }
        public float Factor { get; set; }

        public bool MarkedForDelete
        {
            get { return _markedForDelete; }
            set
            {
                _markedForDelete = value;
                Notify(ReflectionHelper.GetPropertyName(() => this.MarkedForDelete));
            }
        }

        private void Notify(string argument)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(argument));
            }
        }

        public ImageInformation()
        {
        }


        public ImageInformation(int xResolution, int yResolution, long fileSize)
        {
            XResolution = xResolution;
            YResolution = yResolution;
            FileSize = fileSize;
        }

        public ImageInformation(string fileName, string filePath, int xResolution, int yResolution, long fileSize)
        {
            FileName = fileName;
            FilePath = filePath;
            XResolution = xResolution;
            YResolution = yResolution;
            FileSize = fileSize;
        }

        public static bool operator >(ImageInformation lhs, ImageInformation rhs)
        {
            return ((lhs.XResolution > rhs.XResolution) && (lhs.YResolution > rhs.YResolution) && (lhs.FileSize > rhs.FileSize)) ||
                   ((lhs.XResolution == rhs.XResolution) && (lhs.YResolution > rhs.YResolution) && (lhs.FileSize > rhs.FileSize)) ||
                   ((lhs.XResolution > rhs.XResolution) && (lhs.YResolution == rhs.YResolution) && (lhs.FileSize > rhs.FileSize)) ||
                   ((lhs.XResolution > rhs.XResolution) && (lhs.YResolution > rhs.YResolution) && (lhs.FileSize == rhs.FileSize)) ||
                   ((lhs.XResolution == rhs.XResolution) && (lhs.YResolution == rhs.YResolution) && (lhs.FileSize > rhs.FileSize)) ||
                   ((lhs.XResolution > rhs.XResolution) && (lhs.YResolution == rhs.YResolution) && (lhs.FileSize == rhs.FileSize)) ||
                   ((lhs.XResolution == rhs.XResolution) && (lhs.YResolution == rhs.YResolution) && (lhs.FileSize > rhs.FileSize));
        }

        public static bool operator <(ImageInformation lhs, ImageInformation rhs)
        {
            return ((lhs.XResolution < rhs.XResolution) && (lhs.YResolution < rhs.YResolution) && (lhs.FileSize < rhs.FileSize)) ||
                   ((lhs.XResolution == rhs.XResolution) && (lhs.YResolution < rhs.YResolution) && (lhs.FileSize < rhs.FileSize)) ||
                   ((lhs.XResolution < rhs.XResolution) && (lhs.YResolution == rhs.YResolution) && (lhs.FileSize < rhs.FileSize)) ||
                   ((lhs.XResolution < rhs.XResolution) && (lhs.YResolution < rhs.YResolution) && (lhs.FileSize == rhs.FileSize)) ||
                   ((lhs.XResolution == rhs.XResolution) && (lhs.YResolution == rhs.YResolution) && (lhs.FileSize < rhs.FileSize)) ||
                   ((lhs.XResolution < rhs.XResolution) && (lhs.YResolution == rhs.YResolution) && (lhs.FileSize == rhs.FileSize)) ||
                   ((lhs.XResolution == rhs.XResolution) && (lhs.YResolution == rhs.YResolution) && (lhs.FileSize < rhs.FileSize));
        }

        public static bool operator ==(ImageInformation lhs, ImageInformation rhs)
        {
            return ((lhs.XResolution == rhs.XResolution) && (lhs.YResolution == rhs.YResolution) && (lhs.FileSize == rhs.FileSize));
        }

        public static bool operator !=(ImageInformation lhs, ImageInformation rhs)
        {
            return !(lhs == rhs);
        }

        public override bool Equals(object obj)
        {
            return obj as ImageInformation == this;
        }

        public override int GetHashCode()
        {
            return GetHashCode();
        }
    }
}
