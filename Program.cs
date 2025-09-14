using GLFE.DirectoryUtils;

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
					case ConsoleKey.D:				    
						Console.WriteLine("Omg D Key Pressed");
						break;
					case ConsoleKey.P:				    
						DA.PrintExplorer();
						break;
				}

				Console.Write("Waiting For KeyPress... ");
				Console.ReadLine();
                Console.Clear();

                DA.PrintHeader();
            }
        }

    }
}
