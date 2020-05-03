using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
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
        public async void UploadFromFB()
        {
            var loads = await fbOperations.ExportFromFBToDG();
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
        public async void UploadTeachersOpp()
        {
            var loads = await fbOperations.ExportTeachersOpp();
            if (loads != null)
            {
                TeachersOppList = loads;
            }
            Console.WriteLine("Загрузка завершена");
        }
    }
}
