using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSIGNMENT3.Models;
using Microsoft.Data.SqlClient;


namespace ASSIGNMENT3.DBHandler
{
    /// <summary>
    /// Handle users table in Database
    /// </summary>
    public class UserDBHandler : BaseDBHandler
    {


        /// <summary>
        /// Contructor
        /// </summary>
        public UserDBHandler()
        {
            AddAdminToDB();
        }

        /// <summary>
        /// Check whether User exist in Database or not
        /// </summary>
        /// <param name="uname">string</param>
        /// <param name="pswd">string</param>
        /// <returns>True/False</returns>
        /// 
        public bool IsUserExist(string uname, string pswd)
        {
            try
            {
                String query = $"select * from users where username = @u and password=@p";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@u", uname.Trim()));
                cmd.Parameters.Add(new SqlParameter("@p", pswd.Trim()));
                con.Open();
                dr = cmd.ExecuteReader();
                bool flag = dr.HasRows;
                dr.Close();
                con.Close();
                return flag;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

        /// <summary>
        /// Add Admin to Database if not exist
        /// </summary>
        private void AddAdminToDB()
        {
            if (!IsUserExist("iamadmin", "iamadmin"))
            {
                AddUserToDB("iamadmin", "iamadmin", "admin@gmail.com");
            }
        }

        /// <summary>
        /// Check whether username exist in Database or not
        /// </summary>
        /// <param name="uname">string</param>
        /// <returns>True/False</returns>
        public bool IsUsernameExist(string uname)
        {
            try
            {
                String query = $"select * from users where username = @u";
                //String query = $"select * from user";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@u", uname.Trim()));
                con.Open();
                dr = cmd.ExecuteReader();
                bool flag = dr.HasRows;
                dr.Close();
                con.Close();
                return flag;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Check whether username exist in Database or not except specific UserID
        /// </summary>
        /// <param name="uname">string</param>
        /// <param name="id">int</param>
        /// <returns>True/False</returns>
        public bool IsUsernameExist(string uname, int id)
        {
            try
            {
                String query = $"select * from users where username = @u and userid != @uid";
                //String query = $"select * from user";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@u", uname.Trim()));
                cmd.Parameters.Add(new SqlParameter("@uid", id));
                con.Open();
                dr = cmd.ExecuteReader();
                bool flag = dr.HasRows;
                dr.Close();
                con.Close();
                return flag;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Return specific user from Database
        /// </summary>
        /// <param name="uname">string</param>
        /// <param name="pswd">string</param>
        /// /// <returns></returns>
        public User GetUserFromDB(string uname, string pswd)
        {
            try
            {
                User user = null;
                String query = $"select * from users where username = @u and password=@p";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@u", uname.Trim()));
                cmd.Parameters.Add(new SqlParameter("@p", pswd));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    user = new User { UserID = dr.GetInt32(0), Username = dr.GetString(1).Trim(), Password = dr.GetString(2).Trim(), Email = dr.GetString(3).Trim(), About = dr.GetString(4).Trim() };
                }
                dr.Close();
                con.Close();
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Update user in Database
        /// </summary>
        /// <param name="user">User</param>
        /// /// <returns></returns>
        public bool UpdateUserInDB(User user)
        {
            try
            {
                String query = $"update users set username = @u,  password = @p, email = @em, about = @ab where userid = @uid";
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@u", user.Username.Trim()));
                cmd.Parameters.Add(new SqlParameter("@p", user.Password.Trim()));
                cmd.Parameters.Add(new SqlParameter("@em", user.Email.Trim()));
                cmd.Parameters.Add(new SqlParameter("@ab", user.About.Trim()));
                cmd.Parameters.Add(new SqlParameter("@uid", user.UserID));
                int rowsDeleted = cmd.ExecuteNonQuery();
                con.Close();
                return rowsDeleted > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Return specific user from Database
        /// </summary>
        /// <param name="uid">int</param>
        /// /// <returns></returns>
        public User GetUserFromDB(int uid)
        {
            try
            {
                User user = null;
                String query = $"select * from users where userid = @uid";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@uid", uid));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    user = new User { UserID = dr.GetInt32(0), Username = dr.GetString(1).Trim(), Password = dr.GetString(2).Trim(), Email = dr.GetString(3).Trim(), About = dr.GetString(4).Trim() };
                }
                dr.Close();
                con.Close();
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        /// <summary>
        /// Check whether email exist in Database or not
        /// </summary>
        /// <param name="email">string</param>
        /// <returns>True/False</returns>
        public bool IsEmailExist(string email)
        {
            try
            {
                String query = $"select * from users where email = @em";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@em", email.Trim()));
                con.Open();
                dr = cmd.ExecuteReader();
                bool flag = dr.HasRows;
                dr.Close();
                con.Close();
                return flag;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Check whether email exist in Database or not except specific id
        /// </summary>
        /// <param name="email">string</param>
        /// <param name="id">int</param>
        /// <returns>True/False</returns>
        public bool IsEmailExist(string email, int id)
        {
            try
            {
                String query = $"select * from users where email = @em and userid != @uid";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@em", email.Trim()));
                cmd.Parameters.Add(new SqlParameter("@uid", id));
                con.Open();
                dr = cmd.ExecuteReader();
                bool flag = dr.HasRows;
                dr.Close();
                con.Close();
                return flag;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Get All Users from database
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsersFromDB()
        {
            try
            {
                List<User> list = new List<User>();
                String query = "select * from users";
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr.GetString(1).Trim() != "iamadmin")
                        {
                            list.Add(new User { UserID = dr.GetInt32(0), Username = dr.GetString(1).Trim(), Password = dr.GetString(2).Trim(), Email = dr.GetString(3).Trim(), About = dr.GetString(4).Trim() });
                        }
                    }
                }
                con.Close();
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Get Password of specific user
        /// </summary>
        /// <param name="uname">string</param>
        /// <param name="email">string</param>
        /// <returns>True/False</returns>
        public string GetPassword(string uname, string email)
        {
            try
            {
                String query = $"select * from users where username = @u and email = @em";
                String pswd = null;
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@u", uname.Trim()));
                cmd.Parameters.Add(new SqlParameter("@em", email.Trim()));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    pswd = dr.GetString(2).Trim();
                }
                dr.Close();
                con.Close();
                return pswd;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }



        /// <summary>
        /// Add user to Database
        /// </summary>
        /// <param name="uname">string</param>
        /// <param name="pswd">string</param>
        /// <param name="email">string</param>
        /// <returns>True/False</returns>
        public bool AddUserToDB(string uname, string pswd, string email)
        {
            try
            {
                if (IsUsernameExist(uname))
                {
                    return false;
                }
                String query = $"insert into users (username,password,email,about) values(@u, @p, @em, @ab)";
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@u", uname.Trim()));
                cmd.Parameters.Add(new SqlParameter("@p", pswd.Trim()));
                cmd.Parameters.Add(new SqlParameter("@em", email.Trim()));
                cmd.Parameters.Add(new SqlParameter("@ab", "My Name is " + uname.Trim() + " and I am a user of BlogLand. I love to use BlogLand"));
                int rowsInserted = cmd.ExecuteNonQuery();
                con.Close();
                return rowsInserted > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Update username in Database
        /// </summary>
        /// <param name="user">User</param>
        /// /// <returns></returns>
        public bool UpdateUsernameInDB(User user)
        {
            try
            {
                String query = $"update users set username = @u where userid = @uid";
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@u", user.Username.Trim()));
                cmd.Parameters.Add(new SqlParameter("@uid", user.UserID));
                int rowsDeleted = cmd.ExecuteNonQuery();
                con.Close();
                return rowsDeleted > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Update passowrd in Database
        /// </summary>
        /// <param name="user">User</param>
        /// /// <returns></returns>
        public bool UpdatePasswordInDB(User user)
        {
            try
            {
                String query = $"update users set password = @p where userid = @uid";
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@p", user.Password.Trim()));
                cmd.Parameters.Add(new SqlParameter("@uid", user.UserID));
                int rowsDeleted = cmd.ExecuteNonQuery();
                con.Close();
                return rowsDeleted > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Update email in Database
        /// </summary>
        /// <param name="user">User</param>
        /// /// <returns></returns>
        public bool UpdateEmailInDB(User user)
        {
            try
            {
                String query = $"update users set email = @em where userid = @uid";
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@em", user.Email.Trim()));
                cmd.Parameters.Add(new SqlParameter("@uid", user.UserID));
                int rowsDeleted = cmd.ExecuteNonQuery();
                con.Close();
                return rowsDeleted > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Delete user from Database
        /// </summary>
        /// <param name="uid">int</param>
        /// /// <returns></returns>
        public bool DeleteUserFromDB(int uid)
        {
            try
            {
                String query = $"delete from users where userid = @uid";
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@uid", uid));
                int rowsDeleted = cmd.ExecuteNonQuery();
                con.Close();
                return rowsDeleted > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Update about in Database
        /// </summary>
        /// <param name="user">User</param>
        /// /// <returns></returns>
        public bool UpdateAboutInDB(User user)
        {
            try
            {
                String query = $"update users set about = @ab where userid = @uid";
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@ab", user.About.Trim()));
                cmd.Parameters.Add(new SqlParameter("@uid", user.UserID));
                int rowsDeleted = cmd.ExecuteNonQuery();
                con.Close();
                return rowsDeleted > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}



//CREATE TABLE[dbo].[users]
//(

//   [userid] INT NOT NULL PRIMARY KEY IDENTITY,

//   [username] NCHAR(20) NOT NULL,

//   [password] NCHAR(20) NOT NULL,

//   [email] NCHAR(50) NOT NULL,

//   [about] NTEXT NOT NULL
//)
