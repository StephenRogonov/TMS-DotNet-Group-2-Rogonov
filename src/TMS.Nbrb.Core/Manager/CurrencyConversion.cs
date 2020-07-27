using System;

namespace TMS.Nbrb.Core.Manager
{
    public static class CurrencyConversion
    {
        public static void Conversion(int initial, float coefficient)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conversion result = " + initial * coefficient);
            Console.ResetColor();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
