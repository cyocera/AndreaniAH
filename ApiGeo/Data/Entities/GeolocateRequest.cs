namespace ApiGeo
{
    public class GeolocateRequest
    {
        public string calle { get; set; }
        public string numero { get; set; }
        public string ciudad { get; set; }
        public string codigo_postal { get; set; }
        public string provincia { get; set; }
        public string pais { get; set; }
    }

    public class GeoProcesorResquest : GeolocateRequest
    {
        public int id { get; set; }
    }
}
