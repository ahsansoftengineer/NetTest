using NPOI.XWPF.Model;
using NPOI.XWPF.UserModel;

public static class GenerateNPOI
{
  public static void Generate()
  {
    // Create a new document
    XWPFDocument document = new XWPFDocument();
    SetMargin(document);
    AddImage(document);
    AddPagination(document);

    // Add a heading
    AddHeading(document, "Demo Receipt", 20, ParagraphAlignment.CENTER);
    AddHeading(document, "Muhammad Ahsan", 16);
    AddPara(document, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed accumsan tincidunt lacus, at scelerisque ex blandit nec.");

    AddTable(document);


    // Save the document
    FileStream stream = new FileStream("pdf/Sample2.docx", FileMode.Create, FileAccess.Write);
    document.Write(stream);
    GenerateDocToPDF.Generate();
  
    Console.WriteLine("Word document created successfully.");
  }


  private static void AddPara(this XWPFDocument document, string Heading)
  {
    XWPFParagraph heading = document.CreateParagraph();
    XWPFRun headingRun = heading.CreateRun();
    headingRun.SetText(Heading);
    headingRun.IsBold = false;
    heading.Alignment = ParagraphAlignment.LEFT;
    headingRun.FontSize = 12;
  }
  private static void AddHeading(this XWPFDocument document, string Heading, int FontSize,
  ParagraphAlignment align = ParagraphAlignment.LEFT)
  {
    XWPFParagraph heading = document.CreateParagraph();
    XWPFRun headingRun = heading.CreateRun();
    headingRun.SetText(Heading);
    headingRun.IsBold = true;
    heading.Alignment = align;
    headingRun.FontSize = FontSize;
  }

  private static void AddTable(XWPFDocument document)
  {
    XWPFTable table = document.CreateTable(3, 6);

    table.Width = 60000000;

    table.SetCellMargins(5, 10, 5, 10);
    // Fill table with data
    for (int row = 0; row < 3; row++)
    {
      for (int col = 0; col < 6; col++)
      {
        var rowz = table.GetRow(row);
        rowz.Height = 50;
        var cellz = rowz.GetCell(col);
        cellz.SetText($"Row {row + 1}, Col {col + 1}");
      }
    }
  }

  private static void SetMargin(XWPFDocument document)
  {
    var sectPr = document.Document.body.AddNewSectPr();
    var pageMar = sectPr.AddPageMar();
    pageMar.top = 700;    // 1440 = 1 inch
    pageMar.bottom = 700;
    pageMar.left = 700;
    pageMar.right = 1000;
  }

  private static void AddPagination(XWPFDocument document)
  {
    // Setting Pagging
    // Add page numbering in the footer
    XWPFHeaderFooterPolicy policy = new XWPFHeaderFooterPolicy(document);
    XWPFFooter footer = policy.CreateFooter(XWPFHeaderFooterPolicy.DEFAULT);
    XWPFParagraph footerParagraph = footer.CreateParagraph();
    footerParagraph.Alignment = ParagraphAlignment.CENTER;
    XWPFRun footerRun = footerParagraph.CreateRun();
    footerRun.SetText("Page ");
    // footerRun.AddField(FieldType.PAGE);
    footerRun.SetText(" of ");
    // footerRun.AddField(FieldType.NUM_PAGES);
  }

  private static void AddImage(XWPFDocument document)
  {
    string imagePath = "Assets/user.jpg";
    FileStream imageData = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
    string pictureData = document.AddPictureData(imageData, (int)PictureType.JPEG);
    imageData.Close();

    XWPFParagraph imageParagraph = document.CreateParagraph();
    XWPFRun imageRun = imageParagraph.CreateRun();
    imageRun.AddPicture(new FileStream(imagePath, FileMode.Open, FileAccess.Read),
                        (int)PictureType.JPEG,
                        imagePath,
                        1400000, // width in EMU (English Metric Unit)
                        2000000); // height in EMU
  }
}
