using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Reflection;
using Documents;

namespace Documents
{
    abstract class Construct
    {
        public void Run(StagesList stages)
        {
            Header();
            Body(stages);
            Report();
        }
        protected abstract void Header();
        protected abstract void Body(StagesList stages);
        protected abstract void Report();
    }
    class Action : Construct
    {
        ConsoleWrite consoleWrite = new ConsoleWrite();
        ConsoleMoveReader consoleMoveReader = new ConsoleMoveReader();
        List<MoveStages> moveStages = new List<MoveStages>();
        protected override void Header()
        {
            consoleWrite.Print("Процесс согласования документа\n", Console.WindowWidth / 3);
            consoleWrite.Print("Введите 'y(yes)' если согласовано, либо 'n(no)' если не согласовано\n");
        }
        protected override void Body(StagesList stages)
        {
            List<string> strList = new List<string>();

            List<MoveStages> moveStages1 = new List<MoveStages>();

            foreach (var item in stages.Stages)
            {
                moveStages.Add(consoleMoveReader.GetInputData(item));
            }
        }
        protected override void Report()
        {
            consoleWrite.PrintN();
            consoleWrite.Print("\nНажмите любую клавишу, чтобы открыть отчёт");
            Console.ReadLine();
            Console.Clear();
            consoleWrite.Print("Таблица согласования", Console.WindowWidth / 3);
            consoleWrite.PrintN();

            moveStages.Insert(0, new MoveStages { Name = "Наименование этапа", Performer = "Согласующий", Decision = "Результат", Comment = "Комментарий" });

            Console.Write("|\t");
            Console.Write(moveStages[0].Name);
            Console.Write("\t|\t");
            Console.Write(moveStages[0].Performer);
            Console.Write("\t|\t");
            Console.Write(moveStages[0].Decision);
            Console.Write("\t|\t");
            Console.Write(moveStages[0].Comment);
            Console.Write("\t|");
            Console.WriteLine();
            consoleWrite.PrintN();

            for (int i = 1; i < moveStages.Count; i++)
            {
                Console.Write("|");
                Console.Write(moveStages[i].Name);
                Console.Write("\t|");
                Console.Write(moveStages[i].Performer);
                Console.Write("\t|");

                if(moveStages[i].Decision.ToLower() == "y" || moveStages[i].Decision.ToLower() == "yes")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Согласовано");
                    Console.ResetColor();
                }
                else if(moveStages[i].Decision.ToLower() == "n" || moveStages[i].Decision.ToLower() == "no")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Не согласовано");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("Пропущено");
                    Console.ResetColor();
                }

                Console.Write("\t|");
                Console.Write(moveStages[i].Comment);
                Console.Write("|");
                Console.WriteLine();
                consoleWrite.PrintN();
            }

            string path = Path.GetFullPath(@"..\..\..\" + @$"\test_res.json");
            string json = JsonSerializer.Serialize<List<MoveStages>>(moveStages);

            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(json);
            }
        }
    }
    class ConsoleMoveReader
    {
        public MoveStages GetInputData(Stages stages)
        {
            MoveStages moveStages = new MoveStages();
            ConsoleWrite consoleWrite = new ConsoleWrite();

            moveStages.Name = stages.Name;
            moveStages.Performer = stages.Performer;

            consoleWrite.PrintN();
            Console.WriteLine("Наименование этапа: " + moveStages.Name);
            Console.WriteLine("Согласующий: " + moveStages.Performer);
            Console.Write("Согласовано: ");
            moveStages.Decision = Console.ReadLine();
            Console.Write("Комментарий: ");
            moveStages.Comment = Console.ReadLine();

            return moveStages;
        }
    }
    class PrintReport
    {

    }
    class ConsoleWrite
    {
        public void Print(string text)
        {
            Console.WriteLine(text);
        }
        public void Print(string text, int width = 0, int height = 0)
        {
            Console.SetCursorPosition(width, height);
            Console.WriteLine(text);
        }
        public void PrintN()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("-");
            }
        }
    }
}
