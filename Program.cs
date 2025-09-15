using GLFE.DirectoryHelper;
using GLFE.ClipboardHelper;

namespace GLFE
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationLoop();
        }

        static void ApplicationLoop()
        {
            var DA = new DirectoryRecover();
            while (true)
            {

                ConsoleKeyInfo Info = Console.ReadKey(true);

                switch (Info.Key)
                {
                    case ConsoleKey.B:
                        DA.InitExplorerList(PreviousPath: true);
                        break;
                    case ConsoleKey.M:
                        ChangeDirOptionHandler(ref DA);
                        break;
                    case ConsoleKey.D:
                        DeleteOptionHandler(ref DA);
                        break;
                    case ConsoleKey.P:
                        DA.PrintExplorer();
                        break;
                    case ConsoleKey.R:
                        RenameOrMoveHandler(ref DA);
                        break;
                }
                Cleaner();
                DA.PrintHeader();
            }
        }

        static void RenameOrMoveHandler(ref DirectoryRecover DA)
        {
            Console.WriteLine("Original Path...");
            string OriginalPath = DA.FindPath();

            if (!OriginalPath.Equals("err"))
            {
                Console.Write("(R)ename or (M)ove? ");
                ConsoleKeyInfo Catch = Console.ReadKey(true);

                Console.Write("Where To Path/File...");
                string WhereToPath = Console.ReadLine() ?? "err";


                try
                {
                    if (DA.IsPathDir(OriginalPath))
                    {
                        Directory.Move(sourceDirName: OriginalPath, destDirName: WhereToPath);
                    }
                    else
                    {
                        File.Move(sourceFileName: OriginalPath, destFileName: WhereToPath);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something wen't wrong while moving / renaming file.\n" + e);
                }
            }

        }

        static void ChangeDirOptionHandler(ref DirectoryRecover DA)
        {
            string res = DA.FindPath();

            if (!res.Equals("err"))
            {
                DA.InitExplorerList(res);
            }
        }

        static void DeleteOptionHandler(ref DirectoryRecover DA)
        {
            string path = ClipboardUtils.GetFromClipboard();

            if (string.IsNullOrEmpty(path))
            {
                Console.Write("File / Directory: ");
                path = Console.ReadLine() ?? "err";
            }

            Console.Write($"Are you sure? [{path}]");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                DA.RemoveFileOrDirectory(path);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\nOperation Aborted");
            }
        }

        static void Cleaner()
        {
            Console.WriteLine("\nWaiting For KeyPress... ");
            Console.ReadLine();
            Console.Clear();

        }
    }
}
