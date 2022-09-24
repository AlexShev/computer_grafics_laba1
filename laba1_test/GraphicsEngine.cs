using laba1_test.Shapes;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace laba1_test
{
    /// <summary>
    /// Класс для работы с объектом bitmap, способный рисовать объекты типа IShape
    /// </summary>
    public class GraphicsEngine : IDisposable
    {
        /// <summary>
        /// Объект для работы с объектом bitmap
        /// </summary>
        /// <param name="width">ширена изображения</param>
        /// <param name="height">высота изображения</param>
        public GraphicsEngine(int width, int height)
        {
            Bitmap = new Bitmap(width, height);

            _filledPolygon = new Stack<IShape>();
            _linePolygon = new Stack<IShape>();

            _pen = new Pen(BackColor);
            _brush = new SolidBrush(BackColor);
        }

        /// <summary>
        /// Рисование заливки фигуры
        /// </summary>
        /// <param name="shape">фигура</param>
        public void FillShape(IShape shape)
        {
            _brush.Color = shape.FillColor;

            // запомнить для стирания
            _filledPolygon.Push(shape);

            //Отрисовка фигуры по заданным координатам
            _grafic.FillPolygon(_brush, shape.GetPoints());
        }

        /// <summary>
        /// Рисование Контура фигуры
        /// </summary>
        /// <param name="shape">фигура</param>
        public void DrowShape(IShape shape)
        {
            _pen.Color = shape.LineColor;
            _pen.Width = shape.LineWidth;

            // запомнить для стирания
            _linePolygon.Push(shape);

            //Отрисовка фигуры по заданным координатам
            _grafic.DrawPolygon(_pen, shape.GetPoints());
        }

        /// <summary>
        /// Отрисовка заполненной фигуры цветом фона
        /// </summary>
        /// <param name="shape">фигура для стирания</param>
        private void ClearRegion(IShape shape)
        {
            _brush.Color = BackColor;

            _grafic.FillPolygon(_brush, shape.GetPoints());
        }

        /// <summary>
        /// Отрисовка контура фигуры цветом фона
        /// </summary>
        /// <param name="shape">фигура для стирания</param>
        private void ClearLine(IShape shape)
        {
            _pen.Color = BackColor;
            _pen.Width = shape.LineWidth;

            _grafic.DrawPolygon(_pen, shape.GetPoints());
        }

        /// <summary>
        /// Удаление фигур путём их рисования цветом фона
        /// </summary>
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

        /// <summary>
        /// Установка фона
        /// </summary>
        public void SetDefault()
        {
            _filledPolygon?.Clear();
            _linePolygon?.Clear();
            _grafic.Clear(BackColor);
        }

        /// <summary>
        /// Закрытие ресурсов
        /// </summary>
        public void Dispose()
        {
            _grafic?.Dispose();
            _buffer?.Dispose();

            _pen?.Dispose();
            _brush?.Dispose();
        }

        /// <summary>
        /// Цвет фона (по умолчанию белый)
        /// </summary>
        public Color BackColor { set; get; } = Color.White;

        /// <summary>
        /// Свойство картинки
        /// </summary>
        public Bitmap Bitmap
        {
            // установка ного буфера
            set
            {
                // закрытие старых ресурсов
                _buffer?.Dispose();
                _buffer = value;
                _grafic?.Dispose();

                // создание нового объекта графика
                _grafic = Graphics.FromImage(_buffer);
                // установка значий по умолчанию
                SetDefault();
            }

            // возвращаем копию картинки
            get => new Bitmap(_buffer);
        }

        // карандаш
        private readonly Pen _pen;
        // кисть
        private readonly SolidBrush _brush;
        // объект графики для изменения буфера
        private Graphics _grafic;

        // очереди отрисованных фигур, используются в методе стирания
        private readonly Stack<IShape> _filledPolygon;
        private readonly Stack<IShape> _linePolygon;

        // картинка
        private Bitmap _buffer;
    }
}
