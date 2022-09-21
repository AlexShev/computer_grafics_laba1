using laba1_test.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace laba1_test
{
    public class GraphicsEngine : IDisposable
    {
        public GraphicsEngine(Bitmap bitmap)
        {
            Bitmap = bitmap;

            _filledPolygon = new Stack<IShape>();
            _linePolygon = new Stack<IShape>();

            _pen = new Pen(BackColor);
            _brush = new SolidBrush(BackColor);
        }

        public void FillShape(IShape shape)
        {
            _brush.Color = shape.FillColor;

            _filledPolygon.Push(shape);

            //Отрисовка фигуры по заданным координатам
            _grafic.FillPolygon(_brush, shape.GetPoints());
        }

        public void DrowShape(IShape shape)
        {
            _pen.Color = shape.LineColor;
            _pen.Width = shape.LineWidth;

            _linePolygon.Push(shape);

            //Отрисовка фигуры по заданным координатам
            _grafic.DrawPolygon(_pen, shape.GetPoints());
        }

        public void ClearRegion(IShape shape)
        {
            _brush.Color = BackColor;
            
            _grafic.FillPolygon(_brush, shape.GetPoints());
        }

        public void ClearLine(IShape shape)
        {
            _pen.Color = BackColor;
            _pen.Width = shape.LineWidth;

            _grafic.DrawPolygon(_pen, shape.GetPoints());
        }

        public void Clear()
        {
            while (_filledPolygon.Count > 0)
            {
                ClearRegion(_filledPolygon.Pop());
            }
            
            while (_linePolygon.Count > 0)
            {
                ClearLine(_linePolygon.Pop());
            }
        }

        public void SetDefalte()
        {
            _grafic.Clear(BackColor);
        }

        public void Dispose()
        {
            _grafic?.Dispose();
            _buffer?.Dispose();

            _pen?.Dispose();
            _brush?.Dispose();
        }

        public Color BackColor { set; get; } = Color.White;
        
        public Bitmap Bitmap
        {
            set
            {
                _buffer = value;
                _grafic?.Dispose();

                _filledPolygon?.Clear();
                _linePolygon?.Clear();

                _grafic = Graphics.FromImage(_buffer);
                _grafic.Clear(BackColor);
            }

            get => new Bitmap(_buffer);
        }

        private readonly Pen _pen;
        private readonly SolidBrush _brush;

        private Graphics _grafic;
        private readonly Stack<IShape> _filledPolygon;
        private readonly Stack<IShape> _linePolygon;

        private Bitmap _buffer;
    }
}
