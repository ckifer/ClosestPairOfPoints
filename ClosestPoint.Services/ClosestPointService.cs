using ClosestPoints.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClosestPoint.Services
{
    // business logic service class
    public class ClosestPointService
    {
        public double GetClosestPoints(List<Point> points)
        {
            // order list by X
            List<Point> sortedByX = points.OrderBy(p => p.X).ToList();
            // order list by Y
            List<Point> sortedByY = points.OrderBy(p => p.Y).ToList();

            // now find smallest distance between two points in both halves
            return GetClosestPointsRecursive(sortedByX, sortedByY);
        }

        private double GetClosestPointsRecursive(List<Point> sortedByX, List<Point> sortedByY)
        {
            // cant compare less than 2 points
            if (sortedByX.Count < 2)
            {
                return double.MinValue;
            }

            // only 2 points, then that 2 points are the pair of closest points
            if (sortedByX.Count == 2)
            {
                return Distance(sortedByX[0], sortedByX[1]);
            }

            // split array into left and right halves for x axis
            List<Point> leftX = sortedByX.Take(sortedByX.Count / 2).ToList();
            List<Point> rightX = sortedByX.Skip(sortedByX.Count / 2).ToList();
            double midX = leftX.Last().X;

            // split array into left and right halves for y axis based on x to make quadrants (kinda)
            List<Point> leftY = sortedByY.Where(p => p.X <= midX).ToList();
            List<Point> rightY = sortedByY.Where(p => p.X > midX).ToList();

            // find closest pair points from left and right recursive
            double leftDist= GetClosestPointsRecursive(leftX, leftY);
            double rightDist = GetClosestPointsRecursive(rightX, rightY);


            // get the closer distance from each half
            double closestDistance = double.MinValue;
            // if not left then right
            if (leftDist == double.MinValue)
            {
                closestDistance = rightDist;
            }
            // if not right then left
            else if (rightDist == double.MinValue)
            {
                closestDistance = leftDist;
            }
            // the closest distance is whatever is less than the other
            else
            {
                closestDistance = leftDist < rightDist ? leftDist : rightDist;
            }

            // think about those that cross the midpoint line
            // get sub array of all the points whose X is closer than distance to the mid point (look for something smaller than current smallest distance)
            return FindDistanceCloserToDivider(sortedByY, midX, closestDistance);
        }

        private double FindDistanceCloserToDivider(List<Point> sortedByY, double middleX, double closestDistance)
        {
            // find all the points within minimum distance of middle X
            var pointsInDividerArea = sortedByY.Where(p => Math.Abs(middleX - p.X) <= closestDistance);

            // Check all points sorted by y
            for (int i = 0; i < sortedByY.Count - 1; i++)
            {
                // need to check only the 7 points
                for (int j = i + 1; j < i + 8 && j < sortedByY.Count; j++)
                {
                    // Check if this pair of points is closer than current clostestDistance
                    double distance = Distance(sortedByY[i], sortedByY[j]);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                    }
                }
            }
            return closestDistance;
        }

        // calculate distance between two points
        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }
    }
}
