using ClosestPoint.Services;
using ClosestPoints.Models;
using System;
using System.Collections.Generic;

namespace ClosestPoints.Program
{
    public class Application : IApplication
    {
        private readonly ClosestPointService _service;

        // constructor injection
        public Application(ClosestPointService service)
        {
            _service = service;
        }

        public void Run()
        {
            // data
            List<Point> points = new List<Point>()
            {
                new Point(2, 4),
                new Point(6, 7),
                new Point(3, 19),
                new Point(16, 4),
                new Point(12, 12),
                new Point(20, 18),
                new Point(11, 17),
                new Point(17, 17),
                new Point(1, 0),
                new Point(0, 20),
                new Point(15, 6)
            };
            // call service
            double minDistance = _service.GetClosestPoints(points);
            // output
            Console.WriteLine($"Closest distance is: {minDistance}");
        }
    }
}
