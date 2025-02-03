using DataBaseExtractor;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

class Program
{
    static void Main()
    {
        try
        {
            DbUtility dbUtility = new DbUtility();
            string umlContent = dbUtility.GetUmlString();

            string outputDirectory = "UML_Outputs";
            string? fileName = ConfigurationManager.AppSettings["FileName"];

            IoUtility ioUtility = new IoUtility();
            ioUtility.SaveFile(outputDirectory, fileName, umlContent);

            Console.WriteLine($"UMLファイルが生成されました: {Path.Combine(outputDirectory,string.IsNullOrEmpty(fileName) ?string.Empty: fileName)}");
        } catch (Exception ex) {
            Console.WriteLine($"エラーが発生しました: {ex.Message}");
        }

    }
}
