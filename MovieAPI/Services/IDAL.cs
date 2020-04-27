using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Services
{
    public interface IDAL
    {
        Movie GetMovieById(int id);
        string[] GetMovieCategories();
        IEnumerable<Movie> GetMoviesByCategory(string category);
        IEnumerable<Movie> GetMoviesAll();
        Movie GetRandomMovie();
        Movie GetRandomMovieByCategory(string category);
    }
}
