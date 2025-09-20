namespace GLFE.Utils
{
    internal struct Explorer()
    {
        public List<string> FileList = new();
        public List<string> DirectoryList = new();
    }

    internal class DirectoryRecover
    {
        static string _homePath = $"/home/{Environment.UserName}";
        static string _currentPath = _homePath;
        static string _previousPath = string.Empty;
        static bool _isFirstTime = true;

        private Explorer _explorerVar = new Explorer();

        public DirectoryRecover()
        {
            PrintHeader();
            InitExplorerList();
        }

        public void InitExplorerList(string path = "", bool previousPath = false)
        {
            if (_isFirstTime)
            {
                _isFirstTime = false;
                FillStructure();
            }

            if (previousPath)
            {
                path = _previousPath;
            }

            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                _previousPath = _currentPath;
                _currentPath = path;

                if (_explorerVar.DirectoryList.Any())
                {
                    _explorerVar.DirectoryList.Clear();
                }

                if (_explorerVar.FileList.Any())
                {
                    _explorerVar.FileList.Clear();
                }

                FillStructure();
            }
        }

        private void FillStructure()
        {
            foreach (var item in Directory.GetDirectories(_currentPath)) _explorerVar.DirectoryList.Add(item);
            foreach (var item in Directory.GetFiles(_currentPath)) _explorerVar.FileList.Add(item);
        }

        public void PrintHeader()
        {
            Console.Write($"{Environment.UserName}, Current Working At: {_currentPath}\t");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[p - print DIR], ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[m - move to], ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[r - Move or Rename file / directory ], ");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("[b - back to last dir], ");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("[d - delete file|directory].");

            Console.ResetColor();
        }

        public void PrintExplorer()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < _explorerVar.DirectoryList.Count(); i++)
                Console.WriteLine($"[{i + 1}]\t-\t{_explorerVar.DirectoryList[i]}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < _explorerVar.FileList.Count(); i++)
                Console.WriteLine($"[{_explorerVar.DirectoryList.Count() + i + 1}]\t-\t{_explorerVar.FileList[i]}");


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
                    List<string> indexList = _explorerVar.DirectoryList.Concat(_explorerVar.FileList).ToList();
                    try
                    {
                        path = indexList.ElementAt(holder - 1);
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

                if (Directory.Exists(res) || File.Exists(res))
                {
					Console.WriteLine(res);
                    return res;

                }
            }

            return "err";
        }
        public void MoveOrRename(string path)
        {
				
        }
        public bool RemoveFileOrDirectory(string path)
        {
            if (Path.Exists(path))
            {
                if (IsPathDir(path))
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
		public bool IsPathFile(string path) => File.Exists(path);
		public bool IsPathDir(string path) => Directory.Exists(path);
    }
}
