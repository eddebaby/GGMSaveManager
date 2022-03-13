using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GGMSaveManager
{
    class Toolkit
    {

        /// <summary>
        /// Multiplies two 32-bit values and then divides the 64-bit result by a 
        /// third 32-bit value. The final result is rounded to the nearest integer.
        /// </summary>
        public static int MulDiv(int nNumber, int nNumerator, int nDenominator)
        {
            return (int)Math.Round((float)nNumber * nNumerator / nDenominator);
        }

        /*public static int MulDiv(int number, int numerator, int denominator)
        {
            return (int)(((long)number * numerator + (denominator >> 1)) / denominator);
        }*/

        public static int MulDiv(float number, float numerator, float denominator)
        {
            return MulDiv((int)number, (int)numerator, (int)denominator); ;
        }

        public static Size ScaleSize(Size size, float width, float height)
        {
            Size newSize;
            //(new Size(50, 14), dialogUnits.Width / 4, dialogUnits.Height / 8)
            //Size buttonSize = new Size(MulDiv(50, (int)dialogUnits.Width, 4), MulDiv(14, (int)dialogUnits.Height, 8));

            //return new Size(MulDiv(50, width, 4), MulDiv(14, height, 8));
            return new Size(size.Width * (int)width, size.Height * (int)height);
        }
    }
}
