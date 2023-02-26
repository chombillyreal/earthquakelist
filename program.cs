using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace EarthquakeList
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/4.5_month.geojson";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                JObject jsonObject = JObject.Parse(json);

                JArray earthquakes = (JArray)jsonObject["features"];

                foreach (var earthquake in earthquakes)
                {
                    string place = (string)earthquake["properties"]["place"];
                    double magnitude = (double)earthquake["properties"]["mag"];
                    string time = (string)earthquake["properties"]["time"];

                    if (place.Contains("Turkey"))
                    {
                        DateTime dateTime = UnixTimeStampToDateTime(double.Parse(time) / 1000);

                        Console.WriteLine("Place: {0}", place);
                        Console.WriteLine("Magnitude: {0}", magnitude);
                        Console.WriteLine("Time: {0}\n", dateTime);
                    }
                }
            }

            Console.ReadLine();
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
