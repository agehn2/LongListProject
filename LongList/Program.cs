using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongList
{
    class Program
    {
        static List<(string, bool)> myList = new List<(string, bool)>();

        static void Main(string[] args)
        {
            ReadFile();
            bool exit = false;
            do
            {
                SaveList();
                Console.Clear();
                ReadList();
                Console.WriteLine("Make a selection:\n\t 1.View next page.\n\t 2.Add a task to your list\n\t 3.Mark task as complete or started\n\t 4.Exit Program");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Clear();

                        break;
                    case 2:
                        Console.Clear();
                        myList.Add((AddToList(), true));
                        break;
                    case 3:
                        SelectComOrStarted();
                        break;
                    case 4:
                        exit = true;
                        break;
                    default:
                        exit = true;
                        break;
                }
            } while (!exit);
        }






        static void ChangeColor(int i)
        {
            if ((myList[i].Item2) == true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }

        }

        static void SelectComOrStarted()
        {
            int z = 0;
            do
            {
                try
                {

                    Console.Clear();
                    ReadList();
                    Console.WriteLine("Choose:");
                    Console.WriteLine("\t 1.Mark a task on your list as complete.");
                    Console.WriteLine("\t 2.Mark a task as started \"will mark task as complete and reenter it at\n\t the bottom of your list\"");
                    Console.WriteLine("\t 3.Exit back to main menu");
                    int choice = int.Parse(Console.ReadLine());
                    if (choice == 1)
                    {
                        Console.Clear();
                        ReadList();
                        Console.WriteLine("Choose the corresponding number to the task you would like to mark as complete");
                        int markedComplete = int.Parse(Console.ReadLine());
                        myList[(markedComplete) - 1] = (myList[(markedComplete) - 1].Item1, false);
                    }
                    else if (choice == 2)
                    {
                        Console.Clear();
                        ReadList();
                        Console.WriteLine("Choose the corresponding number to the task you would like mark as started\n\"Will mark orinigal as complete and then readd it to the bottom of the list\"");
                        int markedComplete = int.Parse(Console.ReadLine());
                        myList[(markedComplete) - 1] = (myList[(markedComplete) - 1].Item1, false);
                        myList.Add((myList[(markedComplete) - 1].Item1, true));
                    }
                    else if (choice == 3)
                    {
                        z = 1;
                    }
                    else
                    {
                        z = 0;
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("Input not allowed");
                    Console.ReadKey();
                    z = 1;
                }
            } while (z == 0);
        }

        static string AddToList()
        {
            string addTask = "";
            bool exit = false;
            do       
            {    
                    Console.WriteLine("What would you like to add to your list?");
                    addTask = ($"{Console.ReadLine()}");
                    Console.WriteLine($"You entered\"{addTask}\" ");
                    Console.WriteLine("Enter Y if youd like to add this to list, Enter N if youd like to reenter your task");
                    string reEnter = Console.ReadLine();
                    exit = !(reEnter == "N");
                              
            } while (!exit);
            return addTask;                
        }

        static void ReadList()
        {
            Console.WriteLine("Completed task are Highlighted in grey");
            Console.WriteLine("My List");
            Console.WriteLine("===================");
            for (int i = 0; i < myList.Count; i++)
            {
                ChangeColor(i);
                Console.WriteLine($"{i + 1}. {myList[i].Item1}");
                Console.ResetColor();
            }
        }

        static void SaveList()
        {
            string path = @"SimpleScanningList.txt";
            File.WriteAllText(path, String.Empty);
            try
            {
                for (int i = 0; i < myList.Count; i++)
                {
                    string appendText = myList[i].Item1 + "," + myList[i].Item2 + Environment.NewLine;
                    File.AppendAllText(path, appendText);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The specified path is invalid");
                Console.ReadKey();
            }
        }

        static void ReadFile()
        {
            string path = @"SimpleScanningList.txt";

            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(path);

                while (!file.EndOfStream)
                {
                    string stringSeperator = file.ReadLine();
                    string[] stringSeparators = stringSeperator.Split(new char[] { ',' });
                    myList.Add((stringSeparators[0], bool.Parse(stringSeparators[1])));
                }
                file.Close();
            }
            catch (FileNotFoundException) {; }
            File.WriteAllText(path, String.Empty);
        }
    }
}
