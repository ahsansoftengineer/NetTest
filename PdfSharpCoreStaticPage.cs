using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using System;
using System.IO;

public static class PdfSharpCoreStaticPage
{

  private static string fF = "Arial";
  private static XFont h1 = new XFont(fF, 20, XFontStyle.Bold);
  private static XFont h2 = new XFont(fF, 16, XFontStyle.Bold);
  private static XFont h3 = new XFont(fF, 14, XFontStyle.Bold);
  private static XFont p = new XFont(fF, 12, XFontStyle.Regular);

  private static double tableStartX = 50;
  private static double tableStartY = 250;
  private static double colWidth = 70;
  private static double rowHeight = 20;
  private static PdfDocument doc = new PdfDocument();

  private static PdfPage page = doc.AddPage();
  public static MemoryStream GeneratePdf()
  {
    var gfx = XGraphics.FromPdfPage(page);
    gfx.MUH = PdfFontEncoding.Unicode;
    gfx.SmoothingMode = XSmoothingMode.HighQuality;

    // Define fonts

    // Add image
    
    XImage image = XImage.FromFile("Assets/user.jpg"); // Replace with your image path
    gfx.DrawImage(image, 50, 50, 100, 150);
    
    gfx.Headingz("Duplicate Stock Items Report", h1, XStringFormats.TopCenter);
    gfx.Headingz("NC - Main Inventory", h2, XStringFormats.TopLeft);
    string[] headers = { "Location", "Lot", "Expiry", "R.MTH", "PG", "OH QTY", "AVL QTY" };

    foreach (var item in headers.Select((value, i) => new { i, value }))
    {
      gfx.DrawCell(tableStartX + item.i * colWidth, tableStartY, colWidth, rowHeight, item.value, h3);
    }
    string[,] rows = new string[,]
    {
        { "SOR1B2", "290323-S", "2026-04-08", "21.29", "GROUP A", "25", "25" },
         { "SOR1B2", "290323-S", "2026-04-08", "21.29", "GROUP A", "25", "25" }
    };
    for (int row = 0; row < rows.GetLength(0); row++)
    {
      for (int col = 0; col < rows.GetLength(1); col++)
      {
        gfx.DrawCell(tableStartX + col * colWidth, tableStartY + (row + 1) * rowHeight, colWidth, rowHeight, rows[row, col], p);
      }
    }




    var ms = new MemoryStream();
    doc.Save(ms, false);
    ms.Seek(0, SeekOrigin.Begin);
    using (var fileStream = new FileStream("file2.pdf", FileMode.Create, FileAccess.Write))
    {
      ms.CopyTo(fileStream);
    }
    return ms;
  }
  // Helper method to draw a table cell with borders
  private static void DrawCell(this XGraphics gfx, double x, double y, double width, double height, string text, XFont font)
  {
    gfx.DrawRectangle(XPens.Black, x, y, width, height);
    gfx.DrawString(text, font, XBrushes.Black, new XRect(x, y, width, height), XStringFormats.Center);
  }
  private static void Headingz(this XGraphics gfx, string Heading, XFont font, XStringFormat align)
  {
    gfx.DrawString(Heading, font, XBrushes.Black,
    new XRect(50, 100, page.Width, 0), align);
  }
  // private static XGraphics AddTableAllHeading(this XGraphics gfx)
  // {
  //   gfx
  //     .DrawTableHeading("Location")
  //     .DrawTableHeading("Lot")
  //     .DrawTableHeading("Expiry")
  //     .DrawTableHeading("R.MTH")
  //     .DrawTableHeading("PG")
  //     .DrawTableHeading("OH QTY")
  //     .DrawTableHeading("AVL QTY");
  //   return gfx;
  // }

  // private static XGraphics DrawTableHeading(this XGraphics gfx, string Name)
  // {
  //   gfx.DrawString(
  //    Name, h3, XBrushes.Black,
  //    new XRect(
  //       tableStartX,
  //       tableStartY,
  //       colWidth,
  //       rowHeight
  //     ),
  //    XStringFormats.Center);
  //   return gfx;
  // }
}
