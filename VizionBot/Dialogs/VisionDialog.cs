using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace VizionBot.Dialogs
{
    [Serializable]
    public class VisionDialog : IDialog<object>
    {
        private string image;

        public VisionDialog(String image)
        {
            this.image = image;
        }

        public async Task StartAsync(IDialogContext context)
        {

        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result as Activity;
        }
            
    }

}