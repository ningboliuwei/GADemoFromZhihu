using System;
using System.Collections.Generic;
using System.Linq;

namespace GADemoFromZhihu
{
    public class TestFunction
    {
        //解空间的下界
        public static double LowerBound;
        //解空间的上界
        public static double UpperBound;


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
                    path += "b";
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
                f1 = 0;
            else
                f1 = Math.Abs(9980 - x) + k;
            //

            if (x > 9980)
                if (y - 50 < 0)
                    f2 = 0;
                else
                    f2 = Math.Abs(50 - y) + k;

            return -(f1 + f2);
        }

        //equilateral —— 等边， isosceles 等腰，scalene 一般
        public static string TriangleTypeTest(int x, int y, int z)
        {
            var type = "";

            if (x + y > z && x + z > y && y + z > x)
                if (x == y && y == z)
                {
                    type = "equilateral triangle";
                }
                else
                {
                    if (x == y || y == z || x == z)
                        type = "isosceles triangle";
                    else
                        type = "scalene triangle";
                }
            else
                type = "not a triangle";

            return type;
        }

        public static string TriangleTypeTestPathCoverage(int x, int y, int z)
        {
            var type = "";
            var path = "";

            if (x + y > z && x + z > y && y + z > x)
            {
                path += "a";
                if (x == y && y == z)
                {
                    path += "b";
                    type = "equilateral triangle";
                }
                else
                {
                    path += "c";
                    if (x == y || y == z || x == z)
                    {
                        path += "d";
                        type = "isosceles triangle";
                    }
                    else
                    {
                        path += "e";
                        type = "scalene triangle";
                    }
                }
            }
            else
            {
                path += "f";
                type = "not a triangle";
            }

            return path;
        }

        public static double StubbedTriangleTypeTest(int x, int y, int z)
        {
            var f1 = 0;
            var f2 = 0;
            var f3 = 0;
            var f4 = 0;
            var f5 = 0;
            var k = 1;


            if (x + y > z && x + z > y && y + z > x)
            {
                f1 = 0;

                if (x == y && y == z)
                {
                    f2 = 0;
                    //                    type = "equilateral triangle";
                }
                else
                {
                    //这里真的是 MAX 吗？
                    f2 = new List<int> {Math.Abs(x - y), Math.Abs(y - z)}.Sum();

                    if (x == y || y == z || x == z)
                    {
                        f3 = 0;
                        //                        type = "isosceles triangle";
                    }
                    else
                    {
                        f3 = new List<int> {Math.Abs(x - y), Math.Abs(y - z), Math.Abs(x - z)}.Min();
                        //                        type = "scalene triangle";
                    }
                }
            }
            else
            {
                f1 = new List<int> {z - (x + y) + k, y - (x + z) + k, x - (y + z) + k}.Min();
                //                type = "not a triangle";
            }
            return -Math.Abs(f1 + f2 + f3);
        }

        public static double StubbedTriangleTypeTestPathCoverage(int x, int y, int z)
        {
            var path = "";
            if (x + y > z && x + z > y && y + z > x)
            {
                path += "a";
                if (x == y && y == z)
                {
                    path += "b";
                    //                    type = "equilateral triangle";
                }
                else
                {
                    path += "c";
                    //这里真的是 MAX 吗？

                    if (x == y || y == z || x == z)
                    {
                        path += "d";
                        //                        type = "isosceles triangle";
                    }
                    else
                    {
                        path += "e";
                        //                        type = "scalene triangle";
                    }
                }
            }
            else
            {
                path += "f";
            }
            //                type = "not a triangle";
            return path.Length / 6.0;
        }

        //计算下一天的日期
        public static string NextDate(int year, int month, int day)
        {
            var errorMessage = "Invalid date";

            if (year >= 1950 && year < 2050 && month >= 1 && month <= 12 && day >= 1 && day <= 31)
            {
                //大月
                if (month == 12)
                {
                    if (day == 31)
                    {
                        year++;
                        day = 1;
                        month = 1;
                    }
                    else
                    {
                        day++;
                    }
                }
                else
                {
                    if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10)
                    {
                        if (day == 31)
                        {
                            day = 1;
                            month++;
                        }
                        else
                        {
                            day++;
                        }
                    }
                    else
                    {
                        if (month == 4 || month == 6 || month == 9 || month == 11)
                        {
                            if (day == 31)
                            {
                                return errorMessage;
                            }
                            if (day == 30)
                            {
                                day = 1;
                                month++;
                            }
                            else
                            {
                                day++;
                            }
                        }
                        else
                        {
                            if (month == 2)
                            {
                                var endDay = 0;

                                if (year % 400 == 0)
                                {
                                    endDay = 29;
                                }
                                else
                                {
                                    if (year % 100 == 0)
                                    {
                                        endDay = 28;
                                    }
                                    else
                                    {
                                        if (year % 4 == 0)
                                        {
                                            endDay = 29;
                                        }
                                        else
                                        {
                                            endDay = 28;
                                        }
                                    }
                                }

                                if (day > endDay)
                                {
                                    return errorMessage;
                                }
                                if (day == endDay)
                                {
                                    day = 1;
                                    month++;
                                }
                                else
                                {
                                    day++;
                                }
                            }
                        }
                    }
                }
                return $"{year}-{month}-{day}";
            }
            return errorMessage;
        }
    }
}