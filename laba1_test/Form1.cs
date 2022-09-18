using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace laba1_test
{
    public partial class Form1 : Form
    {
        // фигура для отрисовки
        private Shape _shape;
        // объект ответсвенный за трансформирования фигуры
        private ShapeTransformer _transformer;
        // объект для отрисовки фигуры
        private GraphicsEngine _graphicsEngine;

        public Form1()
        {
            InitializeComponent();

            _shape = new Shape(100, 100, 10);
            _transformer = new ShapeTransformer(_shape, 20, 309, 1);
            _transformer.Height = pictureBox1.Height;
            _transformer.Width = pictureBox1.Width;

            _graphicsEngine = new GraphicsEngine(pictureBox1.CreateGraphics());

            comboBox1.SelectedIndex = 0;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Contains(comboBox1.Text))
            {
                timer1.Enabled = true;
                comboBox1.Enabled = false;
                
                _graphicsEngine.Clearing();
                _graphicsEngine.Draw(_shape, Color.RoyalBlue);
            }
            else
            {
                MessageBox.Show("Сначала выберите метод стирания");
            }
        }

        private void Sp_Scroll(object sender, EventArgs e)
        {
            timer1.Interval = 1000 / SpeedBar.Value;
        }

        private void Dx_Scroll(object sender, EventArgs e)
        {
            _transformer.SetStep(StepBar.Value);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            comboBox1.Enabled = true;
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;
            pictureBox1.Height = control.Height;
            pictureBox1.Width = control.Width - panel1.Width;

            _graphicsEngine.SetGrafics(pictureBox1.CreateGraphics());

            _transformer.Height = pictureBox1.Height;
            _transformer.Width = pictureBox1.Width;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Стирание фигуры заданным методом
            _graphicsEngine.Clearing();

            // Определение расстояния от центра ромба до вершин
            _transformer.ChangeRadius();

            // Перемещение фигуры
            _transformer.MoveShape();

            //Отрисовка фигуры цветом RoyalBlue
            _graphicsEngine.Draw(_shape, Color.RoyalBlue);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _graphicsEngine.Clearing();

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    _graphicsEngine.Clearning = GraphicsEngine.ClearningMode.ScreenClear;
                    break;
                case 1:
                    _graphicsEngine.Clearning = GraphicsEngine.ClearningMode.ShapeClear;
                    break;
            }
            
        }
    }
}