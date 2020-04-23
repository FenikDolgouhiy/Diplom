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
                FirebaseResponse responce = await client.GetAsync("TeachersLoad/" + i);
                
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
            if (loadList != null)
            {
                for (int i = 0; i < loadList.Count; i++)
                {
                    var id = loadList[i].Id;
                    var teacher = loadList[i].Teacher;
                    var subject = loadList[i].Subject;
                    var group = loadList[i].Group;
                    var totalhours = loadList[i].TotalHours;
                    var weekcontent = loadList[i].Weeks;
                    var hourperweek = loadList[i].HoursPerWeek;
                    var daysofpractice = loadList[i].DaysOfPractice;

                    var loadDTO = new LoadDTO
                    {
                        Id = id,
                        Teacher = teacher,
                        Subject = subject,
                        Group = group,
                        TotalHours = totalhours,
                        Weeks = weekcontent,
                        HoursPerWeek = hourperweek,
                        DaysOfPractice = daysofpractice
                    };
                    SetResponse responce = await client.SetAsync("TeachersLoad/" + i, loadDTO);
                    LoadDTO result = responce.ResultAs<LoadDTO>();
                    //MessageBox.Show("Data Inserted " + result.IDnagr);
                }
            }
        }
    }
 }
