# PDF Automation in .NET with IronPDF

## Overview

This project automates PDF report generation using **.NET** and **IronPDF**.
It generates **summary and detailed PDFs** from HTML templates, adds **watermarks**, merges multiple PDFs, and creates **PNG previews** of each page.

---

## Features

* Generate **Summary PDF** with key metrics
* Generate **Detailed PDF** with product performance
* Add **CONFIDENTIAL watermark**
* Merge PDFs into a **final report**
* Rasterize PDF pages into **PNG images**

---

## Project Structure

```
PdfAutomationDemo/
 ├── Templates/
 │    └── MonthlySummary.html
 ├── Output/
 ├── Program.cs
 ├── SummaryPdfGenerator.cs
 ├── DetailedPdfGenerator.cs
 ├── PdfMerger.cs
 └── PdfHelpers.cs
```

---

## Setup

1. Clone the repository:

```bash
git clone https://github.com/0samaHaider/iron-pdf-report-pipeline.git
cd YOUR-REPO
```

2. Install IronPDF:

```bash
dotnet add package IronPdf
```

3. Ensure the `Templates/MonthlySummary.html` file exists.

4. Run the application:

```bash
dotnet run
```

Output PDFs will be generated in **Output/** and images in **Output/Images/**.

---

If you want, I can also create an **even shorter version**, like 5–6 lines, perfect for GitHub’s first impression. Do you want me to do that?
