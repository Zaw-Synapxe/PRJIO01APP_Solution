using JQueryAJAX.WebApp.Exceptions;
using JQueryAJAX.WebApp.Models;

namespace JQueryAJAX.WebApp.Data
{
    public class MyMovies
    {
        private List<Movie> _movies = new()
        {
            new Movie
            {
                Id = 1,
                Rating = 5,
                ReleaseDate = new DateTime(2001, 7, 12),
                Title = "Shrek"
            },
            new Movie
            {
                Id = 2,
                Rating = 3,
                ReleaseDate = new DateTime(2010, 7, 22),
                Title = "Inception"
            },
            new Movie
            {
                Id = 3,
                Rating = 4,
                ReleaseDate = new DateTime(1999, 6, 17),
                Title = "The Matrix"
            }
        };

        public IEnumerable<Movie> GetAll()
        {
            return _movies;
        }

        public Movie Get(int id)
        {
            //if (id <= 0)
            //    return null;

            //return _movies.SingleOrDefault(x => x.Id == id);

            if (id <= 0)
                throw new ArgumentException("Id cannot be zero or less.");

            return _movies.SingleOrDefault(x => x.Id == id);
        }

        public void Delete(int id)
        {
            //Movie toDelete = Get(id);
            //_movies.Remove(toDelete);

            Movie toDelete = Get(id);

            if (toDelete == null)
                throw new MovieNotFoundException();

            _movies.Remove(toDelete);
        }

        public void Create(Movie movie)
        {
            _movies.Add(movie);
        }

        //
    }
}
