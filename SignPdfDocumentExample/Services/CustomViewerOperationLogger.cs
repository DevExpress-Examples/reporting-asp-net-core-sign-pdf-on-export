using System.IO;
using DevExpress.Office.DigitalSignatures;
using DevExpress.Pdf;
using DevExpress.XtraReports.Web.ClientControls;
using DevExpress.XtraReports.Web.WebDocumentViewer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SignPdfDocumentExample.Services {
    public class CustomViewerOperationLogger : WebDocumentViewerOperationLogger {
        readonly ILogger<CustomViewerOperationLogger> logger;
        readonly IWebHostEnvironment webHostEnvironment;

        public CustomViewerOperationLogger(ILogger<CustomViewerOperationLogger> logger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment) {
            this.logger = logger;
            this.webHostEnvironment = webHostEnvironment;
        }

        public override void CustomizeExportDocumentOnFinish(string documentId, string exportOperationId, ExportedDocument exportedDocument) {
            if(exportedDocument.ContentType == "application/pdf") {
                string certificateFile = Path.Join(webHostEnvironment.ContentRootPath, "Signatures", "certificate.pfx");
                using(PdfDocumentSigner documentSigner = new PdfDocumentSigner(new MemoryStream(exportedDocument.Bytes))) {
                    var signer = new Pkcs7Signer(certificateFile, "123", HashAlgorithmType.SHA256);
                    PdfSignatureBuilder signature = new PdfSignatureBuilder(signer);
                    signature.ContactInfo = "John Smith";
                    signature.Reason = "I Agree";
                    MemoryStream stream = new MemoryStream();
                    documentSigner.SaveDocument(stream, signature);
                    exportedDocument.Bytes = stream.ToArray();
                }
                logger.LogInformation($"Exported document {documentId} signed with \"certificate.pfx\" signature certificate.");
            }
        }
    }
}
