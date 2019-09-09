using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace графы
{
    class Table
    {
        private uint cols, rows;
        public uint COLS
        {
            get { return cols; } 
        }

        private DataGridView dataGridView1;
        public DataGridView DGV
        {
            get { return dataGridView1; }
        }


        public Table(uint n, DataGridView t)
        {
            cols = rows = n;
            dataGridView1 = t;
            
        }

        public void initDGV()
        {
            for(int i = 0; i < cols ; i++)
            {
                for (int j = 0; j < cols ; j++)
                {
                    dataGridView1[i, j].Value = "0";
                }
            }
        }

        public void changeSize()
        {
            dataGridView1.Width = dataGridView1.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) +
                       dataGridView1.RowHeadersWidth + 2;

            dataGridView1.Height = dataGridView1.Rows.GetRowsHeight(DataGridViewElementStates.Visible) +
                       dataGridView1.ColumnHeadersHeight;
        }

        public void createTable()
        {
            dataGridView1.RowHeadersWidth = 50;
            dataGridView1.AllowUserToAddRows = false;

            char name = 'A';
            for (int i = 0; i < cols; i++)
            {
                var tmp = new DataGridViewColumn();
                tmp.HeaderText = name.ToString();
                tmp.Width = 40;
                tmp.Name = name.ToString();
                tmp.CellTemplate = new DataGridViewTextBoxCell();

                dataGridView1.Columns.Add(tmp);

                name++;
            }

            name = 'A';
            for (int i = 0; i < cols; i++)
            {
                var tmp = new DataGridViewRow();

                tmp.HeaderCell.Value = name.ToString();
                dataGridView1.Rows.Add(tmp);
                name++;
            }

            changeSize();

            initDGV();
            
            dataGridView1.Visible = true;
        }

        public void deleteTable()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();            
            dataGridView1.Refresh();
            cols = rows = 0;
        }
    }
}
