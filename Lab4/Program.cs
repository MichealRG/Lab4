using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Lab4
{

    class Program
    {
        static async Task  Main(string[] args)
        {
            //zainstalowanie restsharpa zeby korzystac z internetowego API
            //var client = new RestClient("http://www.google.pl"); //pobranie strony googla
            //var request = new RestRequest("/");
            //var respose = client.Get(request); 
            //Console.WriteLine(respose.Content);

            //takei samo działanie jak wyzej pobieranie jednej strony po drugiej dopoki jednej nie pobierze to nie pobiera drugiej
            var stopwatch = new Stopwatch();

            var google = new WebSite("https://wwww.googel.pl");
            var ath = new WebSite("https://ath.bielsko.pl");
            var fb = new WebSite("https://facebook.com");
            var wiki = new WebSite("https://en.wikipedia.org/");
            var anyapi = new WebSite("https://any-api.com/");
            var plany = new WebSite("https://plany.ath.bielsko.pl");

            //stopwatch.Start();
            //google.Download("/");
            //Console.WriteLine(stopwatch.Elapsed);
            //ath.Download("/");
            //Console.WriteLine(stopwatch.Elapsed);
            //fb.Download("/");
            //Console.WriteLine(stopwatch.Elapsed);
            //wiki.Download("/wiki/.NET_Core");
            //Console.WriteLine(stopwatch.Elapsed);
            //anyapi.Download("/wiki/.NET_Core");
            //Console.WriteLine(stopwatch.Elapsed);
            //plany.Download("/plan.php?type=0&id=12647");
            //Console.WriteLine(stopwatch.Elapsed);
            //ath.Download("/graficzne-formy-przekazu-informacji/");
            //Console.WriteLine(stopwatch.Elapsed);
            //stopwatch.Stop();
            //Console.WriteLine(google.Download("/")); //wyswietlenie doctype...


            //zeby się działo równolegle trzeba korzystać z asyncu ale tutaj jeszcze nie wykonały ale odpowiedz juz jest bo task dostał obietnice ze sie wykona (robimy to gdy dane mamy wyslac ale nie sa nam potrzebne)
            //--------------------------------

            var tasks = new List<Task<IRestResponse>>();

            stopwatch.Start();
            tasks.Add(google.DownloadAsync("/"));
            Console.WriteLine(stopwatch.Elapsed);
            tasks.Add(ath.DownloadAsync("/"));
            Console.WriteLine(stopwatch.Elapsed);
            tasks.Add(fb.DownloadAsync("/"));
            Console.WriteLine(stopwatch.Elapsed);
            tasks.Add(wiki.DownloadAsync("/wiki/.NET_Core"));
            Console.WriteLine(stopwatch.Elapsed);
            tasks.Add(anyapi.DownloadAsync("/wiki/.NET_Core"));
            Console.WriteLine(stopwatch.Elapsed);
            tasks.Add(plany.DownloadAsync("/plan.php?type=0&id=12647"));
            Console.WriteLine(stopwatch.Elapsed);
            tasks.Add(ath.DownloadAsync("/graficzne-formy-przekazu-informacji/"));
            Console.WriteLine(stopwatch.Elapsed);

            Console.WriteLine("--------------");

            //Task.WhenAny(tasks).GetAwaiter().GetResult(); //ten task zaskoczy zaskoczy gdy ktorykowliek watkow sie zakonczy
            //Console.WriteLine(stopwatch.Elapsed);

            //Console.WriteLine( Task.WhenAny(tasks).Result.Result.Content);
            ////tutaj dostane cała strone
            
            Task.WhenAll(tasks).GetAwaiter().GetResult(); //tutaj await trzeba było metoda zrobic asyncowa
            Console.WriteLine(stopwatch.Elapsed); // przez tego awaitera oczkuje na wykonanie taskow zanim zakonczy program


            //tutaj  wyswietli wszystkie pobrane strony w htmlu
            var htmlCodes = Task.WhenAll(tasks).Result;
            foreach (var item in htmlCodes)
            {
                Console.WriteLine(item.Content.Length);
                // Console.WriteLine(item.Content);
            }

            //wiekszosc taskow pewnie była gotowa w sek ale czekało na najdłuzsze bo jest WHEN ALL
            //--------------------------------

            //teraz z awaitami gdy chcemy miec jakies dane UWAGA MAIN MUSI BYC ASYNC
            // stopwatch.Start();
            // Console.WriteLine(  await google.DownloadAsync("/")); //kazde wywolanie z ktorego interesuje nas wynik musi byc await ale robienie go od razu nie jest najlepszym pomysłem
            // Console.WriteLine(stopwatch.Elapsed);
            // Console.WriteLine(await  ath.DownloadAsync("/"));
            // Console.WriteLine(stopwatch.Elapsed);
            // Console.WriteLine(  await  fb.DownloadAsync("/"));
            // Console.WriteLine(stopwatch.Elapsed);
            // Console.WriteLine(  await wiki.DownloadAsync("/wiki/.NET_Core"));
            // Console.WriteLine(stopwatch.Elapsed);
            // Console.WriteLine(  await anyapi.DownloadAsync("/wiki/.NET_Core"));
            // Console.WriteLine(stopwatch.Elapsed);
            // Console.WriteLine(  await plany.DownloadAsync("/plan.php?type=0&id=12647"));
            // Console.WriteLine(stopwatch.Elapsed);
            // Console.WriteLine(  await ath.DownloadAsync("/graficzne-formy-przekazu-informacji/"));
            // Console.WriteLine(stopwatch.Elapsed);
            // //inne opcja to GetAwaiter.GetReuslt
            // //albo cw(google.DownloadAsync("/").Result); //czeka na wynik tak jak await
            // Console.WriteLine("--------------");

            //// await Task.WhenAny(tasks); //ten task zaskoczy zaskoczy gdy ktorykowliek watkow sie zakonczy
            // Console.WriteLine(stopwatch.Elapsed);


            //  await Task.WhenAll(tasks); //tutaj await trzeba było metoda zrobic asyncowa
            Console.WriteLine(stopwatch.Elapsed); // przez tego awaitera oczkuje na wykonanie taskow zanim zakonczy program
                                                  //wiekszosc taskow pewnie była gotowa w sek ale czekało na najdłuzsze bo jest WHEN ALL
            stopwatch.Stop();

            Console.ReadKey(true);
            //cwiczenie 
            //pobierz na rok 2019 liste coachow 
            //https://api.collegefootballdata.com/api/docs/?url=/api-docs.json

        }
    }
}
