using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.Models;
using MovieAPI.Services;
using Microsoft.AspNetCore.Http;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private IDAL dal;

        public CategoryController(IDAL dalObject)
        {
            dal = dalObject;
        }

        //[HttpGet]
        //public string[] GetCategories()
        //{
        //    return dal.GetMovieCategories();
        //}

        [HttpGet]
        public IEnumerable<Movie> GetMoviesByCategory(string category)
        {
            return dal.GetMoviesByCategory(category);
        }

        [HttpGet("Random")]
        public Movie GetRandomMovie()
        {
            return dal.GetRandomMovie();
        }
        [HttpGet("RandomByCat")]
        public Movie GetRandomMovieByCategory(string category)
        {
            return dal.GetRandomMovieByCategory(category);
        }
    }


}
