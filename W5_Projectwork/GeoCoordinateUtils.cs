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

        public static async Task<Dictionary<string, double>> GetGeoCoordinatesAsync(string postalCode)
        {

            if (IsValidPostalCode(postalCode))
            {
                string geoJSON = await GeoCoordinatesUtil.DigiTransitRestClient(postalCode);

                dynamic result = JsonConvert.DeserializeObject<dynamic>(geoJSON);
                dynamic latitude = (double)result.features[0].geometry.coordinates[0];
                dynamic longitude = (double)result.features[0].geometry.coordinates[1];

                Dictionary<string, double> postalCodeGeoCoordinates = new Dictionary<string, double>
            {
                {"lat", latitude},
                {"lon", longitude}
            };

                return postalCodeGeoCoordinates;
            }

            else
            {

                return new Dictionary<string, double>
            {
                { "lat", 0},
                { "lon", 0}
                };
            }


        }

        public static bool IsValidPostalCode(string postalCode)
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


