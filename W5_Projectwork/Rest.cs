﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace W5_Projectwork
{
    public class Rest
    {


        public static async Task<List<HelsinkiEvent>> HelsinkiApiRestClient(string url)
        {
            string eventsUrl = "http://open-api.myhelsinki.fi/";
            string urlParams = url;

            return await ApiHelper.RunAsync <List<HelsinkiEvent>> (eventsUrl, urlParams);
        }
        //public Place HelsinkiApiRestClient (string baseUrl, List<string> tagList)
        //{
        //string placesUrl = "http://open-api.myhelsinki.fi/v2/places/";
        //}

    }
    //string baseUrl, List<string> tagList, double latitude, double longitude
}
