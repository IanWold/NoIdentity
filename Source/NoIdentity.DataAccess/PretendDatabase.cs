using System;
using System.Collections.Generic;
using System.Text;

namespace NoIdentity.DataAccess
{
    static class PretendDatabase
    {
        public struct PretendDatabase_User
        {
            public int Id;
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
                FirstName = "Bob",
                LastName = "Guy",
                Username = "bobguy",
                Password = "a"
            },
            new PretendDatabase_User()
            {
                Id = 0,
                FirstName = "Sarah",
                LastName = "Parson",
                Username = "sparson",
                Password = "b"
            },
            new PretendDatabase_User()
            {
                Id = 0,
                FirstName = "Dalai",
                LastName = "Lama",
                Username = "dalama",
                Password = "c"
            }
        };
    }
}
