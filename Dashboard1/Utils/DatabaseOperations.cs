using Dashboard1.View;
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
        LoadOfTeachers lof = new LoadOfTeachers();
        public void ImportExelToDG()
        {
            
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Таблицы Excel'97 (*.xls)|*.xls|Taблицы Excel'2007 (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            opf.ShowDialog();
            DataTable tb = new DataTable();
            string filename = opf.FileName;
            if (filename == "")
                return;
            string ConStr = String.Format(
                            "Provider=Microsoft.ACE.OLEDB.12.0;extended properties=\"excel 8.0;\";data source={0}", filename);
            System.Data.DataSet ds = new System.Data.DataSet("EXCEL");
            OleDbConnection cn = new OleDbConnection(ConStr);
            cn.Open();
            DataTable schemaTable = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            string sheet1 = (string)schemaTable.Rows[0].ItemArray[2];
            string select = String.Format("SELECT * FROM [{0}]", sheet1);
            OleDbDataAdapter ad = new OleDbDataAdapter(select, cn);
            ad.Fill(ds);
            DataTable t = ds.Tables[0];
            cn.Close();
           
            this.lof.datagridTeachLoad.ItemsSource = t.DefaultView;
        }
       
    }

}
