using iBizSMSV1R1.Data;
using iBizSMSV1R1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;



public class myGlobalFunctions
{
    static SqlConnection cnn = new SqlConnection();   
    static SqlCommand cmd = new SqlCommand();
    static DataSet ds = new DataSet();
    static SqlDataAdapter da = new SqlDataAdapter();
    static SqlDataReader dr;

    public static string sql { get; set; }

    //static string sqlconnectionString = "Data Source=DESKTOP-7M2L3JV\\mssqlserveribiz;Initial Catalog = PeregrineDB2; User Id = sa; Password=P@ssw0rd;MultipleActiveResultSets=true;";
    static string sqlconnectionString = "Data Source=sql5063.site4now.net;Initial Catalog = db_a79181_peregrinedb; User Id = db_a79181_peregrinedb_admin; Password=P3r3grin3P@55;MultipleActiveResultSets=true;";    
    public static string InsertDeleteUpdate(string sqlquery)
    {
        string result ="";
        try
        {
            cnn = new SqlConnection(sqlconnectionString);
            cnn.Open();
            cmd.Connection = cnn;
            cmd.CommandText = sqlquery;
            cmd.ExecuteNonQuery();
            cnn.Close();
            cnn.Dispose();
            result = "Updated";

            return result;
        }
        catch (Exception ex)
        {
            if (cnn.State.ToString().Equals("Open"))
            {
                cnn.Close();
                cnn.Dispose();
            }

            return result;
        }

        
    }      

}
