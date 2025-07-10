using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_4
{
    public class SystemOfFunctions
    {
        List<Function> allFunctions;
        bool[,] propertiesOfFunctionSystem;
        bool systemComplete;

        private const int numberOfProperties = 5;

        public int NumberOfFunctions
        {
            get
            {
                return allFunctions.Count;
            }
            set { }
        }

        public SystemOfFunctions(List<string> func)
        {
            if (func.Count == 0)
            {
                throw new ArgumentNullException("Система функций пуста!");
            }
            allFunctions = new List<Function>();
            for(int i = 0; i < func.Count; i++)
            {
                allFunctions.Add(new Function(func[i]));
            }
            propertiesOfFunctionSystem = FillingPropertiesOfFunctionSystem();
            systemComplete = IsSystemComplete();
        }

        public bool[,] FillingPropertiesOfFunctionSystem()
        {
            bool[,] tableOfProperties = new bool[allFunctions.Count, numberOfProperties];
            for(int i = 0;i < allFunctions.Count; i++)
            {
                for(int j = 0; j < numberOfProperties; j++)
                {
                    tableOfProperties[i, j] = allFunctions[i].features[j];
                }
            }
            return tableOfProperties;
        }

        public void ShowPropertiesOfFunctionSystem()
        {
            Console.WriteLine($"     | T0| T1| S | M | L");
            for(int i = 0; i < NumberOfFunctions ; i++)
            {
                Console.Write($"F{i + 1}".PadLeft(4, ' ') + " ");
                for(int j = 0; j < numberOfProperties; j++)
                {
                    Console.Write($"| {((propertiesOfFunctionSystem[i, j] == true) ? '+' : ' ')} ");
                }
                Console.WriteLine();
            }
        }

        public void ShowAllTruthTables()
        {
            for (int i = 0; i < NumberOfFunctions; i++)
            {
                Console.WriteLine($"F{i + 1}");
                allFunctions[i].ShowTruthTable();
            }
        }

        public bool IsSystemComplete()
        {
            for (int i = 0; i < numberOfProperties; i++)
            {
                bool flagIsComplete = false;
                for (int j = 0; j < NumberOfFunctions; j++)
                {
                    if (propertiesOfFunctionSystem[j, i] == false)
                    {
                        flagIsComplete = true;
                    }
                }
                if (!flagIsComplete)
                {
                    return false;
                }
            }
            return true;
        }

        public void ShowSystemComplete()
        {
            if (systemComplete)
            {
                Console.WriteLine("Система полная");
            }
            else
            {
                Console.WriteLine("Система НЕ полная");
            }
        }
    }
}
