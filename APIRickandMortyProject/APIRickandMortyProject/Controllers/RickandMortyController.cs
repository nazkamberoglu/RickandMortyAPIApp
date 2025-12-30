using APIRickandMortyProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using static System.Net.WebRequestMethods;

namespace APIRickandMortyProject.Controllers
{
    public class RickandMortyController : Controller
    {
        private static List<Result> favoriteChars=new List<Result>();
        public IActionResult Index()
        {
            string url = "https://rickandmortyapi.com/api/character";
            string json = new WebClient().DownloadString(url);
            Root data=JsonConvert.DeserializeObject<Root>(json);

            foreach (var character in data.results)
            {
                List<Episode> episodes = new List<Episode>();
                
                
                foreach (var epUrl in character.episode)
                {
                    string epJson=new WebClient().DownloadString(epUrl);    
                    Episode ep=JsonConvert.DeserializeObject<Episode>(epJson);
                    episodes.Add(ep);
                }

                character.EpisodeDetails = episodes;

            }


            return View(data);
        }



        public IActionResult Index2()
        {
            string url = "https://rickandmortyapi.com/api/episode";
            string json = new WebClient().DownloadString(url);
            EpisodeRoot data1=JsonConvert.DeserializeObject<EpisodeRoot>(json);

            foreach (var item in data1.results)
            {
                List<Result> charList = new List<Result>();

                foreach (var charUrl in item.characters)
                {
                    string charJson = new WebClient().DownloadString(charUrl);
                    Result character=JsonConvert.DeserializeObject<Result>(charJson);
                    charList.Add(character);


                }

                item.charDetails = charList;
            }

            return View(data1);
        }


        public IActionResult CharDetailPage(int id)
        {
            string url = $"https://rickandmortyapi.com/api/character/{id}";

            string json= new WebClient().DownloadString(url);

            Result data=JsonConvert.DeserializeObject<Result>(json);


            
                List<Episode> episodes = new List<Episode>();


                foreach (var epUrl in data.episode)
                {
                    string epJson = new WebClient().DownloadString(epUrl);
                    Episode ep = JsonConvert.DeserializeObject<Episode>(epJson);
                    episodes.Add(ep);
                }

                data.EpisodeDetails = episodes;

            


            
           
            return View(data);

        }



        [HttpPost]
        public IActionResult ToggleFavorites(int id)
        {

            string url = $"https://rickandmortyapi.com/api/character/{id}";
            string json=new WebClient().DownloadString(url);    

            Result character= JsonConvert.DeserializeObject<Result>(json);

            if (!favoriteChars.Any(x=>x.id==character.id))
            {
                favoriteChars.Add(character);

                return Json(new { success = true, action = "added", count = favoriteChars.Count });
            }

            else
            {

                favoriteChars.RemoveAll(c => c.id == character.id);
                return Json(new { success = true, action = "removed", count = favoriteChars.Count });
            }

        }

        

        public IActionResult Favorites()
        { return View(favoriteChars.ToList()); }  

    }
}





