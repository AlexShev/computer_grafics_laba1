using laba1_test.Shapes;
using laba1_test.Transforms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba1_test.Transforms
{
    public class ShapeColorСhanger : IShapeTransformer
    {
        private Color[] _colors;
        private int _position;
        private IShape _shape;

        public ShapeColorСhanger(IShape shape, Color[] colors)
        {
            _shape = shape;
            _colors = colors;
            _position = 0;
        }

        public void Transform()
        {
            _shape.LineColor = _colors[_position];

            ++_position;
            _position %= _colors.Length;
        }
    }
}
