using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Controllers
{
    public class CppCompilerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompileAndRun(string code)
        {
            string fileName = $"temp_{Guid.NewGuid().ToString().Substring(0, 8)}.cpp";
            string filePath = Path.Combine(Path.GetTempPath(), fileName);

            try
            {
                // Save the code to a temporary file
                await System.IO.File.WriteAllTextAsync(filePath, code);

                // Compile the code
                Process compileProcess = Process.Start(new ProcessStartInfo
                {
                    FileName = "g++",
                    Arguments = $"{filePath} -o {Path.ChangeExtension(filePath, ".exe")}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                });

                compileProcess.WaitForExit();

                // Check if compilation was successful
                if (compileProcess.ExitCode != 0)
                {
                    string compileError = compileProcess.StandardError.ReadToEnd();
                    return BadRequest($"Compilation error: {compileError}");
                }

                // Execute the compiled program
                Process executeProcess = Process.Start(new ProcessStartInfo
                {
                    FileName = Path.ChangeExtension(filePath, ".exe"),
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                });

                string output = await executeProcess.StandardOutput.ReadToEndAsync();

                return Ok(output);
            }
            finally
            {
                // Clean up temporary files
                System.IO.File.Delete(filePath);
                System.IO.File.Delete(Path.ChangeExtension(filePath, ".exe"));
            }
        }


    }
}
