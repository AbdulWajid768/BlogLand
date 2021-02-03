using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSIGNMENT3.Models;
using Microsoft.Data.SqlClient;


namespace ASSIGNMENT3.DBHandler
{
    /// <summary>
    /// Handle posts table in Database
    /// </summary>
    public class PostDBHandler : BaseDBHandler
    {
        /// <summary>
        /// Return all the potss from Database
        /// </summary>
        /// <returns>List<Product></returns>
        public List<Post> GetAllPostsFromDB()
        {
            try
            {
                List<Post> list = new List<Post>();
                String query = "select * from posts";
                con.Open();
                cmd = new SqlCommand(query, con);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        list.Add(new Post { PostID = dr.GetInt32(0), UserID = dr.GetInt32(1), Username = dr.GetString(2).Trim(), Title = dr.GetString(3).Trim(), Content = dr.GetString(4).Trim(), CurrentDateTime = dr.GetDateTime(5) });
                    }
                }
                con.Close();
                list.Reverse();
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Check whether post id exist in Database or not
        /// </summary>
        /// <param name="pid">int</param>
        /// <returns>True/False</returns>
        public bool IsPostIDExist(int pid)
        {
            try
            {
                String query = $"select * from posts where postid = @pid";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@pid", pid));
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
        /// Delete post from Database
        /// </summary>
        /// <param name="pid">int</param>
        /// <returns>True/False</returns>
        public bool DeletePostFromDB(int pid)
        {
            try
            {
                String query = $"delete from posts where postid = @pid";
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@pid", pid));
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
        /// Delete post from Database on bases of UserID
        /// </summary>
        /// <param name="uid">int</param>
        /// <returns>True/False</returns>
        public bool DeletePostByUserID(int uid)
        {
            try
            {
                String query = $"delete from posts where userid = @uid";
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
        /// Add post to the Database
        /// </summary>
        /// <param name="post">Post</param>
        /// <returns>True/False</returns>
        public bool AddPostToDB(Post post)
        {
            try
            {
                String query = $"insert into posts (userid, username, title, content, curdatetime) values(@uid, @un, @t, @c, @dt)";
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@uid", post.UserID));
                cmd.Parameters.Add(new SqlParameter("@un", post.Username.Trim()));
                cmd.Parameters.Add(new SqlParameter("@t", post.Title.Trim()));
                cmd.Parameters.Add(new SqlParameter("@c", post.Content.Trim()));
                cmd.Parameters.Add(new SqlParameter("@dt", post.CurrentDateTime));
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
        /// Return specific post from Database
        /// </summary>
        /// <param name="pid">int</param>
        /// <returns></returns>
        public Post GetPostFromDB(int pid)
        {
            try
            {
                Post post = null;
                String query = $"select * from posts where postid = @pid";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@pid", pid));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    post = new Post { PostID = dr.GetInt32(0), UserID = dr.GetInt32(1), Username = dr.GetString(2).Trim(), Title = dr.GetString(3).Trim(), Content = dr.GetString(4).Trim(), CurrentDateTime = dr.GetDateTime(5) };
                }
                dr.Close();
                con.Close();
                return post;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Update post in database
        /// </summary>
        /// <param name="post">Post</param>
        /// <returns>true/false</returns>
        public bool UpdatePostInDB(Post post)
        {
            try
            {
                String query = $"update posts set title = @t, content = @c, curdatetime = @dt where postid = @pid";
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@t", post.Title.Trim()));
                cmd.Parameters.Add(new SqlParameter("@c", post.Content.Trim()));
                cmd.Parameters.Add(new SqlParameter("@dt", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@pid", post.PostID));
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
        /// Update username in database
        /// </summary>
        /// <param name="oldName">string</param>
        /// <param name="newName">string</param>
        /// <returns>true/false</returns>
        public bool UpdateUsernameInDB(string oldName, string newName)
        {
            try
            {
                String query = $"update posts set username = @nn where username = @on";
                con.Open();
                cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(new SqlParameter("@nn", newName.Trim()));
                cmd.Parameters.Add(new SqlParameter("@on", oldName.Trim()));
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

//CREATE TABLE[dbo].[posts]
//(

//   [postid] INT NOT NULL PRIMARY KEY IDENTITY,

//   [userid] INT NOT NULL,

//   [username] NCHAR(20) NOT NULL,

//   [title] NCHAR(20) NOT NULL,

//   [content] NTEXT NOT NULL,

//   [curdatetime] DATETIME NOT NULL
//)
