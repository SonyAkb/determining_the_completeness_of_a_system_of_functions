using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DM_4
{
    public class AdditionalFunctions
    {
        public static void Separate(int length = 20, char symbol = '-')
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write(symbol);
                
            }
            Console.WriteLine();
        }

        public static char[] nameVariables = new char[] { 'x', 'y', 'z', 'w' };

        public static List<string> GetDataFromFile(string filePath = "")
        {
            List<string> data = new List<string>();

            if(filePath == "")
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                filePath = Path.Combine(currentDirectory, "Data.txt");
            }

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    int numberOfLines = int.Parse(reader.ReadLine());

                    for (int i = 0; i < numberOfLines; i++)
                    {
                        string line = reader.ReadLine();
                        data.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }

            return data;
        }
    }
}
