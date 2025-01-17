﻿using Greet.V1;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace TestRunner
{
    class Program
    {
        const string httpEndpoint = @"http://localhost:4999";
        const string grpcEndpoint = @"http://localhost:5000";

        public class BookListDTO
        {
            public List<BookDTO> books { get; set; }

        }

        static void Main(string[] args) 
        {
            callHttpEndpoint();
            callGrpcEndpointAsync();
            Console.ReadLine();
        }

        static void callHttpEndpoint()
        {
            using (var client = new HttpClient())
            {
                BookDTO book = new BookDTO { Title = "Hello", Author = "Jacob" };

                client.BaseAddress = new Uri(httpEndpoint);

                var bookResponse = client.PostAsJsonAsync("/v1/hello/books", book).Result;
                if (bookResponse.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                {
                    Console.Write("Error");
                }

                List<BookDTO> booksList = new List<BookDTO>
                {
                    book,
                    book,
                    book
                };
                BookListDTO books = new BookListDTO { books = booksList };

                var bookListResponse = client.PostAsJsonAsync("/v1/hello/bookslist", books).Result;
                if (bookListResponse.IsSuccessStatusCode)
                {
                    Console.Write("Success");
                }
                else
                {
                    Console.Write("Error");
                }
            }
        }

        // TODO: gRPC call
        static async Task callGrpcEndpointAsync()
        {
            var input = new BookDTORequest();
            input.BookDTO.Add(new BookDTO() { Author = "Jacob", Title = "Hello" });
            input.BookDTO.Add(new BookDTO() { Author = "Jacob", Title = "Hello" });
            var channel = GrpcChannel.ForAddress(grpcEndpoint);
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.BookCollectionAsync(input);

            Console.WriteLine(reply.Total);
        }

    }
}
