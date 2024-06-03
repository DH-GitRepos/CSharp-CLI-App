using System.Collections.Generic;

namespace DTOs
{
    public class Report_DTO
    {
        public string Status { get; private set; }

        public string Message { get; private set; }

        public string ReportType { get; private set; }

        public List<string> ReportContents { get; private set; }

        public Report_DTO(string status, string message, string reportType, List<string> reportContents)
        {
            this.Status = status;
            this.Message = message;
            this.ReportType = reportType;
            this.ReportContents = reportContents;
        }

        public Report_DTO(string status, string message, string reportType)
        {
            this.Status = status;
            this.Message = message;
            this.ReportType = reportType;
        }
    }
}
