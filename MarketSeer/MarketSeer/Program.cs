using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MarketSeer
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("MarketSeer BETA v0.1" + Environment.NewLine);

                List<string> urlDecodedArgList = args[0].Trim('+').Split(';').ToList();
                urlDecodedArgList = urlDecodedArgList.Select(s => s.Trim('+').Replace("+", " ")).ToList();

                ValidateParms(urlDecodedArgList);

                string region = urlDecodedArgList[0],
                    item = urlDecodedArgList[1];
                int quantity = 10;

                if (urlDecodedArgList.Count > 2 && !string.IsNullOrWhiteSpace(urlDecodedArgList[2]))
                {
                    quantity = Convert.ToInt32(urlDecodedArgList[2]);
                }

                using (WebClient webClient = WebUtils.GetJSONClient())
                {
                    Uri marketUri = new Uri(@"https://esi.tech.ccp.is/v1/markets/" + UniverseFactory.GetRegionID(region) + @"/orders/?type_id=" + ItemFactory.GetItemID(item));
                    string result = webClient.DownloadString(marketUri);

                    List<OrderInfo> orderInfoMessages = new JavaScriptSerializer().Deserialize<List<OrderInfo>>(result);

                    Console.WriteLine("BUY ORDERS...");
                    foreach (OrderInfo orderInfo in orderInfoMessages.Where(x => x.is_buy_order == true).OrderByDescending(x => x.price))
                    {
                        Console.Write(UniverseFactory.GetStationName(orderInfo.location_id) + ", ");
                        Console.Write(orderInfo.volume_remain + " left, ");
                        Console.Write(orderInfo.price.ToString("0.00") + " ISK" + Environment.NewLine);
                    }

                    Console.WriteLine(Environment.NewLine + "SELL ORDERS...");
                    foreach (OrderInfo orderInfo in orderInfoMessages.Where(x => x.is_buy_order == false).OrderBy(x => x.price))
                    {
                        Console.Write(UniverseFactory.GetStationName(orderInfo.location_id) + ", ");
                        Console.Write(orderInfo.volume_remain + " left, ");
                        Console.Write(orderInfo.price.ToString("0.00") + " ISK" + Environment.NewLine);
                    }                    
                }
            }
            catch (Exception ex)
            {
                ExitApp(args[0] + Environment.NewLine + ex.Message);
            }
            finally
            {
                PressEscapeToQuit();
            }
        }

        private static void ExitApp(string message)
        {
            Console.WriteLine(message);
            System.Threading.Thread.Sleep(1000);
            PressEscapeToQuit();
        }

        static void PressEscapeToQuit()
        {
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Press ESC to quit.");

            ConsoleKey nextKey = Console.ReadKey(true).Key;
            if (nextKey == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
        }

        private static void ValidateParms(List<string> parms)
        {
            try
            {
                if (parms.Count < 2)
                {
                    ExitApp("ur bad");
                }

                if (parms.Count >= 3)
                {
                    try
                    {
                        Convert.ToInt32(parms[2]);
                    }
                    catch
                    {
                        ExitApp("ur bad");
                    }
                }
            }
            catch
            {
                ExitApp("ur bad");
            }
        }
    }
}