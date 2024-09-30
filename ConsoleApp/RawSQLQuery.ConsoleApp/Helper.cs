using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RawSQLQuery.ConsoleApp
{
    public static class Helper
    {
        public static string connectionString = "Server=DESKTOP-99Q87I2\\MSSQLSERVER2017;Database=BusinessERP;User ID=sa;Password=dev123456;MultipleActiveResultSets=true";

        public static List<Department> GetAllDepartment(SqlConnection connection)
        {
            try
            {
                List<Department> lsitDepartment = new();
                string query = "SELECT * FROM Department";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader _SqlDataReader = command.ExecuteReader())
                    {
                        lsitDepartment = DataReaderMapToList<Department>(_SqlDataReader);
                    }
                }

                return lsitDepartment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<T> DataReaderMapToList<T>(IDataReader _IDataReader)
        {
            List<T> list = new List<T>();
            T _Objetc = default(T);
            while (_IDataReader.Read())
            {
                _Objetc = Activator.CreateInstance<T>();
                foreach (PropertyInfo _PropertyInfo in _Objetc.GetType().GetProperties())
                {
                    if (!object.Equals(_IDataReader[_PropertyInfo.Name], DBNull.Value))
                    {
                        _PropertyInfo.SetValue(_Objetc, _IDataReader[_PropertyInfo.Name], null);
                    }
                }
                list.Add(_Objetc);
            }
            return list;
        }


        //
    }
}
