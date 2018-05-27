using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;
using System.Drawing;

namespace VizionBot
{
    public static class GetImageAnalysis
    {
        private static string subscriptionKey = "36888b9029e344b1901d99c3a1c3c6cc";
        private static string contentType = "application/json";
        private static string uriBase = "https://eastasia.api.cognitive.microsoft.com/vision/v1.0/analyze";

        public static async Task<string> GetAnalysis(Image image)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                string requestParameters ="visualFeatures=Categories,Description,Color";
                string uri = uriBase + "?" + requestParameters;
                HttpResponseMessage response;
                byte[] byteData = ImageToByteArray(image);
                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response = await client.PostAsync(uri, content);
                }
                string contentString = await response.Content.ReadAsStringAsync();
                RootObject jsonObj = JsonConvert.DeserializeObject<RootObject>(contentString);
                return jsonObj.description.captions[0].text; 
            }
            catch (Exception e)
            {
                return "I cannot analyze that";
            }
        }
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            using (FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }
        static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
    }
}