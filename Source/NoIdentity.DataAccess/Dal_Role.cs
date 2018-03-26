using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoIdentity.DataAccess
{
    public class Dal_Role
    {
        #region Construction

        public Dal_Role() { }

        private Dal_Role(PretendDatabase.PretendDatabase_Role role)
        {
            Id = role.Id;
            Name = role.Name;
        }

        #endregion

        #region Fields

        public int Id;
        public string Name;

        #endregion

        #region Data Access

        public static Dal_Role GetById(int id) =>
            PretendDatabase.Roles.FirstOrDefault(r => r.Id == id) is PretendDatabase.PretendDatabase_Role role
            ? new Dal_Role(role)
            : null;

        #endregion
    }
}
