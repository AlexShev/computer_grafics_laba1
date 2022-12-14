using System;
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
        public Rhomb(float leftX, float topY, float height, float width)
        {
            _leftTopConer = new PointF(leftX, topY);
            _height = height;
            _width = width;

            _points = new PointF[Size];

            // расчёт остальных точек
            CalcAnglePosition();
        }

        public PointF[] GetPoints() => _points;
        public float GetHeight() => _height + _halfLineProjectionWidthOY * 2;
        public float GetWidth() => _width + _halfLineProjectionWidthOX * 2;
        public PointF GetLeftTopConer() => _leftTopConer;

        public Color FillColor { get; set; }
        public Color LineColor { get; set; }
        public float LineWidth
        {
            get => _lineWidth; 
            
            set
            {
                _lineWidth = value;

                _halfLineProjectionWidthOY =
                    _lineWidth / (float)Math.Pow(1.0/(1 + (_height * _height) / (_width * _width)), 0.5) / 2;

                _halfLineProjectionWidthOX =
                    _lineWidth / (float)Math.Pow(1.0 / (1 + (_width * _width) / (_height * _height)), 0.5) / 2;


                // смещение всей фигуры на половину проекции ширены линии
                _leftTopConer.X += _halfLineProjectionWidthOX;
                _leftTopConer.Y += _halfLineProjectionWidthOY;
                CalcAnglePosition();

                // возвращение положения назад
                _leftTopConer.X -= _halfLineProjectionWidthOX;
                _leftTopConer.Y -= _halfLineProjectionWidthOY;
            }
        }

        public void Offset(float dx, float dy)
        {
            // смещение верхнего левого угла
            _leftTopConer.X += dx;
            _leftTopConer.Y += dy;

            // перерасчёт остальных чисел
            for (int i = 0; i < 4; i++)
            {
                _points[i].X += dx;
                _points[i].Y += dy;
            }
        }

        // рерасчёт координат относительно левого верхнего угла
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
        private readonly PointF[] _points;

        // для удобства храниться левый верхний угол
        private PointF _leftTopConer;

        // высота и ширина
        private float _height;
        private float _width;
        private float _lineWidth;


        // половина проекции ширины линии на оси
        private float _halfLineProjectionWidthOX;
        private float _halfLineProjectionWidthOY;
    }
}
