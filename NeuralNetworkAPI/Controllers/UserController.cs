using Microsoft.AspNetCore.Mvc;
using NeuralNetworkAPI.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public List<string> Get()
        {
            return Repositories.Users.GetAll().Select(x => x.Username).ToList();
        }
    }
}
