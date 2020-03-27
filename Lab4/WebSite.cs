using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class WebSite
    {
        public WebSite(string baseLink)
        {
            _client = new RestClient(baseLink);
        }
        public RestClient _client { get; private set; }
        public string Download(string path)
        {
            var request = new RestRequest(path, Method.GET);
            var response = _client.Execute(request);
            return response.Content;
        }     
        public Task<IRestResponse> DownloadAsync(string path) //takie samo tylko dajemy promise ze zwrocimy
        {
            var request = new RestRequest(path, Method.GET);
            var response = _client.ExecuteAsync(request);
            return response; //respone jest tu taskiem ktory ma sie zakonczyc wyjatkiem
        }
    }
}
