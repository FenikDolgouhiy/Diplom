using Dashboard1.Model;
using Dashboard1.View;
using Dashboard1.ViewModel;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard1.Utils
{
    public class FBOperations
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "KRgcq6RJREaLwFzPdmETvLxjm1OSxZBOPY3ONSXb ",
            BasePath = "https://fir-dipp.firebaseio.com/"
        };
        IFirebaseClient client;
        
        public FBOperations()
        {
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {
                MessageBox.Show("Подключения к Базе данных успешно");
            }
        }
        public async Task ExportDGToFB(List<LoadDTO> teachLoad)
        {
            DataGrid dataGridTeachers = new DataGrid();
            dataGridTeachers.ItemsSource = teachLoad;
            
            if (dataGridTeachers != null)
            {
                for (int j = 0; j < dataGridTeachers.Columns.Count; j++)
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

                    var loadDTO = new LoadDTO
                    {
                        Id = idcontent.Text,
                        Teacher = teachcontent.Text,
                        Subject = subcontent.Text,
                        Group = grcontent.Text,
                        TotalHours = chcontent.Text,
                        Weeks = weekcontent.Text,
                        HoursPerWeek = hpwcontent.Text,
                        DaysOfPractice = cpwcontent.Text
                    };
                    SetResponse responce = await client.SetAsync("NagrNaPrepodov/" + j, loadDTO);
                    LoadDTO result = responce.ResultAs<LoadDTO>();
                    //MessageBox.Show("Data Inserted " + result.IDnagr);
                }
            }
        }
    }
 }
