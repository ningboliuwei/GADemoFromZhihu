using System;

namespace GADemoFromZhihu
{
    public class TestFunction
    {
        //解空间的下界
        public static double LowerBound = -1;
        //解空间的上界
        public static double UpperBound = 2;

        public double Function1(double x)
        {
            return x * Math.Sin(10 * Math.PI * x) + 2.0;
        }

        public static double Function2(double x)
        {
            return x + 10 * Math.Sin(5 * x) + 7 * Math.Cos(4 * x);
        }

        //a —— 非三角形， b —— 等边三角形，c —— 等腰三角形，d —— 一般三角形
        public static string TriangleType(int x, int y, int z)
        {
            string type;

            if (x + y <= z || x + z <= y || y + z <= x)
                type = "a";
            else if (x == y && y == z)
                type = "b";
            else if (x == y || y == z || x == z)
                type = "c";
            else
                type = "d";

            return type;
        }
    }
}