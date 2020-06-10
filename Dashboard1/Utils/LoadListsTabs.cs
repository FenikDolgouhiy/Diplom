using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Dashboard1.Model;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace Dashboard1.Utils
{
    public class LoadListsTabs
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "KRgcq6RJREaLwFzPdmETvLxjm1OSxZBOPY3ONSXb ",
            BasePath = "https://fir-dipp.firebaseio.com/"
        };
        IFirebaseClient client;

        public LoadListsTabs()
        { 
            client = new FireSharp.FirebaseClient(config);
            
        }
        public async void DeleteAllCabinets()
        {

            await client.DeleteAsync("Cabinets");
            MessageBox.Show("База данных была полностью очищенна");
        }
        public async Task<List<string>> ReturnGroups()
        {
            List<string> result = new List<string>();
            List<string> res = new List<string>();
            var response = await client.GetAsync("TeachersLoad/");
            var list = response.ResultAs<List<LoadDTO>>();
            if (list != null)
                foreach (var item in list)
                    result.Add(item.Group);

            foreach (var x in result)
                if (!res.Contains(x))
                    res.Add(x);
            return res;
        }
        public async Task<List<string>> ReturnSubjects()
        {
            List<string> result = new List<string>();
            List<string> res = new List<string>();
            var response = await client.GetAsync("TeachersLoad/");
            var list = response.ResultAs<List<LoadDTO>>();
            if (list != null)
                foreach (var item in list)
                    result.Add(item.Subject);

            foreach (var x in result)
                if (!res.Contains(x))
                    res.Add(x);
            return res;
        }
        public async Task<List<TeachersWeekLoad>> ReturnTeachers()
        {
            List<TeachersWeekLoad> teachersWeeks = new List<TeachersWeekLoad>();
            List<LoadDTO> loadDTO = new List<LoadDTO>();
            var resp = await client.GetAsync("TeachersLoad/");
            var responce = await client.GetAsync("TeachersWeekLoad/");
            var listTeach = resp.ResultAs<List<LoadDTO>>();
            var list = responce.ResultAs<List<TeachersWeekLoad>>();
            if (listTeach == null)
            {
                teachersWeeks.Add(new TeachersWeekLoad
                {
                    Teacher = null,
                    Monday = null,
                    Tuesday = null,
                    Wednesday = null,
                    Thursday = null,
                    Friday = null
                });
                return teachersWeeks;
            }
            listTeach = listTeach.GroupBy(a => a.Teacher).Select(g => g.First()).ToList();
            int i = 0;
            if (list != null)
                foreach (var item in list)
                {
                    teachersWeeks.Add(new TeachersWeekLoad
                    {
                        Teacher = listTeach[i].Teacher,
                        Monday = item.Monday,
                        Tuesday = item.Tuesday,
                        Wednesday = item.Wednesday,
                        Thursday = item.Thursday,
                        Friday = item.Friday
                    });
                    i++;
                }
            else
                foreach (var item in listTeach)
                {
                    teachersWeeks.Add(new TeachersWeekLoad
                    {
                        Teacher = listTeach[i].Teacher,
                        Monday = null,
                        Tuesday = null,
                        Wednesday = null,
                        Thursday = null,
                        Friday = null
                    });
                    i++;
                }
            return teachersWeeks;
        }
        public async Task<List<Cabinets>> GetTeacherAndCabs()
        {
            List<Cabinets> teachersWeeks = new List<Cabinets>();
            List<LoadDTO> loadDTO = new List<LoadDTO>();
            var resp = await client.GetAsync("TeachersLoad/");
            var responce = await client.GetAsync("Cabinets/");
            var listTeach = resp.ResultAs<List<LoadDTO>>();
            var list = responce.ResultAs<List<Cabinets>>();
            if (listTeach == null)
            {
                teachersWeeks.Add(new Cabinets
                {
                    TeacherName = null,
                    Cabinet = null
                });
                return teachersWeeks;
            }
            listTeach = listTeach.GroupBy(a => a.Teacher).Select(g => g.First()).ToList();
            int i = 0;
            if (list!=null)
                foreach (var item in list)
                {
                    teachersWeeks.Add(new Cabinets
                    {
                        TeacherName = listTeach[i].Teacher,
                        Cabinet = item.Cabinet
                    });
                    i++;
                }
            else
                foreach (var item in listTeach)
                {
                    teachersWeeks.Add(new Cabinets
                    {
                        TeacherName = listTeach[i].Teacher,
                        Cabinet = null
                    });
                    i++;
                }
            return teachersWeeks;
        }
        public async Task LoadCabinetsToDB(List<Cabinets> cabinets)
        {
            if (cabinets != null)
            {
                await client.SetAsync("Cabinets/", cabinets);

                MessageBox.Show("Данные в Базу данных были загружены");
            }
        }
    }
}
