using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<string> allFunctions = AdditionalFunctions.GetDataFromFile();

                SystemOfFunctions system = new SystemOfFunctions(allFunctions);
                system.ShowAllTruthTables();
                Console.WriteLine("Свойства системы функций");
                system.ShowPropertiesOfFunctionSystem();
                Console.WriteLine();
                system.ShowSystemComplete();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка! : {ex.Message}");
            }
        }
    }
}
