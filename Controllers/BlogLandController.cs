using ASSIGNMENT3.DBHandler;
using ASSIGNMENT3.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSIGNMENT3.Controllers
{
    /// <summary>
    /// Handle All the View in BlogLand Web Application
    /// </summary>
    public class BlogLandController : Controller
    {
        static PostDBHandler postDB;
        static UserDBHandler userDB;
        static User currentUser;
        static int? currentPostID;

        /// <summary>
        /// Contructor
        /// </summary>
        static BlogLandController()
        {
            userDB = new UserDBHandler();
            postDB = new PostDBHandler();
            currentUser = new User();
            currentPostID = null;
        }
        /// <summary>
        /// Handle Home Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        public ViewResult Home()
        {
            if(!HttpContext.Request.Cookies.ContainsKey("Logined"))
            {
                return View("Login");
            }
            List<Post> postList = postDB.GetAllPostsFromDB();
            return View(postList);
        }

        /// <summary>
        /// Handle AdminHome Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        public ViewResult AdminHome()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Admin"))
            {
                return View("Login");
            }
            List<User> userList = userDB.GetAllUsersFromDB();
            return View("UserList",userList);
        }

        /// <summary>
        /// Handle ForgetPassword Post request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ViewResult ForgetPassword(ForgetPassword forgetPassword)
        {
            if (ModelState.IsValid)
            {
                String pswd = userDB.GetPassword(forgetPassword.Username, forgetPassword.Email);
                if (pswd != null)
                {
                    return View("PasswordRecoverd", pswd);
                }
                else
                {
                    return View("PasswordNotRecovered");
                }
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Handle Profile Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult Profile()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Logined"))
            {
                return View("Login");
            }
            return View(currentUser);
        }
        /// <summary>
        /// Handle Change Username Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult ChangeUsername(int id)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Logined"))
            {
                return View("Login");
            }
            return View(currentUser);
        }

        /// <summary>
        /// Handle Change Username Post request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ViewResult ChangeUsername(ChangeUsername uname)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Logined"))
            {
                return View("Login");
            }
            if (ModelState.IsValid)
            {
                if (userDB.IsUsernameExist(uname.Username, currentUser.UserID))
                {
                    ModelState.AddModelError("Username", "This Username already used by other user, try another one");
                    return View();
                }
                else
                {
                    postDB.UpdateUsernameInDB(currentUser.Username, uname.Username);
                    currentUser.Username = uname.Username;
                    userDB.UpdateUsernameInDB(currentUser);
                    return View("Profile", currentUser);
                }
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Handle Change Password Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult ChangePassword(int id)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Logined"))
            {
                return View("Login");
            }
            return View(currentUser);
        }

        /// <summary>
        /// Handle Change Password Post request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ViewResult ChangePassword(ChangePassword pswd)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Logined"))
            {
                return View("Login");
            }
            if (ModelState.IsValid)
            {
                currentUser.Password = pswd.Password;
                userDB.UpdatePasswordInDB(currentUser);
                return View("Profile", currentUser);
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Handle Change Email Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult ChangeEmail(int id)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Logined"))
            {
                return View("Login");
            }
            return View(currentUser);
        }

        /// <summary>
        /// Handle Change Email Post request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ViewResult ChangeEmail(ChangeEmail em)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Logined"))
            {
                return View("Login");
            }
            if (ModelState.IsValid)
            {
                if (userDB.IsEmailExist(em.Email, currentUser.UserID))
                {
                    ModelState.AddModelError("Email", "This Email already used by other user, try another one");
                    return View();
                }
                else
                {
                    currentUser.Email = em.Email;
                    userDB.UpdateEmailInDB(currentUser);
                    return View("Profile", currentUser);
                }
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Handle Change About Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult ChangeAbout(int id)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Logined"))
            {
                return View("Login");
            }
            return View(currentUser);
        }


        /// <summary>
        /// Handle Change About Post request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ViewResult ChangeAbout(ChangeAbout ab)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Logined"))
            {
                return View("Login");
            }
            if (ModelState.IsValid)
            {
                currentUser.About = ab.About;
                userDB.UpdateAboutInDB(currentUser);
                return View("About", currentUser);
            }
            else
            {
                return View();
            }
        }


        /// <summary>
        /// Handle About Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult About()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Logined"))
            {
                return View("Login");
            }
            return View(currentUser);
        }


        /// <summary>
        /// Handle Forget Password Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult ForgetPassword()
        {
            return View();
        }

        /// <summary>
        /// Handle Signup Postrequest 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ViewResult Signup(User user)
        {
            bool IsError = false;
            if (ModelState.IsValid)
            {
                if (userDB.IsUsernameExist(user.Username))
                {
                    IsError = true;
                    ModelState.AddModelError("Username", "This Username already used by other user, try another one");
                }
                if (userDB.IsEmailExist(user.Email))
                {
                    IsError = true;
                    ModelState.AddModelError("Email", "This Email already used by other user, try another one");
                }
                if (user.Password != user.ConfirmPassword)
                {
                    IsError = true;
                    ModelState.AddModelError("ConfirmPassword", "Password does not match");
                }
                if (IsError)
                {
                    return View();
                }
                else
                {
                    userDB.AddUserToDB(user.Username, user.Password, user.Email);
                    currentUser = userDB.GetUserFromDB(user.Username, user.Password);
                    List<Post> postList = postDB.GetAllPostsFromDB();
                    HttpContext.Response.Cookies.Append("Logined", "true");
                    return View("Home", postList);
                }
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Handle Signup Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult Signup()
        {
            return View();
        }

        /// <summary>
        /// Handle Add User Post request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ViewResult AddUser(User user)
        {
            bool IsError = false;
            if (ModelState.IsValid)
            {
                if (userDB.IsUsernameExist(user.Username))
                {
                    IsError = true;
                    ModelState.AddModelError("Username", "This Username already used by other user, try another one");
                }
                if (userDB.IsEmailExist(user.Email))
                {
                    IsError = true;
                    ModelState.AddModelError("Email", "This Email already used by other user, try another one");
                }
                if (user.Password != user.ConfirmPassword)
                {
                    IsError = true;
                    ModelState.AddModelError("ConfirmPassword", "Password does not match");
                }
                if (IsError)
                {
                    return View();
                }
                else
                {
                    userDB.AddUserToDB(user.Username, user.Password, user.Email);
                    List<User> userList = userDB.GetAllUsersFromDB();
                    return View("UserList", userList);
                }
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Handle Add User Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult AddUser()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Admin"))
            {
                return View("Login");
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Handle Delete User Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult DeleteUser(int id)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Admin"))
            {
                return View("Login");
            }
            else
            {
                userDB.DeleteUserFromDB(id);
                postDB.DeletePostByUserID(id);
                List<User> userList = userDB.GetAllUsersFromDB();
                HttpContext.Response.Cookies.Append("Admin", "true");
                return View("UserList", userList);
            }
        }

        /// <summary>
        /// Handle User Details Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult UserDetails(int id)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Admin"))
            {
                return View("Login");
            }
            User user = userDB.GetUserFromDB(id);
            return View(user);
        }

        /// <summary>
        /// Handle Edit User Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult EditUser(int id)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Admin"))
            {
                return View("Login");
            }
            User user = userDB.GetUserFromDB(id);
            user.ConfirmPassword = user.Password;
            return View(user);
        }

        /// <summary>
        /// Handle Edit User Post request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ViewResult EditUser(User user)
        {
            bool IsError = false;
            if (ModelState.IsValid)
            {
                if (userDB.IsUsernameExist(user.Username, user.UserID))
                {
                    IsError = true;
                    ModelState.AddModelError("Username", "This Username already used by other user, try another one");
                }
                if (userDB.IsEmailExist(user.Email, user.UserID))
                {
                    IsError = true;
                    ModelState.AddModelError("Email", "This Email already used by other user, try another one");
                }
                if (user.Password != user.ConfirmPassword)
                {
                    IsError = true;
                    ModelState.AddModelError("ConfirmPassword", "Password does not match");
                }
                if (IsError)
                {
                    return View();
                }
                else
                {
                    userDB.UpdateUserInDB(user);
                    List<User> userList = userDB.GetAllUsersFromDB();
                    return View("UserList", userList);
                }
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Handle Post Details Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult PostDetails(int id)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Logined"))
            {
                return View("Login");
            }
            Post post = postDB.GetPostFromDB(id);
            if (post.UserID == currentUser.UserID)
            {
                return View("PostDetailsCanChange", post);
            }
            else
            {
                return View("PostDetailsCannotChange", post);
            }
        }

        /// <summary>
        /// Handle Update Post Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult UpdatePost(int id)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Logined"))
            {
                return View("Login");
            }
            Post post = postDB.GetPostFromDB(id);
            currentPostID = id;
            return View(post);
        }


        /// <summary>
        /// Handle Update Post, Post request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ViewResult UpdatePost(Post post)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Logined"))
            {
                return View("Login");
            }
            if (ModelState.IsValid)
            {
                post.PostID = currentPostID.Value;
                currentPostID = null;
                postDB.UpdatePostInDB(post);
                List<Post> postList = postDB.GetAllPostsFromDB();
                return View("Home", postList);
            }
            else
            {
                return View(post);
            }
        }

        /// <summary>
        /// Handle Delete Post Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult DeletePost(int id)
        {
            if (!HttpContext.Request.Cookies.ContainsKey("Logined"))
            {
                return View("Login");
            }
            postDB.DeletePostFromDB(id);
            List<Post> postList = postDB.GetAllPostsFromDB();
            return View("Home", postList);
        }



        /// <summary>
        /// Handle Create Post Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public ViewResult CreatePost(Post post)
        {
            if (ModelState.IsValid)
            {
                post.UserID = currentUser.UserID;
                post.Username = currentUser.Username;
                post.CurrentDateTime = DateTime.Now;
                postDB.AddPostToDB(post);
                List<Post> postList = postDB.GetAllPostsFromDB();
                return View("Home", postList);
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Handle Logout Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult Logout()
        {
            currentUser = new User();
            HttpContext.Response.Cookies.Delete("Logined");
            return View("Login");
        }

        /// <summary>
        /// Handle Admin Logout Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult AdminLogout()
        {
            currentUser = new User();
            HttpContext.Response.Cookies.Delete("Admin");
            return View("Login");
        }


        /// <summary>
        /// Handle Login Get request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        /// <summary>
        /// Handle Login Post request 
        /// </summary>
        /// <returns>ViewResult</returns>
        [HttpPost]
        public ViewResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                if (userDB.IsUserExist(login.Username, login.Password))
                {
                    currentUser = userDB.GetUserFromDB(login.Username, login.Password);
                    if(IsUserAdmin(login.Username, login.Password))
                    {
                        List<User> userList = userDB.GetAllUsersFromDB();
                        HttpContext.Response.Cookies.Append("Admin", "true");
                        return View("UserList", userList);
                    }
                    else
                    {
                        List<Post> postList = postDB.GetAllPostsFromDB();
                        HttpContext.Response.Cookies.Append("Logined", "true");
                        return View("Home", postList);
                    }                 
                }
                else
                {
                    ModelState.AddModelError("Password", "Incorrect Username or Password\nTry Again or Sign up");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Check whether the user is admin or not
        /// </summary>
        /// <returns>true/false</returns>
        private bool IsUserAdmin(string username, string password)
        {
            return username == "iamadmin" && password == "iamadmin";
        }
    }
}
