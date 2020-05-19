using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard1.Logic
{
    public class Group //массив групп.
    {
        public string Name;//имя группы
        public int Weeks;//Количество недель
        public int Practice;//Недели практик. Временно бесполезны
        public Rozklad[,] TimeTable = new Rozklad[5, 6];//Таблица расписания
        public string[,] TotalTimetable = new string[5, 6];
        public Group()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                    TimeTable[i, j] = new Rozklad();
            }
        }

        public Group(string N, int W, int P) : this()
        {
            Name = N;
            Weeks = W;
            Practice = P;
        }

    }

}
