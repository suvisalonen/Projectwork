﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using W5_Projectwork_Places;
using System.Linq;


namespace W5_Projectwork
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Tervetuloa tapahtumahakuun");

            Console.WriteLine("Valitse 1 jos haluat hakea paikkoja, 2 jos haluat hakea tapahtumia");

            await Input.menuSelectionLogic();





        }

        public class Places
        {
            public static async Task menuPlaces()
            {
                //Places
                Dictionary<string, string> EventTags = new Dictionary<string, string>();
                EventTags.Add("1", "v2/places/?tags_filter=restaurants");
                EventTags.Add("2", "v2/places/?tags_filter=pubs");
                EventTags.Add("3", "v2/places/?tags_filter=parks");

                Console.WriteLine("Millaisia tapahtumia haluat etsiä:");
                Console.WriteLine("1) Ravintolat");
                Console.WriteLine("2) Pubit");
                Console.WriteLine("3) Puistot");

                bool correctKeyLoop = true;
                while (correctKeyLoop)
                {
                    var tagInput = Console.ReadLine();

                    if (EventTags.ContainsKey(tagInput))
                    {
                        var answer = await SearchWithTag(tagInput, EventTags);
                        PrintPlace(answer);
                        correctKeyLoop = false;

                    }
                    else
                    {
                        Console.WriteLine("Pahoittelut, valitsemaasi lukua ei löytynyt valikosta. Valitse uudelleen.");
                    }
                }


            }

            public static async Task<List<Place>> SearchWithTag(string tag, Dictionary<string, string> tagDictionary)
            {

                var urlParams = tagDictionary[tag];
                HelsinkiPlaces places = await Rest.HelsinkiApiRestClient(urlParams);
                List<Place> placesList = places.data.ToList();
                //hakumetodi 

                //hakutulosten tallennus listaan?

                return placesList;

            }
            public static void PrintPlace(List<Place> placesList) //Roosan tekemä
            {
                foreach (var item in placesList)
                {
                    Console.WriteLine("\n" + item.name.fi);
                    Console.WriteLine("Paikan kuvaus: \n {0}", item.description.intro);
                    Console.WriteLine("Osoite: \n {0}", item.location.address.street_address);
                    Console.WriteLine("Paikan sivut: \n {0} \n", item.info_url);
               
                }
            }

        }


        public class Input
        {

            public static async Task menuSelectionLogic()
            {


                bool correctInputLoop = true;
                while (correctInputLoop)
                {
                    string input = Console.ReadLine();

                    if (input == "1")
                    {

                        await Places.menuPlaces();
                        correctInputLoop = false;
                    }

                    else if (input == "2")
                    {

                        await Events.menuEvents();

                        correctInputLoop = false;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a correct input");
                    }
                }

            }

            public class Events
            {
                public static async Task menuEvents()
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



                    bool correctKeyLoop = true;
                    while (correctKeyLoop)
                    {
                        var tagInput = Console.ReadLine();

                        if (EventTags.ContainsKey(tagInput))
                        {
                            var answer = AskADate(await SearchWithTag(tagInput, EventTags));
                            PrintEvent(answer);
                            correctKeyLoop = false;

                        }
                        else
                        {
                            Console.WriteLine(
                                "Pahoittelut, valitsemaasi lukua ei löytynyt valikosta. Valitse uudelleen.");
                        }
                    }
                }

                //Suvin tekemä metodi
                public static async Task<List<HelsinkiEvent>> SearchWithTag(string tag,
                    Dictionary<string, string> tagDictionary)
                {

                    var urlParams = tagDictionary[tag];

                    var events = await Rest.HelsinkiApiRestClientV2(urlParams);
                    //hakumetodi 

                    //hakutulosten tallennus listaan?


                    return events;

                }

                //Ilari
                public static List<HelsinkiEvent> AskADate(List<HelsinkiEvent> events)
                {



                    //kysy päivämäärää tai printtaa päivän mukaan
                    //
                    Console.WriteLine("Syötä haluamasi päivämäärä: ");


                    bool ParseSucces = false;
                    while (ParseSucces == false)
                    {
                        string userInput = Console.ReadLine();
                        ParseSucces = DateTime.TryParse(userInput, out DateTime input);
                        if (ParseSucces)
                        {
                            foreach (var item in DateFilterList(events, input))
                            {
                                Console.WriteLine(item);
                            }

                            return DateFilterList(events, input);

                        }
                        else
                        {
                            Console.WriteLine("try again");

                           
                        }
                    }

                    return new List<HelsinkiEvent>();


                }


                //käydään lista läpi ja haetaan päivämäärän mukaiset tapahtumat uuteen listaan
                public static List<HelsinkiEvent> DateFilterList(List<HelsinkiEvent> events, DateTime input)
                {
                    List<HelsinkiEvent> FilteredList = new List<HelsinkiEvent>();

                    foreach (var item in events) //collection= list muuttuja joka tulee choose a tag metodista
                    {
                        if (item.eventDates.startingDay >= input && input >= item.eventDates.endingDay)
                            FilteredList.Add(item);
                        else if (input == null)
                            Console.WriteLine("Ei tapahtumia");
                    }



                    return FilteredList;

                }



                public static void PrintEvent(List<HelsinkiEvent> listOfEvents) //Roosan tekemä
                {
                    foreach (var item in listOfEvents)
                    {
                        Console.WriteLine(item.name.fi);
                        Console.WriteLine("Osoite: \n {0}", item.location.address.streetAddress);
                        Console.WriteLine("Aikataulu: \n {0} - {1}", item.eventDates.startingDay, item.eventDates.endingDay);
                        Console.WriteLine("Tapahtuman sivut: \n {0}", item.infoUrl);
                    }


                }
            }




        }
    }
}
