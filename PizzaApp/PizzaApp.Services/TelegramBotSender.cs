using PizzaApp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace PizzaApp.Services
{
    public class TelegramBotSender : ISender
    {
        public void SendMessage(string number)
        {
            TelegramBotClient botClient = new TelegramBotClient("832540995:AAH63xL0NmwqSN4_7baZmMBrAaFloWvBFsc");

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(me.Username);

            var random = new Random();
            botClient.OnMessage += SendMessage;


            botClient.StartReceiving();
            void SendMessage(object sender, MessageEventArgs messageEventArgs)
            {
                var chatId = messageEventArgs.Message.Chat.Id;
                var info = messageEventArgs.Message.Contact.PhoneNumber;
                botClient.SendTextMessageAsync(chatId, $"{random.Next(999999)}, {info}");
            }
            async Task GetContactPhone()
            {
                // var offset = 0;

                var Updates = await botClient.GetUpdatesAsync();

                foreach (var update in Updates)
                {
                    Console.WriteLine("aaaa");
                    if (update.Type == UpdateType.Message)
                    {
                        Console.WriteLine("bbb");
                        var cc = update.Message.Contact.PhoneNumber;
                        //string ph = message.Contact.PhoneNumber;
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, cc);
                        break;
                    }
                    // offset = update.Id + 1;
                }

            }
        }
    }
}
