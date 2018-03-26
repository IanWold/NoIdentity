using NoIdentity.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoIdentity.Business
{
    public class Role
    {
        private Role(Dal_Role dal)
        {
            Id = dal.Id;
            Name = dal.Name;
        }

        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        #endregion

        #region Business Methods

        public static Role GetById(int id) =>
            Dal_Role.GetById(id) is Dal_Role dal
            ? new Role(dal)
            : throw new ArgumentException("Id is incorrect");

        #endregion
    }
}
