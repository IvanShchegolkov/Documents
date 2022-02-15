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
        //static void Main(string[] args)
        //{
        //    string path = Path.GetFullPath(@"..\..\..\");
        //    Console.WriteLine(path)
        //}
        static async Task Main()
        {
            string path = Path.GetFullPath(@"..\..\..\" + @$"\test.json");
            StagesList stages = await JsonFileReader.ReadAsync<StagesList>(path);




            Console.WriteLine(path);
            Console.WriteLine(stages);
        }
        public static class JsonFileReader
        {
            public static async Task<T> ReadAsync<T>(string filePath)
            {
                using FileStream stream = File.OpenRead(filePath);
                return await JsonSerializer.DeserializeAsync<T>(stream);
            }
        }
    }
}







//{
//    "Stages": [
//      {
//        "Name": "Согласование инициатором",
//    "Performer": "Инициатор"
//      },
//    {
//        "Name": "Согласование инициатором",
//    "Performer": "Инициатор"
//    }
//  ]
//}
