using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalQandAForum.DomainModels;

namespace TechnicalQandAForum.Repositories
{
    public interface IUserRepository
    {
        void InsertUser(User user);
        void UpdateUserDetails(User user);
        void UpdateUserpassword(User user);
        void DeleteUser(int userId);
        List<User> GetUsers();
        List<User> GetUserByEmailAndPassword(string email, string passwordHash);
        List<User> GetUserByEmail(string email);
        List<User> GetUserByUserId(int userId);
        int GetLatestUserId();
    }
    public class UserRepository : IUserRepository
    {
        TechnicalQandAForumDbContext dbContext;

        public UserRepository()
        {
            dbContext = new TechnicalQandAForumDbContext();
        }

        public void InsertUser(User user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

        public void UpdateUserDetails(User user)
        {
            User us = dbContext.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();
            if (us != null)
            {
                us.Name = user.Name;
                us.Mobile = user.Mobile;
                dbContext.SaveChanges();
            }
        }

        public void UpdateUserpassword(User user)
        {
            User us = dbContext.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();
            if (us != null)
            {
                us.PasswordHash = user.PasswordHash;
                dbContext.SaveChanges();
            }
        } 

        public void DeleteUser(int userId)
        {
            User us = dbContext.Users.Where(u => u.UserId == userId).FirstOrDefault();
            if (us != null)
            {
                dbContext.Users.Remove(us);
                dbContext.SaveChanges();
            }
        }

        public List<User> GetUsers()
        {
            List<User> users = dbContext.Users.Where(u => u.IsAdmin == false).OrderBy(u => u.Name).ToList();
            return users;
        }

        public List<User> GetUserByEmailAndPassword(string email, string passwordHash)
        {
            List<User> users = dbContext.Users.Where(u => u.Email == email && u.PasswordHash == passwordHash).ToList();
            return users;
        }
        public List<User> GetUserByEmail(string email)
        {
            List<User> users = dbContext.Users.Where(u => u.Email == email).ToList();
            return users;
        }

        public List<User> GetUserByUserId(int userId)
        {
            List<User> users = dbContext.Users.Where(u => u.UserId == userId).ToList();
            return users;
        }

        public int GetLatestUserId()
        {
            int uid = dbContext.Users.Select(u => u.UserId).Max();
            return uid;
        }
    }
}