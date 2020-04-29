namespace Logic
{
    public class Rozklad //массив расписания 
    {
        public Lesson EvenWeek = new Lesson(); //пара на чётную неделю
        public Lesson OddWeek = new Lesson();  //пар на нечетную неделю

        public static bool IsTrue(int k, string subject, Rozklad[,] timeTable) //условие для проверки количества пар по предмету в день
        {
            int counter = 0;
            for (int j = 0; j < 6; j++)
            {
                if (timeTable[k, j].EvenWeek.Subject == subject || timeTable[k, j].OddWeek.Subject == subject)
                    counter++;
            }
             if (counter >= 2)
                    return false;
             else return true;
        }

        public static bool IsHalf(ref int k, ref int l, TeacherList Prep, Rozklad[,] timeTable, ref int pH) //условие для проверки количества пар по предмету в день
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (timeTable[i, j].EvenWeek.Subject != null && timeTable[i, j].OddWeek.Subject == null && Prep.TOpp[i, j, 0] == true)
                    {
                        k = i;
                        l = j;
                        pH = 1;
                        return true;
                    }
                    if (timeTable[i, j].EvenWeek.Subject == null && timeTable[i, j].OddWeek.Subject != null && Prep.TOpp[i, j, 1] == true)
                    {
                        k = i;
                        l = j;
                        pH = 2;
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
