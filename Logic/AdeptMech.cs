using System;
using System.Linq;

namespace Logic
{
    public class AdeptMech
    {
        static public Group CreatRoz(ref TeacherList[] Gr, Group OK)
        {
            Group OKKT = OK;
            TeacherList temp; //список преподов для сортировки



            for (int i = 0; i < Gr.Length - 1; i++)
            {
                for (int j = i + 1; j < Gr.Length; j++)
                {
                    if (Num_TOpp(Gr[i].TOpp) - OccHours(Gr[i]) > (Num_TOpp(Gr[j].TOpp) - OccHours(Gr[j])))
                    {
                        temp = Gr[i];
                        Gr[i] = Gr[j];
                        Gr[j] = temp;
                    }
                }
            }
            for (int id = 0, N = Gr.Length; GroupHours(Gr, OK.Name) > 0 && N > 0; id++, N--)
            {
                OKKT = OK;
                for (int l = 1; l < 5; l++) //Пары
                {
                    for (int k = 0; k < 5; k++) //Дни
                    {
                        for (int i = 0; i < Gr.Length; i++) //цикл для выбора преподавателей 
                        {

                            for (int j = 0; j < Gr[i].subjects.Length; j++) //цикл для выбора предметов
                            {
                                if (Rozklad.IsTrue(k, Gr[i].subjects[j].Subject, OKKT.TimeTable) &&
                                    Gr[i].opportunities[k, l] &&
                                    (Gr[i].subjects[j].Group == OKKT.Name) &&
                                    (Gr[i].TOpp[k, l, 1] || Gr[i].TOpp[k, l, 0]))//условие выбора дня 
                                {
                                    if (Gr[i].subjects[j].hoursPerWeek > 1 &&
                                        OKKT.TimeTable[k, l].EvenWeek.Subject == null &&
                                        OKKT.TimeTable[k, l].OddWeek.Subject == null &&
                                        (Gr[i].TOpp[k, l, 1] && Gr[i].TOpp[k, l, 0]))  //ставим пару предмета в свободный день 
                                    {
                                        {
                                            OKKT.TimeTable[k, l].EvenWeek.Subject = Gr[i].subjects[j].Subject;
                                            OKKT.TimeTable[k, l].OddWeek.Subject = Gr[i].subjects[j].Subject;
                                            OKKT.TimeTable[k, l].EvenWeek.Teacher = Gr[i].teacherName;
                                            OKKT.TimeTable[k, l].OddWeek.Teacher = Gr[i].teacherName;

                                            Gr[i].subjects[j].hoursPerWeek -= 2;
                                            Gr[i].TOpp[k, l, 1] = false;
                                            Gr[i].TOpp[k, l, 0] = false;
                                        }
                                    }
                                    else if ((Gr[i].subjects[j].hoursPerWeek > 0) && ((OKKT.TimeTable[k, l].EvenWeek.Subject == null) && (Gr[i].TOpp[k, l, 0] == true)) && ((OKKT.TimeTable[k, l].OddWeek.Subject != null) || (Gr[i].TOpp[k, l, 1] == false) || (Gr[i].subjects[j].hoursPerWeek == 1)))
                                    {
                                        {
                                            OKKT.TimeTable[k, l].EvenWeek.Subject = Gr[i].subjects[j].Subject;
                                            OKKT.TimeTable[k, l].EvenWeek.Teacher = Gr[i].teacherName;


                                            Gr[i].subjects[j].hoursPerWeek -= 1;
                                            Gr[i].TOpp[k, l, 0] = false;
                                        }
                                    }
                                    else if ((Gr[i].subjects[j].hoursPerWeek > 0) && ((OKKT.TimeTable[k, l].OddWeek.Subject == null) && (Gr[i].TOpp[k, l, 1] == true)) && ((OKKT.TimeTable[k, l].EvenWeek.Subject != null) || (Gr[i].TOpp[k, l, 0] == false) || (Gr[i].subjects[j].hoursPerWeek == 1)))
                                    {
                                        // if (IsWindow(k, l, OKKT.Test, 2) == false)
                                        {
                                            OKKT.TimeTable[k, l].OddWeek.Subject = Gr[i].subjects[j].Subject;
                                            OKKT.TimeTable[k, l].OddWeek.Teacher = Gr[i].teacherName;

                                            Gr[i].subjects[j].hoursPerWeek -= 1;
                                            Gr[i].TOpp[k, l, 1] = false;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                for (int l = 1; l < 5; l++) //Пары
                {
                    for (int k = 0; k < 5; k++) //Дни
                    {
                        for (int i = 0; i < Gr.Length; i++) //цикл для выбора преподавателей 
                        {

                            for (int j = 0; j < Gr[i].subjects.Length; j++) //цикл для выбора предметов
                            {
                                if (Rozklad.IsTrue(k, Gr[i].subjects[j].Subject, OKKT.TimeTable) &&
                                    Gr[i].opportunities[k, l] &&
                                    Gr[i].subjects[j].Group == OKKT.Name &&
                                    (Gr[i].TOpp[k, l, 1] || Gr[i].TOpp[k, l, 0]))//условие выбора дня 
                                {

                                    if ((Gr[i].subjects[j].hoursPerWeek > 1) && (OKKT.TimeTable[k, l].EvenWeek.Subject == null) &&
                                        (OKKT.TimeTable[k, l].OddWeek.Subject == null) &&
                                        (Gr[i].TOpp[k, l, 1] && Gr[i].TOpp[k, l, 0]))  //ставим пару предмета в свободный день 
                                    {
                                        //if (IsWindow(k, l, OKKT.Test, 0) == false)
                                        {
                                            OKKT.TimeTable[k, l].EvenWeek.Subject = Gr[i].subjects[j].Subject;
                                            OKKT.TimeTable[k, l].OddWeek.Subject = Gr[i].subjects[j].Subject;
                                            OKKT.TimeTable[k, l].EvenWeek.Teacher = Gr[i].teacherName;
                                            OKKT.TimeTable[k, l].OddWeek.Teacher = Gr[i].teacherName;

                                            Gr[i].subjects[j].hoursPerWeek -= 2;
                                            Gr[i].TOpp[k, l, 1] = false;
                                            Gr[i].TOpp[k, l, 0] = false;
                                        }
                                    }
                                    else if ((Gr[i].subjects[j].hoursPerWeek > 0) && ((OKKT.TimeTable[k, l].EvenWeek.Subject == null) && (Gr[i].TOpp[k, l, 0] == true)) && ((OKKT.TimeTable[k, l].OddWeek.Subject != null) || (Gr[i].TOpp[k, l, 1] == false) || (Gr[i].subjects[j].hoursPerWeek == 1)))
                                    {
                                        // if (IsWindow(k, l, OKKT.Test, 1) == false)
                                        {
                                            OKKT.TimeTable[k, l].EvenWeek.Subject = Gr[i].subjects[j].Subject;
                                            OKKT.TimeTable[k, l].EvenWeek.Teacher = Gr[i].teacherName;


                                            Gr[i].subjects[j].hoursPerWeek -= 1;
                                            Gr[i].TOpp[k, l, 0] = false;
                                        }
                                    }
                                    else if ((Gr[i].subjects[j].hoursPerWeek > 0) && ((OKKT.TimeTable[k, l].OddWeek.Subject == null) && (Gr[i].TOpp[k, l, 1] == true)) && ((OKKT.TimeTable[k, l].EvenWeek.Subject != null) || (Gr[i].TOpp[k, l, 0] == false) || (Gr[i].subjects[j].hoursPerWeek == 1)))
                                    {
                                        //if (IsWindow(k, l, OKKT.Test, 2) == false)
                                        {
                                            OKKT.TimeTable[k, l].OddWeek.Subject = Gr[i].subjects[j].Subject;
                                            OKKT.TimeTable[k, l].OddWeek.Teacher = Gr[i].teacherName;

                                            Gr[i].subjects[j].hoursPerWeek -= 1;
                                            Gr[i].TOpp[k, l, 1] = false;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                PermutateArr(ref Gr, N);
                if (id == 60)
                {
                    Console.WriteLine("Группа Зависла " + OK.Name);
                    break;
                }
            }
            return OKKT;
        }
        static public int OccHours(TeacherList Prep_OKKT)
        {
            return (Prep_OKKT.subjects.Sum(sub => sub.hoursPerWeek) + 1) / 2;
        }
        static public int GroupHours(TeacherList[] teachers, string groupName)
        {
            return teachers.SelectMany(teacher => teacher.subjects).Sum(subject => subject.hoursPerWeek);
        }
        static int Num_TOpp(bool[,,] TOpp)
        {
            int counter = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    for (int k = 0; k < 2; k++)
                        if (TOpp[i, j, k] == true)
                        {
                            counter++;
                        }
                }
            }
            return counter / 2;
        }

        static void PermutateArr(ref TeacherList[] arr, int n)
        {

            TeacherList temp = arr[n - 1];
            for (var j = n - 1; j > 0; j--)
            {
                arr[j] = arr[j - 1];
            }
            arr[0] = temp;

        }
    }

}
