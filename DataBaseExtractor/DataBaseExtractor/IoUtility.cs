using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseExtractor
{
    internal class IoUtility
    {
        public void SaveFile(string directoryName, string? fileName, string content) {
            if (string.IsNullOrEmpty(fileName)) { return; }

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var outputFilePath = Path.Combine(documentsPath,directoryName, fileName);

            if (!Directory.Exists(Path.GetDirectoryName(outputFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));
            }

            using (FileStream fs = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
            {
                writer.Write(content);
            }

        }
    }
}
