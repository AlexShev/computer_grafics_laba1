using System.Drawing;

namespace laba1_test.Shapes
{
    public class Rhomb : IShape
    {
        public Rhomb(int leftX, int topY, int height, int width)
        {
            _leftConer = new Point(leftX, topY);
            _height = height;
            _width = width;

            _points = new Point[Size];

            CalcAnglePosition();
        }

        public Point[] GetPoints() => _points;
        public int GetHeight() => _height;
        public int GetWidth() => _width;
        public Point GetLeftTopConer() => _leftConer;

        public Color FillColor { get; set; }
        public Color LineColor { get; set; }
        public int LineWidth { get; set; }

        public void Offset(int dx, int dy)
        {
            _leftConer.Offset(dx, dy);

            CalcAnglePosition();
        }


        private void CalcAnglePosition()
        {
            _points[0].X = _leftConer.X;
            _points[0].Y = _leftConer.Y + _height / 2;

            _points[1].X = _leftConer.X + _width / 2;
            _points[1].Y = _leftConer.Y;

            _points[2].X = _leftConer.X + _width;
            _points[2].Y = _leftConer.Y + _height / 2;
            
            _points[3].X = _leftConer.X + _width / 2;
            _points[3].Y = _leftConer.Y + _height;
        }

        private const int Size = 4;

        private readonly Point[] _points;
        private Point _leftConer;
        
        private int _height;
        private int _width;
    }
}
