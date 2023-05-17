using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System;

namespace SignPdfDocumentExample.PredefinedReports {
    public static class ReportsFactory {
        static ReportsFactory() {
            Reports.Add("TestReport", () => new TestReport());
        }
        public static Dictionary<string, Func<XtraReport>> Reports = new Dictionary<string, Func<XtraReport>>();
    }
}
