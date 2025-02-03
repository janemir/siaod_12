using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace _12_13_LR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            numericUpDown1.Value = 1000;
            dataGridView1.RowCount = 7; // строки 
            dataGridView1.ColumnCount = 6;
            string[] sorts = { "Обмен", "Выбор", "Включение", "Шелла", "Быстрая", "Линейная", "Встроенная" };
            for (int i = 0; i < dataGridView1.RowCount; i++)
                dataGridView1.Rows[i].Cells[1].Value = sorts[i];
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.CornflowerBlue;
            dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.CornflowerBlue;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int arraySize = (int)numericUpDown1.Value;
            int[] array = GenerateRandomArray(arraySize);

            // Сортировка методом прямого обмена
            var bubbleSortResult = BubbleSort(array.Clone() as int[]);
            dataGridView1.Rows[0].Cells[2].Value = bubbleSortResult.Comparisons;
            dataGridView1.Rows[0].Cells[3].Value = bubbleSortResult.Swaps;
            dataGridView1.Rows[0].Cells[4].Value = bubbleSortResult.TimeElapsed.TotalMilliseconds;
            dataGridView1.Rows[0].Cells[5].Value = IsSorted(bubbleSortResult.SortedArray);

            // Сортировка методом прямого выбора
            var selectionSortResult = SelectionSort(array.Clone() as int[]);
            dataGridView1.Rows[1].Cells[2].Value = selectionSortResult.Comparisons;
            dataGridView1.Rows[1].Cells[3].Value = selectionSortResult.Swaps;
            dataGridView1.Rows[1].Cells[4].Value = selectionSortResult.TimeElapsed.TotalMilliseconds;
            dataGridView1.Rows[1].Cells[5].Value = IsSorted(selectionSortResult.SortedArray);
        }

        private int[] GenerateRandomArray(int size)
        {
            Random random = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(0, 1000);
            }
            return array;
        }

        private (int[] SortedArray, int Comparisons, int Swaps, TimeSpan TimeElapsed) BubbleSort(int[] array)
        {
            int comparisons = 0;
            int swaps = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int n = array.Length;
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 1; i < n; i++)
                {
                    comparisons++;
                    if (array[i - 1] > array[i])
                    {
                        int temp = array[i - 1];
                        array[i - 1] = array[i];
                        array[i] = temp;
                        swapped = true;
                        swaps++;
                    }
                }
                n--;
            } while (swapped);

            stopwatch.Stop();
            return (array, comparisons, swaps, stopwatch.Elapsed);
        }

        private (int[] SortedArray, int Comparisons, int Swaps, TimeSpan TimeElapsed) SelectionSort(int[] array)
        {
            int comparisons = 0;
            int swaps = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    comparisons++;
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }
                if (minIndex != i)
                {
                    int temp = array[i];
                    array[i] = array[minIndex];
                    array[minIndex] = temp;
                    swaps++;
                }
            }

            stopwatch.Stop();
            return (array, comparisons, swaps, stopwatch.Elapsed);
        }

        private bool IsSorted(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] > array[i])
                {
                    return false;
                }
            }
            return true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
