using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Практическая_21__2_
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int[,] mas;
        int Ctroka;
        int Kolonna;
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Proga_Inf(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Дана целочисленная матрица размера M * N. Найти номер первого из ее столбцов, содержащих максимальное количество одинаковых элементов.", "О программе");
        }
        private void Sosdat(object sender, RoutedEventArgs e)
        {
            bool a1, a2;
            a1 = Int32.TryParse(Stolb.Text, out Kolonna);
            a2 = Int32.TryParse(Stroka.Text, out Ctroka );
            if (a1 == true && a2 == true)
            {
                mas = new int[Ctroka, Kolonna];
                Datagrid.ItemsSource = Class.ToDataTable(mas).DefaultView;
            }
            else MessageBox.Show("Ввeдите цифры", "Ошибка");
        }
        private void Reashenie(object sender, RoutedEventArgs e)
        {
            int kol = 0;
            int myxa = 0;
            int value = 0;
            for (int j = mas.GetLength(1) - 1; j > -1; j--)
            {
                kol = 0;
                for (int i = 0; i < mas.GetLength(0) - 1; i++)
                {
                    if (mas[i, j] == mas [i + 1, j])
                    {
                        kol++;
                    }
                    if (kol >= myxa)
                    {
                        myxa = kol;
                        value = j + 1;
                    }
                }
                rez.Text = Convert.ToString(value);
            }
        }

        private void Otshistit(object sender, RoutedEventArgs e)
        {
            Datagrid.ItemsSource = null;
            mas = null;
            Stolb.Clear();
            Stroka.Clear();
            rez.Clear();
        }

        private void CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            int indexColumn = e.Column.DisplayIndex;
            int indexRow = e.Row.GetIndex();
            mas[indexRow, indexColumn] = Convert.ToInt32(((TextBox)e.EditingElement).Text);
        }

        private void Zapolnit(object sender, RoutedEventArgs e)
        {
            bool f1, f2;
            f1 = Int32.TryParse(Stolb.Text, out Kolonna);
            f2 = Int32.TryParse(Stroka.Text, out Ctroka);
            if (f1 == true && f2 == true)
            {
                mas = new int[Ctroka, Kolonna];
                Random rnd = new Random();
                for (int i = 0; i < Ctroka; i++)
                {
                    for (int j = 0; j < Kolonna; j++)
                    {
                        mas[i, j] = rnd.Next(5);
                    }
                }
                Datagrid.ItemsSource = Class.ToDataTable(mas).DefaultView;
            }
            else MessageBox.Show("Ввeдите цифры", "Ошибка");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Все файлы (*.*) | *.* |Текстовые файлы| *.txt";
            save.FilterIndex = 2;
            save.Title = "Сохранение таблицы";

            if (save.ShowDialog() == true)
            {
                
                StreamWriter writer = new StreamWriter(save.FileName);
                writer.WriteLine(mas.GetLength(0));
                writer.WriteLine(mas.GetLength(1));
                for (int i = 0; i < Ctroka; i++)
                {
                    for (int j = 0; j < Kolonna; j++)
                    {
                        writer.WriteLine(mas[i, j]);
                    }
                }
                writer.Close();
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = ".txt";
            open.Filter = "Все файлы (*.*) | *.* |Текстовые файлы| *.txt";
            open.FilterIndex = 2;
            open.Title = "Сохранение таблицы";

            if (open.ShowDialog() == true)
            {
                StreamReader reader = new StreamReader(open.FileName);
                Ctroka = Convert.ToInt32(reader.ReadLine());
                Kolonna = Convert.ToInt32(reader.ReadLine());
                mas = new int[Ctroka, Kolonna];
                for (int i = 0; i < Ctroka; i++)
                {
                    for (int j = 0; j < Kolonna; j++)
                    {
                        mas[i, j] = Convert.ToInt32(reader.ReadLine());
                    }
                }
                reader.Close();
                Datagrid.ItemsSource = Class.ToDataTable(mas).DefaultView;
            }
        }
    }
}
