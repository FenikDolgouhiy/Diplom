using Dashboard1.Model;
using Dashboard1.View;
using Dashboard1.ViewModel;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Dashboard1.Logic;

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
            MessageBox.Show("Подключение к БД успешно");
        }

        public async Task<List<LoadDTO>> ExportFromFBToDG()
        {
            List<LoadDTO> result = new List<LoadDTO>();


            var response = await client.GetAsync("TeachersLoad/");
            var list = response.ResultAs<List<LoadDTO>>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    result.Add(new LoadDTO
                    {
                        Id = item.Id,
                        Teacher = item.Teacher,
                        Subject = item.Subject,
                        Group = item.Group,
                        TotalHours = item.TotalHours,
                        Weeks = item.Weeks,
                        HoursPerWeek = item.HoursPerWeek,
                        DaysOfPractice = item.DaysOfPractice
                    });
                }
            }
            List<TeachersOpp> TeachResult = new List<TeachersOpp>();
            response = await client.GetAsync("TeachersWeekLoad/");
            var Teachlist = response.ResultAs<List<TeachersOpp>>();
            if (Teachlist != null)
            {
                foreach (var item in Teachlist)
                {
                    TeachResult.Add(new TeachersOpp
                    {

                        Teacher = item.Teacher,
                        Monday = item.Monday,
                        Tuesday = item.Tuesday,
                        Wednesday = item.Wednesday,
                        Thursday = item.Thursday,
                        Friday = item.Friday

                    }); ;
                }
            }
            AlgoLogic.BData = result;
            AlgoLogic.TeachersUpload = TeachResult;
            return result;
        }
        public async Task ExportDGToFB(List<LoadDTO> loadList)
        {
            if (loadList != null)
            {
                await client.SetAsync("TeachersLoad/", loadList);
            }
            MessageBox.Show("Данные в Базу данных были загружены");
        }
        public async void DeleteSelectedItem(LoadDTO loadDTO)
        {
            loadDTO.Id = (Convert.ToInt32(loadDTO.Id) - 1).ToString();
            await client.DeleteAsync("TeachersLoad/" + loadDTO.Id);
        }
        public async void DeleteAllInfoFromFB()
        {
            await client.DeleteAsync("TeachersLoad");
            MessageBox.Show("База данных была полностью очищенна");
        }
        public async Task LoadTeachersListToDb(List<TeachersWeekLoad> teachersWeekLoad)
        {
            if (teachersWeekLoad != null)
            {
                await client.SetAsync("TeachersWeekLoad/", teachersWeekLoad);

                MessageBox.Show("Данные в Базу данных были загружены");
            }
        }
        //Я ТУТ
        public async Task<List<LoadDTOBD>> ExportFromFBToDGBD()
        {
            List<LoadDTOBD> result = new List<LoadDTOBD>();
            var response = await client.GetAsync("TeachersLoad/");
            System.Threading.Thread.Sleep(5000);
            var list = response.ResultAs<List<LoadDTOBD>>();

            if (list != null)
            {
                foreach (var item in list)
                {
                    result.Add(new LoadDTOBD
                    {
                        Id = item.Id,
                        Teacher = item.Teacher,
                        Subject = item.Subject,
                        Group = item.Group,
                        TotalHours = item.TotalHours,
                        Weeks = item.Weeks,
                        HoursPerWeek = item.HoursPerWeek,
                        DaysOfPractice = item.DaysOfPractice
                    });
                }
            }
            return result;
        }

        public async Task LoadTeachersListToDb(List<TeachersOpp> teachersWeekLoad)
        {
            if (teachersWeekLoad != null)
            {
                await client.SetAsync("TeachersWeekLoad/", teachersWeekLoad);

                Console.WriteLine("Данные в Базу данных были загружены");
            }
        }

        public async Task<List<TeachersOpp>> ExportTeachersOpp()
        {
            List<TeachersOpp> result = new List<TeachersOpp>();
            var response = await client.GetAsync("TeachersWeekLoad/");
            var list = response.ResultAs<List<TeachersOpp>>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    result.Add(new TeachersOpp
                    {

                        Teacher = item.Teacher,
                        Monday = item.Monday,
                        Tuesday = item.Tuesday,
                        Wednesday = item.Wednesday,
                        Thursday = item.Thursday,
                        Friday = item.Friday

                    }); ;
                }
            }
            return result;
        }
        public async Task LoadTimetableToFB(List<TimetablesList> GroupsTimetable)
        {
            if (GroupsTimetable != null)
            {
                await client.SetAsync("GroupsTimetable/", GroupsTimetable);


            }
        }
    }
 }
