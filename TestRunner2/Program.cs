using Greet.V1;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TestRunner2
{
    class Program
    {
        const string grpcEndpoint = @"http://localhost:5000";
        const string httpEndpoint = @"http://localhost:4999";

        public class BookListDTO
        {
            public List<BookDTO> books { get; set; }

        }
        static async Task Main(string[] args)
        {
            await callGrpcEndpointAsync();
            await callHttpEndpointAsync();
        }

        static async Task callHttpEndpointAsync()
        {
            using (var client = new HttpClient())
            {
                BookDTO book = new BookDTO { Title = "Hello", Author = "Jacob" };

                client.BaseAddress = new Uri(httpEndpoint);

                List<BookDTO> booksList = new List<BookDTO>
                {
                    book,
                    book,
                    book
                };

                BookListDTO books = new BookListDTO { books = booksList };

                var bookListResponse = await client.PostAsJsonAsync("/v1/hello/bookslist", books);
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

        static async Task callGrpcEndpointAsync()
        {
            var input = new BookDTORequest();
            input.BookDTO.Add(new BookDTO() { Author = "Jacob", Title = "Hello" });
            input.BookDTO.Add(new BookDTO() { Author = "Jacob", Title = "Hello" });
            var channel = GrpcChannel.ForAddress(grpcEndpoint);
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.BookCollectionAsync(input);

            if (reply.Total != 2)
            {
                throw new ApplicationException();
            }
        }
    }
}
