using GLFE.ClipboardHelper;

namespace GLFE.DirectoryHelper
{
    struct Explorer
    {
        public List<string> FileList;
        public List<string> DirectoryList;

        public Explorer()
        {
            FileList = new List<string>();
            DirectoryList = new List<string>();

        }
    }

    class DirectoryRecover
    {
        static string _HOME_PATH = $"/home/{Environment.UserName}";
        static string _CURRENT_PATH = _HOME_PATH;
        static string _PREVIOUS_PATH = string.Empty;
        static bool _IS_FIRST_TIME = true;

        private Explorer ExplorerVar = new Explorer();

        public DirectoryRecover()
        {
            PrintHeader();
            InitExplorerList();
        }

        public void InitExplorerList(string path = "")
        {
            if (_IS_FIRST_TIME)
            {
                _IS_FIRST_TIME = false;
                FillStructure();
            }

            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
				_PREVIOUS_PATH = _CURRENT_PATH;
                _CURRENT_PATH = path;

                if (ExplorerVar.DirectoryList.Any())
                {
                    ExplorerVar.DirectoryList.Clear();
                }

                if (ExplorerVar.FileList.Any())
                {
                    ExplorerVar.FileList.Clear();
                }

                FillStructure();
            }
        }

        private void FillStructure()
        {
            foreach (var item in Directory.GetDirectories(_CURRENT_PATH)) ExplorerVar.DirectoryList.Add(item);
            foreach (var item in Directory.GetFiles(_CURRENT_PATH)) ExplorerVar.FileList.Add(item);
        }
        public void PrintHeader()
        {
            Console.Write($"{Environment.UserName}, Current Working At: {_CURRENT_PATH}\t");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[p - print DIR], ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[m - move to], ");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("[d - delete file|directory].");

            Console.ResetColor();
        }

        public void PrintExplorer()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < ExplorerVar.DirectoryList.Count(); i++)
                Console.WriteLine($"[{i + 1}]\t-\t{ExplorerVar.DirectoryList[i]}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < ExplorerVar.FileList.Count(); i++)
                Console.WriteLine($"[{ExplorerVar.DirectoryList.Count() + i + 1}]\t-\t{ExplorerVar.FileList[i]}");


            Console.ResetColor();
            SetToClipboard();
        }

        private void SetToClipboard()
        {
            string res = PathSelection();
            if (!res.Equals("err"))
            {
                if (ClipboardUtils.SetterClipboard(res))
                    Console.WriteLine("Copied to clipboard.");
            }
        }

        private string PathSelection()
        {
            Console.Write("Copy a file/directory path? ");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                return FindPath();
            }

            return "err";
        }

        public string FindPath()
        {
            int holder;
            string path;

            Console.Write("\nIndex Or Path: ");
            string res = Console.ReadLine() ?? "err";

            if (res.ToLower().Equals("index"))
            {
                Console.Write("\nIndex: ");
                res = Console.ReadLine() ?? "err";

                if (int.TryParse(res, out holder) && holder >= 0)
                {
                    List<string> IndexList = ExplorerVar.DirectoryList.Concat(ExplorerVar.FileList).ToList();
                    try
                    {
                        path = IndexList.ElementAt(holder - 1);
                        Console.WriteLine($"Found {path}");
                        return path;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Index {holder} not found on Explorer...\n" + e);
                    }
                }

            }

            if (res.ToLower().Equals("path"))
            {
                Console.Write("\nPath [FULL]: ");
                res = Console.ReadLine() ?? "err";

                if (Directory.Exists(res))
                {
                    return res;

                }
            }

            return "err";
        }

        public bool RemoveFileOrDirectory(string path)
        {
            if (Path.Exists(path))
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path);
                    return true;
                }
                else
                {
                    File.Delete(path);
                    return true;
                }
            }

            return false;
        }
    }

}
