using laba1_test.Shapes;
using System;
using System.Drawing;

namespace laba1_test.Transforms
{
    public class ShapeMover : IShapeTransformer
    {
        public ShapeMover(IShape shape, int dx, int angle)
        {
            _dx = dx;
            _tg = Math.Tan(angle * Math.PI / 180);
            _dy = Convert.ToInt32(_tg * Math.Abs(dx));

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

        public void Transform()
        {
            Point leftTop = Shape.GetLeftTopConer();
            int dx = 0, dy = 0;
            int height = Shape.GetHeight(), width = Shape.GetWidth();

            if (leftTop.X + _dx + width > Width) // Касание правой границы
            {
                dx =  Width - (leftTop.X + _dx + width);

                _dx *= -1;
            }
            else if (leftTop.X + _dx < 0) // Касание левой границы
            {
                _dx *= -1;

                dx = _dx - leftTop.X;
            }
            else
            {
                dx = _dx;
            }

            if (leftTop.Y + _dy + height > Height) // Касание правой границы
            {
                dy = Height - (leftTop.Y + _dy + height);

                _dy *= -1;
            }
            else if (leftTop.Y + _dy < 0) // Касание левой границы
            {
                _dy *= -1;

                dy = _dy - leftTop.Y;
            }
            else
            {
                dy = _dy;
            }

            Shape.Offset(dx, dy);
        }

        public IShape Shape { set; get; }
        public int Height { set; get; }
        public int Width { set; get; }

        private int _dx;
        private int _dy;
        private double _tg;
    }
}
