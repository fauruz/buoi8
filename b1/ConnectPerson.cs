using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace b1
{
    internal class ConnectPerson
    {
        string constr = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        public bool addData(Person ps)
        {
            SqlConnection con = new SqlConnection(constr);
            con.Open();       
            try
            {
                string sql5 = "insert into dbo.Person values('" + ps.Id + "',N'" + ps.Name + "')";
                SqlCommand cmd5 = new SqlCommand(sql5, con);
                cmd5.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Duplicate Id !");
                return false;
            }
                
            //string sql = "select count(*) from Employee where EmpId = '" + ps.Id + "';";
            //SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.CommandType = CommandType.Text;
            //kt = (int)cmd.ExecuteScalar();
            //if (kt == 0)
            //{
            //    string sql2 = "insert into dbo.Employee values('" + emp.Id + "',N'" + emp.Name + "','" + emp.Gender + "','" + id + "')";
            //    SqlCommand cmd2 = new SqlCommand(sql2, con);
            //    cmd2.ExecuteNonQuery();
            //}
            con.Close();
            return true;
        }
        public string getRow()
        {
            string b = "";
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.CommandText = "select Name from Person";
                da.SelectCommand.Connection = con;
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    b += row["Name"].ToString();
                    b += "\n";
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return b;
        }
        public List<Person> getData()
        {
            List<Person> lps = new List<Person>();
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                da.SelectCommand = new SqlCommand();
                da.SelectCommand.CommandText = "select * from Person";
                da.SelectCommand.Connection = con;
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    Person ps = new Person();
                    ps.Id = Convert.ToInt32(row["Id"].ToString());
                    ps.Name = row["Name"].ToString();
                    lps.Add(ps);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return lps;
        }
        public DataTable getTable()
        {
            SqlConnection con = new SqlConnection(constr);
            SqlDataAdapter da;
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                string sql = "SELECT * from Person";
                da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
        public bool updateData(Person ps)
        {
            int kt = 0;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string sql = "select count(*) from Person where Id = " + ps.Id;
            SqlCommand cmd = new SqlCommand(sql, con);
            kt = (int)cmd.ExecuteScalar();
            if(kt == 0)
            {
                addData(ps);
            }
            else
            {
                try
                {
                    string sql1 = "Update Person" +
                        " Set Name = '" + ps.Name + "' where Id = " + ps.Id + ";";
                    SqlCommand cmd1 = new SqlCommand(sql1, con);
                    cmd1.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            con.Close() ;
            return true;
        }
        public bool deleteData(Person ps)
        {
            int kt = 0;
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            string sql = "select count(*) from Person where Id = " + ps.Id;
            SqlCommand cmd = new SqlCommand(sql, con);
            kt = (int)cmd.ExecuteScalar();
            if (kt == 0)
            {
                MessageBox.Show("Không tồn tại Id!");
                return false;
            }
            else
            {
                try
                {
                    string sql1 = "Delete From Person" +
                        " where Id = " + ps.Id + ";";
                    SqlCommand cmd1 = new SqlCommand(sql1, con);
                    cmd1.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            con.Close();
            return true;
        }
    }
}
