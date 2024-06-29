using System;
using Aspose.Words;
public static class GenerateDocToPDF
{
  public static void Generate()
  {
    // Create a Document object
    Document doc = new Document("pdf/Sample2.docx");


    // Save the document to PDF
    doc.Save("pdf/Sample2.pdf", SaveFormat.Pdf);

  }
}