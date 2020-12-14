# DistributedCacheDemo
 
 This project demonstrates how to use Redis-based distributed caching in an ASP.NET Core Web API.

To run the project you need to get an API key from [here](https://developers.themoviedb.org/3/getting-started/introduction) as the API makes an 
external API call to TMDB site.

Then update the following line in `TmdbApiCall` class with your API key:

```
const string apiKey = "your API key";
```

This project was originally developed as a demo project for the following post:
