# Reporting for ASP.NET Core - Use the Web Document Viewer to Sign an Exported PDF Document

This example demonstrates how to sign an exported PDF document. 

## Add Signature Capabilities to the Document Viewer's UI

**Files:**  [CustomPdfSignatureOptionsProviderAsync.cs](./SignPdfDocumentExample/Services/CustomPdfSignatureOptionsProviderAsync.cs)
 / [CustomPdfSignatureOptionsProvider.cs](./SignPdfDocumentExample/Services/CustomPdfSignatureOptionsProvider.cs)

To pass a collection of signatures to the Web Document Viewer, implement the [IPdfSignatureOptionsProvider](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.IPdfSignatureOptionsProvider?v=23.1&p=netframework) or [IPdfSignatureOptionsProviderAsync](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.IPdfSignatureOptionsProviderAsync?v=23.1&p=netframework) interface. 

In this example, the `CustomPdfSignatureOptionsProviderAsync` class implements the [IPdfSignatureOptionsProviderAsync](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.IPdfSignatureOptionsProviderAsync?v=23.1&p=netframework) interface. The [GetAvailableOptionsAsync](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.IPdfSignatureOptionsProvider.GetAvailableOptions?v=23.1&p=netframework) method returns the dictionary of signature identifiers and [PdfSignatureOptions](https://docs.devexpress.com/CoreLibraries/DevExpress.XtraPrinting.PdfSignatureOptions?v=23.1) objects. This dictionary defines signatures available in the Web Document Viewer.


Select a signature to sign the exported document from the **Signature** drop down list in the **PDF Export Options** section: 

![Signature Options](Images/signature-options.png)

The selected signature is used to digitally sign the exported PDF document. The [XRPdfSignature](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XRPdfSignature?v=23.1) control adds a visual signature:

![Signed PDF Document](Images/visual-signature.png)

## Customize the Exported Document

**File:** [CustomViewerOperationLogger.cs](./SignPdfDocumentExample/Services/CustomViewerOperationLogger.cs)

The example uses the [PDF Document API](https://www.devexpress.com/products/net/office-file-api/pdf/) to add a digital signature to a PDF document. Override the [CustomizeExportDocumentOnFinish](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.WebDocumentViewerOperationLogger.N----F-y-----8-p) method to retrieve and modify the exported document.

> **Note**:
> If you use this method to sign a PDF document, the [XRPdfSignature](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XRPdfSignature?v=23.1) control doesn't add a visual signature representation. To confirm that the document is signed, open it in the PDF editor.

The `CustomViewerOperationLogger` service inherits the [WebDocumentViewerOperationLogger](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.WebDocumentViewerOperationLogger) class. The [CustomizeExportDocumentOnFinish](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.Web.WebDocumentViewer.WebDocumentViewerOperationLogger.N----F-y-----8-p) method creates a [PKCS#7](https://docs.devexpress.com/OfficeFileAPI/DevExpress.Pdf.Pkcs7Signer) signature with a certificate and a password specified in the object constructor. [PdfSignatureBuilder](https://docs.devexpress.com/OfficeFileAPI/DevExpress.Pdf.PdfSignatureBuilder) properties allow you to specify the signer’s name, location, contact information, and the reason for signing.  

To sign the exported document, call the [PdfDocumentSigner.SaveDocument](https://docs.devexpress.com/OfficeFileAPI/DevExpress.Pdf.PdfDocumentSigner.SaveDocument(System.String-DevExpress.Pdf.PdfSignatureBuilder--)) method and pass the created [PdfSignatureBuilder](https://docs.devexpress.com/OfficeFileAPI/DevExpress.Pdf.PdfSignatureBuilder) object as a parameter.

## Files to Review

- [CustomViewerOperationLogger.cs](./SignPdfDocumentExample/Services/CustomViewerOperationLogger.cs)
- [CustomPdfSignatureOptionsProvider.cs](./SignPdfDocumentExample/Services/CustomPdfSignatureOptionsProvider.cs)
- [Startup.cs](./SignPdfDocumentExample/Startup.cs#L21)

## Documentation

- [Print and Export Reports in ASP.NET Core Applications](https://docs.devexpress.com/XtraReports/401841/web-reporting/asp-net-core-reporting/print-and-export-reports-in-asp-net-core-application)
- [Export to PDF (Reporting)](https://docs.devexpress.com/XtraReports/2574/detailed-guide-to-devexpress-reporting/store-and-distribute-reports/export-reports/export-to-pdf)
- [Sign PDF Documents (PDF Document API)](https://docs.devexpress.com/OfficeFileAPI/114623/pdf-document-api/document-security/sign-documents)

## More Examples

- [PDF Document API - Apply Multiple Signatures](https://github.com/DevExpress-Examples/pdf-document-api-multiple-signatures)
- [PDF Document API - Add a Visual Signature to a PDF Document](https://github.com/DevExpress-Examples/pdf-document-api-add-visual-signature-to-pdf-document)

