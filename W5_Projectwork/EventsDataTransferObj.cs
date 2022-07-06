using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace W5_Projectwork
{
    class HelsinkiEventDataTransferObject
    {
        public string PostalCode { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string FilterTags { get; set; }
        public DateTime FilterDate { get; set; }

        public HelsinkiEventDataTransferObject() 
        { 
        }

        public async Task UpdateCoordinates() 
        {
            Dictionary<string, double> coordinates = await GeoCoordinatesUtil.GetGeoCoordinatesAsync(PostalCode);

            this.lat = coordinates["lat"];
            this.lon = coordinates["lon"];
        }
    }

}
