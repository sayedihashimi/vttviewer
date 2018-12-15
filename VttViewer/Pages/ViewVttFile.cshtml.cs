using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SubtitlesParser.Classes;
using System.Text;

namespace VttViewer.Pages {

    public class ViewVttFileModel : PageModel {
        [BindProperty]
        public string Filename { get; set; }

        private IVttFiles VttFiles { get; set; }
        private IVttReader VttReader { get; set; }

        public List<SubtitleItem> Subtitles { get; set; } = new List<SubtitleItem>();

        public ViewVttFileModel(IVttFiles vttFiles, IVttReader vttReader) {
            VttFiles = vttFiles;
            VttReader = vttReader;
        }

        public void OnGet(string filename) {
            if (!string.IsNullOrEmpty(filename)) {
                filename = filename.Trim();
                if (!filename.EndsWith(".vtt", StringComparison.OrdinalIgnoreCase)) {
                    filename += ".vtt";
                }
            }

            Filename = filename;
            string fullpath = VttFiles.GetFullFilepath(Filename);
            Subtitles = VttReader.GetSubtitlesFromFile(fullpath);
        }

        public string GetStringFor(SubtitleItem item) {
            var builder = new StringBuilder();
            if(item != null && item.Lines != null && item.Lines.Count > 0) {
                foreach(var line in item.Lines) {
                    builder.Append(line);
                    builder.Append(" ");
                }
                // remove the last " "
                builder.Remove(builder.Length - 1, 1);
            }
            return builder.ToString();
        }

        public string GetTimeStringFor(SubtitleItem item) {
            var startTimeStr = (item.StartTime/1000).ToString("#.#s");
            var endTimeStr = (item.EndTime / 1000).ToString("#.#s");
            return $"{startTimeStr} → {endTimeStr}";
        }
    }
}
