using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Text.Json;

namespace RateLimitApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static async Task GetRateLimit()
        {
            string github_pat;
            Console.WriteLine("Enter Github PAT");
            github_pat = Console.ReadLine();
            //github_pat = "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";

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + github_pat);

            var streamTask = client.GetStreamAsync("https://api.github.com/rate_limit");
            var rate = await JsonSerializer.DeserializeAsync<Rate>(await streamTask);
            Console.WriteLine(rate.resources.core.limit);
            Console.WriteLine(rate.resources.core.remaining);
            if (rate.resources.core.remaining/rate.resources.core.limit < .1)
            {
                Environment.Exit(1);
            }
            else
            {
                Environment.Exit(0);
            }
        }
        static async Task Main(string[] args)
        {
            try
            {
                await GetRateLimit();
            }
            catch (Exception err)
            {
                Console.WriteLine("ERROR: " + err.Message);
            }
        }
    }
}
