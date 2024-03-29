﻿using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using W5_Projectwork_Places;

namespace W5_Projectwork
{
    public class Rest
    {


        public static async Task<HelsinkiPlaces> HelsinkiApiRestClient(string url)
        {
            string eventsUrl = "http://open-api.myhelsinki.fi/";
            string urlParams = url;

            return await ApiHelper.RunAsync<HelsinkiPlaces>(eventsUrl, urlParams);
        }
        //public Place HelsinkiApiRestClient (string baseUrl, List<string> tagList)
        //{
        //string placesUrl = "http://open-api.myhelsinki.fi/v2/places/";
        //}

        public static async Task<List<HelsinkiEvent>> HelsinkiApiRestClientV2(string url)
        {
            string eventsUrl = "http://open-api.myhelsinki.fi/";
            string urlParams = url;

            //Sampsa, Mukailtu tätä: https://www.newtonsoft.com/json/help/html/SerializingJSONFragments.htm

            string events = await ApiHelper.GetJSONAsync<string>(eventsUrl, urlParams);

            try
            {

                JObject eventsJson = JObject.Parse(events);

                IList<JToken> eventDataPartOfResponse = eventsJson["data"].Children().ToList();

                IList<HelsinkiEvent> helsinkiEvents = new List<HelsinkiEvent>();

                foreach (JToken hellEvent in eventDataPartOfResponse)
                {
                    HelsinkiEvent helsinkiEventData = hellEvent.ToObject<HelsinkiEvent>();
                    helsinkiEvents.Add(helsinkiEventData);
                }
                return new List<HelsinkiEvent>(helsinkiEvents);
            }
            catch (System.Exception e)
            {

                Console.WriteLine(e);
                return new List<HelsinkiEvent>();
            }


            
        }

    }

}
