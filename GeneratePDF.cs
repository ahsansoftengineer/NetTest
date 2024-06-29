
using sharpPDF;
using sharpPDF.Enumerators;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace NetTest.GeneratePDF
{
    public static class GenerateTable
    {
        public static predefinedFont fontBold = predefinedFont.csHelveticaBold;
        public static predefinedFont font = predefinedFont.csHelvetica;
        public static predefinedAlignment align = predefinedAlignment.csCenter;
        public static void PrintTable()
        {
            // Create a new PDF document
            pdfDocument myDoc = new pdfDocument("My PDF Document", "Me");

            // Add a new page to the document
            pdfPage myPage = myDoc.addPage();
            // Add main title
            int yAxis = 750;

            myPage.AddImage(50, yAxis);
            myPage.addText("Muhammad Ahsan Moin", 200, yAxis -= 25, fontBold, 22, predefinedColor.csBlue);
            myPage.addText("NC - Main Inventory", 50, yAxis -= 25, fontBold, 16);

            // Add first section
            myPage.AddSection(                "Natural Calm Original 16Oz",
                "SKU_NEW_3-S",
                "salman barcode",
                "60.00",
                "100.00",
                       new string[,] { { "S1R1B2", "1142533-S", "2025-06-30", "12.02", "GROUP C", "48", "48" } }, yAxis -= 30);

            // Add second section
            myPage.AddSection("Multi Vitamin 110", "MSTESTSKU110-S", "3245667", "50.55", "62.55",
                       new string[,] { { "S2R3B4", "MSLOT111-S", "2025-06-12", "11.43", "GROUP D", "10", "20" } }, yAxis -= 120);

            // Save the document
            myDoc.createPDF(@"./file.pdf");

            Console.WriteLine("PDF created successfully!");
        }

        private static void AddSection(this pdfPage page, string title, string sku, string barcode, string cost, string listPrice, string[,] tableData, int startY)
        {
            page.addText(title, 50, startY, fontBold, 12);

            // Add SKU and Barcode
            page.addText("SKU", 50, startY - 20, fontBold, 10);
            page.addText(sku, 100, startY - 20, font, 10);
            page.addText("Barcode", 50, startY - 40, fontBold, 10);
            page.addText(barcode, 100, startY - 40, font, 10);

            // Add Cost and List Price
            page.addText("Cost", 450, startY - 20, fontBold, 10);
            page.addText(cost, 500, startY - 20, font, 10);
            page.addText("List Price", 450, startY - 40, fontBold, 10);
            page.addText(listPrice, 500, startY - 40, font, 10);

            // Create table
            pdfTable table = new pdfTable();

            table.borderSize = 1;
            table.AddHeader("Location", 60);
            table.AddHeader("Lot", 100);
            table.AddHeader("Expiry", 80);
            table.AddHeader("R.MTH", 60);
            table.AddHeader("PG", 80);
            table.AddHeader("OH QTY", 60);
            table.AddHeader("AVL QTY", 60);

            for (int i = 0; i < tableData.GetLength(0); i++)
            {
                pdfTableRow row = table.createRow();
                for (int j = 0; j < tableData.GetLength(1); j++)
                {
                    row[j].columnValue = tableData[i, j];
                }
                table.addRow(row);
            }

            page.addTable(table, 50, startY - 50);
        }

        private static void AddHeader(this pdfTable table, string Value, int columnAlign)
        {
            table.tableHeader.addColumn(new pdfTableColumn(Value, align, columnAlign));
        }

        public static void AddImage(this pdfPage myPage, int x, int y)
        {

            string imagePath = "user.jpg";

            // Convert the image to a byte array
            Stream imgData = ConvertImageToByteArray(imagePath);

            // Add image to the top left corner
            if (imgData != null)
            {
                myPage.addImage(imgData, x, y, 100, 50); // 100 width, 50 height
            }
        }

      
        private static Stream ConvertImageToByteArray(string imagePath)
        {
            using var image = Image.Load<Rgba32>(imagePath);
            using var ms = new MemoryStream();
            image.SaveAsJpeg(ms); // or image.SaveAsPng(ms);
            return ms;
        }
        // private static byte[] ConvertImageToByteArray(string imagePath)
        // {
        //     using var image = Image.Load<Rgba32>(imagePath);
        //     using var ms = new MemoryStream();
        //     image.SaveAsJpeg(ms); // or image.SaveAsPng(ms);
        //     return ms.ToArray();
        // }

    }

}
