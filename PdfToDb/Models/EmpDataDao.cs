using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PdfToDb.Models
{
    public class Common
    {
        public static readonly string CnStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Emp.mdf;Integrated Security=True";
    }
    public class EmpDataDao
    {
        public static bool Add(EmpDataViewModel model)
        {
            using(var cn=new SqlConnection(Common.CnStr))
            {
                using(var cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "insert into EmpData values (@Code,@Name,@WorkHours,@OverTime)";
                    cmd.Parameters.AddWithValue("@Code", model.Code);
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@WorkHours", model.WorkHours);
                    cmd.Parameters.AddWithValue("@OverTime", model.OverTime);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public static List<EmpDataViewModel> Gets()
        {
            var list = new List<EmpDataViewModel>();
            using(var cn=new SqlConnection(Common.CnStr))
            {
                using(var cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "select * from EmpData";
                    cn.Open();
                    var read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        list.Add(new EmpDataViewModel
                        {
                            Code = read.GetInt32(0),
                            Name = read.GetString(1),
                            WorkHours = read.GetString(2),
                            OverTime = read.GetString(3)
                        });
                    }
                }
            }
            return list;
        }
    }
}