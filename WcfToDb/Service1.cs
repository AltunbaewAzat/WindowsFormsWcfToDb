using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfToDb
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class Service1 : IService1
    {
        public Service1()
        {
            ConnectToDb();
        }

        SqlConnection conn;
        SqlCommand comm;

        SqlConnectionStringBuilder connStringBuilder;

        public void ConnectToDb()
        {
            connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = @"(localdb)\MSSQLLocalDB";
            connStringBuilder.InitialCatalog = "WCF";
            //connStringBuilder.Encrypt = true;
            connStringBuilder.TrustServerCertificate = true;
            connStringBuilder.ConnectTimeout = 30;
            connStringBuilder.AsynchronousProcessing = true;
            connStringBuilder.MultipleActiveResultSets = true;
            connStringBuilder.IntegratedSecurity = true;

            conn = new SqlConnection(connStringBuilder.ToString());
            comm = conn.CreateCommand();
        }
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public int InsertPerson(Person p)
        {
            try
            {
                comm.CommandText = "INSERT INTO TPerson VALUES(@Id, @Name, @Age)";
                comm.Parameters.AddWithValue("Id", p.Id);
                comm.Parameters.AddWithValue("Name", p.Name);
                comm.Parameters.AddWithValue("Age", p.Age);

                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public int UpdatePerson(Person p)
        {
            try
            {
                comm.CommandText = "UPDATE TPerson SET Name = @Name, Age = @Age WHERE Id = @Id";
                comm.Parameters.AddWithValue("Id", p.Id);
                comm.Parameters.AddWithValue("Name", p.Name);
                comm.Parameters.AddWithValue("Age", p.Age);

                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();                
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public int DeletePerson(Person p)
        {
            try
            {
                comm.CommandText = "DELETE TPerson WHERE Id = @Id";
                comm.Parameters.AddWithValue("Id", p.Id);

                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public Person GetPerson(Person p)
        {
            Person person = new Person();
            try
            {
                comm.CommandText = "SELECT * FROM TPerson WHERE Id = @Id";
                comm.Parameters.AddWithValue("Id", p.Id);

                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    person.Id = Convert.ToInt32(reader[0]);
                    person.Name = reader[1].ToString();
                    person.Age = Convert.ToInt32(reader[2]);
                }
                return person;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
