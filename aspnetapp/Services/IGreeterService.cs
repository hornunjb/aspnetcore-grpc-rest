using System.Threading.Tasks;
using Grpc.Core;
using Greet.V1;

namespace aspnetapp.Services
{
    /// <summary>
    /// Greeting Service
    /// </summary>
    public interface IGreeterService
    {
        /// <summary>
        /// A method that takes in a request name and responds with a hello message
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context);
    }
}