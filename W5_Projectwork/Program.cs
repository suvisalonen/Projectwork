using System;

namespace W5_Projectwork
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Tervetuloa tapahtumahakuun");

            Console.WriteLine("");



        }

        public class Input
        {

            public static void menuSelectionLogic()
            {
               

                bool i = true;
                while (i)
                {

                    if (input == "1")
                    {
                        //events
                        i = false;
                    }

                    else if (input == "2")
                    {
                        //activities
                        i = false;
                    }
                    else
                    {
                        Console.WriteLine("ERROR!");
                    }
                }

            }
           
	


	


        }
    }
}
