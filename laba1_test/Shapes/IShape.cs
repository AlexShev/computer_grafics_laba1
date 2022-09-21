using System.Drawing;

namespace laba1_test.Shapes
{
    public interface IShape
    {
        Point[] GetPoints();
        int GetHeight();
        int GetWidth();
        Point GetLeftTopConer();

        Color FillColor { get; set; }
        Color LineColor { get; set; }
        int LineWidth { get; set; }

        void Offset(int dx, int dy);
    }
}
