using Newtonsoft.Json;
using System.Collections.Generic;

namespace Geocodificador
{
    public class OpenStreetMapResponse
    {
        public int place_id { get; set; }
        public string licence { get; set; }
        public string osm_type { get; set; }
        public string osm_id { get; set; }
        public List<string> boundingbox { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public string display_name { get; set; }

        [JsonProperty("class")]
        public string Class { get; set; }
        public string type { get; set; }
        public decimal importance { get; set; }
        public OpenAddress address { get; set; }

    }

    public class OpenAddress
    {
        public string house_number { get; set; }
        public string road { get; set; }
        public string suburb { get; set; }
        public string city { get; set; }

        [JsonProperty("ISO3166-2-lvl8")]
        public string ISO3166_2_lvl8 { get; set; }
        public string city_district { get; set; }
        public string state { get; set; }

        [JsonProperty("ISO3166-2-lvl4")]
        public string ISO3166_2_lvl4 { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
    }
}
