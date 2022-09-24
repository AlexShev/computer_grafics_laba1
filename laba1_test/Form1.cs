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
        // фигура для отрисовки
        private IShape _shape;
        // объект ответсвенный за перемещение фигуры
        private ShapeMover _shapeMover;
        // объект ответственный за перекрашивание фигуры
        private ShapeColorСhanger _shapeColorСhanger;
        // объект для отрисовки фигуры
        private GraphicsEngine _graphicsEngine;


        // запущена анимация или нет
        private bool _started = false;

        // мьютекс для организации доступа к значению поля GraphicsEngin
        private Mutex _mutex = new Mutex();
        // поток для изменения цвета
        private LoopingThread _colorChangerTread;
        // поток для перерасчёта координат
        private LoopingThread _shapeMoverTread;


        public Form1()
        {
            InitializeComponent();

            // создание фигуры ромб в лквом в верхем углу с диагоналями 100
            _shape = new Shapes.Rectangle(0, 0, 400, 100);
            // задание своств фигуры для отображения
            _shape.LineWidth = 10;
            _shape.FillColor = Color.RoyalBlue;
            _shape.LineColor = Color.Black;

            // объект для расчёта новых координат, ему необходимы знать размеры окна
            _shapeMover = new ShapeMover(_shape, StepBar.Value, 60);
            _shapeMover.Height = pictureBox1.Height;
            _shapeMover.Width = pictureBox1.Width;

            // объект ответственный за смену цвета, цвет меняется по очереди
            _shapeColorСhanger = new ShapeColorСhanger(_shape, new Color[] {
                Color.Red,
                Color.Green,
                Color.Black,
                Color.Yellow,
            });

            // создаётся объект для отрисовки
            _graphicsEngine = new GraphicsEngine(pictureBox1.Width, pictureBox1.Height);
            _graphicsEngine.BackColor = Color.White;

            // создание потока, который повторяет действие с определённым интервалом
            // поток ответственный за смену цвета
            _colorChangerTread = new LoopingThread(_shapeColorСhanger.Transform);
            _colorChangerTread.PauseBetween = 1000;

            // поток ответственный за перерасчёт координат
            _shapeMoverTread = new LoopingThread(()=>
            {
                // захват мьютекса
                _mutex.WaitOne();

                // Стирание фигуры заданным методом
                _graphicsEngine.Clear();

                // Перемещение фигуры
                _shapeMover.Transform();

                //Отрисовка фигуры
                _graphicsEngine.FillShape(_shape);
                _graphicsEngine.DrowShape(_shape);

                // освобождение мьютекса
                _mutex.ReleaseMutex();
            });

            _shapeMoverTread.PauseBetween = 100 / speedBar.Value;

            // установка FPS
            timer1.Interval = 1000 / (FPSbar.Value * 10);
        }

        // начало/остановка анимации
        private void StartButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            // если не анимация не запущенна
            if (!_started)
            {
                timer1.Enabled = true;
                button.Text = "стоп";

                _started = true;
                // запуск потоков для анимации
                _colorChangerTread.Start();
                _shapeMoverTread.Start();
            }
            else
            {
                timer1.Enabled = false;
                button.Text = "старт";

                _started = false;
                // остановка потоков для анимации
                _colorChangerTread.Pause();
                _shapeMoverTread.Pause();
            }

        }


        // изменение FPS картинки
        private void Sp_Scroll(object sender, EventArgs e)
        {
            // 1000 / x*10 = 100 / x
            timer1.Interval = 100 / FPSbar.Value;
        }

        private void Dx_Scroll(object sender, EventArgs e)
        {
            _shapeMover.SetStep(StepBar.Value);
        }

        private void SpeedBar_Scroll(object sender, EventArgs e)
        {
            _shapeMoverTread.PauseBetween = 100 / speedBar.Value;
        }

        // реагирование на изменение размера формы
        private void Form1_Resize(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;
            pictureBox1.Height = control.Height;
            pictureBox1.Width = control.Width - panel1.Width;

            // изменение размера формы положительны
            if (pictureBox1.Height > 0 && pictureBox1.Width > 0)
            {
                // задаём новый битмат и отрисовываем, что есть на данный момент
                _graphicsEngine.Bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                _graphicsEngine.SetDefault();

                _graphicsEngine.FillShape(_shape);
                _graphicsEngine.DrowShape(_shape);

                // выводим новую картинку
                pictureBox1.Image = _graphicsEngine.Bitmap;

                // изменяем рамки в которых вычисляется траектория
                _shapeMover.Height = pictureBox1.Height;
                _shapeMover.Width = pictureBox1.Width;
            }
        }

        // вывод буфера в картинку
        private void timer1_Tick(object sender, EventArgs e)
        {
            _mutex.WaitOne();
            pictureBox1.Image = _graphicsEngine.Bitmap;
            _mutex.ReleaseMutex();
        }
    }
}