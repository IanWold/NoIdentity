using System;
using System.Linq;

namespace NoIdentity.DataAccess
{
    public class Dal_User
    {
        public Dal_User() { }

        private Dal_User(PretendDatabase.PretendDatabase_User db)
        {
            Id = db.Id;
            FirstName = db.FirstName;
            LastName = db.LastName;
            Username = db.Username;
            Password = db.Password;
        }

        #region Fields

        public int Id;
        public string FirstName;
        public string LastName;
        public string Username;
        public string Password;

        #endregion

        #region Data Access

        public static Dal_User GetById(int id)
        {
            if (PretendDatabase.Users.FirstOrDefault(u => u.Id == id) is PretendDatabase.PretendDatabase_User user)
                return new Dal_User(user);
            else return null;
        }

        public static Dal_User GetByUsernameAndPassword(string username, string password)
        {
            if (PretendDatabase.Users.FirstOrDefault(u => u.Username == username && u.Password == password) is PretendDatabase.PretendDatabase_User user)
                return new Dal_User(user);
            else return null;
        }

        #endregion
    }
}
