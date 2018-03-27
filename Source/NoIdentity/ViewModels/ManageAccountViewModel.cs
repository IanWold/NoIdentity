using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoIdentity.Business;
using System.ComponentModel;

namespace NoIdentity.ViewModels
{
    public class ManageAccountViewModel
    {
        public ManageAccountViewModel() { }

        public ManageAccountViewModel(User user)
        {
            Username = user.Username;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }

        #region Properties

        public string Username { get; set; }

        [DisplayName("Old Password")]
        public string OldPassword { get; set; }

        [DisplayName("Verify Old Password")]
        public string VerifyOldPassword { get; set; }

        [DisplayName("New Password")]
        public string NewPassword { get; set; }

        [DisplayName("Verify New Password")]
        public string VerifyNewPassword { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        #endregion
    }
}
