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
            
        }

        public async Task<List<LoadDTO>> ExportFromFBToDG()
        {
            
            List<LoadDTO> result = new List<LoadDTO>();

            try
            {

                 await client.GetAsync("TeachersLoad/");
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так со связью с БД, проверьте подключение к инетрнету");

                Process.GetCurrentProcess().Kill();
            }
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
    }
 }
