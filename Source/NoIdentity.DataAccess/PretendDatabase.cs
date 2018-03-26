using System;
using System.Collections.Generic;
using System.Text;

namespace NoIdentity.DataAccess
{
    internal static class PretendDatabase
    {
        #region Users

        public struct PretendDatabase_User
        {
            public int Id;
            public int RoleId;
            public string FirstName;
            public string LastName;
            public string Username;
            public string Password;
        }

        public static List<PretendDatabase_User> Users = new List<PretendDatabase_User>()
        {
            new PretendDatabase_User()
            {
                Id = 0,
                RoleId = 0,
                FirstName = "Bob",
                LastName = "Guy",
                Username = "bobguy",
                Password = "a"
            },
            new PretendDatabase_User()
            {
                Id = 0,
                RoleId = 0,
                FirstName = "Sarah",
                LastName = "Parson",
                Username = "sparson",
                Password = "b"
            },
            new PretendDatabase_User()
            {
                Id = 0,
                RoleId = 1,
                FirstName = "Dalai",
                LastName = "Lama",
                Username = "dalama",
                Password = "c"
            }
        };

        #endregion

        #region Roles

        public struct PretendDatabase_Role
        {
            public int Id;
            public string Name;
        }

        public static List<PretendDatabase_Role> Roles = new List<PretendDatabase_Role>()
        {
            new PretendDatabase_Role()
            {
                Id = 0,
                Name = "User"
            },
            new PretendDatabase_Role()
            {
                Id = 1,
                Name = "Administrator"
            }
        };
    
        #endregion
    }
}
