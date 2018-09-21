using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IO_Example
{
    class Program
    {
        //A BRANCH COMMENT

        public class Unit
        {
            private string name;
            private int x;
            private int y;

            public Unit(string name, int x, int y)
            {
                this.name = name;
                this.x = x;
                this.y = y;
            }

            public override string ToString()
            {
                return "My name is " + name + ". My position is [" + x + ", " + y + "].";
            }

            private string saveMe()
            {
                return name + ", " + x + ", " + y;
            }

            public void Save()
            {
                if (Directory.Exists("saves") != true)
                {
                    Directory.CreateDirectory("saves");
                    Console.WriteLine("Directory created");                   
                }

                FileStream file = new FileStream("saves/save.file", FileMode.Append, FileAccess.Write);
                StreamWriter writer = new StreamWriter(file);
                writer.WriteLine(saveMe());
                writer.Close();
                file.Close();
            }
        }

        static void Main(string[] args)
        {
            if(Directory.Exists("saves") != true)
            {
                Directory.CreateDirectory("saves");
                Console.WriteLine("Directory created");
            }
            else
            {
                Console.WriteLine("Directory already exists");
                Console.WriteLine("Trying to create file");
                
                if(File.Exists("saves/save.file") != true)
                {
                    File.Create("saves/save.file").Close();
                    Console.WriteLine("Created file");
                }
                else
                {
                    Console.WriteLine("File already exists");
                }
            }
            CreateUnits();
            LoadFile();
        }

        static void CreateUnits()
        {
            bool inputting = true;

            while (inputting == true)
            {
                Console.WriteLine("Please create a unit");
                Console.Write("Please enter a name");
                string name = Console.ReadLine();

                Console.Write("Please enter x pos");
                int x = Convert.ToInt32(Console.ReadLine());

                Console.Write("Please enter y pos");
                int y = Convert.ToInt32(Console.ReadLine());

                Unit newUnit = new Unit(name, x, y);
                newUnit.Save();

                Console.WriteLine("Do you want to create another unit Y/N");
                string input = Console.ReadLine();
                if (input == "N")
                {
                    inputting = false;
                }

            }
            
        }

        static void LoadFile()
        {
            //FileStream file = new FileStream("saves/save.file", FileMode.Open, FileAccess.Read);

            //StreamReader reader = new StreamReader(file);

            //string line = reader.ReadLine();

            //while(line != null)
            //{
            //    string[] unit = line.Split(',');
            //    string name = unit[0];
            //    int x = Convert.ToInt32(unit[1]);
            //    int y = Convert.ToInt32(unit[2]);

            //    Unit newUnit = new Unit(name, x, y);
            //    Console.WriteLine(newUnit.ToString());

            //    line = reader.ReadLine();
            //}

            //reader.Close();
            //file.Close();

            FileStream file = new FileStream("saves/save.file", FileMode.Open, FileAccess.Read);
            string[] completeFile = File.ReadAllLines("saves/save.file");
            Unit[] units = new Unit[completeFile.Length];

            for(int i = 0; i < completeFile.Length; i++)
            {
                string[] unit = completeFile[i].Split(',');
                string name = unit[0];
                int x = Convert.ToInt32(unit[1]);
                int y = Convert.ToInt32(unit[2]);

                units[i] = new Unit(name, x, y);              
            }

            for (int i = 0; i < completeFile.Length; i++)
            {
                Console.WriteLine(units[i].ToString());
            }

            
        }
    }
}
