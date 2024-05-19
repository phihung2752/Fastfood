using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
public class DataConnection
{
    private readonly string conStr;

    public DataConnection()
    {
        conStr = "Data Source=DESKTOP-T51IFUH\\SQLEXPRESS;Initial Catalog=phihung;Integrated Security=True";
    }

    public SqlConnection GetConnection()
    {
        try
        {
            SqlConnection connection = new SqlConnection(conStr);
            return connection;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error creating SQL connection: " + ex.Message);
            throw;
        }
    }

    public void OpenConnection(SqlConnection connection)
    {
        try
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error opening SQL connection: " + ex.Message);
            throw;
        }
    }

    public void CloseConnection(SqlConnection connection)
    {
        try
        {
            if (connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error closing SQL connection: " + ex.Message);
            throw;
        }
    }
}
