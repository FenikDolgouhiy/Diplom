using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard1.Logic
{
    public class AlgoLogic
    {
        public static void LogicRun()
        {

            DBLoad a = new DBLoad();
            a.UploadFromFB();
            
            string[] N_Prepods = new string[a.UploadList.Count];
            string[] N_Groups = new string[a.UploadList.Count];
            string[] N_Subjects = new string[a.UploadList.Count];
            int[] N_PerWeek = new int[a.UploadList.Count];
            int[] N_Weeks = new int[a.UploadList.Count];
            int[] N_Total = new int[a.UploadList.Count];
            for (int i = 0; i < a.UploadList.Count; i++)
            {
                N_Prepods[i] = a.UploadList[i].Teacher;
                N_Groups[i] = a.UploadList[i].Group;
                N_Subjects[i] = a.UploadList[i].Subject;
                N_PerWeek[i] = Convert.ToInt32(a.UploadList[i].HoursPerWeek);
                N_Weeks[i] = Convert.ToInt32(a.UploadList[i].Weeks);
                N_Total[i] = Convert.ToInt32(a.UploadList[i].TotalHours);
            }
            a.UploadTeachersOpp();
            
            string[] P_Prepods = new string[a.TeachersOppList.Count];
            string[] P_Opps = new string[a.TeachersOppList.Count];
            for (int i = 0; i < a.TeachersOppList.Count; i++)
            {
                P_Prepods[i] = a.TeachersOppList[i].Teacher;
                P_Opps[i] = a.TeachersOppList[i].Monday +
                            a.TeachersOppList[i].Tuesday +
                            a.TeachersOppList[i].Wednesday +
                            a.TeachersOppList[i].Thursday +
                            a.TeachersOppList[i].Friday;
            }

            int Number_of_prep = P_Prepods.Length;//Количество преподавателей для инициализации 

            TeacherList[] Prep_okkt; //список преподов
            Prep_okkt = new TeacherList[Number_of_prep];

            IEnumerable<String> IE_G_Group = N_Groups.Distinct();
            string[] G_Groups = IE_G_Group.ToArray();
            int[] G_Practice = new int[G_Groups.Count()];
            for (int i = 0; i < G_Practice.Length; i++)
                G_Practice[i] = 0;
            int[] G_Weeks = new int[G_Groups.Length];
            for (int i = 0, j = 0; i < G_Groups.Length && j < N_Groups.Length; j++)
            {
                if (G_Groups[i] == N_Groups[j])
                {
                    G_Weeks[i] = N_Weeks[j];
                    i++;
                    j = 0;
                }
            }
            //Дальше идёт инициализация основных объектов данными из массивов.
            Group[] okkt = new Group[G_Groups.Length];
            for (int i = 0; i < G_Groups.Length; i++)
            {
                okkt[i] = new Group(G_Groups[i], G_Weeks[i], G_Practice[i]);
            }
            for (int i = 0; i < Number_of_prep; i++)
            {

                Prep_okkt[i] = new TeacherList(Subjects.SubjectsCount(P_Prepods[i], N_Prepods), P_Prepods[i], P_Opps[i]);
            }
            for (int i = 0; i < Prep_okkt.Length; i++)
            {
                for (int j = 0, k = 0; j < N_Groups.Length; j++)
                {
                    if (Prep_okkt[i].teacherName == N_Prepods[j])
                    {
                        Prep_okkt[i].subjects[k] = new Subjects(N_Subjects[j], N_Total[j], N_Weeks[i], N_Groups[j], okkt);
                        k++;
                    }
                }
            }
            TeacherList temp; //список преподов для сортировки
            for (int i = 0; i < Prep_okkt.Length - 1; i++)//Сортировка по убыванию свободных мест
            {
                for (int j = i + 1; j < Prep_okkt.Length; j++)
                {
                    if (Prep_okkt[i].possibilitiesCount - AdeptMech.OccHours(Prep_okkt[i]) > Prep_okkt[j].possibilitiesCount - AdeptMech.OccHours(Prep_okkt[j]))
                    {
                        temp = Prep_okkt[i];
                        Prep_okkt[i] = Prep_okkt[j];
                        Prep_okkt[j] = temp;
                    }
                }
            }
            Group temp2;

            for (int i = 0; i < okkt.Length - 1; i++)//Сортировка групп по количеству часов. Почему-то не работает (наверное(я не знаю))
            {
                for (int j = i + 1; j < okkt.Length; j++)
                {
                    if (AdeptMech.GroupHours(Prep_okkt, okkt[i].Name) < AdeptMech.GroupHours(Prep_okkt, okkt[j].Name))
                    {
                        temp2 = okkt[i];
                        okkt[i] = okkt[j];
                        okkt[j] = temp2;
                    }
                }
            }

            for (int i = 0; i < okkt.Length; i++)//Создание расписания погруппно
            {
                okkt[i] = AdeptMech.CreatRoz(ref Prep_okkt, okkt[i]);
            }


            String[] Workout = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" }; //вывод расписания
            for (int d = 0; d < okkt.Length; d++)
            {
                
                for (int i = 0; i < 5; i++)
                {
                    
                    for (int k = 0; k < 6; k++)
                    {

                        if (okkt[d].TimeTable[i, k].EvenWeek.Subject == okkt[d].TimeTable[i, k].OddWeek.Subject)
                        {
                            okkt[d].TotalTimetable[i, k] = okkt[d].TimeTable[i, k].EvenWeek.Subject + " " + okkt[d].TimeTable[i, k].EvenWeek.Teacher;
                            
                        }
                        else
                        {
                            okkt[d].TotalTimetable[i, k] = okkt[d].TimeTable[i, k].EvenWeek.Subject + " " + okkt[d].TimeTable[i, k].EvenWeek.Teacher + "|" + okkt[d].TimeTable[i, k].OddWeek.Subject + " " + okkt[d].TimeTable[i, k].OddWeek.Teacher;
                            
                        }
                    }
                }
            }
            
            
            AdeptMech.UploadTimetable(okkt);
            

        }

    }
}
