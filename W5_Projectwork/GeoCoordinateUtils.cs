using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace W5_Projectwork
{
    public class GeoCoordinatesUtil
    {
        GeoCoordinatesUtil() { }

        private static async Task<string> DigiTransitRestClient(string postalCode)
        {
            const string DIGITRANSIT_REST_BASEURL = @"http://api.digitransit.fi/geocoding/v1/search";
            string queryParams = @$"?text={postalCode}&size=1";
            string result = await ApiHelper.GetJSONAsync<string>(DIGITRANSIT_REST_BASEURL, queryParams);
            return result;
        }

        public static async Task<Dictionary<string, string>> GetGeoCoordinatesAsync(string postalCode)
        {

            if (IsValidPostalCodeFormat(postalCode))
            {
                string latitude = "";
                string longitude = "";
                try
                {
                    string geoJSON = await GeoCoordinatesUtil.DigiTransitRestClient(postalCode);
                    dynamic result = JsonConvert.DeserializeObject<dynamic>(geoJSON);


                    if (result.features.Count != 0)
                    {
                        latitude = (string)result.features[0].geometry.coordinates[1];
                        longitude = (string)result.features[0].geometry.coordinates[0];
                    }
                    else
                    {
                        latitude = "";
                        longitude = "";
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }

                Dictionary<string, string> postalCodeGeoCoordinates = new Dictionary<string, string>
            {
                {"lat", latitude},
                {"lon", longitude}
            };

                return postalCodeGeoCoordinates;
            }
            else
            {
                throw new ArgumentException("Invalid postalcode format");
            }
        }


        public static bool IsValidPostalCodeFormat(string postalCode)
        {
            if (IsDigitsOnly(postalCode) && postalCode.Length == 5)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }


    }



}


