using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPClient
{
    public partial class Game : Form
    {
        public int size = 50;
        public int level = 1;
        public int thepoint = 1;
        List<Point> points = new List<Point>();
        public Game()
        {
            InitializeComponent();

            typeof(DataGridView).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty, null, grid, new object[] { true });

            points.Add(new Point(0, 0));
            points.Add(new Point(0, 1));
            points.Add(new Point(0, 2));

            SetCanvas();
            SetTarget();

            MoveSnake(points.LastOrDefault());
        }

        enum Directions { Up, Down, Right, Left};
        Directions currentDirection = Directions.Down;

        void SetCanvas ()
        {
            grid.RowTemplate.Height = grid.Height/size;
            for (int i = 0; i < size; i++) grid.Columns.Add(i.ToString(), i.ToString());
            for (int i = 0; i < size; i++) grid.Rows.Add();
            //grid.CurrentCell.Style.BackColor = grid.CurrentCell.Style.SelectionBackColor = this.BackColor;
        }

        private void grid_KeyUp(object sender, KeyEventArgs e)
        {
            if (!session.Enabled) return;
            if(e.KeyCode == Keys.Up && currentDirection != Directions.Down) currentDirection = Directions.Up;
            if (e.KeyCode == Keys.Down && currentDirection != Directions.Up) currentDirection = Directions.Down;
            if (e.KeyCode == Keys.Right && currentDirection != Directions.Left) currentDirection = Directions.Right;
            if (e.KeyCode == Keys.Left && currentDirection != Directions.Right) currentDirection = Directions.Left;
        }

        Random ran = new Random();

        void SetTarget()
        {
            grid.Rows[ran.Next(1, size-1)].Cells[ran.Next(1, size-1)].Style.BackColor = Color.Red;
            //Console.WriteLine(ran.Next(1, size-1));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            grid.Focus();
            if (button2.Text == "Pause game")
            {
                button2.Text = "Start game";
                session.Stop();
            }
            else
            {
                button2.Text = "Pause game";
                session.Start();
            }
        }

        Point headPoss = new Point(0,0);

        private void session_Tick(object sender, EventArgs e)
        {
            try
            {
                var point = points.LastOrDefault();

                if(currentDirection == Directions.Up) point.X--;
                if(currentDirection == Directions.Down) point.X++;
                if(currentDirection == Directions.Left) point.Y--;
                if(currentDirection == Directions.Right) point.Y++;

                MoveSnake(point); 
            }
            catch
            {
                session.Stop();
                MessageBox.Show("Game over");
                ResetGame();
            }
        }

        void ResetGame()
        {
            points.Clear();
            foreach (DataGridViewRow row in grid.Rows)
            {
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    //row.Cells[col.Index].Style.BackColor = Color.Green; //doesn't work
                    //col.Cells[row.Index].Style.BackColor = Color.Green; //doesn't work
                    grid.Rows[row.Index].Cells[col.Index].Style.BackColor = Color.FromArgb(19, 9, 46); //doesn't work
                }
            }
            grid.Refresh();
            SetTarget();
            points.Add(new Point(0, 0));
            points.Add(new Point(0, 1));
            points.Add(new Point(0, 2));
            currentDirection = Directions.Down;
            MoveSnake(points.LastOrDefault());

            
            button2.Text = "Start game";
        }

        void MoveSnake(Point newPoint)
        {
            bool over = grid.Rows[newPoint.X].Cells[newPoint.Y].Style.BackColor == title.BackColor;
            foreach (var point in points)
                grid.Rows[point.X].Cells[point.Y].Style.BackColor = grid.Rows[point.X].Cells[point.Y].Style.SelectionBackColor = Color.FromArgb(19, 9, 46);

            if (grid.Rows[newPoint.X].Cells[newPoint.Y].Style.BackColor == Color.Red)
            {
                label2.Text = "Point: " + (points.Count-2).ToString();
                SetTarget();
            }
            else
            {
                points.RemoveAt(0);
            }

            points.Add(newPoint);

            foreach(var point in points)
                grid.Rows[point.X].Cells[point.Y].Style.BackColor = grid.Rows[point.X].Cells[point.Y].Style.SelectionBackColor = title.BackColor;

            var p = points.LastOrDefault();
            grid.CurrentCell = grid.Rows[p.X].Cells[p.Y];

            if (over)
            {
                session.Stop();
                //throw new Exception("Game Over");
                MessageBox.Show("Game Over");
                ResetGame();
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            session.Stop();
            this.Close();
        }
    }
}
