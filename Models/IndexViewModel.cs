using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test1.Models{
    public class IndexViewModel{
        public List<string> Categories {get;set;}
        public int NumJokes {get;set;}
        public int PickID {get;set;}
        public List<JokeValue> Jokes {get;set;}
    }
}