using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestRunner
{
    class Program
    {
        const string httpEndpoint = @"http://localhost:4999";
        const string grpc = @"http://localhost:5000";

        public class BookDTO
        {
            public string title { get; set; }
            public string author { get; set; }
        }

        public class BookListDTO
        {
            public List<BookDTO> books { get; set; }

        }

        static void Main(string[] args)
        {
            // HTTP call
            using (var client = new HttpClient())
            {
                BookDTO book = new BookDTO { title = "hello", author = "jacob"};
                client.BaseAddress = new Uri(httpEndpoint);
                var response = client.PostAsJsonAsync("/v1/hello/books", book).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                    Console.Write("Error");
            }

            using (var client = new HttpClient())
            {
                BookListDTO bookList = new BookListDTO();
                client.BaseAddress = new Uri(httpEndpoint);
                var response = client.PostAsJsonAsync("/v1/hello/bookList", bookList).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                    Console.Write("Error");
            }
        }

        // TODO: gRPC call
        static void callGrpcEndpoint(string endpointAddress)
        {

        }

    }
}
