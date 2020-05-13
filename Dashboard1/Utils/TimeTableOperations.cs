using Dashboard1.Model;
using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.Office.Interop.Word;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Office.Interop.Excel;

using Application = Microsoft.Office.Interop.Excel.Application;
using System.Windows;
using System;
using System.Diagnostics;

namespace Dashboard1.Utils
{
    public class TimeTableOperations
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "KRgcq6RJREaLwFzPdmETvLxjm1OSxZBOPY3ONSXb ",
            BasePath = "https://fir-dipp.firebaseio.com/"
        };
        IFirebaseClient client;
        public TimeTableOperations()
        {
            client = new FireSharp.FirebaseClient(config);
        }
        public async Task<List<string>> LoadGroupsToComboBox()
        {
            try
            {
                await client.GetAsync("TeachersLoad/");
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так со связью с БД, проверьте подключение к интернету");

                Process.GetCurrentProcess().Kill();
            }
            List<LoadDTO> loadDTO = new List<LoadDTO>();
            List<string> result = new List<string>();
            var responce = await client.GetAsync("TeachersLoad/");
            var listTeach = responce.ResultAs<List<LoadDTO>>();
            listTeach = listTeach.GroupBy(a => a.Group).Select(g => g.First()).ToList();
            if (listTeach != null)
            {
                foreach (var item in listTeach)
                {
                    result.Add(item.Group);
                }
            }
            return result;
        }
        public async Task<List<FullTimeTable>> LoadFullTimeTable(string selItem)
        {
            List<FullTimeTable> res = new List<FullTimeTable>();
            List<DataTableFromDB> fromDBs = new List<DataTableFromDB>();
            var responce = await client.GetAsync("GroupsTimetable/");
            var ListTeach = responce.ResultAs<List<DataTableFromDB>>();
            foreach (var item in ListTeach)
            {
                if (selItem == item.Group)
                {
                    fromDBs.Add(new DataTableFromDB
                    {
                        Monday0 = item.Monday0,
                        Monday1 = item.Monday1,
                        Monday2 = item.Monday2,
                        Monday3 = item.Monday3,
                        Monday4 = item.Monday4,
                        Monday5 = item.Monday5,
                        Tuesday0 = item.Tuesday0,
                        Tuesday1 = item.Tuesday1,
                        Tuesday3 = item.Tuesday3,
                        Tuesday4 = item.Tuesday4,
                        Tuesday2 = item.Tuesday2,
                        Tuesday5 = item.Tuesday5,
                        Wednesday0 = item.Wednesday0,
                        Wednesday1 = item.Wednesday1,
                        Wednesday2 = item.Wednesday2,
                        Wednesday3 = item.Wednesday3,
                        Wednesday4 = item.Wednesday4,
                        Wednesday5 = item.Wednesday5,
                        Thursday0 = item.Thursday0,
                        Thursday1 = item.Thursday1,
                        Thursday2 = item.Thursday2,
                        Thursday3 = item.Thursday3,
                        Thursday4 = item.Thursday4,
                        Thursday5 = item.Thursday5,
                        Friday0 = item.Friday0,
                        Friday1 = item.Friday1, 
                        Friday2 = item.Friday2,
                        Friday3 = item.Friday3,
                        Friday4 = item.Friday4,
                        Friday5 = item.Friday5,
                        Group = item.Group
                    });

                }
            }

            foreach (var item in fromDBs)
            {
                res.Add(new FullTimeTable
                {
                    Lesson = "0",
                    Monday = item.Monday0,
                    Tuesday = item.Tuesday0,
                    Wednesday = item.Wednesday0,
                    Thursday = item.Thursday0,
                    Friday = item.Friday0,
                });
                res.Add(new FullTimeTable
                {

                    Lesson = "1",
                    Monday = item.Monday1,
                    Tuesday = item.Tuesday1,
                    Wednesday = item.Wednesday1,
                    Thursday = item.Thursday1,
                    Friday = item.Friday1,
                });
                res.Add(new FullTimeTable
                {
                    Lesson = "2",
                    Monday = item.Monday2,
                    Tuesday = item.Tuesday2,
                    Wednesday = item.Wednesday2,
                    Thursday = item.Thursday2,
                    Friday = item.Friday2,
                });
                res.Add(new FullTimeTable
                {
                    Lesson = "3",
                    Monday = item.Monday3,
                    Tuesday = item.Tuesday3,
                    Wednesday = item.Wednesday3,
                    Thursday = item.Thursday3,
                    Friday = item.Friday3,
                });
                res.Add(new FullTimeTable
                {
                    Lesson = "4",
                    Monday = item.Monday4,
                    Tuesday = item.Tuesday4,
                    Wednesday = item.Wednesday4,
                    Thursday = item.Thursday4,
                    Friday = item.Friday4
                });
                res.Add(new FullTimeTable
                {
                    Lesson = "5",
                    Monday = item.Monday5,
                    Tuesday = item.Tuesday5,
                    Wednesday = item.Wednesday5,
                    Thursday = item.Thursday5,
                    Friday = item.Friday5
                });
            }
            return res;
        }

        public async void ListToExcel(List<FullTimeTable> list, string SelectedGroup)
        {
            List<DataTableFromDB> dataTableFromDBs = new List<DataTableFromDB>();

            var responce = await client.GetAsync("GroupsTimetable/");
            var LoadTimeTableCourse = responce.ResultAs<List<DataTableFromDB>>();

            LoadTimeTableCourse = LoadTimeTableCourse.GroupBy(a => a.Group).Select(g => g.First()).ToList();
            
            Microsoft.Office.Interop.Excel.Application exApp = new Microsoft.Office.Interop.Excel.Application();
            exApp.Workbooks.Add();

            for (int i = 0; i < LoadTimeTableCourse.Count; i++)
            {
                if (LoadTimeTableCourse[i].Group == SelectedGroup)
                {
                    dataTableFromDBs = LoadTimeTableCourse;
                }
            }

            Worksheet workSheet = (Worksheet)exApp.ActiveSheet;
            workSheet.Cells[1, "A"] = "№ пары";
            for (int i = 0; i < 6; i++)
            {
                workSheet.Cells[i + 2, "A"] = i;
            }

            workSheet.Cells[1, "B"] = "Monday";
            workSheet.Cells[1, "C"] = "Tuedsday";
            workSheet.Cells[1, "D"] = "Wendesday";
            workSheet.Cells[1, "E"] = "Thursday";
            workSheet.Cells[1, "F"] = "Friday";

            for (int i = 0; i < list.Count; i++)
            {
                workSheet.Cells[i+2, "B"] = list[i].Monday;
                workSheet.Cells[i + 2, "C"] = list[i].Tuesday;
                workSheet.Cells[i + 2, "D"] = list[i].Wednesday;
                workSheet.Cells[i + 2, "E"] = list[i].Thursday;
                workSheet.Cells[i + 2, "F"] = list[i].Friday;
            }
            workSheet.SaveAs(@"C:\Users\"+SelectedGroup+"TimeTable.xlsx");
            MessageBox.Show("Данные были загружены");
            exApp.Quit();
        }
    }
}