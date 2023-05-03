/* Task:
 * Write a console application that consumes the Chuck Norris API to retrieve and display a random joke category. 
 */

using System;
using System.Net.Http;
using Newtonsoft.Json;

class Program
{
    
    static async Task Main(string[] args)
    {
        HttpClient client = new HttpClient();

        bool endapp = false;
        while (!endapp)
        {
            string url = "";

            Console.WriteLine(Environment.NewLine + "Press r for a random joke, or c for a list of categories. Press q to quit.");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.KeyChar == 'r')
            {
                url = "https://api.chucknorris.io/jokes/random";
            }

            else if (key.KeyChar == 'c')
            {
                Console.WriteLine(Environment.NewLine + "Enter the number for the category you would like. Press Enter at the end." + Environment.NewLine);

                HttpResponseMessage response = await client.GetAsync("https://api.chucknorris.io/jokes/categories");
                string json = await response.Content.ReadAsStringAsync();
                dynamic List = JsonConvert.DeserializeObject(json);

                int i = 0;
                foreach (string category in List)
                {
                    i++;
                    Console.WriteLine(i + ": " + category);
                }

                string catSelected = Console.ReadLine();
                int catInt;
                try { catInt = Int32.Parse(catSelected); if (catInt < 1 || catInt > 16) { throw new ArgumentOutOfRangeException(); } } 
                catch { Console.WriteLine("Must be a valid number!"); continue; }

                string chosenCategory = List[catInt - 1];

                url = "https://api.chucknorris.io/jokes/random?category=" + chosenCategory;
                
            }

            else if (key.KeyChar == 'q') { endapp = true; }
            else
            {
                Console.WriteLine(Environment.NewLine +  "You must press the correct button");
                continue;
            }

            if (url != "")
            {
                HttpResponseMessage response = await client.GetAsync(url);

                string json = await response.Content.ReadAsStringAsync();
                dynamic jsonString = JsonConvert.DeserializeObject(json);

                string joke = jsonString.value;
                Console.WriteLine(Environment.NewLine + joke);
            }

        }

        
    }


}
