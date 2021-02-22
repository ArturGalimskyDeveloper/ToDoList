
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace TodoList
{
    public class BotConnection
    {
        TelegramBotClient _botClient;

        public BotConnection()
        {
            _botClient = new TelegramBotClient(Configuration.TOKEN);
            var me = _botClient.GetMeAsync().Result;

            Console.Title = me.Username;

            _botClient.OnMessage +=BotOnMessageReceived;

            _botClient.StartReceiving(Array.Empty<UpdateType>());
            Console.WriteLine($"Start listening for @{me.Username}");

            Console.ReadLine();
            _botClient.StopReceiving();
        }

        private async void BotOnMessageReceived(object sender,MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if(message == null || message.Type != MessageType.Text)
            {
                return;
            }

            switch(message.Text.Split(' ').First())
            {
                case "/list":
                    await SendAllTasks(message);
                    break;
            }

        }

        async System.Threading.Tasks.Task SendAllTasks(Message message)
        {
            string res = string.Join(",",TaskDataMapper.GetAll());
            await _botClient.SendTextMessageAsync(
                chatId: message.Chat,
                text: res
            );
        }

        async System.Threading.Tasks.Task Usage(Message message)
        {
            const string usage = "Usage:\n" +
                                    "/list   - send all tasks\n";
            await _botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: usage,
                replyMarkup: new ReplyKeyboardRemove()
            );
        }
    }
}