using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace графы
{
    class Matrix
    {
        private uint cols, rows;
        private Table table;
        private int[,] a;
        private string nodes = "abcdefghijklmnopqrstuvwxyz";

        public HashSet<char> inStSub,outStSub;

       

        public Matrix(Table t)
        {
            table = t;
            cols = rows = table.COLS;
            a = new int[cols, rows];
            readMatrix();
            inStSub = new HashSet<char>();
            outStSub = new HashSet<char>();
        }

        //считываем матрицу смежности из таблицы
        public void readMatrix()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    a[i, j] = (int)Convert.ChangeType(table.DGV[j, i].Value, typeof(int));
                }
            }
        }

        //запускаем команду dot и преобразуем наш граф в png картинку
        public void output()
        {
            System.Diagnostics.Process.Start("cmd", "/c dot -Tpng Graph.txt -o graf.png");
        }

        public void createGraph()
        {
            //создаем txt файл и записываем туда код для создания графа
            File.WriteAllText("Graph.txt", "digraph{ \n");
            //added
            for (int i = 0; i < cols; i++)
            {
                File.AppendAllText("Graph.txt", nodes[i].ToString() + "\n");
            }
            //
            for (int i = 0; i < cols; i++ )
            {
                for (int j = 0; j < cols; j++ )
                {
                    if (a[i,j] != 0)
                    {
                        File.AppendAllText("Graph.txt", nodes[i] + "->" + nodes[j] + "\n");
                    }
                }
            }
            File.AppendAllText("Graph.txt", "}");
            output();
        }    
        
        //внутр уст internally stable subset
        public void task1()
        {
            File.WriteAllText("inStSb.txt", "");
            for (int i = 0; i < cols; i++ )
            {
                
                HashSet<char> tmp = new HashSet<char>();
                
                for (int j = 0; j < cols; j++)
                {
                    if (a[i,j] == 0)
                    {
                        int flag = 1;
                        for (int k = 0; k < cols; k++)
                        {
                            if(a[j,k] != 0 && a[i,k] == 0)
                            {
                                flag = 0;
                            }
                        }
                        if(flag == 1)
                        {
                            tmp.Add(nodes[j]);
                        }
                    }
                }
                
                if(tmp.Count > inStSub.Count)
                {
                    inStSub = tmp;
                }
               
                foreach (char c in tmp)
                {
                    File.AppendAllText("inStSb.txt", c.ToString() );
                }
                File.AppendAllText("inStSb.txt", "\n");

            }

            File.WriteAllText("task.txt", "digraph{ \n");
            foreach (char c in inStSub)
            {
                File.AppendAllText("task.txt", c.ToString() + ";");
            }
            File.AppendAllText("task.txt", "}");
            System.Diagnostics.Process.Start("cmd", "/c dot -Tpng task.txt -o Task.png");
        }

        //test vneshne ustoychiv
        public void test()
        { 
            int[,] f = new int[cols, rows]; //f = a V e
            
            for(int i = 0; i < cols; i++)
            {
                for(int j = 0; j < rows; j++)
                {
                    f[i, j] = a[i, j];
                    if (i == j) f[i, j] = 1;
                }
            }

            //формируем днф
            string expr = ""; //днф
            int flag = 0;

            for(int i = 0; i < cols; i++)
            {
                flag = 0;
                expr += "(";
                for (int j = 0; j < rows; j++)
                {
                    
                    if (f[i, j] == 1)
                    {
                        if (flag != 0)
                            expr += " | ";
                        //expr += nodes[j];
                        expr += j+1;
                        flag = 1;
                    }

                }
                expr += ")";
                if (i != cols - 1)
                    expr += " & ";
            }

            File.WriteAllText("testTask2.txt", expr);


            //таблица истиности
            int[,] truthT = new int[(int)Math.Pow(2, cols),cols];
            for(int i = 0; i < truthT.GetLength(0); i++)
            {
                int tmp = i;
                for(int j = (int)cols - 1; j>=0; j-- )
                {
                    truthT[i, j] = tmp & i;
                    tmp >>= 1;
                }
            }



            int[] res = new int[(int)Math.Pow(2, cols)];
            
            for(int i = 0; i < truthT.GetLength(0); i++)
            {
                for(int j = 0; j < cols; j++)
                {

                }
            }
            
        } 

        //внешне устойчивые подмножества
        public void task2()
        {
           

            for (int i = 0; i < cols; i++)
            {
                 //множество путей из вершины Гv
                HashSet<char> gamma = new HashSet<char>();

                for(int j = 0; j < cols; j++)
                {
                    if (a[i, j] != 0) gamma.Add(nodes[j]);
                }

                if (gamma.Count == 0) outStSub.Add(nodes[i]);
            }


        }

        private int[] findL(int[,] tmp)
        {
            
            int[] l = new int[cols];
            for (int i = 0; i < cols; i++)
            {
                l[i] = 0;
                for (int j = 0; j < cols; j++)
                {
                    l[i] += tmp[i, j];
                }

            }
            return l;
        }

        private int[,] updateTmp(int[,] tmp, int num)
        {
            for (int i = 0; i < cols; i++)
            {               
                 tmp[i, num] = 0;
            }
            return tmp;
        }

        public void task3()
        {
            File.WriteAllText("levels.txt", "");
            int[,]tmp = new int[cols, cols];
            int[] l = new int[cols];
            int num=-1;
            for (int i = 0; i < cols; i++)
            {
                for(int j = 0; j<cols; j++)
                {
                    tmp[i, j] = a[i, j];
                }
            }

            l = findL(tmp);
            for(int i = 0; i < cols; i++)
            {
                if (l[i]==0)
                {
                    num = i; 
                }
            }
            tmp = updateTmp(tmp, num);
            
        }
    }
}
