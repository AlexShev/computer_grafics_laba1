using laba1_test.Shapes;
using laba1_test.Transforms;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace laba1_test
{
    public partial class Form1 : Form
    {
        private bool _started = false;


        // фигура для отрисовки
        private Rhomb _shape;
        // объект ответсвенный за перемещение фигуры
        private ShapeMover _shapeMover;
        // объект ответственный за перекрашивание фигуры
        private ShapeColorСhanger _shapeColorСhanger;
        // объект для отрисовки фигуры
        private GraphicsEngine _graphicsEngine;

        private Mutex _mutex = new Mutex();

        public Form1()
        {
            InitializeComponent();

            _shape = new Rhomb(0, 0, 100, 100);
            _shape.LineWidth = 4;
            _shape.FillColor = Color.RoyalBlue;
            _shape.LineColor = Color.Black;

            _shapeMover = new ShapeMover(_shape, 10, 45);
            _shapeMover.Height = pictureBox1.Height;
            _shapeMover.Width = pictureBox1.Width;

            _shapeColorСhanger = new ShapeColorСhanger(_shape, new Color[] {
                Color.Red,
                Color.Green,
                Color.Black,
                Color.Yellow,
            });

            new Thread(() =>
            {
                while (true)
                {
                    _shapeColorСhanger.Transform();
                    Thread.Sleep(1000);
                }
            }).Start();

            // надо захерачить мьютекс
            //new Thread(() =>
            //{
            //    while (true)
            //    {
                //// Стирание фигуры заданным методом
                //_graphicsEngine.Clear();

                //// Перемещение фигуры
                //_shapeMover.Transform();

                ////Отрисовка фигуры цветом RoyalBlue
                //_graphicsEngine.FillShape(_shape);
                //_graphicsEngine.DrowShape(_shape);
            //    }


            //}).Start();

            _graphicsEngine = new GraphicsEngine(new Bitmap(pictureBox1.Width, pictureBox1.Height));
            _graphicsEngine.BackColor = Color.White;

            timer1.Interval = 1000 / (SpeedBar.Value * 10);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            
            if (!_started)
            {
                timer1.Enabled = true;
                
                _graphicsEngine.Clear();
                _graphicsEngine.SetDefalte();
                _graphicsEngine.FillShape(_shape);

                button.Text = "стоп";

                _started = true;
            }
            else
            {
                timer1.Enabled = false;
                button.Text = "старт";

                _started = false;
            }

        }

        private void Sp_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = 1000 / (SpeedBar.Value * 10);
        }

        private void Dx_Scroll(object sender, EventArgs e)
        {
            _shapeMover.SetStep(StepBar.Value);
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;
            pictureBox1.Height = control.Height;
            pictureBox1.Width = control.Width - panel1.Width;

            if (pictureBox1.Height > 0 && pictureBox1.Width > 0)
            {
                _graphicsEngine.Bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                _graphicsEngine.SetDefalte();

                _shapeMover.Height = pictureBox1.Height;
                _shapeMover.Width = pictureBox1.Width;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Стирание фигуры заданным методом
            _graphicsEngine.Clear();

            // Перемещение фигуры
            _shapeMover.Transform();

            //Отрисовка фигуры цветом RoyalBlue
            _graphicsEngine.FillShape(_shape);
            _graphicsEngine.DrowShape(_shape);

            pictureBox1.Image = _graphicsEngine.Bitmap;
        }
    }
}