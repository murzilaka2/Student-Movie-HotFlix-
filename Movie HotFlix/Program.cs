using Microsoft.AspNetCore.Builder;
using Movie_HotFlix.Api;
using Movie_HotFlix.Html;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.UseStaticFiles();

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Response.Redirect("/notFound");
    }
});

app.Map("/", async (HttpContext context) =>
{
    int currentPage = 1;
    string page = context.Request.Query["page"];
    if (int.TryParse(page, out int pageNumber))
    {
        currentPage = pageNumber;
    }
    try
    {
        var allCategories = await MovieApi.GetGenreList();
        var popularMovies = await MovieApi.GetPopularMovies(page: 1);
        var movies = await MovieApi.GetPopularMovies(page: currentPage);
        await context.Response.WriteAsync(HtmlParts.GetHtmlPage($"""
        {HtmlParts.GetPopularMoviesSection(popularMovies)}
        {HtmlParts.GetMoviesSection(movies, allCategories)}
        {HtmlParts.GetHtmlPages(movies.Page, 500)}
        """));
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        context.Response.Redirect("/");
    }
});

app.Map("/category", async (HttpContext context) =>
{
    int id = 1, currentPage = 1;
    string categoryId = context.Request.Query["categoryId"];
    string page = context.Request.Query["page"];
    if (int.TryParse(categoryId, out int newId))
    {
        id = newId;
    }
    if (int.TryParse(page, out int pageNumber))
    {
        currentPage = pageNumber;
    }
    try
    {
        var allCategories = await MovieApi.GetGenreList();
        var movies = await MovieApi.GetMoviesByGenre(genreId: id, page: currentPage);
        await context.Response.WriteAsync(HtmlParts.GetHtmlPage($"""
        {HtmlParts.GetMoviesSection(movies, allCategories, id)}
        {HtmlParts.GetHtmlPages(movies.Page, movies.Total_Pages, action: "/category", categoryId: id)}
        """));
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        context.Response.Redirect("/");
    }

});

app.Map("/search", async (HttpContext context) =>
{
    var allCategories = await MovieApi.GetGenreList();
    if (context.Request.Method == "POST")
    {
        var form = context.Request.Form;
        string query = form["searchQuery"];
        try
        {
            var movies = await MovieApi.GetMoviesByName(name: query, page: 1);
            await context.Response.WriteAsync(HtmlParts.GetHtmlPage($"""
        {HtmlParts.GetMoviesSection(movies, allCategories)}
        {HtmlParts.GetHtmlPages(movies.Page, movies.Total_Pages, action: "search", query: query)}
        """, query: query));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            context.Response.Redirect("/");
        }
    }
    else
    {
        int currentPage = 1;
        string page = context.Request.Query["page"];
        string query = context.Request.Query["query"];
        if (int.TryParse(page, out int pageNumber))
        {
            currentPage = pageNumber;
        }
        try
        {
            var movies = await MovieApi.GetMoviesByName(name: query, page: currentPage);
            await context.Response.WriteAsync(HtmlParts.GetHtmlPage($"""
        {HtmlParts.GetMoviesSection(movies, allCategories)}
        {HtmlParts.GetHtmlPages(movies.Page, movies.Total_Pages, action: "search", query: query)}
        """, query: query));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            context.Response.Redirect("/");
        }
    }
});

app.Map("/details/{movieId}", async (string movieId, HttpContext context) =>
{
    if (int.TryParse(movieId, out int id))
    {
        try
        {
            var currentMovie = await MovieApi.GetMovieById(id);
            if (currentMovie != null)
            {
                await context.Response.WriteAsync(HtmlParts.GetHtmlPage($"""
                {HtmlParts.GetMoviePage(currentMovie)}
                """));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            context.Response.Redirect("/");
        }
    }
    else
    {
        context.Response.Redirect("/");
    }
});

app.Map("/notFound", async (HttpContext context) =>
{
    await context.Response.WriteAsync(HtmlParts.GetHtmlPage($"""
        {HtmlParts.GetNotFoundPage()}
        """));
});


app.Run();




