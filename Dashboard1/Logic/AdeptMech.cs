﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard1.Logic
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
                                        if (IsWindow(k, l, OKKT.TimeTable, 0) == false)
                                        {
                                            OKKT.TimeTable[k, l].EvenWeek.Subject = Gr[i].subjects[j].Subject;
                                            OKKT.TimeTable[k, l].OddWeek.Subject = Gr[i].subjects[j].Subject;
                                            OKKT.TimeTable[k, l].EvenWeek.Teacher = Gr[i].teacherName;
                                            OKKT.TimeTable[k, l].OddWeek.Teacher = Gr[i].teacherName;
                                            OKKT.TimeTable[k, l].EvenWeek.Cabinet = Gr[i].Cabinet;
                                            OKKT.TimeTable[k, l].OddWeek.Cabinet = Gr[i].Cabinet;

                                            Gr[i].subjects[j].hoursPerWeek -= 2;
                                            Gr[i].TOpp[k, l, 1] = false;
                                            Gr[i].TOpp[k, l, 0] = false;
                                        }
                                    }
                                    else if ((Gr[i].subjects[j].hoursPerWeek > 0) && ((OKKT.TimeTable[k, l].EvenWeek.Subject == null) && (Gr[i].TOpp[k, l, 0] == true)) && ((OKKT.TimeTable[k, l].OddWeek.Subject != null) || (Gr[i].TOpp[k, l, 1] == false) || (Gr[i].subjects[j].hoursPerWeek == 1)))//Пара в числитель
                                    {
                                        if (IsWindow(k, l, OKKT.TimeTable, 1) == false)
                                        {
                                            OKKT.TimeTable[k, l].EvenWeek.Subject = Gr[i].subjects[j].Subject;
                                            OKKT.TimeTable[k, l].EvenWeek.Teacher = Gr[i].teacherName;
                                            OKKT.TimeTable[k, l].EvenWeek.Cabinet = Gr[i].Cabinet;

                                            Gr[i].subjects[j].hoursPerWeek -= 1;
                                            Gr[i].TOpp[k, l, 0] = false;
                                        }
                                    }
                                    else if ((Gr[i].subjects[j].hoursPerWeek > 0) && ((OKKT.TimeTable[k, l].OddWeek.Subject == null) && (Gr[i].TOpp[k, l, 1] == true)) && ((OKKT.TimeTable[k, l].EvenWeek.Subject != null) || (Gr[i].TOpp[k, l, 0] == false) || (Gr[i].subjects[j].hoursPerWeek == 1)))//Пара в знаменатель
                                    {
                                        if (IsWindow(k, l, OKKT.TimeTable, 2) == false)
                                        {
                                            OKKT.TimeTable[k, l].OddWeek.Subject = Gr[i].subjects[j].Subject;
                                            OKKT.TimeTable[k, l].OddWeek.Teacher = Gr[i].teacherName;
                                            OKKT.TimeTable[k, l].OddWeek.Cabinet = Gr[i].Cabinet;

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


                                        OKKT.TimeTable[k, l].EvenWeek.Subject = Gr[i].subjects[j].Subject;
                                        OKKT.TimeTable[k, l].OddWeek.Subject = Gr[i].subjects[j].Subject;
                                        OKKT.TimeTable[k, l].EvenWeek.Teacher = Gr[i].teacherName;
                                        OKKT.TimeTable[k, l].OddWeek.Teacher = Gr[i].teacherName;
                                        OKKT.TimeTable[k, l].EvenWeek.Cabinet = Gr[i].Cabinet;
                                        OKKT.TimeTable[k, l].OddWeek.Cabinet = Gr[i].Cabinet;
                                        Gr[i].subjects[j].hoursPerWeek -= 2;
                                        Gr[i].TOpp[k, l, 1] = false;
                                        Gr[i].TOpp[k, l, 0] = false;

                                     }
                                    else if ((Gr[i].subjects[j].hoursPerWeek > 0) && ((OKKT.TimeTable[k, l].EvenWeek.Subject == null) && (Gr[i].TOpp[k, l, 0] == true)) && ((OKKT.TimeTable[k, l].OddWeek.Subject != null) || (Gr[i].TOpp[k, l, 1] == false) || (Gr[i].subjects[j].hoursPerWeek == 1)))
                                    {


                                        OKKT.TimeTable[k, l].EvenWeek.Subject = Gr[i].subjects[j].Subject;
                                        OKKT.TimeTable[k, l].EvenWeek.Teacher = Gr[i].teacherName;
                                        OKKT.TimeTable[k, l].EvenWeek.Cabinet = Gr[i].Cabinet;


                                        Gr[i].subjects[j].hoursPerWeek -= 1;
                                        Gr[i].TOpp[k, l, 0] = false;

                                    }
                                    else if ((Gr[i].subjects[j].hoursPerWeek > 0) && ((OKKT.TimeTable[k, l].OddWeek.Subject == null) && (Gr[i].TOpp[k, l, 1] == true)) && ((OKKT.TimeTable[k, l].EvenWeek.Subject != null) || (Gr[i].TOpp[k, l, 0] == false) || (Gr[i].subjects[j].hoursPerWeek == 1)))
                                    {


                                        OKKT.TimeTable[k, l].OddWeek.Subject = Gr[i].subjects[j].Subject;
                                        OKKT.TimeTable[k, l].OddWeek.Teacher = Gr[i].teacherName;
                                        OKKT.TimeTable[k, l].OddWeek.Cabinet = Gr[i].Cabinet;

                                        Gr[i].subjects[j].hoursPerWeek -= 1;
                                        Gr[i].TOpp[k, l, 1] = false;

                                    }
                                }

                            }
                        }
                    }
                }
                for (int DayInWeek = 0; DayInWeek < 5; DayInWeek++)
                {
                    if (HasWindows(DayInWeek, OKKT.TimeTable))
                    {


                        LicWindow(OKKT.TimeTable, Gr, DayInWeek);
                    }
                }


            }

            return OKKT;
        }
        static public int OccHours(TeacherList Prep_OKKT)//Подсчёт количества пар в неделю.
        {
            int counter = 0;
            for (int i = 0; i < Prep_OKKT.subjects.Length; i++)
            {
                counter += Prep_OKKT.subjects[i].hoursPerWeek;
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
                    if (teachers[i].subjects[j].Group == groupName)
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

        static bool HasWindows(int kW, Rozklad[,] TimetableWindow)
        {
            bool pointer1;
            bool pointer2;
            int counter1;
            int counter2;
            int l;
            for (l = 0, counter1 = 0, counter2 = 0, pointer1 = false, pointer2 = false; l <= 5; l++)
            {

                if (pointer1 == false && TimetableWindow[kW, l].EvenWeek.Subject != null)
                {
                    counter1++;
                    pointer1 = true;
                }
                else if (pointer1 == true && TimetableWindow[kW, l].EvenWeek.Subject == null)
                {
                    counter1++;
                    pointer1 = false;
                }
                if (pointer2 == false && TimetableWindow[kW, l].OddWeek.Subject != null)
                {
                    counter2++;
                    pointer2 = true;
                }
                else if (pointer2 == true && TimetableWindow[kW, l].OddWeek.Subject == null)
                {
                    counter2++;
                    pointer2 = false;
                }
            }
            if (counter1 > 2 || counter2 > 2)
                return true;
            else return false;
        }
        static public void LicWindow(Rozklad[,] Roz_LW, TeacherList[] Prep_LW, int day)
        {
            for (int pair = 0; pair < 5; pair++)
            {

                if (Roz_LW[day, pair].EvenWeek.Subject == null && IsWindow(day, pair, Roz_LW, 3))
                {

                    ChngWindow(Roz_LW, Prep_LW, day, pair, 1);
                }
                if (Roz_LW[day, pair].OddWeek.Subject == null && IsWindow(day, pair, Roz_LW, 4))
                {

                    ChngWindow(Roz_LW, Prep_LW, day, pair, 2);
                }

            }
        }
        static public void ChngWindow(Rozklad[,] Roz_CW, TeacherList[] Prep_CW, int day, int pair, int cs)
        {
            for (int Pairz = 0; Pairz < 6; Pairz++)
            {
                for (int Dayz = 0; Dayz < 5; Dayz++)
                {
                    if (cs == 1)
                    {
                        if (Roz_CW[Dayz, Pairz].EvenWeek.Subject != null)
                        {
                            if (LastPair(Pairz, Dayz, Roz_CW, 1) == true || FirstPair(Pairz, Dayz, Roz_CW, 1) == true)
                            {



                                if (Prep_CW[CurrTeacher(Roz_CW[Dayz, Pairz].EvenWeek.Teacher, Prep_CW)].TOpp[day, pair, 0] == true)
                                {
                                    Roz_CW[day, pair].EvenWeek.Subject = Roz_CW[Dayz, Pairz].EvenWeek.Subject;
                                    Roz_CW[day, pair].EvenWeek.Teacher = Roz_CW[Dayz, Pairz].EvenWeek.Teacher;
                                    Roz_CW[day, pair].EvenWeek.Cabinet = Roz_CW[Dayz, Pairz].EvenWeek.Cabinet;
                                    Prep_CW[CurrTeacher(Roz_CW[Dayz, Pairz].EvenWeek.Teacher, Prep_CW)].TOpp[day, pair, 0] = false;
                                    Prep_CW[CurrTeacher(Roz_CW[Dayz, Pairz].EvenWeek.Teacher, Prep_CW)].TOpp[Dayz, Pairz, 0] = true;
                                    Roz_CW[Dayz, Pairz].EvenWeek.Subject = null;
                                    Roz_CW[Dayz, Pairz].EvenWeek.Teacher = null;
                                    Roz_CW[Dayz, Pairz].EvenWeek.Cabinet = null;

                                    return;
                                }
                            }

                        }
                        if (Roz_CW[Dayz, Pairz].OddWeek.Subject != null)
                        {
                            if (LastPair(Pairz, Dayz, Roz_CW, 2) == true || FirstPair(Pairz, Dayz, Roz_CW, 2) == true)
                            {
                                if (Prep_CW[CurrTeacher(Roz_CW[day, pair].OddWeek.Teacher, Prep_CW)].TOpp[day, pair, 0] == true)
                                {
                                    Roz_CW[day, pair].EvenWeek.Subject = Roz_CW[Dayz, Pairz].OddWeek.Subject;
                                    Roz_CW[day, pair].EvenWeek.Teacher = Roz_CW[Dayz, Pairz].OddWeek.Teacher;
                                    Roz_CW[day, pair].EvenWeek.Cabinet = Roz_CW[Dayz, Pairz].OddWeek.Cabinet;
                                    Prep_CW[CurrTeacher(Roz_CW[Dayz, Pairz].OddWeek.Teacher, Prep_CW)].TOpp[day, pair, 0] = false;
                                    Prep_CW[CurrTeacher(Roz_CW[Dayz, Pairz].OddWeek.Teacher, Prep_CW)].TOpp[Dayz, Pairz, 1] = true;
                                    Roz_CW[Dayz, Pairz].OddWeek.Subject = null;
                                    Roz_CW[Dayz, Pairz].OddWeek.Teacher = null;
                                    Roz_CW[Dayz, Pairz].OddWeek.Cabinet = null;


                                    return;
                                }
                            }
                        }
                    }
                    if (cs == 2)
                    {
                        if (Roz_CW[Dayz, Pairz].EvenWeek.Subject != null)
                        {

                            if (LastPair(Pairz, Dayz, Roz_CW, 1) == true || FirstPair(Pairz, Dayz, Roz_CW, 1) == true)
                            {

                                if (Prep_CW[CurrTeacher(Roz_CW[Dayz, Pairz].EvenWeek.Teacher, Prep_CW)].TOpp[day, pair, 1] == true)
                                {
                                    Roz_CW[day, pair].OddWeek.Subject = Roz_CW[Dayz, Pairz].EvenWeek.Subject;
                                    Roz_CW[day, pair].OddWeek.Teacher = Roz_CW[Dayz, Pairz].EvenWeek.Teacher;
                                    Roz_CW[day, pair].OddWeek.Cabinet = Roz_CW[Dayz, Pairz].EvenWeek.Cabinet;
                                    Prep_CW[CurrTeacher(Roz_CW[Dayz, Pairz].EvenWeek.Teacher, Prep_CW)].TOpp[day, pair, 1] = false;
                                    Prep_CW[CurrTeacher(Roz_CW[Dayz, Pairz].EvenWeek.Teacher, Prep_CW)].TOpp[Dayz, Pairz, 0] = true;
                                    Roz_CW[Dayz, Pairz].EvenWeek.Subject = null;
                                    Roz_CW[Dayz, Pairz].EvenWeek.Teacher = null;
                                    Roz_CW[Dayz, Pairz].EvenWeek.Cabinet = null;

                                    return;
                                }
                            }

                        }
                        if (Roz_CW[Dayz, Pairz].OddWeek.Subject != null)
                        {
                            if (LastPair(Pairz, Dayz, Roz_CW, 2) == true || FirstPair(Pairz, Dayz, Roz_CW, 2) == true)
                            {
                                if (Prep_CW[CurrTeacher(Roz_CW[day, pair].OddWeek.Teacher, Prep_CW)].TOpp[day, pair, 1] == true)
                                {
                                    Roz_CW[day, pair].OddWeek.Subject = Roz_CW[Dayz, Pairz].OddWeek.Subject;
                                    Roz_CW[day, pair].OddWeek.Teacher = Roz_CW[Dayz, Pairz].OddWeek.Teacher;
                                    Roz_CW[day, pair].OddWeek.Cabinet = Roz_CW[Dayz, Pairz].OddWeek.Cabinet;
                                    Prep_CW[CurrTeacher(Roz_CW[Dayz, Pairz].OddWeek.Teacher, Prep_CW)].TOpp[day, pair, 1] = false;
                                    Prep_CW[CurrTeacher(Roz_CW[Dayz, Pairz].OddWeek.Teacher, Prep_CW)].TOpp[Dayz, Pairz, 1] = true;
                                    Roz_CW[Dayz, Pairz].OddWeek.Subject = null;
                                    Roz_CW[Dayz, Pairz].OddWeek.Teacher = null;
                                    Roz_CW[Dayz, Pairz].OddWeek.Cabinet = null;

                                    return;
                                }
                            }
                        }
                    }

                }
            }

        }
        static int CurrTeacher(string TeacherName, TeacherList[] ListofTeachers)
        {
            for (int i = 0; i < ListofTeachers.Length; i++)
            {
                if (ListofTeachers[i].teacherName == TeacherName)
                {

                    return i;
                }
            }

            return 0;
        }
        static bool FirstPair(int CurPair, int CurDay, Rozklad[,] TimetableWindow, int cs)
        {
            bool pointer1;
            bool pointer2;
            int counter1;
            int counter2;
            int l;

            for (l = 0, counter1 = 0, counter2 = 0, pointer1 = false, pointer2 = false; l <= 5; l++)
            {
                if (l == CurPair)
                {
                    if (pointer1 == false && counter1 < 1)
                    {
                        pointer1 = true;
                    }
                    if (pointer2 == false && counter2 < 1)
                    {
                        pointer2 = true;
                    }
                    if (pointer1 == true && cs == 1 && counter1 < 1)
                        return true;
                    else if (pointer2 == true && cs == 2 && counter2 < 2)
                        return true;
                }
                if (pointer1 == false && TimetableWindow[CurDay, l].EvenWeek.Subject != null)
                {
                    counter1++;
                    pointer1 = true;
                }
                else if (pointer1 == true && TimetableWindow[CurDay, l].EvenWeek.Subject == null)
                {
                    counter1++;
                    pointer1 = false;
                }
                if (pointer2 == false && TimetableWindow[CurDay, l].OddWeek.Subject != null)
                {
                    counter2++;
                    pointer2 = true;
                }
                else if (pointer2 == true && TimetableWindow[CurDay, l].OddWeek.Subject == null)
                {
                    counter2++;
                    pointer2 = false;
                }


            }
            return false;
        }
        static bool LastPair(int CurPair, int CurDay, Rozklad[,] TimetableWindow, int cs)
        {
            bool pointer1;
            bool pointer2;
            int counter1;
            int counter2;
            int l;

            for (l = 5, counter1 = 0, counter2 = 0, pointer1 = false, pointer2 = false; l >= 0; l--)
            {
                if (l == CurPair)
                {
                    if (pointer1 == false && counter1 < 1)
                    {
                        pointer1 = true;
                    }
                    if (pointer2 == false && counter2 < 1)
                    {
                        pointer2 = true;
                    }
                    if (pointer1 == true && cs == 1 && counter1 < 1)
                        return true;
                    else if (pointer2 == true && cs == 2 && counter2 < 2)
                        return true;
                }
                if (pointer1 == false && TimetableWindow[CurDay, l].EvenWeek.Subject != null)
                {
                    counter1++;
                    pointer1 = true;
                }
                else if (pointer1 == true && TimetableWindow[CurDay, l].EvenWeek.Subject == null)
                {
                    counter1++;
                    pointer1 = false;
                }
                if (pointer2 == false && TimetableWindow[CurDay, l].OddWeek.Subject != null)
                {
                    counter2++;
                    pointer2 = true;
                }
                else if (pointer2 == true && TimetableWindow[CurDay, l].OddWeek.Subject == null)
                {
                    counter2++;
                    pointer2 = false;
                }


            }
            return false;
        }
        public static void UploadTimetable(Group[] OKKT)
        {
            List<TimetablesList> timetableOKKT = new List<TimetablesList>();
            TimetablesList temp;
            for (int i = 0; i < OKKT.Length; i++)
            {
                temp = new TimetablesList();
                temp.Group = OKKT[i].Name;
                temp.Monday0 = OKKT[i].TotalTimetable[0, 0];
                temp.Monday1 = OKKT[i].TotalTimetable[0, 1];
                temp.Monday2 = OKKT[i].TotalTimetable[0, 2];
                temp.Monday3 = OKKT[i].TotalTimetable[0, 3];
                temp.Monday4 = OKKT[i].TotalTimetable[0, 4];
                temp.Monday5 = OKKT[i].TotalTimetable[0, 5];
                temp.Tuesday0 = OKKT[i].TotalTimetable[1, 0];
                temp.Tuesday1 = OKKT[i].TotalTimetable[1, 1];
                temp.Tuesday2 = OKKT[i].TotalTimetable[1, 2];
                temp.Tuesday3 = OKKT[i].TotalTimetable[1, 3];
                temp.Tuesday4 = OKKT[i].TotalTimetable[1, 4];
                temp.Tuesday5 = OKKT[i].TotalTimetable[1, 5];
                temp.Wednesday0 = OKKT[i].TotalTimetable[2, 0];
                temp.Wednesday1 = OKKT[i].TotalTimetable[2, 1];
                temp.Wednesday2 = OKKT[i].TotalTimetable[2, 2];
                temp.Wednesday3 = OKKT[i].TotalTimetable[2, 3];
                temp.Wednesday4 = OKKT[i].TotalTimetable[2, 4];
                temp.Wednesday5 = OKKT[i].TotalTimetable[2, 5];
                temp.Thursday0 = OKKT[i].TotalTimetable[3, 0];
                temp.Thursday1 = OKKT[i].TotalTimetable[3, 1];
                temp.Thursday2 = OKKT[i].TotalTimetable[3, 2];
                temp.Thursday3 = OKKT[i].TotalTimetable[3, 3];
                temp.Thursday4 = OKKT[i].TotalTimetable[3, 4];
                temp.Thursday5 = OKKT[i].TotalTimetable[3, 5];
                temp.Friday0 = OKKT[i].TotalTimetable[4, 0];
                temp.Friday1 = OKKT[i].TotalTimetable[4, 1];
                temp.Friday2 = OKKT[i].TotalTimetable[4, 2];
                temp.Friday3 = OKKT[i].TotalTimetable[4, 3];
                temp.Friday4 = OKKT[i].TotalTimetable[4, 4];
                temp.Friday5 = OKKT[i].TotalTimetable[4, 5];
                timetableOKKT.Add(temp);

            }

            DBLoad a = new DBLoad();
            a.GroupsTimetableList = timetableOKKT;
            a.ImportTimetableToFB();
        }
        static bool IsWindow(int kW, int lW, Rozklad[,] TimetableWindow, int Cs)
        {
            bool pointer1;
            bool pointer2;
            int counter1;
            int counter2;
            int l;
            bool BeforeCheck1 = false;
            bool BeforeCheck2 = false;
            bool AfterCheck1 = false;
            bool AfterCheck2 = false;
            if (Cs == 0)
            {
                for (l = 0, counter1 = 0, counter2 = 0, pointer1 = false, pointer2 = false; l <= lW; l++)
                {
                    if (l == lW)
                    {
                        if (pointer1 == false)
                        {
                            counter1++;
                            pointer1 = true;
                        }
                        if (pointer2 == false)
                        {
                            counter2++;
                            pointer2 = true;
                        }
                        break;
                    }
                    if (pointer1 == false && TimetableWindow[kW, l].EvenWeek.Subject != null)
                    {
                        counter1++;
                        pointer1 = true;
                    }
                    else if (pointer1 == true && TimetableWindow[kW, l].EvenWeek.Subject == null)
                    {
                        counter1++;
                        pointer1 = false;
                    }
                    if (pointer2 == false && TimetableWindow[kW, l].OddWeek.Subject != null)
                    {
                        counter2++;
                        pointer2 = true;
                    }
                    else if (pointer2 == true && TimetableWindow[kW, l].OddWeek.Subject == null)
                    {
                        counter2++;
                        pointer2 = false;
                    }
                }
                if (counter1 > 2 || counter2 > 2)
                    return true;
                else return false;


            }
            else if (Cs == 1)
            {

                for (l = 0, counter1 = 0, counter2 = 0, pointer1 = false, pointer2 = false; l <= lW; l++)
                {
                    if (l == lW)
                    {
                        if (pointer1 == false)
                        {
                            counter1++;
                            pointer1 = true;
                        }
                        if (pointer2 == true)
                        {
                            counter2++;
                            pointer2 = false;
                        }
                        break;
                    }
                    if (pointer1 == false && TimetableWindow[kW, l].EvenWeek.Subject != null)
                    {
                        counter1++;
                        pointer1 = true;
                    }
                    else if (pointer1 == true && TimetableWindow[kW, l].EvenWeek.Subject == null)
                    {
                        counter1++;
                        pointer1 = false;
                    }
                    if (pointer2 == false && TimetableWindow[kW, l].OddWeek.Subject != null)
                    {
                        counter2++;
                        pointer2 = true;
                    }
                    else if (pointer2 == true && TimetableWindow[kW, l].OddWeek.Subject == null)
                    {
                        counter2++;
                        pointer2 = false;
                    }
                }
                if (counter1 > 2 || counter2 > 2)
                    return true;
                else return false;
            }
            else if (Cs == 2)
            {


                for (l = 0, counter1 = 0, counter2 = 0, pointer1 = false, pointer2 = false; l <= lW; l++)
                {
                    if (l == lW)
                    {
                        if (pointer1 == true)
                        {
                            counter1++;
                            pointer1 = false;
                        }
                        if (pointer2 == false)
                        {
                            counter2++;
                            pointer2 = true;
                        }
                        break;
                    }
                    if (pointer1 == false && TimetableWindow[kW, l].EvenWeek.Subject != null)
                    {
                        counter1++;
                        pointer1 = true;
                    }
                    else if (pointer1 == true && TimetableWindow[kW, l].EvenWeek.Subject == null)
                    {
                        counter1++;
                        pointer1 = false;
                    }
                    if (pointer2 == false && TimetableWindow[kW, l].OddWeek.Subject != null)
                    {
                        counter2++;
                        pointer2 = true;
                    }
                    else if (pointer2 == true && TimetableWindow[kW, l].OddWeek.Subject == null)
                    {
                        counter2++;
                        pointer2 = false;
                    }
                }
                if (counter1 > 2 || counter2 > 2)
                    return true;
                else return false;
            }
            else if (Cs == 3)
            {

                for (l = 0, counter1 = 0, counter2 = 0, pointer1 = false, pointer2 = false; l <= 5; l++)
                {
                    if (l == lW)
                    {
                        if (pointer1 == true && counter1 > 0)
                        {
                            BeforeCheck1 = true;
                        }
                        if (pointer2 == true && counter1 > 0)
                        {
                            BeforeCheck2 = true;
                        }
                    }
                    if (l > lW && (AfterCheck1 != true || AfterCheck2 != true))
                    {
                        if (TimetableWindow[kW, l].EvenWeek.Subject != null)
                        {
                            AfterCheck1 = true;
                        }
                        if (TimetableWindow[kW, l].OddWeek.Subject != null)
                        {
                            AfterCheck2 = true;
                        }
                    }
                    if (pointer1 == false && TimetableWindow[kW, l].EvenWeek.Subject != null)
                    {
                        counter1++;
                        pointer1 = true;
                    }
                    else if (pointer1 == true && TimetableWindow[kW, l].EvenWeek.Subject == null)
                    {
                        counter1++;
                        pointer1 = false;
                    }
                    if (pointer2 == false && TimetableWindow[kW, l].OddWeek.Subject != null)
                    {
                        counter2++;
                        pointer2 = true;
                    }
                    else if (pointer2 == true && TimetableWindow[kW, l].OddWeek.Subject == null)
                    {
                        counter2++;
                        pointer2 = false;
                    }


                }
                if (BeforeCheck1 == true && AfterCheck1 == true && counter1 > 2)
                    return true;
                else return false;
            }
            else if (Cs == 4)
            {

                for (l = 0, counter1 = 0, counter2 = 0, pointer1 = false, pointer2 = false; l <= 5; l++)
                {
                    if (l == lW)
                    {
                        if (pointer1 == true && counter1 > 0)
                        {
                            BeforeCheck1 = true;
                        }
                        if (pointer2 == true && counter1 > 0)
                        {
                            BeforeCheck2 = true;
                        }
                    }
                    if (l > lW && (AfterCheck1 != true || AfterCheck2 != true))
                    {
                        if (TimetableWindow[kW, l].EvenWeek.Subject != null)
                        {
                            AfterCheck1 = true;
                        }
                        if (TimetableWindow[kW, l].OddWeek.Subject != null)
                        {
                            AfterCheck2 = true;
                        }
                    }
                    if (pointer1 == false && TimetableWindow[kW, l].EvenWeek.Subject != null)
                    {
                        counter1++;
                        pointer1 = true;
                    }
                    else if (pointer1 == true && TimetableWindow[kW, l].EvenWeek.Subject == null)
                    {
                        counter1++;
                        pointer1 = false;
                    }
                    if (pointer2 == false && TimetableWindow[kW, l].OddWeek.Subject != null)
                    {
                        counter2++;
                        pointer2 = true;
                    }
                    else if (pointer2 == true && TimetableWindow[kW, l].OddWeek.Subject == null)
                    {
                        counter2++;
                        pointer2 = false;
                    }


                }
                if (BeforeCheck2 == true && AfterCheck2 == true && counter2 > 2)
                    return true;
                else return false;
            }
            else return false;
        }
    }

}
