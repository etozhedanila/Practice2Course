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
    public partial class Task : Form
    {
        public Task()
        {
            InitializeComponent();
        }

        private void Task_Load(object sender, EventArgs e)
        {
            pictureBox1.Load("Task.png");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
