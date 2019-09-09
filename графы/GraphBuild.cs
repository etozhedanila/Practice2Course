using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace графы
{
    public partial class GraphBuild : Form
    {
        public GraphBuild()
        {
            InitializeComponent();
        }

        Table table;
        Matrix matr;

        //создание таблицы для ввода матрицы смежности
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Width = 0;
                dataGridView1.Height = 0;
                uint nodes = UInt32.Parse(textBox1.Text);
                if (nodes == 0)
                {
                    MessageBox.Show("!= 0");

                }
                else
                {

                    table = new Table(nodes, dataGridView1);
                    table.createTable();

                    button2.Visible = true;
                    button3.Visible = true;

                    if (nodes > 6)
                    {
                        this.Width = dataGridView1.Location.X + dataGridView1.Width + 100;
                        this.Height = dataGridView1.Location.Y + dataGridView1.Height + 100;
                        button3.Location = new Point(button3.Location.X, button3.Location.Y + 100);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Meh.");
            }
        }

        //считать матрицу смежности и создать граф
        private void button2_Click(object sender, EventArgs e)
        {
            int nodes = dataGridView1.Columns.Count;
            try
            {
                matr = new Matrix(table);
                matr.createGraph();
                button4.Visible = true;
                groupBox1.Visible = true;
                         
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный формат матрицы смежности!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                   
        }

        //сброс
        private void button3_Click(object sender, EventArgs e)
        {
            table.deleteTable();
            table = null;
            dataGridView1.Visible = false;
            textBox1.Text = "";
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            groupBox1.Visible = false;
        }

        //показать граф
        private void button4_Click(object sender, EventArgs e)
        {
            
            Graph graph = new Graph();
            graph.Show();

        }

        //выполнить задание
        private void button5_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                //внутр уст
                string res = "";
                matr.task1();
                foreach(char ch in matr.inStSub)
                {
                    res += ch + " ";
                }
                MessageBox.Show(res);
                Task t = new Task();
                t.Show();
            }

            if (radioButton2.Checked)
            {
                /*string res = "";
                matr.task2();
                foreach (char ch in matr.outStSub)
                {
                    res += ch + " ";
                }
                MessageBox.Show(res);*/
                matr.test();
            }

            if (radioButton3.Checked)
            {
                matr.task3();
            }
            if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked)
            {
                MessageBox.Show("Выберете задание!");
            }
        }
    }
}
