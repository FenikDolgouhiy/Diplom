using Dashboard1.Model;

using LinqToExcel;

using Microsoft.Win32;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard1.Utils
{
    public class DatabaseOperations
    {
        public async Task<List<LoadDTO>> ImportTeacherLoadsFromExcel()
        {
            return await Task.Run(() =>
            {
                List<LoadDTO> result = null;
                var dialog = new OpenFileDialog
                {
                    Filter = "Taблицы Excel'2007 (*.xlsx)|Таблицы Excel'97 (*.xls)|*.xls|*.xlsx|All files (*.*)|*.*"
                };
                if (dialog.ShowDialog() == true)
                {
                    using (var connection = InitiateLoadModelMappings(new ExcelQueryFactory(dialog.FileName)))
                    {
                        result = (from load in connection.Worksheet<LoadDTO>("Лист1") select load).ToList();
                    }
                }
                return result;
            });
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
