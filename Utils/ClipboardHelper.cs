using System.Diagnostics;

namespace GLFE.ClipboardHelper
{

    class ClipboardUtils
    {
        public static bool SetterClipboard(string res)
        {
            try
            {
                Process ProcessVar = new Process
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

                ProcessVar.Start();
                ProcessVar.StandardInput.Write(res);
                ProcessVar.StandardInput.Close();
                ProcessVar.WaitForExit();
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
                Process ProcessVar = new Process
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

                ProcessVar.Start();
                string res = ProcessVar.StandardOutput.ReadToEnd();
                ProcessVar.WaitForExit();

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
