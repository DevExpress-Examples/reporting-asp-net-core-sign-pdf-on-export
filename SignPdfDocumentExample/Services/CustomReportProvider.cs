using DevExpress.XtraReports.Services;
using DevExpress.XtraReports.UI;
using SignPdfDocumentExample.PredefinedReports;

namespace SignPdfDocumentExample.Services {
    public class CustomReportProvider : IReportProvider {
        public XtraReport GetReport(string id, ReportProviderContext context) {
            if(ReportsFactory.Reports.TryGetValue(id, out var report)) {
                return report();
            }
            throw new DevExpress.XtraReports.Web.ClientControls.FaultException(string.Format("Could not find report '{0}'.", id));
        }
    }
}
