using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapinCretinsFormes
{
    public interface IShapeAccessor
    {
        void Save(List<Shape> shapes, string filePath);
        List<Shape> Load(string filePath);
    }
}
