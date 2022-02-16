using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection;
using Documents;

namespace Documents
{
    internal class Program
    {
        static async Task Main()
        {
            string path = Path.GetFullPath(@"..\..\..\" + @$"\test.json");
            StagesList stages = await JsonFileReader.ReadAsync<StagesList>(path);

            Action action = new Action();
            action.Run(stages);


            Console.WriteLine("");
        }
    }
}