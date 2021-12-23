using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsiReportApp.ViewModels
{
    public class ReportViewModel
    {
        
        public string Name { get; set; }
        public System.DateTime Date { get; set; }
        public string Time { get; set; }

        public string User { get; set; }
        public string Local { get; set; }
    }
}
