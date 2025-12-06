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

## Setup

1. Clone the repository:

```bash
git clone https://github.com/0samaHaider/iron-pdf-report-pipeline.git
cd iron-pdf-report-pipeline
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
