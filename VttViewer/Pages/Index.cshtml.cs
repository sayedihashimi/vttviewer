using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace VttViewer.Pages {
    public class IndexModel : PageModel {
        public IndexModel(IHostingEnvironment hostingEnvironment, IVttFiles vttFiles) {
            HostingEnvironment = hostingEnvironment;
            VttFilesHelper = vttFiles;
        }

        private IHostingEnvironment HostingEnvironment { get; set; }

        private IVttFiles VttFilesHelper { get; set; }

        [Required]
        [Display(Name = "VTT File")]
        public IFormFile VttFile { get; set; }

        public void OnGet() {
        }

        public async Task<IActionResult> OnPostAsync() {
            // Perform an initial check to catch FileUpload class attribute violations.
            if (!ModelState.IsValid) {
                return Page();
            }

            var filepath = System.IO.Path.GetTempFileName();

            using (var fileStream = new FileStream(filepath, FileMode.Create)) {
                await VttFile.CopyToAsync(fileStream);
            }

            if (System.IO.File.Exists(filepath)) {
                var guid = System.Guid.NewGuid().ToString();
                string destFilename = $"{guid}.vtt";
                var destFilepath = Path.Combine(VttFilesHelper.GetVttFolderPath(), destFilename);

                System.IO.File.Move(filepath, destFilepath);
                return RedirectToPage("ViewVttFile", new { filename = guid });
            }
            else {
                throw new FileUploadException();
            }
        }

    }
}