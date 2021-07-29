using System;
using System.IO;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DirSearch($@"D:\Workbench\Viewer\mobileviewer");
        }

        static void DirSearch(string sDir)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        Console.WriteLine(f);
                        if (f.Contains("AssemblyInfo.cs"))
                        {
                            string a = File.ReadAllText(f);
                            a = a.Replace($@"[assembly: AssemblyTitle(""PortableViewer", $@"[assembly: AssemblyTitle("" ClearCanvas");
                            File.WriteAllText(f, a);
                        }
                    }
                    DirSearch(d);
                }


            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }
    }
}
