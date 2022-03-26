using System;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Web;

namespace OnlyImageTest
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        
        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpPostedFile postedfile = FileUpload1.PostedFile;

            Stream stream = postedfile.InputStream;
            BinaryReader binaryReader = new BinaryReader(stream);
            byte[] bytes = binaryReader.ReadBytes((int)stream.Length);


            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con =new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Image] ([Image]) VALUES ('@Img');", con);

                
                SqlParameter paramImg = new SqlParameter()
                {
                    ParameterName="@Img",
                    Value= bytes
                };
                cmd.Parameters.Add(paramImg);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
    }
}