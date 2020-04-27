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

    public class MovieController : ControllerBase
    {
        private IDAL dal;
        public MovieController(IDAL dalObject)
        {
            dal = dalObject;
        }


        [HttpGet("categories")]
        public string[] GetCategories()
        {
            return dal.GetMovieCategories();
        }

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return dal.GetMoviesAll();

        }

    }
}