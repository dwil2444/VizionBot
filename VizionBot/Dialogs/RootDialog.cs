using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace VizionBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Give Me An Image Url");
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            context.Call(new VisionDialog(activity.Text), VisDialogResume);
        }

        private async Task VisDialogResume(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            context.Wait(MessageReceivedAsync);
        }
    }
}