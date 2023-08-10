using Movie_HotFlix.Models;

namespace Movie_HotFlix.Api
{
    public class MovieApi
    {
        /// <summary>
        /// Get Popular MovieList With Page
        /// </summary>
        /// <param name="page">Current page number</param>
        /// <returns></returns>
        public static async Task<Movies> GetPopularMovies(int page = 1)
        {
            return await SendApi<Movies>($"https://api.themoviedb.org/3/movie/popular?language=ru-ru&page={page}&include_adult=true");
        }
        /// <summary>
        /// Get Similar MovieList With Page
        /// </summary>
        /// <param name="movieId">Current movie id</param>
        /// <param name="page">Current page number</param>
        /// <returns></returns>
        public static async Task<Movies> GetSimilarMovies(int movieId, int page = 1)
        {
            return await SendApi<Movies>($"https://api.themoviedb.org/3/movie/{movieId}/similar?&language=ru-ru&page={page}&include_adult=true");
        }
        /// <summary>
        /// Get MovieList By Year and Name With Page
        /// </summary>
        /// <param name="name">Movie name to search</param>
        /// <param name="year">Movie release year</param>
        /// <param name="page">Current page number</param>
        /// <returns></returns>
        public static async Task<Movies> GetMoviesByYearAndName(string name, int year, int page = 1)
        {
            return await SendApi<Movies>($"https://api.themoviedb.org/3/search/movie?language=ru-ru&query={name}&page={page}&include_adult=false&year={year}");
        }
        /// <summary>
        /// Get MovieList By Name With Page
        /// </summary>
        /// <param name="name">Movie name to search</param>
        /// <param name="page">Current page number</param>
        /// <returns></returns>
        public static async Task<Movies> GetMoviesByName(string name, int page = 1)
        {
            return await SendApi<Movies>($"https://api.themoviedb.org/3/search/movie?language=ru-ru&query={name}&page={page}&include_adult=true");
        }
        /// <summary>
        /// Get MovieList By Genre With Page
        /// </summary>
        /// <param name="genreId">Genre id form movies search</param>
        /// <param name="page">Current page number</param>
        /// <returns></returns>
        public static async Task<Movies> GetMoviesByGenre(int genreId, int page = 1)
        {
            return await SendApi<Movies>($"https://api.themoviedb.org/3/discover/movie?with_genres={genreId}&page={page}&include_adult=true?language=ru-ru");
        }
        /// <summary>
        /// Get Movie By Id
        /// </summary>
        /// <param name="id">Current movie Id</param>
        /// <returns></returns>
        public static async Task<Movie> GetMovieById(int id)
        {
            return await SendApi<Movie>($"https://api.themoviedb.org/3/movie/{id}?language=ru-ru");
        }
        /// <summary>
        /// Get Genre List
        /// </summary>
        /// <returns></returns>
        public static async Task<Genres> GetGenreList()
        {
            return await SendApi<Genres>("https://api.themoviedb.org/3/genre/movie/list?language=ru-ru");
        }
        private static async Task<T> SendApi<T>(string query)
        {
            string apiKey = GetMovieApiKey();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage = await httpClient.GetAsync(query + $"&api_key={apiKey}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadFromJsonAsync<T>();
                return result;
            }
            else
            {
                throw new Exception("Error. Try again later.");
            }
        }
        private static string GetMovieApiKey()
        {
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            var connectionString = config.GetSection("MovieApi:ApiKey");
            return connectionString.Value;
        }
    }
}
