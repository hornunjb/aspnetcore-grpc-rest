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

        HttpClient _httpClient;
        Greeter.GreeterClient _grpcClient;

        [GlobalSetup]
        public void setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(httpEndpoint);
            var channel = GrpcChannel.ForAddress(grpcEndpoint);
            _grpcClient = new Greeter.GreeterClient(channel);

        }
       
            [Benchmark]
            public async Task callHttpEndpointAsync()
            {
                
                
                    BookDTO book = new BookDTO { Title = "Hello", Author = "Jacob" };


                    List<BookDTO> booksList = new List<BookDTO>
                {
                    book,
                    book,
                    book
                };

                    BookListDTO books = new BookListDTO { books = booksList };

                    var bookListResponse = await _httpClient.PostAsJsonAsync("/v1/hello/bookslist", books);
                    if (bookListResponse.IsSuccessStatusCode)
                    {
                        string returnValue = await bookListResponse.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new ApplicationException();
                    }

        }
        


        [Benchmark]
            public async Task callGrpcEndpointAsync()
            {
                var input = new BookDTORequest();
                input.BookDTO.Add(new BookDTO() { Author = "Jacob", Title = "Hello" });
                input.BookDTO.Add(new BookDTO() { Author = "Jacob", Title = "Hello" });
                

                var reply = await _grpcClient.BookCollectionAsync(input);

                if (reply.Total != 2)
                {
                    throw new ApplicationException();
                }
            }
        
    }
}
