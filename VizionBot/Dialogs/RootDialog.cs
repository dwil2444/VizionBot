using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Drawing;
using System.Net.Http;
using System.IO;

namespace VizionBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Give Me An Image");
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            string content = activity.Attachments[0].ContentUrl;
            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            try
            {
                var httpClient = new HttpClient();
                //var attachmentData = await httpClient.GetByteArrayAsync(content);
                var attachmentData = connector.HttpClient.GetByteArrayAsync(content).Result;
                Image imageFile = await ConvertByteArray(attachmentData);
                string answer = await GetImageAnalysis.GetAnalysis(imageFile);
                await context.PostAsync(answer);
                //context.Call(new VisionDialog(imageFile), VisDialogResume);
            }
            catch (BadImageFormatException e)
            {

            }
            context.Done(true);
        }

        private async Task VisDialogResume(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            context.Wait(MessageReceivedAsync);
        }
        private async Task<Image> ConvertByteArray(byte[] x)
        {
            MemoryStream memstr = new MemoryStream(x);
            Image img = Image.FromStream(memstr);
            return img;
        }
    }
}