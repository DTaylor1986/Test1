using System;
using System.Net;
using System.Net.Http;

namespace Test1.Infrastructure{
    public class HttpHelper{
        public HttpClient Initial(){
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://api.icndb.com");
            return Client;
        }
    }
}