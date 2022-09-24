using System.Drawing;

namespace laba1_test.Shapes
{
    public class Rectangle : IShape
    {
        /// <summary>
        /// Ромб
        /// </summary>
        /// <param name="leftX">координата X левого угла прямоугольника, в который вписана фигурв</param>
        /// <param name="topY">координата Y левого угла прямоугольника, в который вписана фигурв</param>
        /// <param name="height">размер вертикальной диагонали</param>
        /// <param name="width">размер горизонтальной диоганали</param>
        public Rectangle(float leftX, float topY, float height, float width)
        {
            _leftTopConer = new PointF(leftX, topY);
            _height = height;
            _width = width;

            _points = new PointF[SIZE];

            // расчёт остальных точек
            CalcPointPosition();
        }

        public PointF[] GetPoints() => _points;
        public float GetHeight() => _height + _lineWidth;
        public float GetWidth() => _width + _lineWidth;
        public PointF GetLeftTopConer() => _leftTopConer;


        public Color FillColor { get; set; }
        public Color LineColor { get; set; }
        public float LineWidth
        {
            get => _lineWidth;

            set
            {
                _lineWidth = value;

                // смещение всей фигуры на половину проекции ширены линии
                CalcPointPosition();
            }
        }



        public void Offset(float dx, float dy)
        {
            // смещение верхнего левого угла
            _leftTopConer.X += dx;
            _leftTopConer.Y += dy;

            // перерасчёт остальных чисел
            for (int i = 0; i < SIZE; i++)
            {
                _points[i].X += dx;
                _points[i].Y += dy;
            }
        }


        // перасчёт координат относительно левого верхнего угла
        private void CalcPointPosition()
        {
            var halfLineWidth = _lineWidth / 2;

            _points[0].X = _leftTopConer.X + halfLineWidth;
            _points[0].Y = _leftTopConer.Y + halfLineWidth;

            _points[1].X = _points[0].X + _width;
            _points[1].Y = _points[0].Y;

            _points[2].X = _points[1].X;
            _points[2].Y = _points[1].Y + _height;

            _points[3].X = _points[0].X;
            _points[3].Y = _points[2].Y;
        }


        // количество точек
        private const int SIZE = 4;

        // массив точек
        private readonly PointF[] _points;

        // для удобства храниться левый верхний угол
        private PointF _leftTopConer;

        // высота и ширина
        private float _height;
        private float _width;
        private float _lineWidth;
    }
}
