using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard1.Utils;
using Dashboard1.Model;
namespace Dashboard1.Logic
{
    public class DBLoad
    {
        List<LoadDTO> _loadList = new List<LoadDTO>();
        public List<LoadDTO> LoadList
        {
            get { return _loadList; }
            set
            {
                _loadList = value;

            }
        }

        public FBOperations fbOperations = new FBOperations();
        public  void UploadFromFBBD()
        {

        var loads = AlgoLogic.BData;
            if (loads != null)
            {
                UploadList = loads;
            }
            Console.WriteLine("Загрузка завершена.");
        }
        public List<LoadDTO> UploadList
        {
            get { return _loadList; }
            set
            {

                _loadList = value;

            }
        }

        List<TeachersOpp> _teachersOppList = new List<TeachersOpp>();
        public List<TeachersOpp> TeachersOppList
        {
            get { return _teachersOppList; }
            set
            {

                _teachersOppList = value;

            }
        }
        public  void UploadTeachersOpp()
        {
            var loads = AlgoLogic.TeachersUpload;
            if (loads != null)
            {
                TeachersOppList = loads;
            }
            Console.WriteLine("Загрузка завершена");
        }
        List<TimetablesList> _groupsTimetableList = new List<TimetablesList>();
        public async void ImportTimetableToFB()
        {
            await fbOperations.LoadTimetableToFB(_groupsTimetableList);
        }
        public List<TimetablesList> GroupsTimetableList
        {
            get { return _groupsTimetableList; }
            set
            {

                _groupsTimetableList = value;

            }
        }
    }

}
