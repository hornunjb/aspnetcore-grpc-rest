using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Greet.V1;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TestRunner2
{

    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]

    public class Benchmark
    {

        const string grpcEndpoint = @"http://localhost:5000";
        const string httpEndpoint = @"http://localhost:4999";

        public class BookListDTO
        {
            public List<BookDTO> books { get; set; }
        }

        [Benchmark(Baseline = true)]
        public void CallHttpMethod()
        {
            HttpCall.callHttpEndpointAsync();
        }

        [Benchmark]
        public void CallGrpcMethod()
        {
            GrpcCall.callGrpcEndpointAsync();
        }

        public class HttpCall
        {
            public static BookListDTO HttpSetup()
            {
                BookDTO book = new BookDTO { Title = "Hello", Author = "Jacob" };
                List<BookDTO> booksList = new List<BookDTO>
                {
                    book,
                    book,
                    book
                };
                BookListDTO books = new BookListDTO { books = booksList };
                return books;
            }

            public static async Task callHttpEndpointAsync()
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(httpEndpoint);

                    var bookListResponse = await client.PostAsJsonAsync("/v1/hello/bookslist", HttpSetup());
                    if (bookListResponse.IsSuccessStatusCode)
                    {
                        string returnValue = await bookListResponse.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        Console.Write("Error");
                    }
                }
            }
        }

        public class GrpcCall
        {
            public static BookDTORequest GrpcSetup()
            {
                var input = new BookDTORequest();
                input.BookDTO.Add(new BookDTO() { Author = "Jacob", Title = "Hello" });
                input.BookDTO.Add(new BookDTO() { Author = "Jacob", Title = "Hello" });
                input.BookDTO.Add(new BookDTO() { Author = "Jacob", Title = "Hello" });
                return input;
            }

            public static async Task callGrpcEndpointAsync()
            {

                var channel = GrpcChannel.ForAddress(grpcEndpoint);
                var client = new Greeter.GreeterClient(channel);

                var reply = await client.BookCollectionAsync(GrpcSetup());

                if (reply.Total != 3)
                {
                    throw new ApplicationException();
                }
            }
        }
    }
}
