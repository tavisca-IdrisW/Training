using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.EmployeeManagement.Interface;
using Newtonsoft.Json;
using Tavisca.EmployeeManagement.ErrorSpace;
using Tavisca.EmployeeManagement.EnterpriseLibrary;
using System.Data.SqlClient;
using System.Data;

namespace Tavisca.EmployeeManagement.FileStorage
{
    public class EmployeeStorage : IEmployeeStorage
    {
        readonly string EXTENSION = ".emp";

        public Model.Employee Save(Model.Employee employee)
        {
            try
            {

                SqlConnection con = new SqlConnection("Data Source=Training10;Initial Catalog=Employeedb;Persist Security Info=True;User ID=sa;Password=test123!@#");
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter p2 = new SqlParameter("FirstName", employee.FirstName);
                SqlParameter p3 = new SqlParameter("LastName", employee.LastName);
                SqlParameter p4 = new SqlParameter("title", employee.Title);
                SqlParameter p5 = new SqlParameter("Email", employee.Email);
                SqlParameter p6 = new SqlParameter("phno", employee.Phone);
                SqlParameter p7 = new SqlParameter("Doj", employee.JoiningDate.ToString());
                SqlParameter p8 = new SqlParameter("Password", "test123!@#");
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);
                cmd.Parameters.Add(p8);

                cmd.ExecuteNonQuery();
                con.Close();

                return employee;
            }
            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("data.policy", ex);
                if (rethrow) throw;
                return null;
            }
        }

        public Model.Employee Get(string employeeId)
        {
           try
            {
                int flag = 0;
                Model.Employee emp = new Model.Employee();
                List<Model.Remark> tempRemark = new List<Model.Remark>();
                SqlConnection con = new SqlConnection("Data Source=Training9;Initial Catalog=Employee;Persist Security Info=True;User ID=sa;Password=test123!@#");
                SqlConnection conRemark = new SqlConnection("Data Source=Training9;Initial Catalog=Employee;Persist Security Info=True;User ID=sa;Password=test123!@#");
                con.Open();
                conRemark.Open();
                SqlCommand cmd = new SqlCommand("GetbyId", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Id", employeeId));

                SqlDataReader empReader = cmd.ExecuteReader();
                while (empReader.Read())
                {
                    emp.Id = empReader[0].ToString();
                    emp.FirstName = empReader[1].ToString();
                    emp.LastName = empReader[2].ToString();
                    emp.Title = empReader[3].ToString();
                    emp.Email = empReader[4].ToString();
                    emp.Phone = empReader[5].ToString();
                    emp.JoiningDate = DateTime.Parse(empReader[6].ToString());

                    SqlCommand cmdRemark = new SqlCommand("GetRemarkById",conRemark);
                    cmdRemark.CommandType = CommandType.StoredProcedure;
                    cmdRemark.Parameters.Add(new SqlParameter("@Id", employeeId));
                    SqlDataReader cmdRemarkReader = cmdRemark.ExecuteReader();
                    while (cmdRemarkReader.Read())
                    {
                        flag = 1;
                        Model.Remark remark = new Model.Remark();
                        remark.Text = cmdRemarkReader[2].ToString();
                        remark.CreateTimeStamp = Convert.ToDateTime(cmdRemarkReader[3]);
                        tempRemark.Add(remark);
                    }
                }
               if(flag==1)emp.Remarks = tempRemark;
               else
               {
                   Model.Remark remark = new Model.Remark();
                   remark.Text = "";
                   remark.CreateTimeStamp = DateTime.UtcNow;
                   tempRemark.Add(remark);
                   emp.Remarks = tempRemark;
               }
                con.Close();
                conRemark.Close();
                return emp;
            }


            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("data.policy", ex);
                if (rethrow) throw;
                return null;
            }
        
            
        }

        public List<Model.Employee> GetAll()
        {
          
                var employees = new List<Model.Employee>();
                List<Model.Employee> empList = new List<Model.Employee>();
                Model.Employee employee = new Model.Employee();
                Model.Remark remark = new Model.Remark();
                SqlConnection connection = new SqlConnection("Data Source=Training10;Initial Catalog=Employee;Persist Security Info=True;User ID=sa;Password=test123!@#");
                connection.Open();
                SqlCommand cmd = new SqlCommand("GetAll ", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader empReader = cmd.ExecuteReader();

            while (empReader.Read())
            {
                Model.Employee emp = new Model.Employee();
                emp.Id = empReader[0].ToString();
                emp.FirstName = empReader[1].ToString();
                emp.LastName = empReader[2].ToString();
                emp.Title = empReader[3].ToString();
                emp.Email = empReader[4].ToString();
                emp.Phone = empReader[5].ToString();
                emp.JoiningDate = DateTime.Parse(empReader[6].ToString());
                empList.Add(emp);
            }
            connection.Close();
            return empList;

        }

  

        public Model.Remark SaveRemark(string employeeId, Model.Remark remark)
        {
            try
            {
                SqlConnection connection = new SqlConnection("Data Source=Training10;Initial Catalog=Employee;Persist Security Info=True;User ID=sa;Password=test123!@#");
                connection.Open();
                SqlCommand cmdRemark = new SqlCommand("AddRemark", connection);
                cmdRemark.CommandType = CommandType.StoredProcedure;
                cmdRemark.Parameters.Add(new SqlParameter("@EmpId", employeeId));
                cmdRemark.Parameters.Add(new SqlParameter("@Remark", remark.Text));
                cmdRemark.Parameters.Add(new SqlParameter("@RemarkTime", remark.CreateTimeStamp));
                cmdRemark.ExecuteNonQuery();
                connection.Close();
                return remark;
            }
            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("data.policy", ex);
                if (rethrow) throw;
                return null;
            }
        }
   

        public Model.Employee Authenticate(string emailId, string password)
        {
            try
            {

                Model.Employee employee = new Model.Employee();
                SqlConnection connection = new SqlConnection("Data Source=Training10;Initial Catalog=Employee;Persist Security Info=True;User ID=sa;Password=test123!@#");
                connection.Open();
                SqlCommand cmd = new SqlCommand("select EmpId,Password from EmployeeDetails where Email='" + emailId + "'", connection);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    if (password.Equals(dr[1].ToString()) == false)
                    {
                    }
                       
                    else 
                    { 
                        employee=new EmployeeStorage().Get(dr[0].ToString());
                    
                    }
                }
                connection.Close();
                return employee;
            }
            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("data.policy", ex);
                if (rethrow) throw;
                return null;
            }
        }

        public int ChangePassword(string emailId,string oldPassword,string newPassword)
        {
            try
            {

                Model.Employee employee = new Model.Employee();
                SqlConnection connection = new SqlConnection("Data Source=Training10;Initial Catalog=Employeedb;Persist Security Info=True;User ID=sa;Password=test123!@#");
                connection.Open();
                SqlCommand cmd = new SqlCommand("select Password from EmployeeDetails where Email='" + emailId + "'", connection);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    if (oldPassword.Equals(dr[0].ToString()))
                    {
                        SqlConnection connection2 = new SqlConnection("Data Source=Training10;Initial Catalog=Employee;Persist Security Info=True;User ID=sa;Password=test123!@#");
                        connection2.Open();
                        SqlCommand cmd2 = new SqlCommand("Update EmployeeDetails set Password ='" + newPassword + "' where Email='" + emailId + "'", connection2);
                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        return 1;
                    }
             
                }
                connection.Close();
                return 0;
            }
            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("data.policy", ex);
                if (rethrow) throw;
                return 0;
            }
        }


        public List<Model.Remark> GetRemarksById(string employeeId, string pageNumber)
        {
            try
            {
                var remarks = new List<Model.Remark>();
                SqlConnection connection = new SqlConnection("Data Source=Training10;Initial Catalog=Employeedb;Persist Security Info=True;User ID=sa;Password=test123!@#");
                connection.Open();
                SqlCommand cmd = new SqlCommand("GetRemarksByIdPagenated", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", employeeId));
                cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                SqlDataReader remarksReader = cmd.ExecuteReader();

                while (remarksReader.Read())
                {
                    Model.Remark remark = new Model.Remark();
                    remark.Text = remarksReader[3].ToString();
                    remark.CreateTimeStamp = DateTime.Parse(remarksReader[4].ToString());
                    remarks.Add(remark);
                }
                connection.Close();
                return remarks;
            }
            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("data.policy", ex);
                if (rethrow) throw;
                return null;
            }

        }


        public Int32 GetRemarkCount(string employeeId)
        {
            try
            {
                int count=0;
                SqlConnection connection = new SqlConnection("Data Source=Training10;Initial Catalog=Employeedb;Persist Security Info=True;User ID=sa;Password=test123!@#");
                connection.Open();
                SqlCommand cmd = new SqlCommand("CountRemark", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", employeeId));
                SqlDataReader remarksCountReader = cmd.ExecuteReader();

                while (remarksCountReader.Read())
                {
                    count = Int32.Parse(remarksCountReader[0].ToString());
                   
                }
                connection.Close();
                return count;
            }
            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("data.policy", ex);
                if (rethrow) throw;
                return -1;
            }
        }
    }
}
