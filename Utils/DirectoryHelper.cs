namespace GLFE.DirectoryUtils
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
        private Explorer ExplorerVar = new Explorer();

        public DirectoryRecover()
        {
            PrintHeader();
            InitExplorerList();
        }

        public void InitExplorerList()
        {
            foreach (var item in Directory.GetDirectories(_HOME_PATH)) ExplorerVar.DirectoryList.Add(item);
            foreach (var item in Directory.GetFiles(_HOME_PATH)) ExplorerVar.FileList.Add(item);

        }

        public void PrintHeader()
        {
            Console.Write($"{Environment.UserName}, Current Working At: {_CURRENT_PATH}\t");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[p - print DIR], ");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("[d - delete file|directory].");

            Console.ResetColor();
        }

        public void PrintExplorer()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < ExplorerVar.DirectoryList.Count(); i++)
                Console.WriteLine($"[{i + 1}]\t-\t{ExplorerVar.DirectoryList[i]}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < ExplorerVar.FileList.Count(); i++)
                Console.WriteLine($"[{ExplorerVar.DirectoryList.Count() + i + 1}]\t-\t{ExplorerVar.FileList[i]}");

            Console.ResetColor();
        }
    }

}
