using System;
using AreaCalc;

namespace Area_Calc
{
    class Program
    {
        static void Main(string[] args)
        {
            (double, double)[] Figure = new (double, double)[] 
            { (10, 10), (10, 0), (0, 0), (0, 10), (1,15) };

            Console.WriteLine(Basics.PolygonArea(Figure));
        }
    }
}
