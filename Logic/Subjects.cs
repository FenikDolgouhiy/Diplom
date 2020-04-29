namespace Logic
{
    public class Subjects // класс предметов
    {
        public string Subject; //предмет
        public string Group; //группа
        public int hoursSum; //суммарно часов
        public int Weeks; //недель 
        public int hoursPerWeek; //часов в неделю

        public Subjects(string pred, int sum, int weeks, string group, Group[] P) 
        {
            Subject = pred;
            hoursSum = sum;
            Weeks = weeks;
            Group = group;
            for (int i = 0; i < P.Length; i++)
            {
                if (P[i].Name == group)
                {
                    hoursPerWeek = hoursSum / Weeks;
                }
            }
        }

        public static int SubjectsCount(string teacher, string[] TeacherList)//Подсчёт количества предметов у преподавателя
        {
            int counter = 0;
            for (int i = 0; i < TeacherList.Length; i++)
            {
                if (teacher == TeacherList[i])
                    counter++;
            }
            return counter;
        }

    }
}
