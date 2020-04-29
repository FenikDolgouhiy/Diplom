using Logic;

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

            for (int i = 0; i < P_Opps.Length; i++)
            {
                Console.WriteLine(P_Prepods[i] + " " + P_Opps[i]);
            }
            int Number_of_prep = P_Prepods.Length;

            TeacherList[] Prep_okkt; //список преподов
            Prep_okkt = new TeacherList[Number_of_prep];

            Console.OutputEncoding = Encoding.UTF8;



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
            for (int i = 0; i < Prep_okkt.Length - 1; i++)
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
            {
                for (int i = 0; i < okkt.Length - 1; i++)
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
                for (int i = 0; i < okkt.Length; i++)
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
}
