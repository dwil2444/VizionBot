using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Drawing;

namespace VizionBot.Dialogs
{
    [Serializable]
    public class VisionDialog : IDialog<object>
    {
        public Image image;

        public VisionDialog(Image imageFile)
        {
            this.image = (Image)imageFile;
        }

        public async Task StartAsync(IDialogContext context)
        {
            string answer = await GetImageAnalysis.GetAnalysis(this.image);
            await context.PostAsync(answer);
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
            context.Done(true);
        }
            
    }

}