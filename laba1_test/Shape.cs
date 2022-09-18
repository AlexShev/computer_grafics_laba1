
using System.Drawing;

namespace laba1_test
{
    public class Shape
    {
        public Shape(int centerX, int centerY, int radius)
        {
            _center = new Point(centerX, centerY);
            _radius = radius;

            _points = new Point[Size];

            CalcAnglePosition();
        }

        public void SetRadius(int newRadius)
        {
            _radius = newRadius;
        }

        public void MoveTo(Point center)
        {
            _center = center;
            CalcAnglePosition();
        }

        public Point[] GetPoints() => _points;
        public int GetRadius() => _radius;
        public Point GetCenter() => _center;

        private void CalcAnglePosition()
        {
            _points[0].X = _center.X;
            _points[0].Y = _center.Y + _radius;

            _points[1].X = _center.X - _radius;
            _points[1].Y = _center.Y;

            _points[2].X = _center.X;
            _points[2].Y = _center.Y - _radius;
            
            _points[3].X = _center.X + _radius;
            _points[3].Y = _center.Y;
        }

        private const int Size = 4;

        private readonly Point[] _points;
        private Point _center;
        private int _radius;
    }
}
