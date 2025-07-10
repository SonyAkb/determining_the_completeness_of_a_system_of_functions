using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM_4
{
    public class Function
    {
        public int[,] truthTable;//таблица истинности
        public string vector;
        public bool[] features;
        private const int numberOfProperties = 5;

        public int truthTableColumns
        {
            get {  return truthTable.GetLength(1) - 1; }
            set { }
        }
        public int truthTableRows
        {
            get { return truthTable.GetLength(0); }
            set { }
        }

        public Function(string line)
        {
            
            if (string.IsNullOrEmpty(line) || (line.Length != 2 && line.Length != 4 && line.Length != 8 && line.Length != 16))
            {
                throw new ArgumentNullException("Вектор неверной длины!");
            }
            vector = line;
            truthTable = FillingTruthTable(line.Length);
            features = new bool[numberOfProperties];
            CheckingAllProperties();
        }

        public int[,] FillingTruthTable(int size)
        {            
            int rows = size;
            int columns = (int)Math.Log(size, 2);

            int[,] truthTable = new int[rows, columns + 1];// Создаем таблицу истинности нужного размера

            for (int i = 0; i < rows; i++)
            {
                string binaryStr = Convert.ToString(i, 2).PadLeft(columns, '0'); // Используем PadLeft для выравнивания до нужной длины
                for (int j = 0; j < columns; j++)
                {
                    truthTable[i, j] = binaryStr[j] == '1' ? 1 : 0; // Преобразуем символ в целое число
                }
                truthTable[i, columns] = Convert.ToInt32(vector[i] + "");
            }

            return truthTable;
        }

        public void ShowTruthTable()
        {
            
            for (int i = 0;i < truthTableColumns; i++)
            {
                Console.Write($"{AdditionalFunctions.nameVariables[i]} | ");
            }
            Console.WriteLine($"F |");
            AdditionalFunctions.Separate((truthTableColumns + 1) * 4 - 1);

            for (int i = 0; i < truthTable.GetLength(0); i++)
            {
                for (int j = 0; j < truthTable.GetLength(1); j++)
                {
                    Console.Write(truthTable[i, j] + " | ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public bool IsT0()
        {
            if (vector[0] == '0')
            {
                return true;
            }
            return false;
        }

        public bool IsT1()
        {
            if (vector[truthTableRows - 1] == '1')
            {
                return true;
            }
            return false;
        }

        public bool IsS()
        {
            for(int i = 0; i < truthTableRows / 2; i++)
            {
                if (vector[i] == vector[truthTableRows - i - 1])
                {
                    return false;
                }
            }
            return true;
        }

        
        public bool IsM()
        {
            for (int i = 0; i < truthTableRows; i++)
            {
                int startUnits = QuantityUnits(i);
                for (int j = 0; j < truthTableRows; j++)
                {
                    if (i != j && NumberOfDifferences(i, j) == 1 && startUnits == QuantityUnits(j) - 1 )
                    {
                        if (vector[i] == '1' && vector[j] == '0')
                        {
                            return false;
                        } 
                    }
                }
            }
            return true;
        }

        public int NumberOfDifferences(int index1, int index2)
        {
            int num = 0;
            for (int i = 0;i < truthTableColumns; i++)
            {
                if (truthTable[index1, i] != truthTable[index2, i])
                {
                    num++;
                }
            }
            return num;
        }
        public int QuantityUnits(int index)
        {
            int num = 0;

            for (int i = 0; i < truthTableColumns; i++)
            {
                num += truthTable[index, i];
            }

            return num;
        }

        public bool IsL()
        {
            List<List<int>> listPascalTriangleMethod = new List<List<int>>();
            List<int> firstVector = new List<int>();
            for (int i = 0; i < truthTableRows; i++)
            {
                firstVector.Add(Convert.ToInt32(vector[i] + ""));
            }
            listPascalTriangleMethod.Add(firstVector);

            for (int i = 0; i < truthTableRows - 1; i++)
            {
                List<int> tmp = PascalTriangleMethod(listPascalTriangleMethod[i]);
                listPascalTriangleMethod.Add(tmp);
            }

            List<int> polynomiaCoefficientsMonotonicConjunctions = new List<int>();
            for (int i = 0; i < listPascalTriangleMethod.Count; i++)
            {
                polynomiaCoefficientsMonotonicConjunctions.Add(listPascalTriangleMethod[i][0]);
            }
            return CheckingPolynomial(ref polynomiaCoefficientsMonotonicConjunctions);
        }

        public bool CheckingPolynomial(ref List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == 1)
                {
                    if (QuantityUnits(i) > 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public List<int> PascalTriangleMethod(List<int> firstVector)
        {
            List<int> secondVector = new List<int>();
            for (int i = 0; i < firstVector.Count - 1; i++)
            {
                secondVector.Add((firstVector[i] + firstVector[i + 1]) % 2);
            }
            return secondVector;
        }       

        public void CheckingAllProperties()
        {
            features[0] = IsT0();
            features[1] = IsT1();
            features[2] = IsS();
            features[3] = IsM();
            features[4] = IsL();
        }

        public void Show()
        {
            Console.WriteLine("  T0    T1    S    M    L");
            Console.WriteLine($"{features[0]} {features[1]} {features[2]} {features[3]} {features[4]}");
        }

    }

    

}
