using System;
using System.Collections.Generic;
using System.Linq;
using CodeSOSProject.DomainModels;

namespace CodeSOSProject.Repositories
{
    public interface IUsersRepository
    {
        void InsertUser(User u);
        void UpdateUserDetails(User u);
        void UpdateUserPassword(User u);
        void DeleteUser(int UserID);
        List<User> GetUsers();
        List<User> GetUsersByEmailAndPassword(string Email, string PasswordHash);
        List<User> GetUsersByEmail(string Email);
        List<User> GetUsersByUserID(int UserID);
        int GetLatestUserID();

    }
    public class UsersRepository : IUsersRepository
    {
        CodeSOSDatabaseDbContext db;

        public UsersRepository()
        {
            db = new CodeSOSDatabaseDbContext();
        }

        public void InsertUser(User u)
        {
            db.Users.Add(u);
            db.SaveChanges();
        }

        public void UpdateUserDetails(User u)      
        {
            User user = db.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            if (user != null)
            {
                user.Name = u.Name;
                user.Mobile = u.Mobile;
                db.SaveChanges();
            }
        }
        public void UpdateUserPassword(User u)
        {
            User user = db.Users.Where(temp => temp.UserID == u.UserID).FirstOrDefault();
            if (user != null)
            {
                user.PasswordHash = u.PasswordHash;               
                db.SaveChanges();
            }
        }
        public void DeleteUser(int UserID)
        {
            User user = db.Users.Where(temp => temp.UserID == UserID).FirstOrDefault();
            if(user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }       
        }

        public List<User> GetUsers()
        {
            List<User> users = db.Users.Where(temp => temp.IsAdmin == false).OrderBy(temp => temp.Name).ToList();
            return users;
        }

        public List<User> GetUsersByEmailAndPassword(string Email, string PasswordHash)
        {
            List<User> users = db.Users.Where(temp => temp.Email == Email && temp.PasswordHash == PasswordHash).ToList();
            return users;
        }

        public List<User> GetUsersByEmail(string Email)
        {
            List<User> users = db.Users.Where(temp => temp.Email == Email).ToList();
            return users;
        }

        public List<User> GetUsersByUserID(int UserID)
        {
            List<User> users = db.Users.Where(temp => temp.UserID == UserID).ToList();
            return users;
        }

        public int GetLatestUserID()
        {
            int latestUserID = db.Users.Select(temp => temp.UserID).Max();
            return latestUserID;
        }
    }
}
