using System.Collections.Generic;

namespace LapinCretinsFormes
{
    public interface IShapeAccessor
    {
        void Save(List<Shape> shapes, string filePath);
        List<Shape> Load(string filePath);
    }
}
