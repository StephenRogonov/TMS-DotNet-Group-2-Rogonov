using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Nbrb.Core.Manager
{
    public static class CurrencyConversion
    {
        public static void Conversion(int initial, float coefficient)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("result=" + initial * coefficient);
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
