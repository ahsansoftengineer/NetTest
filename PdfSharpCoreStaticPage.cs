using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using System;
using System.IO;

public static class PdfSharpCoreStaticPage
{
  public static MemoryStream GeneratePdf()
  {
    var document = new PdfDocument();
    var page = document.AddPage();
    var gfx = XGraphics.FromPdfPage(page);

    // Define fonts
    var font = new XFont("Arial", 12, XFontStyle.Regular);
    var fontBold = new XFont("Arial", 12, XFontStyle.Bold);

    // Add heading
    gfx.DrawString("Sample Report", fontBold, XBrushes.Black,
        new XRect(0, 20, page.Width, 0), XStringFormats.TopCenter);

    // Add image
    XImage image = XImage.FromFile("Assets/user.jpg"); // Replace with your image path
    gfx.DrawImage(image, 50, 50, 200, 150);

    // Add table headers
    gfx.DrawString("Name", fontBold, XBrushes.Black, new XRect(50, 250, 100, 20), XStringFormats.TopLeft);
    gfx.DrawString("Age", fontBold, XBrushes.Black, new XRect(150, 250, 100, 20), XStringFormats.TopLeft);
    gfx.DrawString("Country", fontBold, XBrushes.Black, new XRect(250, 250, 100, 20), XStringFormats.TopLeft);

    // Add table rows
    DrawTableRow(gfx, font, "John Doe", "30", "USA", 50, 270, page.Width);
    DrawTableRow(gfx, font, "Jane Smith", "25", "Canada", 50, 290, page.Width);
    DrawTableRow(gfx, font, "Ahmed Khan", "35", "India", 50, 310, page.Width);

    var ms = new MemoryStream();
    document.Save(ms, false);
    ms.Seek(0, SeekOrigin.Begin);
    using (var fileStream = new FileStream("file2.pdf", FileMode.Create, FileAccess.Write))
    {
      ms.CopyTo(fileStream);
    }
    return ms;
  }
  // Helper method to draw table row
  private static void DrawTableRow(XGraphics gfx, XFont font, string col1, string col2, string col3, double x, double y, double pageWidth)
  {
    gfx.DrawString(col1, font, XBrushes.Black, new XRect(x, y, 100, 20), XStringFormats.TopLeft);
    gfx.DrawString(col2, font, XBrushes.Black, new XRect(x + 100, y, 100, 20), XStringFormats.TopLeft);
    gfx.DrawString(col3, font, XBrushes.Black, new XRect(x + 200, y, 100, 20), XStringFormats.TopLeft);
  }
}
