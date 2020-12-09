using Microsoft.AspNetCore.Http;
using NeuralNetworkAPI.Data;
using NeuralNetworkAPI.Data.Http;
using System.Net;

namespace NeuralNetworkAPI.Utils
{
    public class Validation
    {
        public static NetworkMetadataResponse Validate(HttpRequest request, HttpResponse response, NetworkMetadata network, out User user)
        {
            user = Authentication.GetUser(request);
            if (user == null) {
                return new NetworkMetadataResponse(response, HttpStatusCode.Unauthorized);
            }
            if (string.IsNullOrEmpty(network.Name)) {
                return new NetworkMetadataResponse(response, HttpStatusCode.BadRequest) { Message = "Network name can't be empty" };
            }
            if (network.HiddenWidth <= 0) {
                return new NetworkMetadataResponse(response, HttpStatusCode.BadRequest) { Message = "HiddenWidth must be greater than 0" };
            }
            if (network.InputCount <= 0) {
                return new NetworkMetadataResponse(response, HttpStatusCode.BadRequest) { Message = "InputCount must be greater than 0" };
            }
            if (network.Layers <= 0) {
                return new NetworkMetadataResponse(response, HttpStatusCode.BadRequest) { Message = "Layers must be greater than 0" };
            }
            if (network.LearningRate <= 0.0f) {
                return new NetworkMetadataResponse(response, HttpStatusCode.BadRequest) { Message = "LearningRate must be greater than 0.0" };
            }
            if (network.OutputCount <= 0) {
                return new NetworkMetadataResponse(response, HttpStatusCode.BadRequest) { Message = "OutputCount must be greater than 0" };
            }
            return null;
        }

        public static NetworkOutputResponse Validate(HttpRequest request, HttpResponse response, NetworkInput input, out User user)
        {
            user = Authentication.GetUser(request);
            if (user == null) {
                return new NetworkOutputResponse(response, HttpStatusCode.Unauthorized);
            }

            if(input.Inputs == null) {
                return new NetworkOutputResponse(response, HttpStatusCode.BadRequest) { Message = "Input must be in binary" };
            }
            for(int i = 0; i < input.Inputs.Length; i++) {
                if(input.Inputs[i] != '0' && input.Inputs[i] != '1') {
                    return new NetworkOutputResponse(response, HttpStatusCode.BadRequest) { Message = "Input must be in binary" };
                }
            }
            if(input.ExpectedOutputs != null) {
                for (int i = 0; i < input.ExpectedOutputs.Length; i++) {
                    if (input.ExpectedOutputs[i] != '0' && input.ExpectedOutputs[i] != '1') {
                        return new NetworkOutputResponse(response, HttpStatusCode.BadRequest) { Message = "Output must be in binary" };
                    }
                }
            }
            return null;
        }

        public static TeachResponse Validate(HttpRequest request, HttpResponse response, LearningDataInput input, out User user)
        {
            user = Authentication.GetUser(request);
            if (user == null) {
                return new TeachResponse(response, HttpStatusCode.Unauthorized);
            }

            foreach(LearningDataCase lCase in input.Cases) {
                if (lCase.Inputs == null) {
                    return new TeachResponse(response, HttpStatusCode.BadRequest) { Message = "Input must be in binary" };
                }
                if (lCase.ExpectedOutputs == null) {
                    return new TeachResponse(response, HttpStatusCode.BadRequest) { Message = "Output must be in binary" };
                }
                for (int i = 0; i < lCase.Inputs.Length; i++) {
                    if (lCase.Inputs[i] != '0' && lCase.Inputs[i] != '1') {
                        return new TeachResponse(response, HttpStatusCode.BadRequest) { Message = "Input must be in binary" };
                    }
                }
                for (int i = 0; i < lCase.ExpectedOutputs.Length; i++) {
                    if (lCase.ExpectedOutputs[i] != '0' && lCase.ExpectedOutputs[i] != '1') {
                        return new TeachResponse(response, HttpStatusCode.BadRequest) { Message = "Output must be in binary" };
                    }
                }
            }
            return null;
        }
    }
}
