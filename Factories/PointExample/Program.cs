using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Factory and inner Factory
 */

namespace PointExample
{
    /* old and bad
        public enum CoordinateSystem
        {
            Cartesian,
            Polar
        }
    */

    public class Point
    {

        /* BAD CAUSE UNCLEAR
         * Le constructeur n'est pas efficace
         * lorsque l'on souhaite en avoir plusieurs
         * avec la même signature, ici:
         * Coordonnée carthésienne ou polaires
         */

        private double x, y;

        /*
        public Point(double a, double b,
            CoordinateSystem system = CoordinateSystem.Cartesian)
        {
            switch (system)
            {
                case CoordinateSystem.Cartesian:
                    x = a;
                    y = b;
                    break;
                case CoordinateSystem.Polar:
                    x = a * Math.Cos(b);
                    y = a * Math.Sin(b);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(system), system, null);
            }
        }
        */

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        public static class Factory
        {
            //factory method
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var point = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
            Console.Read();
        }
    }
}
