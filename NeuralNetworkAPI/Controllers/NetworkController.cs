using Microsoft.AspNetCore.Mvc;
using NeuralNetworkAPI.Data;
using NeuralNetworkAPI.Data.Http;
using NeuralNetworkAPI.Data.Networks;
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
        public NetworkMetadataResponse Create(NetworkMetadata network)
        {
            User user = Authentication.GetUser(Request);
            if(user == null) {
                return new NetworkMetadataResponse(Response, HttpStatusCode.Unauthorized);
            }
            NetworkMetadata data = null;
            try {
                network.Id = -1;
                network.OwnerId = user.Id;
                data = Repositories.Networks.Save(network);
                NetworkManager.InitializeNetwork(data);
            } catch(Exception exception) {
                return new NetworkMetadataResponse(Response, HttpStatusCode.InternalServerError, exception.Message);
            }
            return new NetworkMetadataResponse(Response, HttpStatusCode.OK) {
                Network = data
            };
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
