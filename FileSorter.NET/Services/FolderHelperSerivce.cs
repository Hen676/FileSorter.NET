using System.Collections.Generic;

namespace FileSorter.NET.Services
{
    public class FolderHelperSerivce : IFolderHelperService
    {
        // All keys need to be upper case
        private static readonly Dictionary<string, string> directory = new() {
            { "MKV", "Videos" }, //Videos
            { "MP4", "Videos" },
            { "AVI", "Videos" },
            { "MOV", "Videos" },
            { "WAV", "Videos" },
            { "WMV", "Videos" },

            { "PDF", "Documents" }, //Documents
            { "DOC", "Documents" }, // -Miccrsoft Office
            { "DOCX", "Documents" },
            { "PPT", "Documents" },
            { "PPTX", "Documents" },
            { "PUB", "Documents" },
            { "PUBX", "Documents" },
            { "XLS", "Documents" },
            { "XLSX", "Documents" },
            { "ODT", "Documents" }, // -Open Office
            { "ODS", "Documents" },
            { "ODP", "Documents" },
            { "ODF", "Documents" },

            { "ZIP", "Archives" }, //Archives
            { "7Z", "Archives" },
            { "ARCHIVE", "Archives" },

            { "JPG", "Images" }, //Images
            { "JPEG", "Images" },
            { "GIF", "Images" },
            { "PNG", "Images" },
            { "XCF", "Images" },
            { "SVG", "Images" },
            { "JFIF", "Images" },
            { "ICO", "Images" },

            { "MP3", "Music" }, //Music

            { "CSV", "Data" }, //Data
            { "JSON", "Data" },
            { "TXT", "Data" },

            { "FBX", "Models" }, //Models
            { "STL", "Models" }
        };

        public string GetFolder(bool customFolder, string extenstion)
        {
            extenstion = extenstion.Replace(".", string.Empty).ToUpperInvariant();

            if (extenstion == string.Empty)
                extenstion = "FILE";
            if (customFolder && directory.ContainsKey(extenstion))
                return directory[extenstion];
            return extenstion;
        }
    }
}
