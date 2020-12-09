using Microsoft.AspNetCore.Mvc;
using NeuralNetworkAPI.Data;
using NeuralNetworkAPI.Data.Http;
using NeuralNetworkAPI.Data.Repository;
using NeuralNetworkAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace NeuralNetworkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        public Response Create(NetworkMetadata network)
        {
            User user = Authentication.GetUser(Request);
            if(user == null) {
                return new Response(Response, HttpStatusCode.Unauthorized);
            }
            try {
                Repositories.Networks.Save(new NetworkMetadata() {
                    Name = network.Name,
                    OwnerId = user.Id
                });
            } catch(Exception exception) {
                return new Response(Response, HttpStatusCode.InternalServerError, exception.Message);
            }
            return new Response(Response, HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("getall")]
        [Produces("application/json")]
        public NetworkMetadataListResponse GetAll()
        {
            User user = Authentication.GetUser(Request);
            if (user == null) {
                return new NetworkMetadataListResponse(Response, HttpStatusCode.Unauthorized);
            }
            List<NetworkMetadata> data = null;
            try {
                data = Repositories.Networks.GetAll().Where(x => x.OwnerId == user.Id).ToList();
            } catch (Exception exception) {
                return new NetworkMetadataListResponse(Response, HttpStatusCode.InternalServerError, exception.Message);
            }
            return new NetworkMetadataListResponse(Response, HttpStatusCode.OK) {
                Networks = data
            };
        }
    }
}
