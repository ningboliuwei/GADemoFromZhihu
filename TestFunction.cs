using System;

namespace GADemoFromZhihu
{
    public class TestFunction
    {
        //解空间的下界
        public const double LowerBound = 0;
        //解空间的上界
        public const double UpperBound = 9;

        public static double Function1(double x)
        {
            return x * Math.Sin(10 * Math.PI * x) + 2.0;
        }

        public static double Function2(double x)
        {
            return x + 10 * Math.Sin(5 * x) + 7 * Math.Cos(4 * x);
        }

        public static string BranchTest(int x)
        {
            var path = "#";

            if (x >= 1000)
            {
                path += "a";
                if (x < 1001)
                {
                    path += "b";
                }
            }

            return path;
        }

        public static double BranchTestFitness(int x)
        {
            var f1 = 0;
            var f2 = 0;
            const int k = 1;
            //
            if (1000 - x <= 0)
            {
                f1 = 0;
            }
            else
            {
                f1 = Math.Abs(1000 - x) ;
            }
            //

            if (x > 1000)
            {
                //
                if (1001 - x > 0)
                {
                    f2 = 0;
                }
                else
                {
                    f2=Math.Abs(1001-x) + k;
                }
                //
                if (x < 1001)
                {
                    
                }
            }

            return f1 + f2;
        }

        //a —— 非三角形， b —— 等边三角形，c —— 等腰三角形，d —— 一般三角形
        public static string TriangleType(int x, int y, int z)
        {
            string type = "";

            //            if (x + y <= z || x + z <= y || y + z <= x)
            //                type = "a";
            //            else if (x == y && y == z)
            //                type = "b";
            //            else if (x == y || y == z || x == z)
            //                type = "c";
            //            else
            //                type = "d";
            //
            //            if (x + y > z && x + z > y && y + z > x)
            //            {
            //                if (x == y)
            //                {
            //                    if (y == z)
            //                    {
            //                        type = "b";
            //                    }
            //                    else
            //                    {
            //                        
            //                    }
            //                }
            //                else
            //                {
            //                   
            //                }
            //            }
            //            else
            //            {
            //                type= "a";
            //            }
            //


            return type;
        }
    }
}