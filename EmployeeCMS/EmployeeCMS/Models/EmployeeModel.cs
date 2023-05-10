using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCMS.Models
{
    public class EmployeeModel
    {
        SqlConnection con = new SqlConnection(@"Data Source=ASUS-LAPTOP\SQLEXPRESS01;Initial Catalog=emp;Integrated Security=True");
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Department")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Please Enter Salary")]

        public int Salary { get; set; }

        public List<EmployeeModel> getData()
        {
            List<EmployeeModel> lstEmp = new List<EmployeeModel>();
            SqlDataAdapter apt = new SqlDataAdapter("select * from emp", con);
            DataSet ds = new DataSet();
            apt.Fill(ds);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                lstEmp.Add(new EmployeeModel
                {
                    Id = Convert.ToInt32(dr["id"].ToString()),
                    Name = dr["name"].ToString(),
                    Department = dr["dept"].ToString(),
                    Salary =Convert.ToInt32(dr["salary"].ToString())
                });
            }
            return lstEmp;
        }
        public EmployeeModel getData(string Id)
        {
            EmployeeModel emp = new EmployeeModel();
            SqlCommand cmd = new SqlCommand("select * from emp where id='" + Id +
           "'", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    emp.Id = Convert.ToInt32(dr["Id"].ToString());
                    emp.Name = dr["Name"].ToString();
                    emp.Department = dr["Dept"].ToString();
                    emp.Salary = Convert.ToInt32(dr["Salary"].ToString());
                }
            }
            con.Close();
            return emp;
        }
        //Insert a record into a database table
        public bool insert(EmployeeModel Emp)
        {
            SqlCommand cmd = new SqlCommand("insert into emp values(@name, @dept, @salary)", con);
           
            cmd.Parameters.AddWithValue("@name", Emp.Name);
            cmd.Parameters.AddWithValue("@dept", Emp.Department);
            cmd.Parameters.AddWithValue("@salary", Emp.Salary);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        public bool update(EmployeeModel Emp)
        {
            SqlCommand cmd = new SqlCommand("update emp set Name=@name,Dept = @dept, Salary = @salary where Id = @id", con);
           
            cmd.Parameters.AddWithValue("@name", Emp.Name);
            cmd.Parameters.AddWithValue("@dept", Emp.Department);
            cmd.Parameters.AddWithValue("@salary", Emp.Salary);
            cmd.Parameters.AddWithValue("@id", Emp.Id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        public bool delete(EmployeeModel Emp)
        {
            SqlCommand cmd = new SqlCommand("delete emp where Id = @id", con);
            cmd.Parameters.AddWithValue("@id", Emp.Id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }

    }
}