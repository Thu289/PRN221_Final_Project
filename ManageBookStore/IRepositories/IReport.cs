using ManageBookStore.Models;

namespace ManageBookStore.IRepositories
{
    public interface IReport
    {
        void AddReport(Report report);

        void UpdateReport(Report report);

        void DeleteReport(int reportId);

        List<Report> GetAllReports();

        List<Report> GetReportsByStatus(string status);

        List<Report> GetReportsByAccount(string accountId, string role);

    }
}
