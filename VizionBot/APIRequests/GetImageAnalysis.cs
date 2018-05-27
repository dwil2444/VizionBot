using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using RestSharp;

namespace VizionBot
{
    public static class GetImageAnalysis
    {
        private static string subscriptionKey = "36888b9029e344b1901d99c3a1c3c6cc";
        private static string contentType = "application/json";
        private static string uriBase = "https://eastasia.api.cognitive.microsoft.com/vision/v2.0/analyze?visualFeatures=Description,Categories,Tags,Faces&details=Celebrities,Landmarks&language=en";

        public static async Task<string> GetAnalysis(string image)
        {
            var client = new RestClient(uriBase);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Ocp-Apim-Subscription-Key", subscriptionKey);
            request.AddHeader("Content-Type", contentType);
            ImageObject img = new ImageObject
            {
                url = image  
            };
            string jsonString = JsonConvert.SerializeObject(img, Formatting.None);
            string jsonString2 = jsonString.Replace("\\", "");
            request.AddBody(jsonString2);
            IRestResponse response = client.Execute(request); 
            var dyn = JsonConvert.DeserializeObject<RootObject>(response.Content); 
            return dyn.description.captions[0].text;
        }
    }
}