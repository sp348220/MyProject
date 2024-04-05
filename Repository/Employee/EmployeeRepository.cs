using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DomainObject.Employee;

namespace Repository.Employee
{
    internal class EmployeeRepository
    {
    }
    public class GetEmployeeObjectFactory
    {
        public List<EmployeeDto> getEmplyoyeeMasterData()
        {
            List<EmployeeDto> employeeList = new List<EmployeeDto>();
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-4J6IQKA\\SQLEXPRESS01;Initial Catalog=dmsdb;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("spEmployeeMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operationtype", "SELECT");
            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                EmployeeDto emp = new EmployeeDto();
                emp.Id = (int)dr["Id"];
                emp.Name = dr["Name"].ToString();
                emp.Email = dr["Email"].ToString();
                emp.Phone = dr["Phone"].ToString();
                emp.Address = dr["Address"].ToString();
                emp.City = dr["City"].ToString();
                emp.Region = dr["Region"].ToString();
                emp.PostalCode = dr["PostalCode"].ToString();
                emp.Country = dr["Country"].ToString();
                emp.CreatedDate = (DateTime)dr["CreatedDate"];
                emp.IsActive = (Boolean)dr["IsActive"];
                employeeList.Add(emp);


            }
            return employeeList;
            con.Close();
        }
        public bool AddEmployeeMasterData(EmployeeDto emp)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-4J6IQKA\\SQLEXPRESS01;Initial Catalog=dmsdb;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("spEmployeeMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operationtype", "CREATE");
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Email", emp.Email);
            cmd.Parameters.AddWithValue("@Adress", emp.Address);
            cmd.Parameters.AddWithValue("@Phone", emp.Phone);
            cmd.Parameters.AddWithValue("@City", emp.City);
            cmd.Parameters.AddWithValue("@Country", emp.Country);
            cmd.Parameters.AddWithValue("@Region", emp.Region);
            cmd.Parameters.AddWithValue("@PostalCode", emp.PostalCode);
            cmd.Parameters.AddWithValue("@CreatedDate", emp.CreatedDate);
            cmd.Parameters.AddWithValue("@IsActive", emp.IsActive);
            cmd.Parameters.AddWithValue("@IsDeleted", "0");
            con.Open();
            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                return true;
            }
            else { return false; }
            con.Close();
        }
        public bool UpdateEmployeeMasterData(EmployeeDto emp)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-4J6IQKA\\SQLEXPRESS01;Initial Catalog=dmsdb;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("spEmployeeMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@operationtype", "UPDATE");
            cmd.Parameters.AddWithValue("@Id", emp.Id);
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Email", emp.Email);
            cmd.Parameters.AddWithValue("@Adress", emp.Address);
             cmd.Parameters.AddWithValue("@Phone", emp.Phone);
            cmd.Parameters.AddWithValue("@City", emp.City);
            cmd.Parameters.AddWithValue("@Country", emp.Country);
             cmd.Parameters.AddWithValue("@Region", emp.Region);
            cmd.Parameters.AddWithValue("@PostalCode", emp.PostalCode);
            cmd.Parameters.AddWithValue("@CreatedDate", emp.CreatedDate);
            cmd.Parameters.AddWithValue("@IsActive", emp.IsActive);
            // cmd.Parameters.AddWithValue("@IsDeleted", "0");
            con.Open();
            int i = cmd.ExecuteNonQuery();

            if (i > 0)
            {
                return true;
            }
            else { return false; }
            con.Close();
        }
    }


    }
