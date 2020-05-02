using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class DatabaseOperations
    {
        public async Task<List<LoadDTO>> ImportTeacherLoadsFromExcel(string path)
        {
            return await Task.Run(() =>
            {
                List<LoadDTO> result = null;

                using (var connection = InitiateLoadModelMappings(new ExcelQueryFactory(path)))
                {
                    result = (from load in connection.Worksheet<LoadDTO>("Лист1") select load).ToList();
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
