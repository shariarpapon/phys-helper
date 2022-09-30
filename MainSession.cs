using System;
using System.Collections.Generic;
using System.Data;

namespace PhysHelperCore
{ 
    public static class MainSession
    {
        private const string EXIT_TOKEN = "-e";
        private const string RESET_TOKEN = "-rs";
        private static List<string> OutputHistory;
        private static List<string> ResultHistory;
        private static bool SkippingInput = false;

        private static void Main(params string[] args)
        {
            if (SkippingInput == false) StartNewSession();
            else SkippingInput = false;

            try
            {
                while (true)
                {
                    PrintOutputHistory();

                    Console.Write("Input>> ");
                    string input = Console.ReadLine();
                    if (input.ToLower() == EXIT_TOKEN) Environment.Exit(0);
                    else if (input.ToLower() == RESET_TOKEN) StartNewSession();
                    else if (string.IsNullOrEmpty(input) == false)
                    {
                        string parsed_expression = Parser.ParseExpression(input, ResultHistory);
                        double result = Convert.ToDouble(new DataTable().Compute(parsed_expression, null));
                        OutputHistory.Add($"{input} = {result}");
                        ResultHistory.Add(result.ToString());
                    }
                }
            }
            catch
            {
                Console.WriteLine("FATAL ERROR... Press any key to skip this input..");
                Console.ReadLine();
                SkippingInput = true;
                Main();
            }
        }
        private static void ClearConsole()
        {
            Console.Clear();
            PrintProgramInfo();
            Console.WriteLine("---------------------------------------------------------------------");
        }

        private static void StartNewSession() 
        {
            OutputHistory = new List<string>();
            ResultHistory = new List<string>();
            ClearConsole();
        }

        private static void PrintOutputHistory()
        {
            ClearConsole();
            if (OutputHistory.Count > 0)
            {
                for (int i = 0; i < OutputHistory.Count; i++)
                    Console.WriteLine($"[{i}] {OutputHistory[i]}");
            }
        }

        private static void PrintProgramInfo()
        {
            Console.WriteLine($"{DevInfo.PROGRAM_NAME} >> version: {DevInfo.VERSION_INFO} >> author: {DevInfo.AUTHOR} >> {DevInfo.DEV_YEAR}");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }

}


