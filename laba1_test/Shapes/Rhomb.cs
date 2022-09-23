using System.Drawing;

namespace laba1_test.Shapes
{
    public class Rhomb : IShape
    {
        /// <summary>
        /// Ромб
        /// </summary>
        /// <param name="leftX">координата X левого угла прямоугольника, в который вписана фигурв</param>
        /// <param name="topY">координата Y левого угла прямоугольника, в который вписана фигурв</param>
        /// <param name="height">размер вертикальной диагонали</param>
        /// <param name="width">размер горизонтальной диоганали</param>
        public Rhomb(int leftX, int topY, int height, int width)
        {
            _leftTopConer = new Point(leftX, topY);
            _height = height;
            _width = width;

            _points = new Point[Size];

            // расчёт остальных точек
            CalcAnglePosition();
        }

        public Point[] GetPoints() => _points;
        public int GetHeight() => _height;
        public int GetWidth() => _width;
        public Point GetLeftTopConer() => _leftTopConer;

        public Color FillColor { get; set; }
        public Color LineColor { get; set; }
        public int LineWidth { get; set; }

        public void Offset(int dx, int dy)
        {
            // смещение верхнего левого угла
            _leftTopConer.Offset(dx, dy);

            // перерасчёт остальных чисел
            CalcAnglePosition();
        }

        // перерасчёт координат относительно левого верхнего угла
        private void CalcAnglePosition()
        {
            _points[0].X = _leftTopConer.X;
            _points[0].Y = _leftTopConer.Y + _height / 2;

            _points[1].X = _leftTopConer.X + _width / 2;
            _points[1].Y = _leftTopConer.Y;

            _points[2].X = _leftTopConer.X + _width;
            _points[2].Y = _leftTopConer.Y + _height / 2;
            
            _points[3].X = _leftTopConer.X + _width / 2;
            _points[3].Y = _leftTopConer.Y + _height;
        }

        // количество точек
        private const int Size = 4;

        // массив точек
        private readonly Point[] _points;
        
        // для удобства храниться левый верхний угол
        private Point _leftTopConer;
        
        // высота и ширина
        private int _height;
        private int _width;
    }
}
