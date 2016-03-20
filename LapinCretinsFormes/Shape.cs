using System;
using System.Windows.Media;

namespace LapinCretinsFormes
{
    public class Shape
    {
        public Geometry ShapeGeometry
            { get; private set; }

        public Uri ImageUri
            { get; private set; }

        public Shape(Geometry shapeGeometry, Uri image)
        {
            ShapeGeometry = shapeGeometry;
            ImageUri = image;
        }
    }
}
