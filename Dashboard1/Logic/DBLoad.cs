using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard1.Utils;
namespace Dashboard1.Logic
{
    public class DBLoad
    {
        List<LoadDTOBD> _loadList = new List<LoadDTOBD>();
        public List<LoadDTOBD> LoadList
        {
            get { return _loadList; }
            set
            {
                _loadList = value;

            }
        }

        public FBOperations fbOperations = new FBOperations();
        public async void UploadFromFBBD()
        {
            var loads = await fbOperations.ExportFromFBToDGBD();
            if (loads != null)
            {
                UploadList = loads;
            }
            Console.WriteLine("Загрузка завершена.");
        }
        public List<LoadDTOBD> UploadList
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
        public async void UploadTeachersOpp()
        {
            var loads = await fbOperations.ExportTeachersOpp();
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
