using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace W5_Projectwork
{
    public class Rest
    {
        

        public static async Task<HelsinkiEvent> HelsinkiApiRestClient ()
        {
            string eventsUrl = "http://open-api.myhelsinki.fi/";
            string urlParams = "v1/event/helsinki%3Aaga42pzzvi";

            return await ApiHelper.RunAsync<HelsinkiEvent>(eventsUrl, urlParams);
        }
        //public Place HelsinkiApiRestClient (string baseUrl, List<string> tagList)
        //{
        //string placesUrl = "http://open-api.myhelsinki.fi/v2/places/";
        //}

    }
    //string baseUrl, List<string> tagList, double latitude, double longitude
}
