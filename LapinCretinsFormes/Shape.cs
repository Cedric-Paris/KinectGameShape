using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LapinCretinsFormes
{
    public class Shape
    {
        private Geometry _shapeGeometry;
            public Geometry getShapeGeometry() { return _shapeGeometry; }

        private Uri _imageUri;
            public Uri getImageUri() { return _imageUri; }

        public Shape(Geometry shapeGeometry, Uri image)
        {
            _shapeGeometry = shapeGeometry;
            _imageUri = image;
        }
    }
}
