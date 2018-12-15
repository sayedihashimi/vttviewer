using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
namespace VttViewer {
    public class VttFiles : IVttFiles {
        public VttFiles(IHostingEnvironment hostingEnvironment) {
            HostingEnvironment = hostingEnvironment;
        }

        private IHostingEnvironment HostingEnvironment { get; set; }

        public string GetFullFilepath(string filename) {
            return Path.Combine(GetVttFolderPath(), filename);
        }

        public List<string> GetVttFiles() {
            var result = Directory.GetFiles(GetVttFolderPath(), "*.vtt");
            if (result != null) {
                return result.ToList();
            }

            return null;                    
        }

        public string GetVttFolderPath() {
            return Path.Combine(HostingEnvironment.WebRootPath, "vtt-files");
        }
    }
}
