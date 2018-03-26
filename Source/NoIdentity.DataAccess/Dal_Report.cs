using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoIdentity.DataAccess
{
    public class Dal_Report
    {
        #region Construction

        public Dal_Report() { }

        private Dal_Report(PretendDatabase.PretendDatabase_Report db)
        {
            Id = db.Id;
            UserId = db.UserId;
            Name = db.Name;
            Data = db.Data;
        }

        #endregion Construction

        #region Fields

        public int Id;
        public int UserId;
        public string Name;
        public string Data;

        #endregion

        #region Data Access

        public static Dal_Report GetById(int id) =>
            PretendDatabase.Reports.FirstOrDefault(r => r.Id == id) is PretendDatabase.PretendDatabase_Report report
            ? new Dal_Report(report)
            : null;

        public static IEnumerable<Dal_Report> GetAllByUser(int id) =>
            PretendDatabase.Users.Where(u => u.Id == id).Any()
            ? PretendDatabase.Reports.Where(r => r.UserId == id).Select(r => new Dal_Report(r))
            : null;

        public static IEnumerable<Dal_Report> GetAll() =>
            PretendDatabase.Reports.Select(r => new Dal_Report(r));

        #endregion
    }
}
