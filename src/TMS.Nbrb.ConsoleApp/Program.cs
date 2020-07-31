﻿using System;
using System.Threading;
using TMS.Nbrb.Core.Manager;

namespace TMS.Nbrb.ConsoleApp
{
    class Program
    {
        static readonly CurrenciesTable table = new CurrenciesTable();
        static readonly CurrencyConversion conversion = new CurrencyConversion();
        static readonly DynamicsExport export = new DynamicsExport();

        static void Main()
        {
            Console.Title = "NBRB Converter v.1.0";
            const string welcome = "Welcome to NBRB Converter v.1.0\n\n";
            foreach (char c in welcome)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(c);
                Thread.Sleep(30);
                Console.ResetColor();
            }

            while (true)
            {
                ShowMenu();
                int.TryParse(Console.ReadLine(), out int userInput);
                switch (userInput)
                {
                    case 1:
                        {
                            table.ShowCurrencies();
                        }
                        break;

                    case 2:
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nConversion.");
                            Console.ResetColor();
                            conversion.Conversion();
                        }
                        break;

                    case 3:
                        {
                            export.FileExport();
                        }
                        break;

                    case 4:
                        {
                            Environment.Exit(0);
                        }
                        break;

                    default:
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Action not found.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;
                }
                Console.WriteLine();
            }
        }

        public static void ShowMenu()
        {
            Console.WriteLine("What would you like to do? Select desired option:");
            Console.WriteLine("1. Show all actual currencies for today.");
            Console.WriteLine("2. Currency converter to BYN.");
            Console.WriteLine("3. Currency dynamics over the past weeek export.");
            Console.WriteLine("4. Exit.");
        }        
    }
}