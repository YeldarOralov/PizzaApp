using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using TLSharp.Core;

namespace PizzaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TelegramBotClient botClient = new TelegramBotClient("832540995:AAH63xL0NmwqSN4_7baZmMBrAaFloWvBFsc");

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(me.Username);

            botClient.OnMessage += SendMessage;
            botClient.StartReceiving();
            void SendMessage(object sender, MessageEventArgs messageEventArgs)
            {
                var chatId = messageEventArgs.Message.Chat.Id;
                botClient.SendTextMessageAsync(chatId, "Hello!");
            }

            Console.Read();
        }


    }
}
