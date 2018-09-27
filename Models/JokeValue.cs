using System.Collections.Generic;

namespace Test1.Models{
    public class JokeValue{
        public int ID {get;set;}
        public string Joke {get;set;}
        public List<string> Categories {get;set;}
    }
}