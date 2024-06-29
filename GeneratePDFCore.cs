using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

public class PdfService
{
    public MemoryStream GeneratePdf()
    {
        var document = new PdfDocument();
        var page = document.AddPage();
        var graphics = XGraphics.FromPdfPage(page);
        var font = new XFont("Arial", 20, XFontStyle.Bold);

        // Add Heading
        graphics.DrawString("Sample Report", font, XBrushes.Black,
            new XRect(0, 20, page.Width.Point, page.Height.Point),
            XStringFormats.Center);

        // Add Image
        XImage image = XImage.FromFile("user.jpg"); // Replace with your image path
        graphics.DrawImage(image, 50, 50, 200, 150);

        var ms = new MemoryStream();
        document.Save(ms, false);
        ms.Seek(0, SeekOrigin.Begin);
        using (var fileStream = new FileStream("file2.pdf", FileMode.Create, FileAccess.Write))
        {
            ms.CopyTo(fileStream);
        }
        return ms;
    }
}
