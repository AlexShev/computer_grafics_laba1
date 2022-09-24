using System.Drawing;

namespace laba1_test.Shapes
{
    // интерфейс фигуры
    public interface IShape
    {
        // получить массив точек - задающих фигуру на плоскости
        PointF[] GetPoints();
        // получить высоту
        float GetHeight();
        // получить ширину
        float GetWidth();
        // Получить левый верхний угол (мамые наименьшие координаты) - может не быть частью фигуры
        PointF GetLeftTopConer();

        // Цвет фигуры внутри
        Color FillColor { get; set; }
        // Цвет граници фигуры
        Color LineColor { get; set; }
        // толщина линии
        float LineWidth { get; set; }

        // сместить фигуру на данный вектор
        void Offset(float dx, float dy);
    }
}
