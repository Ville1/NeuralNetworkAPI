using Microsoft.AspNetCore.Mvc;
using NeuralNetworkAPI.Data;
using NeuralNetworkAPI.Data.Http;
using NeuralNetworkAPI.Data.Repository;
using NeuralNetworkAPI.Utils;
using System.Linq;
using System.Net;

namespace NeuralNetworkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public UserResponse Get()
        {
            User user = Authentication.GetUser(Request);
            if (user == null) {
                return new UserResponse(Response, HttpStatusCode.Unauthorized);
            }
            return new UserResponse(Response, HttpStatusCode.OK) {
                Users = Repositories.Users.GetAll().Select(x => new UserResponseData() {
                    Id = x.Id.Value,
                    Username = x.Username
                }).ToList()
            };
        }
    }
}
