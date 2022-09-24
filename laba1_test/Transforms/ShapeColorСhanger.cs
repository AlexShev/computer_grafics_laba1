using laba1_test.Shapes;
using System.Drawing;

namespace laba1_test.Transforms
{
    /// <summary>
    /// Класс для смены цвета линии фигуры
    /// </summary>
    public class ShapeColorСhanger : IShapeTransformer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shape">фигура у которой будет меняться цвет линии</param>
        /// <param name="colors">цвета линии</param>
        public ShapeColorСhanger(IShape shape, Color[] colors)
        {
            _shape = shape;
            _colors = colors;
            _position = 0;
        }

        /// <summary>
        /// Смена цвета
        /// </summary>
        public void Transform()
        {
            _shape.LineColor = _colors[_position];

            // зацикливание
            ++_position;
            _position %= _colors.Length;
        }

        // фигура
        private IShape _shape;

        // цвета линии
        private Color[] _colors;

        // позиция цвета в массиве цветов
        private int _position;
    }
}
