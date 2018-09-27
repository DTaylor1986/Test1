using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Test1.Models;
using Test1.Infrastructure;

namespace Test1.Components{
    public class Joke : ViewComponent{
        public IViewComponentResult Invoke(List<JokeValue> jokes){            
            return View(jokes);
        }
        
    }
}