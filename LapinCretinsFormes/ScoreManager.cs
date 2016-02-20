using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LapinCretinsFormes
{
    /// <summary>
    /// Manages how to calculate the score.
    /// </summary>
    public class ScoreManager
    {
        /// <summary>
        /// Calculates the score from the shape the players have to fill and their overall shadow.
        /// </summary>
        /// <param name="shapeToFill">The shape players have to fill.</param>
        /// <param name="peoplesShadows">The players' overall shadow.</param>
        /// <returns>The players' score.</returns>
        public int ScoreFromGeometries(Geometry shapeToFill, Geometry peoplesShadows)
        {
            double maximumScore = shapeToFill.GetArea();
            double penalties = Geometry.Combine(shapeToFill, peoplesShadows, GeometryCombineMode.Xor, null).GetArea();
            double score = maximumScore - penalties;
            return score > 0 ? (int)score : 0;
        }

        /// <summary>
        /// Calculates the percentage of the shape the players have to fill that is filled by their shadow.
        /// </summary>
        /// <param name="shapeToFill">The shape players have to fill.</param>
        /// <param name="peoplesShadows">The players' overall shadow.</param>
        /// <returns>The percentage of the shape that is filled by the players' overall shadow.</returns>
        public float FilledPercentageFromGeometries(Geometry shapeToFill, Geometry peoplesShadows)
        {
            double maximumArea = shapeToFill.GetArea();
            double filledArea = Geometry.Combine(shapeToFill, peoplesShadows, GeometryCombineMode.Intersect, null).GetArea();
            double percentage = 100 * filledArea / maximumArea;
            return (float) Math.Round(percentage, 2);
        }
    }
}
