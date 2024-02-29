using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Modul_16_ADO_Console_Lessons
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string s = @"Data Source=(localdb)\MSSQLLocalDB;
                        Initial Catalog=MyFirstSQL_2;
                        Integrated Security=True;
                        Pooling=False;
                        Encrypt=True;
                        Trust Server Certificate=False";

            SqlConnectionStringBuilder strCon = new SqlConnectionStringBuilder()
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = @"MyFirstSQL_2",
                IntegratedSecurity = true,
                Pooling = false,
                //UserID = "Admin",
                //Password = "Admin"

            };
            Console.WriteLine(strCon.ConnectionString);
            SqlConnection sqlConnection = new SqlConnection() { ConnectionString = strCon.ConnectionString};

            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            } 

            try
            {
                using (var connection = new SqlConnection(sqlConnection.ConnectionString))
                {   
                    connection.Open();  

                    var sqlString = @"Select * from Bosses, Workers, [Description]";
                    
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["WorkerName"]} {reader[0]} {reader.GetInt32(0)}");
                    }
                    connection.Close();
                    connection.Open();

                    var sqlRec = @"Insert into Workers ( [workerName], [idBoss], [idDescriptions]) VALUES ('SomeName', 5, 5 )";
                    SqlCommand RecordCommand = new SqlCommand(sqlRec, connection);
                    RecordCommand.ExecuteNonQuery();

                }
            }
            catch ( Exception msgE)
            {
                Console.WriteLine(msgE.Message);
            }
        }       
    }
}
