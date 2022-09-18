using System.Collections.Generic;
using System.Drawing;

namespace laba1_test
{
    public class GraphicsEngine
    {
        public enum ClearningMode
        {
            ScreenClear,
            ShapeClear
        }

        public GraphicsEngine(Graphics grafic)
        {
            _grafic = grafic;
            Clearning = ClearningMode.ScreenClear;
            
            _shapes = new Stack<Shape>();
        }

        public void SetRegion(int h, int w)
        {
            _grafic.Clip = new Region(new Rectangle(0, 0, w, h));
        }

        public void SetGrafics(Graphics grafic)
        {
            _grafic.Dispose();

            _grafic = grafic;
        }

        public void Draw(Shape shape, Color color)
        {
            SolidBrush brush = new SolidBrush(color);

            if (Clearning != ClearningMode.ScreenClear)
            {
                _shapes.Push(shape);
            }
            //Отрисовка фигуры по заданным координатам
            _grafic.FillPolygon(brush, shape.GetPoints());
        }

        public void Draw(Shape shape)
        {
            SolidBrush brush = new SolidBrush(Color.White);

            //Отрисовка фигуры по заданным координатам
            _grafic.FillPolygon(brush, shape.GetPoints());
        }

        public void Clearing()
        {
            // Метод стирания определяется пользователем
            switch (Clearning)
            {
                case ClearningMode.ScreenClear:
                    _grafic.Clear(Color.White);
                    break;
                case ClearningMode.ShapeClear:
                    while (_shapes.Count > 0)
                    {
                        Draw(_shapes.Pop());
                    }
                    break;
                default:
                    break;
            }

        }

        private Graphics _grafic;
        public ClearningMode Clearning { set; get; }
        private readonly Stack<Shape> _shapes;
    }
}
