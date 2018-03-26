using System;
using System.Collections.Generic;
using System.Linq;

namespace NoIdentity.DataAccess
{
    public class Dal_User
    {
        #region Construction

        public Dal_User() { }

        private Dal_User(PretendDatabase.PretendDatabase_User db)
        {
            Id = db.Id;
            RoleId = db.RoleId;
            FirstName = db.FirstName;
            LastName = db.LastName;
            Username = db.Username;
            Password = db.Password;
            LastModifiedDate = db.LastModifiedDate;
        }

        #endregion

        #region Fields

        public int Id;
        public int RoleId;
        public string FirstName;
        public string LastName;
        public string Username;
        public string Password;
        public DateTime LastModifiedDate;

        #endregion

        #region Data Access

        public static Dal_User GetById(int id) =>
            PretendDatabase.Users.FirstOrDefault(u => u.Id == id) is PretendDatabase.PretendDatabase_User user
            ? new Dal_User(user)
            : null;

        public static Dal_User GetByUsername(string username) =>
            PretendDatabase.Users.FirstOrDefault(u => u.Username == username) is PretendDatabase.PretendDatabase_User user
            ? new Dal_User(user)
            : null;

        public static IEnumerable<Dal_User> GetAllByRole(int id) =>
            PretendDatabase.Roles.Where(r => r.Id == id).Any()
            ? PretendDatabase.Users.Where(u => u.RoleId == id).Select(u => new Dal_User(u))
            : null;

        #endregion
    }
}
