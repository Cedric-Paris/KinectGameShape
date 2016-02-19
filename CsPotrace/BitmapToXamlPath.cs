using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsPotrace
{
    public class BitmapToXamlPath
    {

        public string ConvertBitmap(Bitmap bitmap)
        {
            bool[,] Matrix;
            ArrayList ListOfCurveArray = new ArrayList();
            Matrix = Potrace.BitMapToBinary(bitmap, 1);
            Potrace.potrace_trace(Matrix, ListOfCurveArray);
            string s = Potrace.Export2StringSVG(ListOfCurveArray, bitmap.Width, bitmap.Height);
            s = s.Replace("\n", " ").Replace("|", " ").Replace("  "," ");
            return s;
        }

    }
}
