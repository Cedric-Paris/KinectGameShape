using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LapinCretinsFormes
{
    public class ScoreManager
    {
        public int ScoreFromGeometries(Geometry shapeToFill, Geometry peoplesShadows)
        {
            double maximumScore = shapeToFill.GetArea();
            double penalties = Geometry.Combine(shapeToFill, peoplesShadows, GeometryCombineMode.Xor, null).GetArea();
            double score = maximumScore - penalties;
            return score > 0 ? (int)score : 0;
        }

        public float FilledPercentageFromGeometries(Geometry shapeToFill, Geometry peoplesShadows)
        {
            double maximumArea = shapeToFill.GetArea();
            double filledArea = Geometry.Combine(shapeToFill, peoplesShadows, GeometryCombineMode.Intersect, null).GetArea();
            double percentage = filledArea / maximumArea;
            return (float) Math.Round(percentage, 2);
        }
    }
}
