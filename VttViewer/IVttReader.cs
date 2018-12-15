using System.Collections.Generic;
using System.IO;
using System.Text;
using SubtitlesParser.Classes;

namespace VttViewer {
    public interface IVttReader {
        List<SubtitleItem> GetSubtitlesFromFile(string filepath);
        List<SubtitleItem> GetSubtitlesFromStream(Stream vttStream, Encoding encoding);
    }
}