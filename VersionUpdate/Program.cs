using System;
using System.Collections.Generic;
using System.IO;

namespace Teste
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            var pathv = $@"C:\Workbench\router";
            var oldver = $@"""2.7.1.0""";
            var newver = $@"""2.8.0.0""";
#else

            Console.WriteLine("Infome a raiz do projeto para a busca");
            var path = Console.ReadLine();
            Console.WriteLine("Informe a versão atual");
            var oldver = Console.ReadLine();
            Console.WriteLine("Informe a nova versão");
            var newver = Console.ReadLine();
#endif
            Console.WriteLine($@"Atualizando Projetos da raiz {pathv}{"\n"} Versão : {oldver} ==> {newver}");
            Console.WriteLine($@"Aperte uma tecla para iniciar");
            Console.ReadLine();
            var ls = DirSearch(pathv, oldver, newver);
            Console.WriteLine("\n\n");

            Console.WriteLine($@"Atualização finalizada, os seguintes arquivos foram atualizados");

            foreach (var item in ls)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }

        static List<string> DirSearch(string sDir, string oldver, string newver)
        {
            try
            {

                var ls = new List<String>();

                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        Console.WriteLine(f);

                        if (f.Contains("AssemblyInfo.cs"))
                        {
                            string a = File.ReadAllText(f);

                            a = a.Replace($@"[assembly: AssemblyVersion({oldver})]", $@"[assembly: AssemblyVersion({newver})]");
                            a = a.Replace($@"[assembly: AssemblyFileVersion({oldver})]", $@"[assembly: AssemblyFileVersion({newver})]");
                            File.WriteAllText(f, a);

                            ls.Add(f);
                        }
                    }

                    ls.AddRange(DirSearch(d, oldver, newver));
                }

                return ls;

            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
                return null;
            }
        }
    }
}
