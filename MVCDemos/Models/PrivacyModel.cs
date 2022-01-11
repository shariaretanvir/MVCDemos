using System.Data.SqlClient;

namespace MVCDemos.Models
{
    public class PrivacyModel
    {
        public string  name { get; set; }
        public string address { get; set; }



        public static void Save(byte[] files)
        {
            using (SqlConnection con = new SqlConnection("Server=DESKTOP-C01O214\\AKASHSQLEXPRESS;Database=TestDB;Trusted_Connection=True;"))
            {
                //string query = "insert into tblFiles values (@Name, @ContentType, @Data)";
                using (SqlCommand cmd = new SqlCommand("insfile"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Files", files);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }


        public static byte[] GetData()
        {
            byte[] data = null;
            using (SqlConnection con = new SqlConnection("Server=DESKTOP-C01O214\\AKASHSQLEXPRESS;Database=TestDB;Trusted_Connection=True;"))
            {
                string query = "select Files from TableWithFile where ID=1";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@Files", files);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        data = (byte[])sdr["Files"];
                        //contentType = sdr["ContentType"].ToString();
                        //fileName = sdr["Name"].ToString();
                    }
                    con.Close();
                }
            }
            return data;
        }
    }
}
