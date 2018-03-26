using NoIdentity.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoIdentity.Business
{
    public class Report
    {
        private Report(Dal_Report dal)
        {
            Id = dal.Id;
            UserId = dal.UserId;
            Name = dal.Name;
            Data = dal.Data;
        }

        #region Properties

        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Data { get; set; }

        public string OwnerName { get; set; }

        #endregion

        #region Business Methods

        public static Report GetById(int id) =>
            Dal_Report.GetById(id) is Dal_Report dal
            ? new Report(dal)
            : throw new ArgumentException("Id is incorrect.");

        public static IEnumerable<Report> GetAllByUser(int id) =>
            Dal_Report.GetAllByUser(id) is IEnumerable<Dal_Report> reports
            ? reports.Select(r => new Report(r))
            : throw new ArgumentException("User Id is incorrect.");

        public static IEnumerable<Report> GetAll() =>
            Dal_Report.GetAll().Select(r => new Report(r));

        #endregion
    }

    public static class ReportExtensions
    {
        public static Report PopulateUserData(this Report report)
        {
            if (Dal_User.GetById(report.UserId) is Dal_User user)
            {
                report.OwnerName = user.FirstName + " " + user.LastName;
                return report;
            }
            else throw new ArgumentException("User Id is incorrect.");
        }

        public static IEnumerable<Report> PopulateUserData(this IEnumerable<Report> reports) =>
            reports.Select(r => r.PopulateUserData());
    }
}
