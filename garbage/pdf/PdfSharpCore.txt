
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace NetTest.GeneratePDF
{
  public static class PdfSharpCoreStaticPage
  {
    public static XFont fontBold = new XFont("Helvetica", 12, XFontStyle.Bold);
    public static XFont font = new XFont("Helvetica", 10, XFontStyle.Regular);

    public static void PrintTable()
    {
      PdfDocument document = new PdfDocument();
      PdfPage page = document.AddPage();

      XGraphics gfx = XGraphics.FromPdfPage(page);
      gfx.MUH = PdfFontEncoding.Unicode;
      gfx.SmoothingMode = XSmoothingMode.HighQuality;
      XImage image = XImage.FromFile("Assets/user.jpg");
      gfx.DrawImage(image, 50, 50, 100, 150);

      int yOffset = 400;
      DrawText(gfx, "Muhammad Ahsan Moin", 200, yOffset += 25, fontBold, XBrushes.Blue);
      DrawText(gfx, "NC - Main Inventory", 50, yOffset += 25, fontBold, XBrushes.Black);

      DrawSection(gfx, "Natural Calm Original 16Oz", "SKU_NEW_3-S", "salman barcode", "60.00", "100.00",
          new string[,] { { "S1R1B2", "1142533-S", "2025-06-30", "12.02", "GROUP C", "48", "48" } }, yOffset += 30);

      DrawSection(gfx, "Multi Vitamin 110", "MSTESTSKU110-S", "3245667", "50.55", "62.55",
          new string[,] { { "S2R3B4", "MSLOT111-S", "2025-06-12", "11.43", "GROUP D", "10", "20" } }, yOffset += 120);

      string filePath = "./file1.pdf";
      document.Save(filePath);
      Console.WriteLine("PDF created successfully at: " + filePath);
    }

    private static void DrawSection(XGraphics gfx, string title, string sku, string barcode, string cost, string listPrice, string[,] tableData, int startY)
    {
      DrawText(gfx, title, 50, startY, fontBold, XBrushes.Black);

      DrawText(gfx, "SKU", 50, startY - 20, fontBold, XBrushes.Black);
      DrawText(gfx, sku, 100, startY - 20, font, XBrushes.Black);
      DrawText(gfx, "Barcode", 50, startY - 40, fontBold, XBrushes.Black);
      DrawText(gfx, barcode, 100, startY - 40, font, XBrushes.Black);

      DrawText(gfx, "Cost", 450, startY - 20, fontBold, XBrushes.Black);
      DrawText(gfx, cost, 500, startY - 20, font, XBrushes.Black);
      DrawText(gfx, "List Price", 450, startY - 40, fontBold, XBrushes.Black);
      DrawText(gfx, listPrice, 500, startY - 40, font, XBrushes.Black);

      DrawTable(gfx, tableData, 50, startY - 50);
    }

    private static void DrawTable(XGraphics gfx, string[,] tableData, int startX, int startY)
    {
      int rowHeight = 20;
      int columnWidth = 80;
      int rowCount = tableData.GetLength(0);
      int columnCount = tableData.GetLength(1);

      for (int i = 0; i < rowCount; i++)
      {
        for (int j = 0; j < columnCount; j++)
        {
          DrawText(gfx, tableData[i, j], startX + j * columnWidth, startY + i * rowHeight, font, XBrushes.Black);
        }
      }
    }

    private static void DrawText(XGraphics gfx, string text, int x, int y, XFont font, XBrush brush)
    {
      gfx.DrawString(text, font, brush, x, y);
    }
  }
}
