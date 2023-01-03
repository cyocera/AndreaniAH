using ApiGeo.Data.Models;
using ApiGeo.Data.UnitOfWork;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System;
using System.Threading.Tasks;

namespace ApiGeo.Managers
{
    public interface IGeolocatorManager
    {
        Task<BaseResponseEntity<GeolocateResponse>> Geolocate(GeolocateRequest entity);
        Task<BaseResponseEntity<GeocodeResponse>> Geocode(int id);
    }
    public class GeolocatorManager : IGeolocatorManager
    {
        private readonly IUnitOfWork uow;
        private readonly IHttpClientFactory clientFactory;
        public GeolocatorManager(IUnitOfWork uow, IHttpClientFactory clientFactory)
        {
            this.uow = uow;
            this.clientFactory = clientFactory;
        }
        public async Task<BaseResponseEntity<GeolocateResponse>> Geolocate(GeolocateRequest entity)
        {
            var result = new BaseResponseEntity<GeolocateResponse>();

            try
            {
                result.Code = 202;
                result.Data = new GeolocateResponse { id = await RegisterAdrress(entity) };
                using (var serv = clientFactory.CreateClient("api.processMessage"))
                {
                    var json = new GeoProcesorResquest
                    {
                        id = result.Data.id,
                        calle = entity.calle,
                        ciudad = entity.ciudad,
                        codigo_postal = entity.codigo_postal,
                        numero = entity.numero,
                        pais = entity.pais,
                        provincia = entity.provincia
                    };
                    var response = await serv.PostAsync("api/DirecctionProcess/process", new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json"));
                }
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<BaseResponseEntity<GeocodeResponse>> Geocode(int id)
        {
            var result = new BaseResponseEntity<GeocodeResponse>();

            try
            {
                var entity = await uow.addressHistory.GetById(id);
                result.Data = new GeocodeResponse { id = entity.Id.ToString(), latitud = entity.latitud, longitud = entity.longitude, estado = entity.status };
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }

        private async Task<int> RegisterAdrress(GeolocateRequest entity)
        {
            var insertAdrress = new AddressHistory
            {
                Country = entity.pais,
                Province = entity.provincia,
                ZipCode = entity.codigo_postal,
                City = entity.ciudad,
                StreetNumber = entity.numero,
                Street = entity.calle,
                status = "PROCESANDO"
            };

            await uow.addressHistory.Insert(insertAdrress);
            uow.Save();

            return insertAdrress.Id;
        }
    }
}
