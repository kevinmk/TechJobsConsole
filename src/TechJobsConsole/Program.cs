using System;
using System.Collections.Generic;

namespace TechJobsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create two Dictionary vars to hold info for menu and data

            // Top-level menu options
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");

            // Column options
            Dictionary<string, string> columnChoices = new Dictionary<string, string>();
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;

            // Allow user to search/list until they manually quit with ctrl+c
            while (true)
            {

                string actionChoice = GetUserSelection("View Jobs", actionChoices);

                if (actionChoice.Equals("list"))
                {
                    string columnChoice = GetUserSelection("List", columnChoices);

                    if (columnChoice.Equals("all"))
                    {
                        PrintJobs(JobData.FindAll());
                    }
                    else
                    {
                        List<string> results = JobData.FindAll(columnChoice);
                        results.Sort();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("\n*** All " + columnChoices[columnChoice] + " Values ***");
                        foreach (string item in results)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(item);
                        }
                    }
                }
                else // choice is "search"
                {
                    // How does the user want to search (e.g. by skill or employer)
                    string columnChoice = GetUserSelection("Search", columnChoices);

                    // What is their search term?
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("\nSearch term: ");
                    string searchTerm = Console.ReadLine();

                    List<Dictionary<string, string>> searchResults;

                    // Fetch results
                    if (columnChoice.Equals("all"))
                    {
                        searchResults = JobData.FindByValue(searchTerm);
                        PrintJobs(searchResults);
                        //        (old code)        Console.WriteLine("Search all fields not yet implemented.");
                    }
                    else
                    {
                        searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm);
                        PrintJobs(searchResults);
                    }
                }
            }
        }

        /*
         * Returns the key of the selected item from the choices Dictionary
         */
        private static string GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {
            int choiceIdx;
            bool isValidChoice = false;
            string[] choiceKeys = new string[choices.Count];

            int i = 0;
            foreach (KeyValuePair<string, string> choice in choices)
            {
                choiceKeys[i] = choice.Key;
                i++;
            }

            do
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n" + choiceHeader + " by:");

                for (int j = 0; j < choiceKeys.Length; j++)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);
                }

                string input = Console.ReadLine();
                bool inputVal = int.TryParse(input, out choiceIdx);
                //                choiceIdx = int.Parse(input);
                if (inputVal)
                {
                    if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choices. Try again.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        isValidChoice = true;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("invalid Choices, Try again");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

            } while (!isValidChoice);

            return choiceKeys[choiceIdx];
        }

        private static void PrintJobs(List<Dictionary<string, string>> someJobs)
        {
            //            Console.WriteLine("printJobs is not implemented yet");
            int count = someJobs.Count;
            if (count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" Nothing was found.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                foreach (Dictionary<string, string> item in someJobs)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n******************************");
                    foreach (KeyValuePair<string, string> itemline in item)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        if (itemline.Key != "S.no")
                            Console.WriteLine(itemline.Key + " : " + itemline.Value);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("******************************\n\n");
                }

            }
        }
    }
}