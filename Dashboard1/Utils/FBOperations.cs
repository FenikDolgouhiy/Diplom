using Dashboard1.Model;

using FireSharp.Config;
using FireSharp.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

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
            return result;
        }
        public async Task ExportDGToFB(List<LoadDTO> loadList)
        {
            if (loadList != null)
            {
                await client.SetAsync("TeachersLoad/", loadList);
            }
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

    }
}
