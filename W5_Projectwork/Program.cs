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
            
            await Input.menuSelectionLogic();

        }

        public class EventHaku
        {
            public int MyProperty { get; set; }

        }
        public class PlaceHaku
        {

        }

        public class Places
        {
            public static async Task menuPlaces()
            {
                //Places
                Dictionary<string, string> EventTags = new Dictionary<string, string>();
                EventTags.Add("1", "v2/places/?tags_filter=restaurants");
                EventTags.Add("2", "v2/places/?tags_filter=Pub");
                EventTags.Add("3", "v2/places/?tags_filter=Park");


                Console.WriteLine("Millaisia paikkoja haluat etsiä:");
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

                //hakutulosten tallennus listaan

                return placesList;

            }
            public static void PrintPlace(List<Place> placesList)
            {

                Console.Clear();
                int consoleWindowHeight = Console.WindowHeight;
                foreach (var item in placesList)
                {
                    //Roosan tekemä tulostus
                    Console.WriteLine("");
                    Console.WriteLine("---- " + item.name.fi + " ----\n");
                    Console.WriteLine("Paikan kuvaus: \n {0} \n", item.description.intro);
                    Console.WriteLine("Osoite: \n {0} \n", item.location.address.street_address);
                    Console.WriteLine("Paikan sivut: \n {0}\n ", item.info_url);
                    Console.WriteLine("--------------------------------------------------");

                    if (Console.CursorTop + 5 > consoleWindowHeight)
                    {
                        Console.Write("Seuraavalle sivulle enterillä");
                        while (Console.ReadKey().Key != ConsoleKey.Enter) { };
                        Console.Clear();
                    }

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
                    Console.Clear();
                    Console.WriteLine("Tervetuloa tapahtumahakuun! Täällä voit hakea vapaa-ajanviettoon soveltuvia paikkoja tai tapahtumia.\n");
                    Console.WriteLine("Mitä haluat tehdä?\n");
                    Console.WriteLine("1) Hae paikkoja");
                    Console.WriteLine("2) Hae tapahtumia");
                    Console.WriteLine("3) Lopeta");
                    string input = Console.ReadLine();

                    if (input == "1")
                    {

                        await Places.menuPlaces();
                        correctInputLoop = true;
                    }

                    else if (input == "2")
                    {

                        await Events.menuEvents();

                        correctInputLoop = true;
                    }
                    else if (input == "3")
                    {
                        correctInputLoop = false;
                    }
                    else
                    {
                        Console.WriteLine("Pahoittelut, lukua ei löytynyt");
                        Console.ReadLine();
                        correctInputLoop = true;
                    }
                }

            }

            public class Events
            {
                public static async Task menuEvents()
                {
                    //Events

                    Dictionary<string, string> postalcodeCoordinates = await GetPostalcodeCoordinatesAsync();
                    int searchRange = 5;


                    Dictionary<string, string> EventTags = new Dictionary<string, string>();
                    EventTags.Add("1", $"v1/events/?tags_search=Musiikki&distance_filter={postalcodeCoordinates["lat"]}%2C{postalcodeCoordinates["lon"]}%2C{searchRange}");
                    EventTags.Add("2", $"v1/events/?tags_filter=Nuorille&distance_filter={postalcodeCoordinates["lat"]}%2C{postalcodeCoordinates["lon"]}%2C{searchRange}");
                    EventTags.Add("3", $"v1/events/?tags_filter=shows&distance_filter={postalcodeCoordinates["lat"]}%2C{postalcodeCoordinates["lon"]}%2C{searchRange}");


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
                    //hakumetodi 
                    var urlParams = tagDictionary[tag];
                    //hakutulosten tallennus listaan?

                    var events = await Rest.HelsinkiApiRestClientV2(urlParams);





                    return events;

                }

                //Sampsa
                public static async Task<Dictionary<string, string>> GetPostalcodeCoordinatesAsync()
                {
                    bool validPostalCodeInputted = false;
                    Dictionary<string, string> postalcodeCoordinates = new Dictionary<string, string>();

                    Console.Clear();
                    Console.WriteLine("Syötä postinumero:");


                    while (!validPostalCodeInputted)
                    {
                        string userInputtedPostalcode = Console.ReadLine();

                        try
                        {
                            postalcodeCoordinates = await GeoCoordinatesUtil.GetGeoCoordinatesAsync(userInputtedPostalcode);
                        }

                        catch (InvalidOperationException)
                        {
                            Console.WriteLine("Ongelma serveriyhteydessä, yritä uudelleen");
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Syötä postinumero oikeassa muodossa.");
                        }

                        if (postalcodeCoordinates.ContainsKey("lat") && !String.IsNullOrEmpty(postalcodeCoordinates["lat"]))
                        {
                            validPostalCodeInputted = true;
                        }


                        if (!validPostalCodeInputted)
                        {
                            Console.WriteLine("Syötä olemassaoleva postinumero:");
                        }
                    }
                    return postalcodeCoordinates;
                }

                //Ilari
                public static List<HelsinkiEvent> AskADate(List<HelsinkiEvent> events)
                {



                    //kysy päivämäärää tai printtaa päivän mukaan
                    //
                    Console.WriteLine("Syötä haluamasi päivämäärä (jätä tyhjäksi jos haluat hakea tapahtumia tänään): ");


                    bool ParseSucces = false;
                    while (ParseSucces == false)
                    {
                        string userInput = Console.ReadLine();
                        ParseSucces = DateTime.TryParse(userInput, out DateTime input);
                        if (userInput == "")
                        {
                            ParseSucces = true;
                            input = DateTime.Today;
                        }
                        if (ParseSucces)
                        {
                            //foreach (var item in DateFilterList(events, input))
                            //{
                            //    Console.WriteLine(item);
                            //}

                            return DateFilterList(events, input);

                        }
                        else
                        {
                            Console.WriteLine("koita uudelleen");


                        }
                    }

                    return new List<HelsinkiEvent>();


                }

                //Ilari
                //käydään lista läpi ja haetaan päivämäärän mukaiset tapahtumat uuteen listaan
                public static List<HelsinkiEvent> DateFilterList(List<HelsinkiEvent> events, DateTime input)
                {
                    List<HelsinkiEvent> FilteredList = new List<HelsinkiEvent>();

                    Console.WriteLine("Listataanko tapahtumat:");
                    Console.WriteLine("1) juuri haetulle päivälle");
                    Console.WriteLine("2) haetusta päivästä 3kk eteenpäin");

                    string switchinput = "";
                    while (switchinput != "1" && switchinput != "2")
                    {
                        switchinput = Console.ReadLine();
                        switch (switchinput)
                        {

                            case "1": //Ilari

                                foreach (var item in events)
                                {
                                    if (item.eventDates.endingDay >= input && input.AddDays(1) >= item.eventDates.startingDay)
                                        FilteredList.Add(item);
                                }

                                break;

                            case "2":
                                foreach (var item in events) //Sampsa
                                {
                                    if (item.eventDates.startingDay >= input && input.AddDays(90) >= item.eventDates.startingDay)
                                        FilteredList.Add(item);


                                }
                                FilteredList.Sort((date1, date2) => DateTime.Compare(date1.eventDates.startingDay, date2.eventDates.startingDay));

                                break;

                            default:
                                Console.WriteLine("Virheellinen valinta");

                                break;
                        }


                    }

                    return FilteredList;


                }

                public static void CheckListEmpty(List<HelsinkiEvent> listOfEvents) // Roosan tekemä metodi
                {
                    if (listOfEvents.Count == 0)
                    {
                        Console.WriteLine("Tapahtumia ei löytynyt. Paina enter palataksesi menuun \n");
                        Console.ReadLine();
                        Console.Clear();
                    }

                }

                public static void PrintEvent(List<HelsinkiEvent> listOfEvents)
                {

                    Console.Clear();
                    CheckListEmpty(listOfEvents);
                    int consoleWindowHeight = Console.WindowHeight;
                    foreach (var item in listOfEvents)
                    {
                        string endingDateText = item.eventDates.endingDay.ToString();
                        if (item.eventDates.endingDay == default(DateTime))
                        {
                            endingDateText = "->";
                        }
                        //Roosan tekemä tulostus
                        Console.WriteLine("");
                        Console.WriteLine("---- " + item.name.fi + " ----\n");
                        Console.WriteLine("Osoite: \n {0} \n", item.location.address.streetAddress);
                        Console.WriteLine("Aikataulu: \n {0} - {1} \n", item.eventDates.startingDay, endingDateText);
                        Console.WriteLine("Tapahtuman sivut: \n {0} \n", item.infoUrl);
                        Console.WriteLine("--------------------------------------------------");

                        if (Console.CursorTop + 5 > consoleWindowHeight)
                        {
                            Console.Write("Seuraavalle sivulle enterillä");
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { };
                            Console.Clear();
                        }
                        

                    }

                    Console.ReadLine();


                }
            }
        }
    }
}
