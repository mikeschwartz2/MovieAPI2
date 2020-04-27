using Dapper;
using Microsoft.Extensions.Configuration;
using MovieAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MovieAPI.Services
{
    public class DALSqlServer : IDAL
    {
        private string connectionString;

        public DALSqlServer(IConfiguration config)
        {
            connectionString = config.GetConnectionString("MovieDB");
        }

        public IEnumerable<Movie> GetMoviesAll()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = "SELECT * FROM Movies";

            IEnumerable<Movie> movies = connection.Query<Movie>(queryString);

            return movies;
        }

        public Movie GetMovieById(int id)
        {
            SqlConnection connection = null;
            string queryString = "SELECT * FROM Movies WHERE Id = @id";
            Movie movie = null;

            try
            {
                connection = new SqlConnection(connectionString);
                movie = connection.QueryFirstOrDefault<Movie>(queryString, new { id = id });
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return movie;
        }

        public string[] GetMovieCategories()
        {
            SqlConnection connection = null;
            string queryString = "SELECT DISTINCT Category FROM Movies";
            IEnumerable<Movie> movies = null;

            try
            {
                connection = new SqlConnection(connectionString);
                movies = connection.Query<Movie>(queryString);
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            if (movies == null)
            {
                return null;
            }
            else
            {
                string[] categories = new string[movies.Count()];
                int count = 0;

                foreach (Movie m in movies)
                {
                    categories[count] = m.Category;
                    count++;
                }

                return categories;
            }

        }

        public int GetNumberOfMovies()
        {
            string queryString = $"SELECT COUNT (*) FROM dbo.Movies";
            var connection = new SqlConnection(connectionString);
            int movieCount = connection.ExecuteScalar<int>(queryString);

            return movieCount;
        }
        public int GetNumberOfMoviesByCategory(string category)
        {
            string queryString = $"SELECT COUNT (*) FROM dbo.Movies WHERE category = '{category}'";
            var connection = new SqlConnection(connectionString);
            int movieCount = connection.ExecuteScalar<int>(queryString);

            return movieCount;
        }

        public Movie GetRandomMovie()
        {
            int numberOfMovies = GetNumberOfMovies();
            numberOfMovies++;
            Random random = new Random();
            int randomId = random.Next(0, numberOfMovies);

            SqlConnection connection = null;
            string queryString = $"SELECT * FROM Movies WHERE Id = {randomId}";
            Movie movie = null;

            try
            {
                connection = new SqlConnection(connectionString);
                movie = connection.QueryFirstOrDefault<Movie>(queryString, new { id = randomId });
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return movie;
        }

        public IEnumerable<Movie> GetMoviesByCategory(string category)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string queryString = "SELECT * FROM Movies WHERE Category = @category";

            IEnumerable<Movie> movies = connection.Query<Movie>(queryString, new { category = category });

            return movies;
        }
        public Movie GetRandomMovieByCategory(string category)
        {
            int numberOfMovies = GetNumberOfMoviesByCategory(category);
            Random random = new Random();
            int randomId = random.Next(0, numberOfMovies);

            IEnumerable<Movie> movies = GetMoviesByCategory(category);

            SqlConnection connection = null;
            string queryString = $"SELECT * FROM Movies WHERE Category = @category LIMIT 1 OFFSET {randomId}";
            Movie movie = null;

            try
            {
                connection = new SqlConnection(connectionString);
                movie = connection.QueryFirstOrDefault<Movie>(queryString, new { id = randomId });
            }
            catch (Exception e)
            {
                //log the error--get details from e
            }
            finally //cleanup!
            {
                if (connection != null)
                {
                    connection.Close(); //explicitly closing the connection
                }
            }

            return movie;


        }

    }
}
