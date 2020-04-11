using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;

namespace Dashboard1
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "KRgcq6RJREaLwFzPdmETvLxjm1OSxZBOPY3ONSXb ",
            BasePath = "https://fir-dipp.firebaseio.com/"
        };
        IFirebaseClient client;
        public MainWindow()
        {
            InitializeComponent();
            client = new FireSharp.FirebaseClient(config);

            if (client != null)
            {
                MessageBox.Show("Подключение к Базе Данных было успешным");
            }
        }
        private void WindowClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void WindowMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }
        private void WindowMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void GridBarraTitulo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    int index = int.Parse(((Button)e.Source).Uid);

        //    GridCursor.Margin = new Thickness(10 + (150 * index), 0, 0, 0);

        //    switch (index)
        //    {
        //        case 0:
        //            GridMainSecond.Background = Brushes.Aquamarine;
        //            break;
        //        case 1:
        //            GridMainSecond.Background = Brushes.Beige;
        //            break;
        //        case 2:
        //            GridMainSecond.Background = Brushes.CadetBlue;
        //            break;
        //        case 3:
        //            GridMainSecond.Background = Brushes.DarkBlue;
        //            break;
        //        case 4:
        //            GridMainSecond.Background = Brushes.Firebrick;
        //            break;
        //        case 5:
        //            GridMainSecond.Background = Brushes.Gainsboro;
        //            break;
        //        case 6:
        //            GridMainSecond.Background = Brushes.HotPink;
        //            break;
        //    }
        //}

        private void Button_Click_MainMenu(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            switch (index)
            {
                case 0:
                    GridMain.Background = Brushes.Aquamarine;
                    break;
                case 1:
                    GridMain.Background = Brushes.Beige;
                    break;
                case 2:
                    GridMain.Background = Brushes.CadetBlue;
                    break;
                case 3:
                    GridMain.Background = Brushes.DarkBlue;
                    break;
                case 4:
                    GridMain.Background = Brushes.Firebrick;
                    break;
                case 5:
                    GridMain.Background = Brushes.Gainsboro;
                    break;
                case 6:
                    GridMain.Background = Brushes.HotPink;
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xlsx";
            openfile.Filter = "(.xlsx)|*.xlsx";
            //openfile.ShowDialog();

            var browsefile = openfile.ShowDialog();

            if (browsefile == true)
            {
                txtFilePath.Text = openfile.FileName;

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                //Static File From Base Path...........
                //Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(AppDomain.CurrentDomain.BaseDirectory + "TestExcel.xlsx", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                //Dynamic File Using Uploader...........
                Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(txtFilePath.Text.ToString(), 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Microsoft.Office.Interop.Excel.Worksheet excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(1); ;
                Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

                string strCellData = "";
                double douCellData;
                int rowCnt = 0;
                int colCnt = 0;

                System.Data.DataTable dt = new System.Data.DataTable();
                for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                {
                    string strColumn = "";
                    strColumn = (string)(excelRange.Cells[1, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                    dt.Columns.Add(strColumn, typeof(string));
                }

                for (rowCnt = 2; rowCnt <= excelRange.Rows.Count; rowCnt++)
                {
                    string strData = "";
                    for (colCnt = 1; colCnt <= excelRange.Columns.Count; colCnt++)
                    {
                        try
                        {
                            strCellData = (string)(excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                            strData += strCellData + "|";
                        }
                        catch (Exception ex)
                        {
                            douCellData = (excelRange.Cells[rowCnt, colCnt] as Microsoft.Office.Interop.Excel.Range).Value2;
                            strData += douCellData.ToString() + "|";
                        }
                    }
                    strData = strData.Remove(strData.Length - 1, 1);
                    dt.Rows.Add(strData.Split('|'));
                }

                dataGridTeachers.ItemsSource = dt.DefaultView;

                excelBook.Close(true, null, null);
                excelApp.Quit();

            }
        }
        private async void ExcelToFirebase()
        {
            client = new FireSharp.FirebaseClient(config);

            for (int j = 0; j < dataGridTeachers.Items.Count-1; j++)
            {
                int i = 0;
                var id = new DataGridCellInfo(dataGridTeachers.Items[j], dataGridTeachers.Columns[i]);
                var idcontent = id.Column.GetCellContent(id.Item) as TextBlock;

                var teach = new DataGridCellInfo(dataGridTeachers.Items[j], dataGridTeachers.Columns[i + 1]);
                var teachcontent = teach.Column.GetCellContent(teach.Item) as TextBlock;

                var sub = new DataGridCellInfo(dataGridTeachers.Items[j], dataGridTeachers.Columns[i + 2]);
                var subcontent = sub.Column.GetCellContent(sub.Item) as TextBlock;

                var gr = new DataGridCellInfo(dataGridTeachers.Items[j], dataGridTeachers.Columns[i + 3]);
                var grcontent = gr.Column.GetCellContent(gr.Item) as TextBlock;

                var ch = new DataGridCellInfo(dataGridTeachers.Items[j], dataGridTeachers.Columns[i + 4]);
                var chcontent = ch.Column.GetCellContent(ch.Item) as TextBlock;

                var week = new DataGridCellInfo(dataGridTeachers.Items[j], dataGridTeachers.Columns[i + 5]);
                var weekcontent = week.Column.GetCellContent(week.Item) as TextBlock;

                var hpw = new DataGridCellInfo(dataGridTeachers.Items[j], dataGridTeachers.Columns[i + 6]);
                var hpwcontent = hpw.Column.GetCellContent(hpw.Item) as TextBlock;

                var cpw = new DataGridCellInfo(dataGridTeachers.Items[j], dataGridTeachers.Columns[i + 7]);
                var cpwcontent = cpw.Column.GetCellContent(cpw.Item) as TextBlock;

                var data = new Data
                {
                    IDnagr = idcontent.Text,
                    Teacher = teachcontent.Text,
                    Subject = subcontent.Text,
                    Group = grcontent.Text,
                    HourCount = chcontent.Text,
                    WeeksCount = weekcontent.Text,
                    HoursPerWeek = hpwcontent.Text,
                    CountPractWeeks = cpwcontent.Text
                };
                SetResponse responce = await client.SetAsync("NagrNaPrepodov/" + j, data);
                Data result = responce.ResultAs<Data>();
                //MessageBox.Show("Data Inserted " + result.IDnagr);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ExcelToFirebase();
            MessageBox.Show("Данные были успешно загружены");
        }
    }
}
