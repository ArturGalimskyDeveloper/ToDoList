
using System;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
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
                case "/add":
                    await AddCommand(message);
                    break;
                case "/check":
                    await CheckCommand(message);
                    break;
                case "/delete":
                    await DeleteCommand(message);
                    break;
                case "/help":
                    await HelpCommand(message);
                    break;
                case "/list":
                    await ListCommand(message);
                    break;
                case "/start":
                    await StartCommand(message);
                    break;
                default:
                    await HelpCommand(message);
                    break;
            }
        }

        async System.Threading.Tasks.Task AddCommand(Message message)
        {
            string res = $"Adding task {message.From.Username}";
            string messageText = message.Text.Remove(0, message.Text.IndexOf(' ') + 1);
            TaskDataMapper.Save(messageText, message.From.Id);
            await _botClient.SendTextMessageAsync(
                chatId: message.Chat,
                text: res
            );
        }

        async System.Threading.Tasks.Task CheckCommand(Message message)
        {
            string res = $"Now your data is check {message.From.Username}";
            await _botClient.SendTextMessageAsync(
                chatId: message.Chat,
                text: res
            );
        }

        async System.Threading.Tasks.Task DeleteCommand(Message message)
        {
            string res = $"Now your data is deleted {message.From.Username}";
            if(!UserDataMapper.Delete(message.From.Id))
            {
                res = $"Your data is already deleted {message.From.Username}";
            }
            await _botClient.SendTextMessageAsync(
                chatId: message.Chat,
                text: res
            );
        }

        async System.Threading.Tasks.Task HelpCommand(Message message)
        {
            const string usage = "Usage:\n" +
                                    "/list   - send all tasks\n";
            await _botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: usage,
                replyMarkup: new ReplyKeyboardRemove()
            );
        }

        async System.Threading.Tasks.Task ListCommand(Message message)
        {
            string res = string.Join(",",TaskDataMapper.GetAll(message.From.Id));
            await _botClient.SendTextMessageAsync(
                chatId: message.Chat,
                text: res
            );
        }

        async System.Threading.Tasks.Task StartCommand(Message message)
        {
            string res = $"Welcome to todo-list bot {message.From.Username}";
            if(!UserDataMapper.Save(new User(message.From.Id,message.From.Username)))
            {
                res = $"{message.From.Username} you are already in system";
            }
            await _botClient.SendTextMessageAsync(
                chatId: message.Chat,
                text: res
            );
        }

    }
}