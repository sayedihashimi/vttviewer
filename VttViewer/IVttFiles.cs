using System.Collections.Generic;

namespace VttViewer {
    public interface IVttFiles {
        string GetFullFilepath(string filename);
        List<string> GetVttFiles();
        string GetVttFolderPath();
    }
}