using System;
using NoIdentity.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace NoIdentity.Business
{
    public class User
    {
        private User(Dal_User dal)
        {
            Id = dal.Id;
            RoleId = dal.RoleId;
            FirstName = dal.FirstName;
            LastName = dal.LastName;
            Username = dal.Username;
            Password = dal.Password;
            LastModifiedDate = dal.LastModifiedDate;
        }

        #region Properties

        public int Id { get; set; }

        public int RoleId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime LastModifiedDate { get; set; }

        #endregion

        #region Business Methods

        public static User GetById(int id) =>
            Dal_User.GetById(id) is Dal_User dal
            ? new User(dal)
            : throw new ArgumentException("Id is incorrect.");

        public static User GetByUsername(string username) =>
            Dal_User.GetByUsername(username) is Dal_User dal
            ? new User(dal)
            : throw new ArgumentException("Username is incorrect.");

        public static IEnumerable<User> GetAllByRole(int id) =>
            Dal_User.GetAllByRole(id) is IEnumerable<Dal_User> users
            ? users.Select(u => new User(u))
            : throw new ArgumentException("Role Id is incorrect.");

        #endregion
    }
}
