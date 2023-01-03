using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Geocodificador.Managers
{
    public interface IGeoProcessorManager
    {
        Task<List<GeoProcessorMessage>> ProcessData(GeoProcesorResquest entiy);
    }

    public class GeoProcessorManager : IGeoProcessorManager
    {
        public async Task<List<GeoProcessorMessage>> ProcessData(GeoProcesorResquest entiy)
        {
            var result = new List<GeoProcessorMessage>();
            try
            {

                //OrderByRoute Number + Street + Province + City + ZipCode + Country 
                var direction = string.Format("{0}{1}{2}{3}{4}{5}",
                    !string.IsNullOrEmpty(entiy.numero) ? string.Format("{0}+", entiy.numero.Trim()) : string.Empty,
                    !string.IsNullOrEmpty(entiy.calle) ? string.Format("{0}+", entiy.calle.Replace(" ", "+").Trim()) : string.Empty,
                    !string.IsNullOrEmpty(entiy.provincia) ? string.Format("{0}+", entiy.provincia.Replace(" ", "+").Trim()) : string.Empty,
                    !string.IsNullOrEmpty(entiy.ciudad) ? string.Format("{0}+", entiy.ciudad).Replace(" ", "+".Trim()) : string.Empty,
                    !string.IsNullOrEmpty(entiy.codigo_postal) ? string.Format("{0}+", entiy.codigo_postal.Trim()) : string.Empty,
                    !string.IsNullOrEmpty(entiy.pais) ? string.Format("{0}", entiy.pais).Replace(" ", "+".Trim()) : string.Empty);

                var listResponse = await GetDataOpenStreetMap(direction);

                if (listResponse.Count != 0)
                {
                    foreach (var item in listResponse)
                    {
                        result.Add(new GeoProcessorMessage
                        {
                            id = entiy.id,
                            latitude = item.lat,
                            longitude = item.lon,
                            status = "TERMINADO"
                        });
                    }
                }
                else
                {
                    result.Add(new GeoProcessorMessage { id = entiy.id, status = "OK pero sin datos" });
                }

            }
            catch (Exception ex)
            {
                result.Add(new GeoProcessorMessage { id = entiy.id, status = ex.Message });
            }

            return result;
        }
        public async Task<List<OpenStreetMapResponse>> GetDataOpenStreetMap(string direction)
        {
            var URL = string.Format(@"https://nominatim.openstreetmap.org/search?q={0}&format=json&polygon_geojson=1&addressdetails=1", direction);
            WebClient webClient = new WebClient();
            webClient.Headers.Add("User-Agent: Other");
            var jsonData = webClient.DownloadData(URL);
            return JsonConvert.DeserializeObject<List<OpenStreetMapResponse>>(Encoding.Default.GetString(jsonData));
        }
    }
}
