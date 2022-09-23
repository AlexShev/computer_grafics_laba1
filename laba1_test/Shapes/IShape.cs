using System.Drawing;

namespace laba1_test.Shapes
{
    // интерфейс фигуры
    public interface IShape
    {
        // получить массив точек - задающих фигуру на плоскости
        Point[] GetPoints();
        // получить высоту
        int GetHeight();
        // получить ширину
        int GetWidth();
        // Получить левый верхний угол (мамые наименьшие координаты) - может не быть частью фигуры
        Point GetLeftTopConer();

        // Цвет фигуры внутри
        Color FillColor { get; set; }
        // Цвет граници фигуры
        Color LineColor { get; set; }
        // толщина линии
        int LineWidth { get; set; }

        // сместить фигуру на данный вектор
        void Offset(int dx, int dy);
    }
}
