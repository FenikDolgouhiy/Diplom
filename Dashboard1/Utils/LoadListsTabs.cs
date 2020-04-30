using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var responce = await client.GetAsync("TeachersLoad/");
            var list = responce.ResultAs<List<LoadDTO>>();
            if (list != null)
                foreach (var item in list)
                    teachersWeeks.Add(new TeachersWeekLoad {Teacher = item.Teacher });


            var res = teachersWeeks.GroupBy(a => a.Teacher).Select(g => g.First()).ToList();
            return res;

        }
    }
}
