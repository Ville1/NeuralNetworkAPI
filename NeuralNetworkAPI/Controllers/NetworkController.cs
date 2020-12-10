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
            //Validate
            User user;
            NetworkMetadataResponse error = Validation.Validate(Request, Response, network, out user);
            if (error != null) {
                return error;
            }

            //Create a new network
            NetworkMetadata data = null;
            try {
                network.Id = null;
                network.OwnerId = user.Id.Value;
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
                data = Repositories.Networks.GetAll().Where(x => x.OwnerId == user.Id.Value).ToList();
            } catch (Exception exception) {
                return new NetworkMetadataListResponse(Response, HttpStatusCode.InternalServerError, exception.Message);
            }
            return new NetworkMetadataListResponse(Response, HttpStatusCode.OK) {
                Networks = data
            };
        }

        [HttpPost]
        [Route("process")]
        [Produces("application/json")]
        public NetworkOutputResponse Process(NetworkInput input)
        {
            //Validate
            User user;
            NetworkOutputResponse error = Validation.Validate(Request, Response, input, out user);
            if (error != null) {
                return error;
            }

            //Fetch network metadata
            NetworkMetadata meta = Repositories.Networks.GetAll().FirstOrDefault(x => x.Id.Value == input.NetworkId);
            if (meta == null) {
                return new NetworkOutputResponse(Response, HttpStatusCode.NotFound);
            }
            if (meta.OwnerId != user.Id.Value) {
                return new NetworkOutputResponse(Response, HttpStatusCode.Unauthorized);
            }

            //Run network
            string data = null;
            try {
                data = NetworkManager.Process(meta, input);
            } catch (Exception exception) {
                return new NetworkOutputResponse(Response, HttpStatusCode.InternalServerError, exception.Message);
            }
            return new NetworkOutputResponse(Response, HttpStatusCode.OK) {
                Output = data
            };
        }

        [HttpPost]
        [Route("teachsimple")]
        [Produces("application/json")]
        public NetworkOutputResponse TeachSimple(NetworkInput input)
        {
            //Validate
            User user;
            NetworkOutputResponse error = Validation.Validate(Request, Response, input, out user);
            if (error != null) {
                return error;
            }

            //Fetch network metadata
            NetworkMetadata meta = Repositories.Networks.GetAll().FirstOrDefault(x => x.Id.Value == input.NetworkId);
            if(meta == null) {
                return new NetworkOutputResponse(Response, HttpStatusCode.NotFound);
            }
            if(meta.OwnerId != user.Id.Value) {
                return new NetworkOutputResponse(Response, HttpStatusCode.Unauthorized);
            }

            //Teach network
            string data = null;
            try {
                data = NetworkManager.Teach(meta, input);
            } catch (Exception exception) {
                return new NetworkOutputResponse(Response, HttpStatusCode.InternalServerError, exception.Message);
            }
            return new NetworkOutputResponse(Response, HttpStatusCode.OK) {
                Output = data
            };
        }

        [HttpPost]
        [Route("teach")]
        [Produces("application/json")]
        public TeachResponse Teach(LearningDataInput input)
        {
            //Validate
            User user;
            TeachResponse error = Validation.Validate(Request, Response, input, out user);
            if(error != null) {
                return error;
            }

            //Fetch network metadata
            NetworkMetadata meta = Repositories.Networks.GetAll().FirstOrDefault(x => x.Id.Value == input.NetworkId);
            if (meta == null) {
                return new TeachResponse(Response, HttpStatusCode.NotFound);
            }
            if (meta.OwnerId != user.Id.Value) {
                return new TeachResponse(Response, HttpStatusCode.Unauthorized);
            }

            //Teach network
            float success = 0.0f;
            try {
                success = NetworkManager.Teach(meta, input);
            } catch (Exception exception) {
                return new TeachResponse(Response, HttpStatusCode.InternalServerError, exception.Message);
            }
            return new TeachResponse(Response, HttpStatusCode.OK) {
                SuccessRate = success
            };
        }
    }
}
