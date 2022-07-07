using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace W5_Projectwork
{
    class HelsinkiEventDataTransferObject
    {
        public string PostalCode { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public double SearchRadius { get; set; }
        public string FilterTags { get; set; }
        public DateTime FilterDate { get; set; }

        public HelsinkiEventDataTransferObject(double searcRadius = 5) 
        {
            this.SearchRadius = searcRadius;
        }

        public async Task UpdateCoordinates() 
        {
            Dictionary<string, string> coordinates = await GeoCoordinatesUtil.GetGeoCoordinatesAsync(PostalCode);

            this.lat = coordinates["lat"];
            this.lon = coordinates["lon"];
        }
    }

}
