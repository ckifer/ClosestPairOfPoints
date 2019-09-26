﻿namespace ClosestPoints.Models
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; } = int.MinValue;

        public int Y { get; set; } = int.MinValue;
    }
}
