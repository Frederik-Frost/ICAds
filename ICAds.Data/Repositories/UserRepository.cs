using System;
using ICAds.Data.DTO;
using ICAds.Data.Models;
using ICAds.Security;

namespace ICAds.Data.Repositories
{
    public class UserRepository
    {
        public static UserModel CreateUser(UserDTO user)
        {
            var hashedPassword = SecurityManager.HashPassword(user.Password);

            UserModel newUser = new UserModel();

            newUser.Id = Guid.NewGuid();
            newUser.Firstname = user.Firstname;
            newUser.Lastname = user.Lastname;
            newUser.Email = user.Email;
            newUser.PasswordHash = hashedPassword.Hash;
            newUser.PasswordSalt = hashedPassword.Salt;
            //newUser.Active = true;
            //newUser.IsAdmin = false;

            using (var db = new AppDataContext())
            {
                db.Users.Add(newUser);
                db.SaveChanges();

                return newUser;
            }


        }

        public static string LoginUser(LoginDTO credentials, UserModel user)
        {
            //// Check if user exists
            //if (user.Email != request.Email)
            //{
            //    return BadRequest("User not found");
            //}

            // Check if passwords match
            if (!SecurityManager.CheckPassword(credentials.Password, user.PasswordHash, user.PasswordSalt)){
                return null;
            }

            // Create Token
            var token = SecurityManager.CreateToken(user.Id, user.Email);

            // Return Token
            return token;
        }


        public static List<UserModel> GetAllUsers()
        {
            using (var db = new AppDataContext())
            {
                List<UserModel> users = db.Users.ToList();
                return users;
            }
        }
    }
}

