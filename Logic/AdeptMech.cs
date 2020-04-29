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



            for (int i = 0; i < Gr.Length - 1; i++) //Сортировка списка преподавателей перед созданием расписанию. Не уверен в полезности, но пусть будет
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
            for (int id = 0, N = Gr.Length; GroupHours(Gr, OK.Name) > 0 && N > 0; id++, N--) //Условие выхода из функции. Если вдруг расписание не собирается, то перемешать массив преподавателей
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
                                    else if ((Gr[i].subjects[j].hoursPerWeek > 0) && ((OKKT.TimeTable[k, l].EvenWeek.Subject == null) && (Gr[i].TOpp[k, l, 0] == true)) && ((OKKT.TimeTable[k, l].OddWeek.Subject != null) || (Gr[i].TOpp[k, l, 1] == false) || (Gr[i].subjects[j].hoursPerWeek == 1)))//Пара в числитель
                                    {
                                        {
                                            OKKT.TimeTable[k, l].EvenWeek.Subject = Gr[i].subjects[j].Subject;
                                            OKKT.TimeTable[k, l].EvenWeek.Teacher = Gr[i].teacherName;


                                            Gr[i].subjects[j].hoursPerWeek -= 1;
                                            Gr[i].TOpp[k, l, 0] = false;
                                        }
                                    }
                                    else if ((Gr[i].subjects[j].hoursPerWeek > 0) && ((OKKT.TimeTable[k, l].OddWeek.Subject == null) && (Gr[i].TOpp[k, l, 1] == true)) && ((OKKT.TimeTable[k, l].EvenWeek.Subject != null) || (Gr[i].TOpp[k, l, 0] == false) || (Gr[i].subjects[j].hoursPerWeek == 1)))//Пара в знаменатель
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
                for (int l = 1; l < 5; l++) //Пары //Повторный цикл, чтобы доставить, например, половинчатые пары. Целесообразность под сомнением но пусть будет
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
                                        //if (IsWindow(k, l, OKKT.Test, 0) == false) Условия по закрытию без окон. Функция неработоспособна, но пусть будет.
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
                PermutateArr(ref Gr, N);// Перестановка массива в случае незакрытия предметов
                if (id == 60)
                {
                    Console.WriteLine("Группа Зависла " + OK.Name);//ЧТобы я мог понять, что что-то пошло не так
                    break;
                }
            }
            return OKKT;
        }
        static public int OccHours(TeacherList Prep_OKKT)//Подсчёт количества пар в неделю.
        {
                int counter = 0;
                for (int i = 0; i < Prep_OKKT.subjects.Length; i++)
                {
                    counter += Prep_OKKT.subjects[i].hoursPerWeek ;
                }
                if (counter % 2 > 0)
                {
                    counter = counter / 2;
                    counter++;
                }
                else counter = counter / 2;
                return counter;
        }
        static public int GroupHours(TeacherList[] teachers, string groupName)//Количество часов по предметам у группы
        {
            int GrHrs = 0;
                for (int i = 0; i < teachers.Length; i++)
                {
                    for (int j = 0; j < teachers[i].subjects.Length; j++)
                    {
                        if (teachers[i].subjects[j].Group  == groupName)
                        {
                            GrHrs += teachers[i].subjects[j].hoursPerWeek;
                        }
                    }
                }
                return GrHrs;
        }
        static int Num_TOpp(bool[,,] TOpp)//Количество свободных пар преподавателя (С учётом поставленных пар)
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

        static void PermutateArr(ref TeacherList[] arr, int n)//Метод перемешивания массива.
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
