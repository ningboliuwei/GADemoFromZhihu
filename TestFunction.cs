﻿using System;

namespace GADemoFromZhihu
{
    public class TestFunction
    {
        //解空间的下界
        public static double LowerBound = 0;
        //解空间的上界
        public static double UpperBound = double.MaxValue;


        public static void SetBound(double lowerBound, double upperBound)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        //解空间 [0,1]
        public static double Function1(double x)
        {
            return x * Math.Sin(10 * Math.PI * x) + 2.0;
        }

        //解空间 7[0,9]
        public static double Function2(double x)
        {
            return x + 10 * Math.Sin(5 * x) + 7 * Math.Cos(4 * x);
        }

        //简单分支函数
        public static string BranchTest1(double x, double y)
        {
            var path = "#";

            if (x >= 9980)
            {
                path += "a";
                if (y < 50)
                {
                    path += "b";
                }
            }

            return path;
        }

        //简单分支函数（插桩，用于计算适应度）
        public static double StubbedBranchTest1(double x, double y)
        {
            var f1 = 0.0;
            var f2 = 0.0;
            const int k = 1;
            //
            if (9980 - x <= 0)
            {
                f1 = 0;
            }
            else
            {
                f1 = Math.Abs(9980 - x) + k;
            }
            //

            if (x > 9980)
            {
                if (y - 50 < 0)
                {
                    f2 = 0;
                }
                else
                {
                    f2 = Math.Abs(50 - y) + k;
                }
            }

            return - (f1 + f2);
        }

        //a —— 非三角形， b —— 等边三角形，c —— 等腰三角形，d —— 一般三角形
        public static string TriangleTypeTest(int x, int y, int z)
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