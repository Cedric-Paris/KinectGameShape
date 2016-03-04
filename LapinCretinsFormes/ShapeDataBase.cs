using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LapinCretinsFormes
{
    class ShapeDataBase
    {

        private static List<Shape> shapesList = new List<Shape>();

        private static Random rand = new Random();

        static ShapeDataBase()
        {
            LoadShapes();
        }

        public static Shape GetRandomShape()
        {
            return shapesList[rand.Next(shapesList.Count)];
        }

        private static void LoadShapes()
        {
            //Information sur les formes --> Ajout dans la list
        }

        public static void SaveShape(Shape shape)
        {
            shapesList.Add(shape);
        }
    }
}
