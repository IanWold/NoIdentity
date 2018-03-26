using System;
using NoIdentity.DataAccess;

namespace NoIdentity.Business
{
    public class User
    {
        private User() { }

        #region Properties

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        #endregion

        #region Business Methods

        public static User GetById(int id)
        {
            if (Dal_User.GetById(id) is Dal_User dal)
            {
                var toReturn = new User();
                toReturn.ReadValues(dal);
                return toReturn;
            }
            else throw new ArgumentException("Id is incorrect.");
        }

        public static User GetByUsernameAndPassword(string username, string password)
        {
            if (Dal_User.GetByUsernameAndPassword(username, password) is Dal_User dal)
            {
                var toReturn = new User();
                toReturn.ReadValues(dal);
                return toReturn;
            }
            else throw new ArgumentException("Username or Password is incorrect.");
        }

        // Need:
        // Create
        // Save
        // Delete
        // Etc...

        #endregion

        #region Data Access

        public void ReadValues(Dal_User dal)
        {
            Id = dal.Id;
            FirstName = dal.FirstName;
            LastName = dal.LastName;
            Username = dal.Username;
            Password = dal.Password;
        }

        #endregion
    }
}
