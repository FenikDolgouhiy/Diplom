﻿using Logic;

using System;
using System.Linq;
using System.Text;

using Excel = Microsoft.Office.Interop.Excel;

namespace TestConsole
{
    class Program
    {
        static void Main()
        {
            //ЗАГРУЗКА ТЕСТОВЫХ ДАННЫХ ИЗ ЭКСЕЛЯ
            /*
            Excel.Application excelApp = new Excel.Application
            {
                Visible = true
            };

            string path = @"C:\OAO\data.xlsx";

            Excel.Workbook workbook = excelApp.Workbooks.Open(path, Type.Missing, true, Type.Missing, Type.Missing,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            Type.Missing, Type.Missing);

            Excel.Worksheet sheet = workbook.Sheets[1]; //ПРЕПОДЫ-УСЛОВИЯ
            Excel.Range SRange;

            SRange = sheet.UsedRange.Columns[1];
            System.Array myvalues = (System.Array)SRange.Cells.Value2;
            string[] P_Prepods = myvalues.OfType<object>().Select(o => o.ToString()).ToArray(); //ПРЕПОДЫ

            SRange = sheet.UsedRange.Columns[2];
            myvalues = (System.Array)SRange.Cells.Value2;
            string[] P_Opps = myvalues.OfType<object>().Select(o => o.ToString()).ToArray(); //УСЛОВИЯ


            sheet = (Excel.Worksheet)workbook.Sheets[2]; //ПРЕПОДЫ-ГРУППА-ПРЕДМЕТ-В НЕДЕЛЮ - ВСЕГО
            SRange = sheet.UsedRange.Columns[1];
            myvalues = (System.Array)SRange.Cells.Value2;
            string[] N_Prepods = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();//ПРЕПОДЫ

            SRange = sheet.UsedRange.Columns[2];
            myvalues = (System.Array)SRange.Cells.Value2;
            string[] N_Groups = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();//ГРУППА

            SRange = sheet.UsedRange.Columns[3];
            myvalues = (System.Array)SRange.Cells.Value2;
            string[] N_Subjects = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();//ПРЕДМЕТ

            SRange = sheet.UsedRange.Columns[4];
            myvalues = (System.Array)SRange.Cells.Value2;
            string[] N_PerWeek = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();//В НЕДЕЛЮ 

            SRange = sheet.UsedRange.Columns[5];
            myvalues = (System.Array)SRange.Cells.Value2;
            string[] N_Total = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();//ВСЕГО

            sheet = (Excel.Worksheet)workbook.Sheets[3];//ГРУППЫ-ПРАКТИКА-НЕДЕЛЬ
            SRange = sheet.UsedRange.Columns[1];
            myvalues = (System.Array)SRange.Cells.Value2;
            string[] G_Groups = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();//ГРУППЫ

            SRange = sheet.UsedRange.Columns[2];
            myvalues = (System.Array)SRange.Cells.Value2;
            string[] G_Practice = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();//ПРАКТИКА

            SRange = sheet.UsedRange.Columns[3];
            myvalues = (System.Array)SRange.Cells.Value2;
            string[] G_Weeks = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();//НЕДЕЛЬ8
            */
            DBLoad a = new DBLoad();
             a.UploadFromFB();
             Console.ReadKey();
             Console.WriteLine("Загрузка завершена. Элементов списка " + a.UploadList.Count);
             string[] N_Prepods = new string[a.UploadList.Count];
             string[] N_Groups  = new string[a.UploadList.Count];
             string[] N_Predms = new string[a.UploadList.Count];
             string[] N_PerWeek = new string[a.UploadList.Count];
             string[] N_Weeks = new string[a.UploadList.Count];
             string[] N_Total = new string[a.UploadList.Count];
             for (int i = 0; i < a.UploadList.Count; i++)
             {
                 N_Prepods[i] = a.UploadList[i].Teacher;
                 N_Groups[i] = a.UploadList[i].Group;
                 N_Predms[i] = a.UploadList[i].Subject;
                 N_PerWeek[i] = a.UploadList[i].HoursPerWeek;
                 N_Weeks[i] = a.UploadList[i].Weeks;
                 N_Total[i] = a.UploadList[i].TotalHours;
                 Console.Write(N_Prepods[i]  + " ");
                 Console.Write(N_Groups[i]  + " ");
                 Console.Write(N_Predms[i] + " ");
                 Console.Write(N_PerWeek[i]  + " ");
                 Console.Write(N_Weeks[i]  + " ");
                 Console.Write(N_Total[i]  + " ");
                 Console.WriteLine();
             }
            a.UploadTeachersOpp();
            Console.ReadKey();
            Console.WriteLine("Загрузка завершена. Элементов списка " + a.TeachersOppList.Count);
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
                Console.Write(P_Prepods[i] + " ");
                Console.Write(P_Opps[i] + " ");
                Console.WriteLine();
            }
            for (int i = 0; i < P_Opps.Length; i++) //Вывод для проверки 
            {
                Console.WriteLine(P_Prepods[i] + " " + P_Opps[i]);
            }
            int Number_of_prep = P_Prepods.Length;//Количество преподавателей для инициализации 

            TeacherList[] Prep_okkt; //список преподов
            Prep_okkt = new TeacherList[Number_of_prep];

            Console.OutputEncoding = Encoding.UTF8;


            //Дальше идёт инициализация основных объектов данными из массивов.
            Group[] okkt = new Group[G_Groups.Length];
            for (int i = 0; i < G_Groups.Length; i++)
            {
                okkt[i] = new Group(G_Groups[i], Convert.ToInt32(G_Weeks[i]), Convert.ToInt32(G_Practice[i]));
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
                        Prep_okkt[i].subjects[k] = new Subjects(N_Subjects[j], Convert.ToInt32(N_Total[j]), (Convert.ToInt32(N_Total[j]) / Convert.ToInt32(N_PerWeek[j])), N_Groups[j], okkt);
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
            
                for (int i = 0; i < okkt.Length - 1; i++)//Сортировка групп по количеству часов. Почему-то не работает (наверное(я не знаю))
                {
                    for (int j = i + 1; j < okkt.Length; j++)
                    {
                        if (AdeptMech.GroupHours(Prep_okkt, okkt[i].Name) < AdeptMech.GroupHours(Prep_okkt, okkt[j].Name))
                        {
                            temp = Prep_okkt[i];
                            Prep_okkt[i] = Prep_okkt[j];
                            Prep_okkt[j] = temp;
                        }
                    }
                }
                for (int i = 0; i < Prep_okkt.Length; i++) //вывод для проверки
                {
                    Console.WriteLine(Prep_okkt[i].teacherName + " " + Prep_okkt[i].subjectsCount + " ");
                    for (int n = 0; n < 5; n++)
                    {
                        for (int m = 0; m < 4; m++)
                        {
                            if (Prep_okkt[i].opportunities[n, m] == true)
                                Console.Write('+');
                            else Console.Write('-');
                        }

                    }
                    Console.Write(" " + Prep_okkt[i].possibilitiesCount + " ");
                    for (int j = 0; j < Prep_okkt[i].subjects.Length; j++)
                    {
                        Console.WriteLine("Предмет " + Prep_okkt[i].subjects[j].Subject + " " + Prep_okkt[i].subjects[j].hoursSum + " " + Prep_okkt[i].subjects[j].hoursPerWeek + " ");
                    }
                }
                for (int i = 0; i < okkt.Length; i++)//Создание расписания погруппно
                {
                    okkt[i] = AdeptMech.CreatRoz(ref Prep_okkt, okkt[i]);
                }


                String[] Workout = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" }; //вывод расписания
                for (int d = 0; d < okkt.Length; d++)
                {
                    Console.WriteLine(okkt[d].Name);
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine(Workout[i]);
                        for (int k = 0; k < 6; k++)
                        {

                            if (okkt[d].TimeTable[i, k].EvenWeek.Subject == okkt[d].TimeTable[i, k].OddWeek.Subject)
                            {
                                Console.WriteLine(k + ". " + okkt[d].TimeTable[i, k].EvenWeek.Subject + " " + okkt[d].TimeTable[i, k].EvenWeek.Teacher);
                            }
                            else Console.WriteLine(k + ". " + okkt[d].TimeTable[i, k].EvenWeek.Subject + " " + okkt[d].TimeTable[i, k].EvenWeek.Teacher + "|" + okkt[d].TimeTable[i, k].OddWeek.Subject + " " + okkt[d].TimeTable[i, k].OddWeek.Teacher);
                        }
                    }
                }
                Console.ReadKey();
            
        }
    }
}
