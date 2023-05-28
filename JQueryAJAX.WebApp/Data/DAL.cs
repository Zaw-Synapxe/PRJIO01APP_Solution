using System.Data;
using System.Data.SqlClient;
using JQueryAJAX.WebApp.Models;

namespace JQueryAJAX.WebApp.Data
{
    public class DAL
    {
        public Response Registration(Users users, SqlConnection connection)
        {
            Response response = new Response();

            try
            {
                SqlCommand cmd = new SqlCommand("sp_register", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", users.Username);
                cmd.Parameters.AddWithValue("@Email", users.Email);
                cmd.Parameters.AddWithValue("@Password", users.Password);

                cmd.Parameters.Add("@ErrorMessage", SqlDbType.Char, 200);
                cmd.Parameters["@ErrorMessage"].Direction = ParameterDirection.Output;

                connection.Open();
                int i = cmd.ExecuteNonQuery();
                string message = (string)cmd.Parameters["@ErrorMessage"].Value;

                connection.Close();

                if (i > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = message;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = message;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;

            }

            return response;

        }

        public Response Login(Users users, SqlConnection connection)
        {
            Response response = new Response();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("sp_login", connection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
                da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "User is Valid !";
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "User is Invalid !";
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = 100;
                response.StatusMessage = ex.Message;
            }

            return response;
        }

        //
    }
}
