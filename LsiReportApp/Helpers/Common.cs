using LsiReportApp.DataAccess;
using LsiReportApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LsiReportApp.Helpers
{
    public static class Common
    {
        public static ReportViewModel ToReportViewModel(Report report)
        {
            var result = new ReportViewModel();

            try
            {
                if (report != null)
                {
                    result = new ReportViewModel
                    {                        
                        Name = report.Name,
                        Local = report.Local,
                        User = report.UserExp,
                        Date = report.Date,
                        Time = report.Date.ToString("HH:mm")
                    };
                }

            }
            catch (Exception ex)
            {
                var errLabel = "An error occured while converting the report data. Exception: ";
                var errCaption = "Data Convertion Error";
#if DEBUG
                Debug.WriteLine(errLabel + ex.ToString());
#endif

                MessageBox.Show(errLabel+ ex.Message, errCaption);
            }
            return result;
        }

        public static IEnumerable<ReportViewModel> ToReportViewModels(IEnumerable<Report> report)
        {
            IEnumerable<ReportViewModel> reportvms = Enumerable.Empty<ReportViewModel>(); ;
            if (report!=null && report.Count()>0)
                reportvms = report.Select(rp => ToReportViewModel(rp));

            return reportvms;
        }

    }
}
