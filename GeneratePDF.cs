
using sharpPDF;
using sharpPDF.Enumerators;
namespace NetTest.GeneratePDF
{
    public class GenerateTable
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
            myPage.addText("NC - Main Inventory", 50, 800, fontBold, 16);

            // Add first section
            AddSection(myPage, "Natural Calm Original 16Oz", "SKU_NEW_3-S", "salman barcode", "60.00", "100.00",
                       new string[,] { { "S1R1B2", "1142533-S", "2025-06-30", "12.02", "GROUP C", "48", "48" } }, 750);

            // Add second section
            AddSection(myPage, "Multi Vitamin 110", "MSTESTSKU110-S", "3245667", "50.55", "62.55",
                       new string[,] { { "S2R3B4", "MSLOT111-S", "2025-06-12", "11.43", "GROUP D", "10", "20" } }, 600);

            // Save the document
            myDoc.createPDF(@"./file.pdf");

            Console.WriteLine("PDF created successfully!");
        }

        private static void AddSection(pdfPage page, string title, string sku, string barcode, string cost, string listPrice, string[,] tableData, int startY)
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
            table.tableHeader.addColumn(new pdfTableColumn("Location", align, 60));
            table.tableHeader.addColumn(new pdfTableColumn("Lot", align, 100));
            table.tableHeader.addColumn(new pdfTableColumn("Expiry", align, 80));
            table.tableHeader.addColumn(new pdfTableColumn("R.MTH", align, 60));
            table.tableHeader.addColumn(new pdfTableColumn("PG", align, 80));
            table.tableHeader.addColumn(new pdfTableColumn("OH QTY", align, 60));
            table.tableHeader.addColumn(new pdfTableColumn("AVL QTY", align, 60));

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
        // public static void PrintTable()
        // {
        //    // Create a new PDF document
        //     pdfDocument myDoc = new pdfDocument("My PDF Document", "Me");

        //     // Add a new page to the document
        //     pdfPage myPage = myDoc.addPage();

        //     // Add text to the page
        //     myPage.addText("Hello, PDF!", 50, 700, sharpPDF.Enumerators.predefinedFont.csHelvetica, 12);

        //     // Add a table to the page
        //     pdfTable myTable = new pdfTable();
        //     myTable.borderSize = 1;
        //     // myTable.coordX = 50;
        //     // myTable.coordY = 650;
        //     myTable.tableHeader.addColumn(new pdfTableColumn("Column 1", sharpPDF.Enumerators.predefinedAlignment.csCenter, 100));
        //     myTable.tableHeader.addColumn(new pdfTableColumn("Column 2", sharpPDF.Enumerators.predefinedAlignment.csCenter, 100));
        //     pdfTableRow myRow = myTable.createRow();
        //     myRow[0].columnValue = "Value 1";
        //     myRow[1].columnValue = "Value 2";
        //     myTable.addRow(myRow);

        //     myPage.addTable(myTable, 50, 650);

        //     // Save the document
        //     myDoc.createPDF(@"./file.pdf");

        //     Console.WriteLine("PDF created successfully!");
        // }
    }
}
