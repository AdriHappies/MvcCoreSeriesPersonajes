using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Collections.Specialized;
using MvcCoreSeriesPersonajes.Models;
using Newtonsoft.Json;
using System.Text;

namespace MvcCoreSeriesPersonajes.Services
{
    public class ServiceSeries
    {
        private string URL;
        private MediaTypeWithQualityHeaderValue Header;
        private NameValueCollection queryString;

        public ServiceSeries(string url)
        {
            this.URL = url;
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
            this.queryString = HttpUtility.ParseQueryString(string.Empty);
        }

        //METODO GENERICO
        private async Task<T> CallApiAsync<T>(string request)
        {
            using(HttpClient client = new HttpClient())
            {
                //api/series?
                request = request + "?" + this.queryString;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                client.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");
                HttpResponseMessage response = await client.GetAsync(this.URL + request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Serie>> GetSeries()
        {
            //PETICION AL SERVICIO
            string request = "/api/series/getseries";
            //LAMAMOS AL METODO GENERICO INDICANDO QUE QUEREMOS RECUPERAR
            List<Serie> series = await this.CallApiAsync<List<Serie>>(request);
            return series;
        }

        public async Task<List<Personaje>> GetPersonajes()
        {
            string request = "/api/series/getpersonajes";
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Serie> FindSerie(int id)
        {
            string request = "/api/series/serie/" + id;
            Serie serie = await this.CallApiAsync<Serie>(request);
            return serie;
        }

        public async Task<Personaje> FindPersonaje(int id)
        {
            string request = "/api/series/personaje/" + id;
            Personaje personaje = await this.CallApiAsync<Personaje>(request);
            return personaje;
        }

        public async Task<List<Personaje>> GetPersonajesSerie(int idserie)
        {
            string request = "/api/series/personajesserie/" + idserie;
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task UpdatePersonajesSerie(int idpersonaje, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/series/UpdatePersonajesSerie/" +idpersonaje + "/" +idserie;
                client.BaseAddress = new Uri(this.URL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                StringContent content = new StringContent(String.Empty);
                await client.PutAsync(request, content);
            }
        }
    }
}
