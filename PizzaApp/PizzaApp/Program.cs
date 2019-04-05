using PizzaApp.DataAccess;
using PizzaApp.Models;
using PizzaApp.Services;
using PizzaApp.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TLSharp.Core;

namespace PizzaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductTableDataService products = new ProductTableDataService();
            foreach(var product in products.GetAll())
            {
                Console.WriteLine($"{product.Name} - {product.Description} - {product.Price}");
            }
            Console.Read();
        }


    }
}
