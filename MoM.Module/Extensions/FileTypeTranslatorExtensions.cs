using System.Collections.Generic;
using System.Linq;

namespace MoM.Module.Extensions
{
    public static class FileTypeTranslatorExtensions
    {
        public static string GetFileExtensionIcon(string fileExtension)
        {
            Dictionary<string, string> extensions = new Dictionary<string, string>
            {
                //images
                { ".jpg", "fa fa-file-image-o" },
                { ".jpeg", "fa fa-file-image-o" },
                { ".png", "fa fa-file-image-o" },
                { ".ico", "fa fa-file-image-o" },
                { ".gif", "fa fa-file-image-o" },
                { ".bmp", "fa fa-file-image-o" },

                //archives
                { ".zip", "fa fa-file-archive-o" },
                { ".rar", "fa fa-file-archive-o" },
                { ".7z", "fa fa-file-archive-o" },

                //audio
                { ".mp3", "fa fa-file-audio-o" },
                { ".wav", "fa fa-file-audio-o" },

                //video
                { ".mov", "fa fa-file-video-o" },
                { ".mpg", "fa fa-file-video-o" },
                { ".mpeg", "fa fa-file-video-o" },
                { ".wmv", "fa fa-file-video-o" },

                //documents
                { ".pdf", "fa fa-file-pdf-o" },
                { ".txt", "fa fa-file-text-o" },
                { ".doc", "fa fa-file-word-o" },
                { ".docx", "fa fa-file-word-o" },
                { ".xls", "fa fa-file-excel-o" },
                { ".xlsx", "fa fa-file-excel-o"},
                { ".ppt", "fa fa-file-powerpoint-o" },
                { ".pptx", "fa fa-file-powerpoint-o"},

                //code
                { ".htm", "fa fa-file-code-o" },
                { ".html", "fa fa-file-code-o" },
            };
            return extensions.FirstOrDefault(x => x.Key == fileExtension.ToLower()).Value;
        }
    }
}
