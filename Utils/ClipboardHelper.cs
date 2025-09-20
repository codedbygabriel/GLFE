using System.Diagnostics;

namespace GLFE.Utils
{
    static class ClipboardUtils
    {
        public static bool SetterClipboard(string res)
        {
            try
            {
                var processVar = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "xclip",
                        Arguments = "-selection clipboard",
                        RedirectStandardInput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                processVar.Start();
                processVar.StandardInput.Write(res);
                processVar.StandardInput.Close();
                processVar.WaitForExit();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("X-clip not installed or faulty configuration " + e);
                return false;
            }
        }

        public static string GetFromClipboard()
        {
            try
            {
                var processVar = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "xclip",
                        Arguments = "-o -selection clipboard",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                processVar.Start();
                string res = processVar.StandardOutput.ReadToEnd();
                processVar.WaitForExit();

                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine("X-clip not installed or faulty configuration " + e);
                return "";
            }
        }

    }
}
