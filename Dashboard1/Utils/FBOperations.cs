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

        public async Task<List<LoadDTO>> ExportFromFBToDG()
        {
            List<LoadDTO> result = new List<LoadDTO>();
            for (int i = 0; ; i++)
            {
                FirebaseResponse responce = await client.GetAsync("TeachersTest/" + i);
                
                LoadDTO obj = responce.ResultAs<LoadDTO>();
                if (obj == null)
                {
                    break;
                }
                result.Add(new LoadDTO
                {
                    Id = obj.Id,
                    Teacher = obj.Teacher,
                    Subject = obj.Subject,
                    Group = obj.Group,
                    TotalHours = obj.TotalHours,
                    Weeks = obj.Weeks,
                    HoursPerWeek = obj.HoursPerWeek,
                    DaysOfPractice = obj.DaysOfPractice
                });
            }
            return result;
        }
        public async Task ExportDGToFB(List<LoadDTO> loadList)
        {
            DeleteAllInfoFromFB();
                for (int i = 0; i < loadList.Count; i++)
                {
                    var loadDTO = new LoadDTO
                    {
                        Id = loadList[i].Id,
                        Teacher = loadList[i].Teacher,
                        Subject = loadList[i].Subject,
                        Group = loadList[i].Group,
                        TotalHours = loadList[i].TotalHours,
                        Weeks = loadList[i].Weeks,
                        HoursPerWeek = loadList[i].HoursPerWeek,
                        DaysOfPractice = loadList[i].DaysOfPractice
                    };
                    SetResponse responce = await client.SetAsync("TeachersTest/" + i, loadDTO);
                    LoadDTO result = responce.ResultAs<LoadDTO>();
                    //MessageBox.Show("Data Inserted " + result.IDnagr);
                }
            
        }
        public async void DeleteSelectedItem(LoadDTO loadDTO)
        {
            loadDTO.Id = (Convert.ToInt32(loadDTO.Id) - 1).ToString();
            FirebaseResponse response = await client.DeleteAsync("TeachersTest/" + loadDTO.Id);
        }
        public async void DeleteAllInfoFromFB()
        {
            FirebaseResponse response = await client.DeleteAsync("TeachersTest");
        }
        
    }
 }
