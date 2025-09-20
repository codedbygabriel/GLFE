using GLFE.Utils;

namespace GLFE
{
    internal static class Program
    {
        private static void Main()
        {
            ApplicationLoop();
        }

        private static void ApplicationLoop()
        {
            var da = new DirectoryRecover();
            while (true)
            {

                var readKey = Console.ReadKey(true);

                switch (readKey.Key)
                {
                    case ConsoleKey.B:
                        da.InitExplorerList(previousPath: true);
                        break;
                    case ConsoleKey.M:
                        ChangeDirOptionHandler(ref da);
                        break;
                    case ConsoleKey.D:
                        DeleteOptionHandler(ref da);
                        break;
                    case ConsoleKey.P:
                        da.PrintExplorer();
                        break;
                    case ConsoleKey.R:
                        RenameOrMoveHandler(ref da);
                        break;
                }
                Cleaner();
                da.PrintHeader();
            }
        }

        private static void RenameOrMoveHandler(ref DirectoryRecover da)
        {
            Console.WriteLine("Original Path...");
            var originalPath = da.FindPath();

            if (!originalPath.Equals("err"))
            {
                Console.Write("Where To Path/File...");
                var whereToPath = Console.ReadLine() ?? "err";

                try
                {
                    if (da.IsPathDir(originalPath))
                    {
                        Directory.Move(sourceDirName: originalPath, destDirName: whereToPath);
                    }
                    else
                    {
                        File.Move(sourceFileName: originalPath, destFileName: whereToPath);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong while moving / renaming file.\n" + e);
                }
            }

        }

        private static void ChangeDirOptionHandler(ref DirectoryRecover da)
        {
            var res = da.FindPath();

            if (!res.Equals("err"))
            {
                da.InitExplorerList(res);
            }
        }

        private static void DeleteOptionHandler(ref DirectoryRecover da)
        {
            var path = ClipboardUtils.GetFromClipboard();

            if (string.IsNullOrEmpty(path))
            {
                Console.Write("File / Directory: ");
                path = Console.ReadLine() ?? "err";
            }

            Console.Write($"Are you sure? [{path}]");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                da.RemoveFileOrDirectory(path);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\nOperation Aborted");
            }
        }

        private static void Cleaner()
        {
            Console.WriteLine("\nWaiting For KeyPress... ");
            Console.ReadLine();
            Console.Clear();

        }
    }
}
