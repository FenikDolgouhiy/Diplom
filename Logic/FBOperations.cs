using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;


namespace Logic
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
                Console.WriteLine("Подключения к Базе данных успешно");
            }
        }

        public async Task<List<LoadDTO>> ExportFromFBToDG()
        {
            List<LoadDTO> result = new List<LoadDTO>();
            for (int i = 0; ; i++)
            {
                Console.WriteLine(i);
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
            Console.WriteLine("Загрузка Завершена.Нажмите Любую клавишу");
            return result;
        }

        public async Task<List<TeachersOpp>> ExportTeachersOpp()
        {
            List<TeachersOpp> result = new List<TeachersOpp>();
            for (int i = 0; ; i++)
            {
                Console.WriteLine(i);
                FirebaseResponse responce = await client.GetAsync("TeachersWeekLoad/" + i);

                TeachersOpp obj = responce.ResultAs<TeachersOpp>();
                if (obj == null)
                {

                    break;
                }
                result.Add(new TeachersOpp
                {

                    Teacher = obj.Teacher,
                    Monday = obj.Monday,
                    Tuesday = obj.Tuesday,
                    Wednesday = obj.Wednesday,
                    Thursday = obj.Thursday,
                    Friday = obj.Friday

                }); ;
            }
            Console.WriteLine("Загрузка Завершена.Нажмите Любую клавишу");
            return result;
        }
    }

}
