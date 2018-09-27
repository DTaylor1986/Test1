using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Test1.Infrastructure;
using Test1.Models;

namespace Test1.Controllers{
    public class HomeController : Controller{
        private HttpHelper api = new HttpHelper();
        public async Task<ViewResult> Index(){
            IndexViewModel model = new IndexViewModel();
            HttpClient client = api.Initial();
            
            //Get Categories
            HttpResponseMessage catResponse = await client.GetAsync("categories");
            catResponse.EnsureSuccessStatusCode();
            string catJson = await catResponse.Content.ReadAsStringAsync();          
            CategoryResult catResult = JsonConvert.DeserializeObject<CategoryResult>(catJson);
            model.Categories = catResult.value;

            //Get Number of jokes
            HttpResponseMessage totalResponse = await client.GetAsync("jokes/count");
            totalResponse.EnsureSuccessStatusCode();
            string totalJson = await totalResponse.Content.ReadAsStringAsync();
            TotalResult totalResult = JsonConvert.DeserializeObject<TotalResult>(totalJson);
            ViewBag.Total = totalResult.value;

            //Get Jokes
            model.Jokes = null;

            return View(model);
        }

        [HttpPost]
        public async Task<ViewResult> Random(IndexViewModel form){
            IndexViewModel model = new IndexViewModel();
            HttpClient client = api.Initial();
            
            //Get Categories
            HttpResponseMessage catResponse = await client.GetAsync("categories");
            catResponse.EnsureSuccessStatusCode();
            string catJson = await catResponse.Content.ReadAsStringAsync();          
            CategoryResult catResult = JsonConvert.DeserializeObject<CategoryResult>(catJson);
            model.Categories = catResult.value;

            //Get Number of jokes
            HttpResponseMessage totalResponse = await client.GetAsync("jokes/count");
            totalResponse.EnsureSuccessStatusCode();
            string totalJson = await totalResponse.Content.ReadAsStringAsync();
            TotalResult totalResult = JsonConvert.DeserializeObject<TotalResult>(totalJson);
            ViewBag.Total = totalResult.value;

            //Decide whether to pick by ID or get randoms
            if(form.PickID != 0){
                //Pick by ID
                HttpResponseMessage pickResponse = await client.GetAsync("jokes/" + form.PickID);
                pickResponse.EnsureSuccessStatusCode();
                string pickJson = await pickResponse.Content.ReadAsStringAsync();
                JokeIndv pickResult = JsonConvert.DeserializeObject<JokeIndv>(pickJson);
                JokeValue value = pickResult.value;
                model.Jokes.Add(value);
            } else {
                //Get random Jokes
                if(form.NumJokes == 0){
                    form.NumJokes = 1;
                }
                HttpResponseMessage jokeResponse = await client.GetAsync("jokes/random/" + form.NumJokes);
                jokeResponse.EnsureSuccessStatusCode();
                string jokeJson = await jokeResponse.Content.ReadAsStringAsync();
                JokeResult jokeResult = JsonConvert.DeserializeObject<JokeResult>(jokeJson);
                model.Jokes = jokeResult.value;
            }

            return View("Index", model);
        }
    }
}