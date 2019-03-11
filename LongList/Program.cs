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
            Console.SetWindowSize(Console.WindowWidth,50);
            ReadFile();
            bool exit = false;
            int a = 0;
            do
            {                                
                SaveList();
                Console.Clear(); 
                DeleteFirst();
                StartIndex(a);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("1.View next page.\t 2.Add a task to your list\t 3.Mark task as complete or started\n4.PreviousPage\t\t 5.Back to first page\t\t 6.Exit Program");
                Console.ResetColor();
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        a = NextPage(a);
                        break;
                    case 2:
                        Console.Clear();
                        myList.Add((AddToList(), true));
                        break;
                    case 3:
                        SelectComOrStarted(a);
                        break;
                    case 4:                       
                        a = PreviousPage(a);
                        break;
                    case 5:
                        a = 0;
                        break;
                    case 6:
                        exit = true;
                        break;
                    default:
                        exit = true;
                        break;
                }            
            } while (!exit);
        }


        //Turns task tuple to false
        static void SelectComOrStarted(int index)
        {
            int z = 0;
            do
            {
                try
                {
                    Console.Clear();
                    StartIndex(index);
                    Console.WriteLine("Choose:");
                    Console.WriteLine("\t 1.Mark a task on your list as complete.");
                    Console.WriteLine("\t 2.Mark a task as started \"will mark task as complete and reenter it at\n\t the bottom of your list\"");
                    Console.WriteLine("\t 3.Exit back to main menu");
                    int choice = int.Parse(Console.ReadLine());   
                    if (choice == 1) //Changes to just Dark Gray
                    {
                        Console.Clear();
                        StartIndex(index);
                        Console.WriteLine("Choose the corresponding number to the task you would like to mark as complete");
                        int markedComplete = int.Parse(Console.ReadLine());
                        myList[(markedComplete) - 1] = (myList[(markedComplete) - 1].Item1, false);
                    }
                    else if (choice == 2) //Changes to Dark Gray and adds it to list
                    {
                        Console.Clear();
                        StartIndex(index);
                        Console.WriteLine("Choose the corresponding number to the task you would like mark as started\n\"Will mark orinigal as complete and then readd it to the bottom of the list\"");
                        int markedComplete = int.Parse(Console.ReadLine());
                        myList[(markedComplete) - 1] = (myList[(markedComplete) - 1].Item1, false);
                        myList.Add((myList[(markedComplete) - 1].Item1, true));
                    }
                    else if (choice == 3) //Will exit to menu
                    {
                        z = 1;
                    }
                    else //Return loop
                    {
                        z = 0;
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("Input not allowed\n Press enter to continue");
                    Console.ReadKey();
                   
                }
            } while (z == 0);
        }
        //Adds Task to List
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
        //Saves list to file
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
        //Reads List from File
        static void ReadFile()
        {
            string path = @"SimpleScanningList.txt";

            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(path);

                while (!file.EndOfStream)
                {
                    string stringSeperator = file.ReadLine();
                    string[] stringSeparators = stringSeperator.Split(new char[] { ',' });              //Splits 2 items to (string, bool)
                    myList.Add((stringSeparators[0], bool.Parse(stringSeparators[1])));
                }
                file.Close(); //program will close file to save to the file.
            }
            catch (FileNotFoundException) {; }
            File.WriteAllText(path, String.Empty);
        }

        //Creates forward and previous page.
         static void StartIndex(int startAtNumber)
         {
            Console.WriteLine("Completed task are Highlighted in grey");
            Console.WriteLine("My List      \"Completed task are Highlighted in grey\"");
            Console.WriteLine("===================");
            int Counter;          
            Counter=((myList.Count) >= (startAtNumber + 25)) ? (startAtNumber + 25) :(myList.Count);
                for (int i = startAtNumber; i < Counter; i++)
                {
                ChangeColor(i);     
                Console.WriteLine($"{i + 1}. {myList[i].Item1}");
                Console.ResetColor();
                }                
         } 
    //Next Page;Next 25
    static int NextPage(int b)
    {    
         b = b + 25;
        if(b > myList.Count) b=b-25;
        return b;
    }
    //Previous page;Last 25
    static int PreviousPage(int c)
    {
        c = c - 25;
        if(c < 0) c=0;
        return c;
    }

        //Changes True to gray:False to DarkGray
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

         static void DeleteFirst()
        {
            do
            {
                
            if(myList[0].Item2 == false)
            {
            myList.RemoveAt(0);          
            }
            
            }while(myList[0].Item2 == false);
        }






    }
}


         