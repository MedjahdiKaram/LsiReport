using DevExpress.Utils.Serializing;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LsiReportApp.ViewModels
{
    public class ReportViewModel
    {

        public string Nazwa { get; set; }
        public System.DateTime Data { get; set; }
        public string Godzina { get; set; }

        public string Uzytkiwnik { get; set; }
        public string Lokal { get; set; }
    }
}
