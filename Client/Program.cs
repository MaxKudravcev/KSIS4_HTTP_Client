using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string url, value;
           
            Console.Write("Enter the url you want to check: ");
            url = Console.ReadLine();
            Console.Write("Enter the value you want to send in POST request: ");
            value = Console.ReadLine();
            Console.WriteLine();

            Task.Run(() => SendRequests(url, value));
            Console.ReadLine();
        }


        /// <summary>
        /// Sends GET, HEAD and POST requests to the specified URL
        /// </summary>
        /// <param name="url">URL to send request to</param>
        /// <param name="value">A value that will be passed within POST request</param>
        /// <returns></returns>
        static async Task SendRequests(string url, string value)
        {
            using (HttpClient client = new HttpClient())
            {
                //Sends GET request and prints server's response
                var result = await client.GetAsync(url);
                Console.WriteLine("---------------------GET-------------------");
                Console.WriteLine("{0} {1} HTTP/{2}\n", result.RequestMessage.Method, result.RequestMessage.RequestUri, result.RequestMessage.Version);
                string resultContent = await result.Content.ReadAsStringAsync();
                Console.WriteLine(result + "\n");
                Console.WriteLine(resultContent + "\n");

                //Sends HEAD request and prints the response
                result = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
                Console.WriteLine("---------------------HEAD------------------");
                Console.WriteLine("{0} {1} HTTP/{2}\n", result.RequestMessage.Method, result.RequestMessage.RequestUri, result.RequestMessage.Version);
                Console.WriteLine(result + "\n");

                //Sends POST request with variable, passed to this method, and print the response
                FormUrlEncodedContent content = new FormUrlEncodedContent(new[] {new KeyValuePair<string, string>("Variable", value)});
                result = await client.PostAsync(url, content);
                Console.WriteLine("---------------------POST------------------");
                Console.WriteLine("{0} {1} HTTP/{2}\n", result.RequestMessage.Method, result.RequestMessage.RequestUri, result.RequestMessage.Version);
                resultContent = await result.Content.ReadAsStringAsync();
                Console.WriteLine(result + "\n");
                Console.WriteLine(resultContent);
            }
        }
    }
}
