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
            public DateTime LastModifiedDate;
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
                Password = "a",
                LastModifiedDate = DateTime.Now
            },
            new PretendDatabase_User()
            {
                Id = 1,
                RoleId = 0,
                FirstName = "Sarah",
                LastName = "Parson",
                Username = "sparson",
                Password = "b",
                LastModifiedDate = DateTime.Now
            },
            new PretendDatabase_User()
            {
                Id = 2,
                RoleId = 1,
                FirstName = "Dalai",
                LastName = "Lama",
                Username = "dalama",
                Password = "c",
                LastModifiedDate = DateTime.Now
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

        #region Reports

        public struct PretendDatabase_Report
        {
            public int Id;
            public int UserId;
            public string Name;
            public string Data;
        }

        public static List<PretendDatabase_Report> Reports = new List<PretendDatabase_Report>()
        {
            new PretendDatabase_Report()
            {
                Id = 0,
                UserId = 0,
                Name = "Report One",
                Data = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna. Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede."
            },
            new PretendDatabase_Report()
            {
                Id = 1,
                UserId = 0,
                Name = "Report Two",
                Data = "Mauris et orci. Aenean nec lorem. In porttitor. Donec laoreet nonummy augue. Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy. Fusce aliquet pede non pede."
            },
            new PretendDatabase_Report()
            {
                Id = 2,
                UserId = 1,
                Name = "Report Three",
                Data = "Suspendisse dapibus lorem pellentesque magna. Integer nulla. Donec blandit feugiat ligula. Donec hendrerit, felis et imperdiet euismod, purus ipsum pretium metus, in lacinia nulla nisl eget sapien. Donec ut est in lectus consequat consequat. Etiam eget dui. Aliquam erat volutpat. Sed at lorem in nunc porta tristique."
            },
            new PretendDatabase_Report()
            {
                Id = 3,
                UserId = 1,
                Name = "Report Four",
                Data = "Proin nec augue. Quisque aliquam tempor magna. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nunc ac magna. Maecenas odio dolor, vulputate vel, auctor ac, accumsan id, felis. Pellentesque cursus sagittis felis. Pellentesque porttitor, velit lacinia egestas auctor, diam eros tempus arcu, nec vulputate augue magna vel risus. Cras non magna vel ante adipiscing rhoncus."
            }
        };

        #endregion
    }
}
