using System;
using System.Collections.Generic;
using System.Text;

namespace W5_Projectwork
{
    class HelsinkiEventDataTransferObject
    {
        public string PostalCode { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string FilterTags { get; set; }
        public DateTime FilterDate { get; set; }

    }
}
