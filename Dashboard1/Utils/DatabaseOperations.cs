using Dashboard1.Model;
using Dashboard1.View;
using LinqToExcel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dashboard1.Utils
{
    public class DatabaseOperations
    {
        public List<LoadDTO> ImportTeacherLoadsFromExcel()
        {
            List<LoadDTO> result = null;
            var dialog = new OpenFileDialog
            {
                Filter = "Таблицы Excel'97 (*.xls)|*.xls|Taблицы Excel'2007 (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == true)
            {
                using (var connection = InitiateLoadModelMappings(new ExcelQueryFactory(dialog.FileName)))
                {
                    result = (from load in connection.Worksheet<LoadDTO>("Лист1") select load).ToList();
                }
            }
            return result;
        }

        private static ExcelQueryFactory InitiateLoadModelMappings(ExcelQueryFactory excelQueryFactory)
        {
            excelQueryFactory.AddMapping<LoadDTO>(x => x.Id, "ID");
            excelQueryFactory.AddMapping<LoadDTO>(x => x.Teacher, "Преподаватель");
            excelQueryFactory.AddMapping<LoadDTO>(x => x.Subject, "Предмет");
            excelQueryFactory.AddMapping<LoadDTO>(x => x.Group, "Группа");
            excelQueryFactory.AddMapping<LoadDTO>(x => x.TotalHours, "Всего часов");
            excelQueryFactory.AddMapping<LoadDTO>(x => x.Weeks, "Недель");
            excelQueryFactory.AddMapping<LoadDTO>(x => x.HoursPerWeek, "Часов в неделю");
            excelQueryFactory.AddMapping<LoadDTO>(x => x.DaysOfPractice, "Кол-во недель практики");
            return excelQueryFactory;
        }

    }
}
