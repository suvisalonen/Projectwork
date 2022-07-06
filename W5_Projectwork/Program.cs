﻿using System;
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
            Dictionary<string, string> EventTags = new Dictionary<string, string>();
            EventTags.Add("1", "v1/events/?tags_search=Musiikki");
            EventTags.Add("2", "v1/events/?tags_filter=Nuorille");
            EventTags.Add("3", "v1/events/?tags_filter=shows");
            var events = await Input.SearchWithTag("1", EventTags);

<<<<<<< HEAD
            HelsinkiEvent response = await Rest.HelsinkiApiRestClient();
           

            ////testausta 
            ///
            List<HelsinkiEvent> eventti = new List<HelsinkiEvent>();
            eventti.Add(response);

            Input.print(eventti);

            ///
            Console.WriteLine(response.name.fi);

=======

            Console.WriteLine(events);


           //HelsinkiEvent response = await Rest.HelsinkiApiRestClient();

            //Console.WriteLine(response.name.fi);
>>>>>>> af98632884d52bfb7ac3fe5ac6eb2ccb5ddae19c
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
                        Dictionary<string, string> EventTags = new Dictionary<string, string>();
                        EventTags.Add("1", "v1/events/?tags_search=Musiikki");
                        EventTags.Add("2", "v1/events/?tags_filter=Nuorille");
                        EventTags.Add("3", "v1/events/?tags_filter=shows");

                        Console.WriteLine("Millaisia tapahtumia haluat etsiä:");
                        Console.WriteLine("1) Musiikkitapahtumat");
                        Console.WriteLine("2) Nuorten tapahtumat");
                        Console.WriteLine("3) Showt");

                        bool a = true;
                        while (a)
                        {
                            var tagInput = Console.ReadLine();

                            if (EventTags.ContainsKey(tagInput))
                            {
                                SearchWithTag(tagInput, EventTags);
                                a = false;

                            }
                            else
                            {
                                Console.WriteLine("Pahoittelut, valitsemaasi lukua ei löytynyt valikosta. Valitse uudelleen.");
                                a = true;
                            }
                        }
                        

                        i = false;
                    }
                    else
                    {
                        Console.WriteLine("ERROR!");
                    }
                }

            }

            //Suvin tekemä metodi
            public static async Task<List<HelsinkiEvent>> SearchWithTag(string tag, Dictionary<string, string> tagDictionary) 
            {

                var urlParams = tagDictionary[tag];

                var events= await Rest.HelsinkiApiRestClient(urlParams);
                //hakumetodi 

                //hakutulosten tallennus listaan?

                return events;
               
            }
            //Ilari
            public static void askADate()
            {
                //kysy päivämäärää tai printtaa päivän mukaan
                //
                Console.WriteLine("Syötä haluamasi päivämäärä: ");


                bool succes = false;
                while (succes == false)
                {
                    string userInput = Console.ReadLine();
                    succes = DateTime.TryParse(userInput, out DateTime input);
                    if (succes)
                    {
                        //käydään lista läpi ja haetaan päivämäärän mukaiset tapahtumat uuteen listaan

                       // List<string> FilteredList = new List<string>();
                       //
                       // foreach (var item in Filted) //collection= list muuttuja joka tulee choose a tag metodista
                       // {
                       //     if (item.startingDay > input > item.endingDay)
                       //         FilteredList.Add(item);
                       // }
                       //

                    }
                    else
                    {
                        Console.WriteLine("try again");
                    }
                }



            }

            
            public static void print(List<HelsinkiEvent> listOfEvents) //Roosan tekemä
            {
                foreach (var item in listOfEvents)
                {
                    Console.WriteLine(item.name.fi);
                    Console.WriteLine("Osoite: \n {0}", item.location);
                    Console.WriteLine("Aikataulu: \n {0}", item.eventDates);
                    Console.WriteLine("Tapahtuman sivut: \n {0}", item.infoUrl);
                }
                

            }
           
	

            
	


        }
    }
}
