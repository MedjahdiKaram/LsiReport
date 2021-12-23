using LsiReportApp.DataAccess;
using LsiReportApp.Helpers;
using LsiReportApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LsiReportApp
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {
        public LsiDatabaseEntities DbContext { get;  }
       

        public MainForm()
        {
            InitializeComponent();
            DbContext = new LsiDatabaseEntities();            
            grdLsiData.DataSource = Common.ToReportViewModels(DbContext.Reports); 
        }


        private void BtnValidate_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        /// <summary>
        /// This void function applies the filter by taking teh data from the UI and apply it on the data the return the filtered data to the grid
        /// </summary>
        protected void ApplyFilter()
        {
            try
            {
                var local = cmbLocal.Text;
                DateTime? from = null, to = null;
                if (!string.IsNullOrEmpty(dateFrom.SelectedText))
                    from = DateTime.Parse(dateFrom.SelectedText);
                if (!string.IsNullOrEmpty(dateTo.SelectedText))
                    to = DateTime.Parse(dateTo.SelectedText);
                var filteredReports = FilterReports(local, from, to);
                grdLsiData.DataSource = filteredReports;
            }
            catch (Exception ex)
            {
                var errLabel = "An error occured while applying the filter Please check you inputs. Exception details: ";
                var errCaption = "Filter Error";
#if DEBUG
                Debug.WriteLine(errLabel + ex.ToString());
#endif

                MessageBox.Show(errLabel + ex.Message, errCaption);
            }
        }

        /// <summary>
        /// This Function is done to filter the data according to the Local and date range.
        /// It apply the filters only for non empty or null values and apply an end when values are present
        /// </summary>
        /// <param name="local">a string Premises name</param>
        /// <param name="from">The date wanting to start filtering from</param>
        /// <param name="to">The date limit you want to filter</param>
        /// <returns>Filter values for non null / empty filter parameters</returns>
        protected IEnumerable<ReportViewModel> FilterReports(string local, DateTime? from, DateTime? to)
        {

            IEnumerable<ReportViewModel> results = Enumerable.Empty<ReportViewModel>();

            try
            {
                var fromValue = from;
                var toValue = to;
                if (fromValue > toValue)
                    (fromValue, toValue) = (toValue, fromValue);

                if (!string.IsNullOrEmpty(local) && fromValue == null && toValue == null)
                    results = Common.ToReportViewModels(DbContext.Reports.Where(rp => rp.Local.ToUpper().Contains(local)));

                else if (!string.IsNullOrEmpty(local) && fromValue != null && to == null)
                    results = Common.ToReportViewModels(DbContext.Reports.Where(rp => rp.Local.ToUpper().Contains(local) && rp.Date >= fromValue));
                else if (!string.IsNullOrEmpty(local) && fromValue != null && toValue != null)
                    results = Common.ToReportViewModels(DbContext.Reports.Where(rp => rp.Local.ToUpper().Contains(local) && rp.Date >= fromValue && rp.Date <= toValue));
                else if (!string.IsNullOrEmpty(local) && fromValue == null && toValue != null)
                    results = Common.ToReportViewModels(DbContext.Reports.Where(rp => rp.Local.ToUpper().Contains(local) && rp.Date <= toValue));
                else if (string.IsNullOrEmpty(local) && fromValue != null && toValue != null)
                    results = Common.ToReportViewModels(DbContext.Reports.Where(rp => rp.Date >= fromValue && rp.Date <= toValue));
                else if (string.IsNullOrEmpty(local) && fromValue != null && toValue == null)
                    results = Common.ToReportViewModels(DbContext.Reports.Where(rp => rp.Date >= fromValue));
                else if (string.IsNullOrEmpty(local) && fromValue == null && toValue != null)
                    results = Common.ToReportViewModels(DbContext.Reports.Where(rp => rp.Date <= toValue));
                else
                    results = Common.ToReportViewModels(DbContext.Reports);
            }
      
            catch (Exception ex)
            {
                var errLabel = "Error When filtering the Data. Exception details: ";
                var errCaption = "Data Filter Error";
#if DEBUG
                Debug.WriteLine(errLabel + ex.ToString());
#endif

                MessageBox.Show(errLabel + ex.Message, errCaption);
            }

            return results;
        }

    }
}
