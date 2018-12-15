using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SubtitlesParser.Classes;
namespace VttViewer {
    public class VttReader : IVttReader {

        public List<SubtitleItem>GetSubtitlesFromFile(string filepath) {
            if (!File.Exists(filepath)) {
                throw new FileNotFoundException("VTT file not found", filepath);
            }

            var fileEncoding = new EncodingHelper().GetEncoding(filepath);
            using (var fileStream = System.IO.File.OpenRead(filepath)) {
                return GetSubtitlesFromStream(fileStream, fileEncoding);
            }
        }

        public List<SubtitleItem> GetSubtitlesFromStream(Stream vttStream, Encoding encoding) {
            var parser = new SubtitlesParser.Classes.Parsers.VttParser();
            return parser.ParseStream(vttStream, encoding);
        }
    }
}
