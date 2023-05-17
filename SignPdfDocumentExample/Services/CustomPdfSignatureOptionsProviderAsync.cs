using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using Microsoft.AspNetCore.Hosting;

namespace SignPdfDocumentExample.Services {
    public class CustomPdfSignatureOptionsProviderAsync : IPdfSignatureOptionsProviderAsync {
        readonly Dictionary<string, PdfSignatureOptions> signatures = new Dictionary<string, PdfSignatureOptions>();
        public CustomPdfSignatureOptionsProviderAsync(IWebHostEnvironment webHostEnvironment) {
            var signatureDictionaryPath = Path.Join(webHostEnvironment.ContentRootPath, "Signatures");
            signatures.Add(Guid.NewGuid().ToString(), new PdfSignatureOptions() {
                Certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(Path.Combine(signatureDictionaryPath, "certificate.pfx"), "123"),
                ContactInfo = "Jane Cooper",
            });
            signatures.Add(Guid.NewGuid().ToString(), new PdfSignatureOptions() {
                Certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(Path.Combine(signatureDictionaryPath, "certificate.pfx"), "123"),
                ContactInfo = "John Smith",
                Location = "Australia",
                Reason = "I Agree",
                ImageSource = DevExpress.XtraPrinting.Drawing.ImageSource.FromFile(Path.Combine(signatureDictionaryPath, "John_Smith.png"))
            });
        }

        public Task<Dictionary<string, PdfSignatureOptions>> GetAvailableOptionsAsync() {
            return Task.FromResult(signatures);
        }
    }
}
