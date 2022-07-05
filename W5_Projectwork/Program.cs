using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace W5_Projectwork
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Tervetuloa tapahtumahakuun");

            Console.WriteLine("Valitse 1 jos haluat hakea paikkoja, 2 jos haluat hakea tapahtumia");

            //Input.menuSelectionLogic();
            Console.WriteLine();


            HelsinkiEvent response = await Rest.HelsinkiApiRestClient();

            Console.WriteLine(response.id);
        }



        public class Input
        {

            public static void menuSelectionLogic()
            {
               

                bool i = true;
                while (i)
                {
                    string input = Console.ReadLine();

                    if (input == "1")
                    {

                        //places
                        i = false;
                    }

                    else if (input == "2")
                    {
                        //Events
                        i = false;
                    }
                    else
                    {
                        Console.WriteLine("ERROR!");
                    }
                }

            }

            public static void chooseATag(List<string> Tags)
            {
                //valitse tägi
                Console.WriteLine("Mitä haluat tehdä");
                foreach (var item in Tags)
                {
                    Console.WriteLine(item);
                }

                bool i = true;
                while (i)
                {
                    string input = Console.ReadLine();

                    if (input == "1")
                    {

                        //1
                        i = false;
                    }

                    else if (input == "2")
                    {
                        //2
                        i = false;
                    }
                    else if (input == "3")
                    {
                        //3
                        i = false;
                    }
                    else
                    {
                        Console.WriteLine("ERROR!");
                    }
                }


                
            }

            public static void askADate()
            {
                //kysy päivämäärää tai printtaa päivän mukaan
                //

                Console.WriteLine("Syötä haluamasi päivämäärä: ");
                string userInput = Console.ReadLine();


            }

            public static void print()
            {
                //printtaus
            }
           
	


	


        }
    }
}
