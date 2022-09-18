using System;
using System.Drawing;

namespace laba1_test
{
    public class ShapeTransformer
    {
        public ShapeTransformer(Shape shape, int dx, int angle, int sign)
        {
            _dx = dx;
            _tg = Math.Tan(angle);
            _dy = Convert.ToInt32(_tg * Math.Abs(dx));

            _sign = sign;
            Shape = shape;
        }

        public void SetStep(int dx)
        {
            // Определение шага по оси ОХ
            // Если шаг был отрицательным, значит и новое значение будет отрицательным
            // Таким образом сохраняется направление движения
            _dx = (_dx > 0) ? dx : -dx;

            // Определение шага по оси ОУ
            // Если шаг был отрицательным, значит и новое значение будет отрицательным
            // Таким образом сохраняется направление движения
            _dy = ((_dy > 0) ? 1 : -1) * Convert.ToInt32(_tg * dx);
        }

        public void ChangeRadius()
        {
            int radius = Shape.GetRadius();
            // Определение расстояния от центра ромба до вершин
            if (radius == 35)
                _sign *= -1;
            if (radius == 5)
                _sign *= -1;

            Shape.SetRadius(radius + 5 * _sign);
        }

        public void MoveShape()
        {
            Point center = Shape.GetCenter();
            int radius = Shape.GetRadius();

            if (center.X + _dx + radius > Width) // Касание правой границы
            {
                _dx *= -1;
                center.X = Width - radius;
            }
            else if (center.X + _dx - radius < 0) // Касание левой границы
            {
                _dx *= -1;
                center.X = radius;
            }
            else
            {
                center.X += _dx;
            }

            if (center.Y + _dy - radius < 0) // Касание верхней границы
            {
                _dy *= -1;
                center.Y = radius;
            }
            else if (center.Y + _dy + radius > Height) // Касание нижней границы
            {
                _dy *= -1;
                center.Y = Height - radius;
            }
            else
            {
                center.Y += _dy;
            }

            Shape.MoveTo(center);
        }

        public Shape Shape { set; get; }
        public int Height { set; get; }
        public int Width { set; get; }

        private int _dx;
        private int _dy;
        private int _sign;
        private double _tg;
    }
}
