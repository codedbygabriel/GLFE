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
                        MoveOptionHandler(ref DA);
                        break;
                    case ConsoleKey.D:
                        DeleteOptionHandler(ref DA);
                        break;
                    case ConsoleKey.P:
                        DA.PrintExplorer();
                        break;
                }
				Cleaner();
                DA.PrintHeader();
            }
        }

		static void MoveOptionHandler(ref DirectoryRecover DA) {
			string res = DA.FindPath();

			if(!res.Equals("err"))
			{
				DA.InitExplorerList(res);
			}
		}

        static void DeleteOptionHandler(ref DirectoryRecover DA)
        {
			string path = ClipboardUtils.GetFromClipboard();

			if (string.IsNullOrEmpty(path)){
				Console.Write("File / Directory: ");
				path = Console.ReadLine() ?? "err";
			}

            Console.Write($"Are you sure? [{path}]");

            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                DA.RemoveFileOrDirectory(path);
				Console.WriteLine();
            } else {
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
