using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;


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
            callHttpEndpoint();
        }

        static void callHttpEndpoint()
        {
            using (var client = new HttpClient())
            {
                BookDTO book = new BookDTO { title = "Hello", author = "Jacob" };

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

                List<BookDTO> booksList = new List<BookDTO>();
                booksList.Add(book);
                booksList.Add(book);
                booksList.Add(book);
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
        static void callGrpcEndpoint()
        {

        }

    }
}
