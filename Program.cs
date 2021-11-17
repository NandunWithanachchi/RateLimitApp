using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RateLimitApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private static async Task GetRateLimit()
        {
            string github_pat;
            
            Console.WriteLine("This application will check rate limit to remaining limit ratio of a given github PAT and return below exit codes");
            Console.WriteLine("0 - If Threshold is greater than or equal to  10%.");
            Console.WriteLine("1 - If Threshold is greater less than 10%.");
            Console.WriteLine("Please Enter Github PAT");
            github_pat = Console.ReadLine();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "1.0");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + github_pat);

            var streamTask = client.GetStreamAsync("https://api.github.com/rate_limit");
            var rate = await JsonSerializer.DeserializeAsync<Rate>(await streamTask);
            
            if (rate.resources.core.limit != 0 ) 
            {
                Console.WriteLine("Rate Limit: " + rate.resources.core.limit);
                Console.WriteLine("Remaining: " + rate.resources.core.remaining);
                Console.WriteLine("Threshold: "+ rate.resources.core.remaining / rate.resources.core.limit*100 + "%");
                if (rate.resources.core.remaining/rate.resources.core.limit < .1)
                {
                    Console.WriteLine("Exit Code 1");
                    Environment.Exit(1);
                }
                else
                {
                    Console.WriteLine("Exit Code 0");
                    Environment.Exit(0);
                }
            }
            else
            {
                throw new Exception("Threshold cannot be determined. Rate limit is 0.");
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
