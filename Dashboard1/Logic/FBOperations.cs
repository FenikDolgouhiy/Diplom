using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace Dashboard1.Logic
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
