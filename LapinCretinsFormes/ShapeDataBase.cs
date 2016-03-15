using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LapinCretinsFormes
{
    class ShapeDataBase // Guarder ?
    {

        private static List<Shape> shapesList = new List<Shape>();

        private static Random rand = new Random();

        static ShapeDataBase()
        {
            // Ajouter les shapes ?
        }

        public static Shape GetRandomShape()
        {
            return shapesList[rand.Next(shapesList.Count)];
        }
    }
}
