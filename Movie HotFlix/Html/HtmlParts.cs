using Movie_HotFlix.Models;
using System.Text;

namespace Movie_HotFlix.Html
{
    public class HtmlParts
    {
        
        public static string GetHtmlPage(string body, string query = "")
        {
            return $"""
                <!DOCTYPE html>
                <html lang="ru">
                <head>
                	<meta charset="utf-8">
                	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
                    {GetStyleSection()}
                    {GetMetaSection()}
                </head>
                <body class="body">
                {GetHeaderSection(query)}
                {body}
                {GetFooterSection()}
                {GetScriptSection()}
                </body>
                </html>
                """;
        }        
        public static string GetHtmlPages(int currentPage, int totalPages, string action = "/", string query = "", int? categoryId = null)
        {
            string pages = $"""
                            <li class="paginator__item paginator__item--prev">
                				<a href="{action}?query={query}&categoryId={categoryId}&page={currentPage - 1}"><i class="icon ion-ios-arrow-back"></i></a>
                			</li>
                			<li class="paginator__item"><a href="{action}?query={query}&categoryId={categoryId}&page={currentPage - 1}">{currentPage - 1}</a></li>
                			<li class="paginator__item paginator__item--active"><a href="{action}?query={query}&categoryId={categoryId}&page={currentPage}">{currentPage}</a></li>
                			<li class="paginator__item"><a href="{action}?query={query}&categoryId={categoryId}&page={currentPage + 1}">{currentPage + 1}</a></li>
                			<li class="paginator__item"><a href="{action}?query={query}&categoryId={categoryId}&page={totalPages}">{totalPages}</a></li>
                			<li class="paginator__item paginator__item--next">
                				<a href="{action}?query={query}&categoryId={categoryId}&page={currentPage + 1}"><i class="icon ion-ios-arrow-forward"></i></a>
                			</li>
                """;
            if (currentPage == 1)
            {
                pages = $"""
                    <li class="paginator__item paginator__item--prev">
                    				<a href="#"><i class="icon ion-ios-arrow-back disable"></i></a>
                    			</li>
                    			<li class="paginator__item paginator__item--active"><a href="{action}?query={query}&categoryId={categoryId}&page={currentPage}">{currentPage}</a></li>
                    			<li class="paginator__item"><a href="{action}?query={query}&categoryId={categoryId}&page={currentPage + 1}">{currentPage + 1}</a></li>
                    			<li class="paginator__item"><a href="{action}?query={query}&categoryId={categoryId}&page={currentPage + 2}">{currentPage + 2}</a></li>
                    			<li class="paginator__item"><a href="{action}?query={query}&categoryId={categoryId}&page={totalPages}">{totalPages}</a></li>
                    			<li class="paginator__item paginator__item--next">
                    				<a href="{action}?query={query}&categoryId={categoryId}&page={currentPage + 1}"><i class="icon ion-ios-arrow-forward"></i></a>
                    			</li>
                    """;
            }
            else if (currentPage < totalPages - 1 && currentPage > 2)
            {
                pages = $"""
                    <li class="paginator__item paginator__item--prev">
                    				<a href="{action}?query={query}&categoryId={categoryId}&page={currentPage - 1}"><i class="icon ion-ios-arrow-back"></i></a>
                    			</li>
                    <li class="paginator__item"><a href="{action}?query={query}&categoryId={categoryId}&page=1">1</a></li>
                    			<li class="paginator__item"><a href="{action}?query={query}&categoryId={categoryId}&page={currentPage - 1}">{currentPage - 1}</a></li>
                    			<li class="paginator__item paginator__item--active"><a href="{action}?query={query}&categoryId={categoryId}&page={currentPage}">{currentPage}</a></li>
                    			<li class="paginator__item"><a href="{action}?query={query}&categoryId={categoryId}&page={currentPage + 1}">{currentPage + 1}</a></li>
                    			<li class="paginator__item"><a href="{action}?query={query}&categoryId={categoryId}&page={totalPages}">{totalPages}</a></li>
                    			<li class="paginator__item paginator__item--next">
                    				<a href="{action}?query={query}&categoryId={categoryId}&page={currentPage + 1}"><i class="icon ion-ios-arrow-forward"></i></a>
                    			</li>
                    """;
            }
            return $"""
                <div class="row">
                	<!-- paginator -->
                	<div class="col-12">
                		<ul class="paginator">
                			{pages}
                		</ul>
                	</div>
                	<!-- end paginator -->
                </div>
                """;
        }
        public static string GetMoviePage(Movie movie)
        {
            var stGenres = new StringBuilder();
            foreach (Genre genre in movie.genres)
            {
                stGenres.Append($"<a href=\"/genres/{genre.id}\">{genre.name}</a>");
            }
            var stCountries = new StringBuilder();
            foreach (ProductionCountry country in movie.production_countries)
            {
                stCountries.Append($"<span>{country.name}</span>, ");
            }
            stCountries = stCountries.Remove(stCountries.Length - 2, 2);
            return $"""
                <section class="section section--details section--bg" data-bg="img/section/details.jpg" style="background: url(&quot;img/section/details.jpg&quot;) center center / cover no-repeat;">
                	<!-- details content -->
                	<div class="container">
                		<div class="row">
                			<!-- title -->
                			<div class="col-12">
                				<h1 class="section__title section__title--mb">{movie.title}</h1>
                			</div>
                			<!-- end title -->

                			<!-- content -->
                			<div class="col-12 col-xl-6">
                				<div class="card card--details">
                					<div class="row">
                						<!-- card cover -->
                						<div class="col-12 col-sm-5 col-md-4 col-lg-3 col-xl-5">
                							<div class="card__cover">
                								<img src="https://image.tmdb.org/t/p/original/{movie.poster_path}" alt="{movie.title}">
                								<span class="card__rate card__rate--green">{movie.vote_average}</span>
                							</div>
                							<a href="http://www.youtube.com/watch?v=0O2aH4XLbto" class="card__trailer">
                                            <i class="icon ion-ios-play-circle"></i> Watch trailer</a>
                						</div>
                						<!-- end card cover -->

                						<!-- card content -->
                						<div class="col-12 col-md-8 col-lg-9 col-xl-7">
                							<div class="card__content">
                								<ul class="card__meta">
                									<li><span>Статус:</span> {movie.status}</li>
                									<li><span>Страны:</span> {stCountries}</li>
                									<li><span>Бюджет:</span> {movie.budget}</li>              									
                									<li><span>Жанры:</span> {stGenres}</li>
                									<li><span>Дата выпуска:</span> {movie.release_date}</li>
                								</ul>
                								<div class="card__description mCustomScrollbar _mCS_2" style="position: relative; 
                                                overflow: visible;"><div id="mCSB_2" class="mCustomScrollBox mCS-custom-bar3 mCSB_vertical 
                                                mCSB_outside" tabindex="0" style="max-height: none;">
                                                <div id="mCSB_2_container" class="mCSB_container" style="position:relative; top:0; left:0;" dir="ltr">
                								
                                                {movie.overview}
                								
                                                </div></div><div id="mCSB_2_scrollbar_vertical" class="mCSB_scrollTools mCSB_2_scrollbar mCS-custom-bar3 mCSB_scrollTools_vertical" style="display: block;"><div class="mCSB_draggerContainer"><div id="mCSB_2_dragger_vertical" class="mCSB_dragger" style="position: absolute; min-height: 30px; display: block; height: 27px; max-height: 110px; top: 0px;"><div class="mCSB_dragger_bar" style="line-height: 30px;"></div><div class="mCSB_draggerRail"></div></div></div></div></div>
                							</div>
                						</div>
                						<!-- end card content -->
                					</div>
                				</div>
                			</div>
                			<!-- end content -->

                			<!-- player -->
                			<div class="col-12 col-xl-6">
                				<div tabindex="0" class="plyr plyr--full-ui plyr--video plyr--html5 plyr--paused plyr--stopped plyr--pip-supported plyr--fullscreen-enabled plyr--captions-enabled plyr__poster-enabled"><div class="plyr__controls"><button class="plyr__controls__item plyr__control" type="button" data-plyr="play" aria-label="Play"><svg class="icon--pressed" role="presentation" focusable="false"><use xlink:href="#plyr-pause"></use></svg><svg class="icon--not-pressed" role="presentation" focusable="false"><use xlink:href="#plyr-play"></use></svg><span class="label--pressed plyr__sr-only">Pause</span><span class="label--not-pressed plyr__sr-only">Play</span></button><div class="plyr__controls__item plyr__progress__container"><div class="plyr__progress"><input data-plyr="seek" type="range" min="0" max="100" step="0.01" value="0" autocomplete="off" role="slider" aria-label="Seek" aria-valuemin="0" aria-valuemax="183.125333" aria-valuenow="0" id="plyr-seek-4892" aria-valuetext="00:00 of 03:03" style="--value:0%;" seek-value="39.79517190929042"><progress class="plyr__progress__buffer" min="0" max="100" value="4.040402209125266" role="progressbar" aria-hidden="true">% buffered</progress><span class="plyr__tooltip" style="left: 38.2346%;">01:10</span></div></div><div class="plyr__controls__item plyr__time--current plyr__time" aria-label="Current time">-03:03</div><div class="plyr__controls__item plyr__volume"><button type="button" class="plyr__control" data-plyr="mute"><svg class="icon--pressed" role="presentation" focusable="false"><use xlink:href="#plyr-muted"></use></svg><svg class="icon--not-pressed" role="presentation" focusable="false"><use xlink:href="#plyr-volume"></use></svg><span class="label--pressed plyr__sr-only">Unmute</span><span class="label--not-pressed plyr__sr-only">Mute</span></button><input data-plyr="volume" type="range" min="0" max="1" step="0.05" value="1" autocomplete="off" role="slider" aria-label="Volume" aria-valuemin="0" aria-valuemax="100" aria-valuenow="100" id="plyr-volume-4892" aria-valuetext="100.0%" style="--value:100%;"></div><button class="plyr__controls__item plyr__control" type="button" data-plyr="captions"><svg class="icon--pressed" role="presentation" focusable="false"><use xlink:href="#plyr-captions-on"></use></svg><svg class="icon--not-pressed" role="presentation" focusable="false"><use xlink:href="#plyr-captions-off"></use></svg><span class="label--pressed plyr__sr-only">Disable captions</span><span class="label--not-pressed plyr__sr-only">Enable captions</span></button><div class="plyr__controls__item plyr__menu"><button aria-haspopup="true" aria-controls="plyr-settings-4892" aria-expanded="false" type="button" class="plyr__control" data-plyr="settings"><svg role="presentation" focusable="false"><use xlink:href="#plyr-settings"></use></svg><span class="plyr__sr-only">Settings</span></button><div class="plyr__menu__container" id="plyr-settings-4892" hidden=""><div><div id="plyr-settings-4892-home"><div role="menu"><button data-plyr="settings" type="button" class="plyr__control plyr__control--forward" role="menuitem" aria-haspopup="true"><span>Captions<span class="plyr__menu__value">Disabled</span></span></button><button data-plyr="settings" type="button" class="plyr__control plyr__control--forward" role="menuitem" aria-haspopup="true"><span>Quality<span class="plyr__menu__value">576p</span></span></button><button data-plyr="settings" type="button" class="plyr__control plyr__control--forward" role="menuitem" aria-haspopup="true"><span>Speed<span class="plyr__menu__value">Normal</span></span></button></div></div><div id="plyr-settings-4892-captions" hidden=""><button type="button" class="plyr__control plyr__control--back"><span aria-hidden="true">Captions</span><span class="plyr__sr-only">Go back to previous menu</span></button><div role="menu"><button data-plyr="language" type="button" role="menuitemradio" class="plyr__control" aria-checked="true" value="-1"><span>Disabled</span></button><button data-plyr="language" type="button" role="menuitemradio" class="plyr__control" aria-checked="false" value="0"><span>English<span class="plyr__menu__value"><span class="plyr__badge">EN</span></span></span></button><button data-plyr="language" type="button" role="menuitemradio" class="plyr__control" aria-checked="false" value="1"><span>Français<span class="plyr__menu__value"><span class="plyr__badge">FR</span></span></span></button></div></div><div id="plyr-settings-4892-quality" hidden=""><button type="button" class="plyr__control plyr__control--back"><span aria-hidden="true">Quality</span><span class="plyr__sr-only">Go back to previous menu</span></button><div role="menu"><button data-plyr="quality" type="button" role="menuitemradio" class="plyr__control" aria-checked="false" value="1080"><span>1080p<span class="plyr__menu__value"><span class="plyr__badge">HD</span></span></span></button><button data-plyr="quality" type="button" role="menuitemradio" class="plyr__control" aria-checked="false" value="720"><span>720p<span class="plyr__menu__value"><span class="plyr__badge">HD</span></span></span></button><button data-plyr="quality" type="button" role="menuitemradio" class="plyr__control" aria-checked="true" value="576"><span>576p<span class="plyr__menu__value"><span class="plyr__badge">SD</span></span></span></button></div></div><div id="plyr-settings-4892-speed" hidden=""><button type="button" class="plyr__control plyr__control--back"><span aria-hidden="true">Speed</span><span class="plyr__sr-only">Go back to previous menu</span></button><div role="menu"><button data-plyr="speed" type="button" role="menuitemradio" class="plyr__control" aria-checked="false" value="0.5"><span>0.5×</span></button><button data-plyr="speed" type="button" role="menuitemradio" class="plyr__control" aria-checked="false" value="0.75"><span>0.75×</span></button><button data-plyr="speed" type="button" role="menuitemradio" class="plyr__control" aria-checked="true" value="1"><span>Normal</span></button><button data-plyr="speed" type="button" role="menuitemradio" class="plyr__control" aria-checked="false" value="1.25"><span>1.25×</span></button><button data-plyr="speed" type="button" role="menuitemradio" class="plyr__control" aria-checked="false" value="1.5"><span>1.5×</span></button><button data-plyr="speed" type="button" role="menuitemradio" class="plyr__control" aria-checked="false" value="1.75"><span>1.75×</span></button><button data-plyr="speed" type="button" role="menuitemradio" class="plyr__control" aria-checked="false" value="2"><span>2×</span></button></div></div></div></div></div><button class="plyr__controls__item plyr__control" type="button" data-plyr="pip"><svg role="presentation" focusable="false"><use xlink:href="#plyr-pip"></use></svg><span class="plyr__sr-only">PIP</span></button><button class="plyr__controls__item plyr__control" type="button" data-plyr="fullscreen"><svg class="icon--pressed" role="presentation" focusable="false"><use xlink:href="#plyr-exit-fullscreen"></use></svg><svg class="icon--not-pressed" role="presentation" focusable="false"><use xlink:href="#plyr-enter-fullscreen"></use></svg><span class="label--pressed plyr__sr-only">Exit fullscreen</span><span class="label--not-pressed plyr__sr-only">Enter fullscreen</span></button></div><div class="plyr__video-wrapper"><video crossorigin="" playsinline="" poster="https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-HD.jpg" id="player" src="https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-576p.mp4">
                					<!-- Video files -->
                					<source src="https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-576p.mp4" type="video/mp4" size="576">
                					<source src="https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-720p.mp4" type="video/mp4" size="720">
                					<source src="https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-1080p.mp4" type="video/mp4" size="1080">

                					<!-- Caption files -->
                					<track kind="captions" label="English" srclang="en" src="https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-HD.en.vtt" default="">
                					<track kind="captions" label="Français" srclang="fr" src="https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-HD.fr.vtt">

                					<!-- Fallback for browsers that don't support the <video> element -->
                					<a href="https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-576p.mp4" download="">Download</a>
                				</video><div class="plyr__poster" style="background-image: url(&quot;https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-HD.jpg&quot;);"></div></div><div class="plyr__captions"></div><button type="button" class="plyr__control plyr__control--overlaid" data-plyr="play" aria-label="Play"><svg role="presentation" focusable="false"><use xlink:href="#plyr-play"></use></svg><span class="plyr__sr-only">Play</span></button></div>
                			</div>
                			<!-- end player -->
                		</div>
                	</div>
                	<!-- end details content -->
                </section>
                """;
        }
        public static string GetPopularMoviesSection(Movies popularMovies)
        {
            var st = new StringBuilder();
            foreach (Movie movie in popularMovies.Results)
            {
                st.Append($"""
                    <div class="card card--big">
                    						<div class="card__cover">
                    							<img src="https://image.tmdb.org/t/p/original/{movie.poster_path}" alt="{movie.title}">
                    							<a href="/details/{movie.id}" class="card__play">
                    								<i class="icon ion-ios-play"></i>
                    							</a>
                    							<span class="card__rate card__rate--yellow">{movie.vote_average}</span>
                    						</div>
                    						<div class="card__content">
                    							<h3 class="card__title"><a href="/details/{movie.id}">{movie.title}</a></h3>
                    							<span class="card__category">
                    								<span>Дата: {movie.release_date} | Счет: {movie.popularity}</span>
                    							</span>
                    						</div>
                    					</div>                    
                    """);
            }
            return $"""
                <section class="home">
                	<div class="container">
                		<div class="row">
                			<div class="col-12">
                				<h1 class="home__title"><b>Новые фильмы</b> ЭТОГО СЕЗОНА</h1>                
                				<button class="home__nav home__nav--prev" type="button">
                					<i class="icon ion-ios-arrow-round-back"></i>
                				</button>
                				<button class="home__nav home__nav--next" type="button">
                					<i class="icon ion-ios-arrow-round-forward"></i>
                				</button>
                			</div>
                
                			<div class="col-12">
                				<div class="owl-carousel home__carousel home__carousel--bg">

                				{st.ToString()}	

                				</div>
                			</div>
                		</div>
                	</div>
                </section>
                """;
        }
        private static string GetCategoriesSection(Genres allCategories, int? categoryId = null)
        {
            int count = 1;
            var stCategories = new StringBuilder("<ul class=\"nav nav-tabs content__tabs\" role=\"tablist\">" +
                $"<li class=\"nav-item\">\r\n    <a class=\"nav-link{(categoryId is null ? " active":"")}\" data-toggle=\"tab\" href=\"/\" " +
                $"role=\"tab\" aria-controls=\"tab-{count++}\" \r\n    aria-selected=\"false\">Все</a>\r\n</li>");
            foreach (Genre genre in allCategories.genres)
            {
                stCategories.Append($"""
                    <li class="nav-item">
                        <a class="nav-link{(categoryId == genre.id ? " active" : "")}" href="/category?categoryId={genre.id}" aria-controls="tab-{count++}">{genre.name}</a>
                    </li>
                    """);
                if (count % 10 == 0)
                {
                    stCategories.Append("</ul><ul class=\"nav nav-tabs content__tabs\" role=\"tablist\">");
                }
            }
            stCategories.Append("""
                </ul>
                <!-- content mobile tabs nav -->
                					<div class="content__mobile-tabs" id="content__mobile-tabs">
                						<div class="content__mobile-tabs-btn dropdown-toggle" role="navigation" id="mobile-tabs" 
                                        data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                							<input type="button" value="Каталог фильмов">
                							<span></span>
                						</div>
                <div class="content__mobile-tabs-menu dropdown-menu" aria-labelledby="mobile-tabs">
                							<ul class="nav nav-tabs" role="tablist">
                """);
            count = 1;
            foreach (Genre genre in allCategories.genres)
            {
                stCategories.Append($"""
                    <li class="nav-item" data-value="movies"><a class="nav-link" 
                    href="/category?categoryId={genre.id}"  aria-controls="tab-{count++}">{genre.name}</a></li>
                    """);
            }

            stCategories.Append("""
                </ul>
                						</div>
                					</div>
                """);

            return stCategories.ToString();
        }
        public static string GetMoviesSection(Movies popularMovies, Genres allCategories, int? categoryId = null)
        {
            var st = new StringBuilder();
            foreach (Movie movie in popularMovies.Results)
            {
                st.Append($"""
                                    <div class="col-6 col-sm-4 col-md-3 col-xl-2">
                    						<div class="card">
                    							<div class="card__cover">
                    								<img src="https://image.tmdb.org/t/p/original/{movie.poster_path}" alt="{movie.title}">
                    								<a href="/details/{movie.id}" class="card__play">
                    								<i class="icon ion-ios-play"></i>
                    							</a>
                    								<span class="card__rate card__rate--green">{movie.vote_average}</span>
                    							</div>
                    							<div class="card__content">
                    								<h3 class="card__title"><a href="/details/{movie.id}">{movie.title}</a></h3>
                    								<span class="card__category">
                    								<span>Дата: {movie.release_date} | Счет: {movie.popularity}</span>
                    							    </span>
                    							</div>
                    						</div>
                    					</div>              
                    """);
            }

            return $"""
                <section class="content">
                	<div class="content__head">
                		<div class="container">
                			<div class="row">
                				<div class="col-12">
                					<!-- content title -->
                					<h2 class="content__title">Каталог фильмов</h2>
                					<!-- end content title -->

                					{GetCategoriesSection(allCategories, categoryId)}

                				</div>
                			</div>
                		</div>
                	</div>

                	<div class="container">
                		<!-- content tabs -->
                		<div class="tab-content">
                			<div class="tab-pane fade show active" id="tab-1" role="tabpanel" aria-labelledby="1-tab">
                				<div class="row row--grid">

                					{st}

                				</div>
                			</div>
                		</div>
                		<!-- end content tabs -->
                	</div>
                </section>
                """;
        }
        public static string GetNotFoundPage()
        {
            return """
                <div class="page-404 section--bg" data-bg="img/section/section.jpg" 
                style="background: url(&quot;img/section/section.jpg&quot;) center center / cover no-repeat;">
                	<div class="container">
                		<div class="row">
                			<div class="col-12">
                				<div class="page-404__wrap">
                					<div class="page-404__content">
                						<h1 class="page-404__title">404</h1>
                						<p class="page-404__text">Страница, которую вы ищете, недоступна!</p>
                						<a href="/" class="page-404__btn">Главная</a>
                					</div>
                				</div>
                			</div>
                		</div>
                	</div>
                </div>
                """;
        }
        static string GetHeaderSection(string query)
        {
            return $"""
                <header class="header">
                	<div class="container">
                		<div class="row">
                			<div class="col-12">
                				<div class="header__content">
                					<!-- header logo -->
                					<a href="/" class="header__logo">
                						<img src="../img/logo.svg" alt="">
                					</a>
                					<!-- end header logo -->

                					<!-- header nav -->
                					<ul class="header__nav">
                						<li class="header__nav-item">
                							<a class="dropdown-toggle header__nav-link" href="/" 
                							role="button" >Главная</a>
                						</li>
                						<li class="header__nav-item">
                							<a class="dropdown-toggle header__nav-link" href="#" 
                							role="button">О нас</a>
                						</li>
                						<li class="header__nav-item">
                							<a class="dropdown-toggle header__nav-link" href="#" 
                							role="button">Контакты</a>
                						</li>
                					</ul>
                					<!-- end header nav -->

                					<!-- header search -->
                					<div class="header__auth">
                						<form action="search" class="header__search" method="post">
                							<input class="header__search-input" name="searchQuery" type="text" placeholder="Поиск..."
                                            value = "{query}">
                							<button class="header__search-button" type="submit">
                								<i class="icon ion-ios-search"></i>
                							</button>
                							<button class="header__search-close" type="button">
                								<i class="icon ion-md-close"></i>
                							</button>
                						</form>

                						<button class="header__search-btn" type="button">
                							<i class="icon ion-ios-search"></i>
                						</button>
                					</div>
                					<!-- end header search -->

                					<!-- header menu btn -->
                					<button class="header__btn" type="button">
                						<span></span>
                						<span></span>
                						<span></span>
                					</button>
                				</div>
                			</div>
                		</div>
                	</div>
                </header>
                """;
        }
        static string GetFooterSection()
        {
            return """
                <footer class="footer">
                	<div class="container">
                		<div class="row">
                			<div class="col-12">
                				<div class="footer__content">
                					<a href="index" class="footer__logo">
                						<img src="img/logo.svg" alt="">
                					</a>

                					<span class="footer__copyright">©HotFlix 2023</span>

                					<nav class="footer__nav">
                						<a href="/">Главная</a>
                						<a href="#">О нас</a>
                						<a href="#">Контакты</a>
                					</nav>

                					<button class="footer__back" type="button">
                						<i class="icon ion-ios-arrow-round-up"></i>
                					</button>
                				</div>
                			</div>
                		</div>
                	</div>
                </footer>
                """;
        }
        static string GetStyleSection()
        {
            return """
                <link rel="stylesheet" href="../css/bootstrap-reboot.min.css">
                <link rel="stylesheet" href="../css/bootstrap-grid.min.css">
                <link rel="stylesheet" href="../css/owl.carousel.min.css">
                <link rel="stylesheet" href="../css/jquery.mCustomScrollbar.min.css">
                <link rel="stylesheet" href="../css/nouislider.min.css">
                <link rel="stylesheet" href="../css/ionicons.min.css">
                <link rel="stylesheet" href="../css/magnific-popup.css">
                <link rel="stylesheet" href="../css/plyr.css">
                <link rel="stylesheet" href="../css/photoswipe.css">
                <link rel="stylesheet" href="../css/default-skin.css">
                <link rel="stylesheet" href="../css/main.css">
                """;
        }
        static string GetMetaSection(string title = "HotFlix")
        {
            return $"""
                <link rel="icon" type="image/png" href="icon/favicon-32x32.png" sizes="32x32">
                <link rel="apple-touch-icon" href="icon/favicon-32x32.png">

                <meta name="description" content="">
                <meta name="keywords" content="">
                <meta name="author" content="">
                <title>{title}</title>
                """;
        }
        static string GetScriptSection()
        {
            return """
                <script src="../js/jquery-3.5.1.min.js"></script>
                <script src="../js/bootstrap.bundle.min.js"></script>
                <script src="../js/owl.carousel.min.js"></script>
                <script src="../js/jquery.magnific-popup.min.js"></script>
                <script src="../js/jquery.mousewheel.min.js"></script>
                <script src="../js/jquery.mCustomScrollbar.min.js"></script>
                <script src="../js/wNumb.js"></script>
                <script src="../js/nouislider.min.js"></script>
                <script src="../js/plyr.min.js"></script>
                <script src="../js/photoswipe.min.js"></script>
                <script src="../js/photoswipe-ui-default.min.js"></script>
                <script src="../js/main.js"></script>
                """;
        }
    }
}
