using System.Text;

class Program
{
    static void Main()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        string projectRoot = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
        string templatePath = Path.Combine(projectRoot, "Templates", "MonthlySummary.html");

        // Read and populate HTML template
        string html = File.ReadAllText(templatePath)
            .Replace("{{month}}", "January 2025")
            .Replace("{{orders}}", "1,542")
            .Replace("{{revenue}}", "98,230")
            .Replace("{{topProduct}}", "Noise Cancelling Headphones");

        // Create watermark and inject into HTML
        string watermarkText = "CONFIDENTIAL";
        string watermarkMarkup = CreateWatermarkMarkup(watermarkText);
        html = InjectWatermark(html, watermarkMarkup);

        // Prepare output directories
        string outputDir = Path.Combine(projectRoot, "Output");
        Directory.CreateDirectory(outputDir);

        var renderer = new ChromePdfRenderer();

        // Generate Summary PDF
        string summaryPath = Path.Combine(outputDir, $"Summary_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
        var summaryPdf = renderer.RenderHtmlAsPdf(html);
        summaryPdf.SaveAs(summaryPath);
        Console.WriteLine($"Summary PDF generated: {summaryPath}");

        // Generate Details PDF
        string detailHtml = @"
                                <html>
                                <head>
                                <style>
                                table { border-collapse: collapse; width: 100%; }
                                th, td { border: 1px solid #555; padding: 8px; }
                                th { background: #f2f2f2; }
                                </style>
                                </head>
                                <body>
                                <h2>Detailed Product Performance</h2>
                                <table>
                                <tr><th>Product</th><th>Orders</th><th>Revenue</th></tr>
                                <tr><td>Headphones</td><td>540</td><td>$32,000</td></tr>
                                <tr><td>Smart Watch</td><td>420</td><td>$21,600</td></tr>
                                <tr><td>Keyboard</td><td>320</td><td>$12,900</td></tr>
                                </table>
                                </body>
                                </html>";

        detailHtml = InjectWatermark(detailHtml, watermarkMarkup);
        string detailsPath = Path.Combine(outputDir, $"Details_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
        var detailsPdf = renderer.RenderHtmlAsPdf(detailHtml);
        detailsPdf.SaveAs(detailsPath);
        Console.WriteLine($"Details PDF generated: {detailsPath}");

        // Merge PDFs
        var mergedPdf = PdfDocument.Merge(summaryPdf, detailsPdf);
        string mergedPath = Path.Combine(outputDir, $"FinalReport_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
        mergedPdf.SaveAs(mergedPath);
        Console.WriteLine($"Merged report created: {mergedPath}");

        // Extract images from merged PDF
        string imagesDir = Path.Combine(outputDir, "Images");
        Directory.CreateDirectory(imagesDir);
        string imagePattern = Path.Combine(imagesDir, $"FinalReport_Page_*.png");
        mergedPdf.RasterizeToImageFiles(imagePattern);
        Console.WriteLine($"Images for merged PDF saved at: {imagesDir}");
    }

    static string CreateWatermarkMarkup(string text)
    {
        var sb = new StringBuilder();
        sb.AppendLine("<style>");
        sb.AppendLine(".pdf-watermark {");
        sb.AppendLine("  position: fixed;");
        sb.AppendLine("  top: 50%;");
        sb.AppendLine("  left: 50%;");
        sb.AppendLine("  transform: translate(-50%, -50%) rotate(-45deg);");
        sb.AppendLine("  font-size: 64px;");
        sb.AppendLine("  color: rgba(0,0,0,0.08);");
        sb.AppendLine("  z-index: 9999;");
        sb.AppendLine("  pointer-events: none;");
        sb.AppendLine("  white-space: nowrap;");
        sb.AppendLine("}");
        sb.AppendLine("@media print { .pdf-watermark { display: block; } }");
        sb.AppendLine("</style>");
        sb.AppendLine($"<div class=\"pdf-watermark\">{System.Net.WebUtility.HtmlEncode(text)}</div>");
        return sb.ToString();
    }

    static string InjectWatermark(string html, string watermarkMarkup)
    {
        if (string.IsNullOrWhiteSpace(html)) return watermarkMarkup;

        string htmlLower = html.ToLowerInvariant();
        if (htmlLower.Contains("<head"))
        {
            int headClose = html.IndexOf("</head>", StringComparison.OrdinalIgnoreCase);
            if (headClose >= 0)
                return html.Insert(headClose, watermarkMarkup);
        }

        int bodyOpen = html.IndexOf("<body", StringComparison.OrdinalIgnoreCase);
        if (bodyOpen >= 0)
        {
            int bodyEnd = html.IndexOf('>', bodyOpen);
            if (bodyEnd >= 0)
                return html.Insert(bodyEnd + 1, watermarkMarkup);
        }

        return $"<html><head>{watermarkMarkup}</head><body>{html}</body></html>";
    }
}