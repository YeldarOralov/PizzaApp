using PizzaApp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace PizzaApp.Services
{
    public class SmsSender : ISender
    {
        public void SendMessage(string number)
        {
            Random random = new Random();
            
            const string accountSid = "ACb0ce4eebe9bc3e3552877e74f8b1c4dc";
            const string authToken = "5b099bd03e1e6c115edbb5faf75d5e49";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: random.Next(999999).ToString(),
                from: new Twilio.Types.PhoneNumber("+17152278964"),
                to: new Twilio.Types.PhoneNumber(number)
            );
        }
    }
}
