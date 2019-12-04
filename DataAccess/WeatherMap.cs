﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;

namespace Weather.DataAccess
{
    /*
     * Tutorial for working with API in UWP:
     * https://www.youtube.com/watch?v=ziLkj4PmcCE
     */
    public class WeatherMap
    {

        public async static Task<RootObject> GetWeather(string zip, string system)
        {
            HttpClient client = new HttpClient();
            string header = "https://api.openweathermap.org/data/2.5/weather?zip=" + zip + "&units=" + system + "&appid=ad255140107924a3cce8f9dc305138c1";
            var response = await client.GetAsync(header);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject)serializer.ReadObject(ms);

            return data;
        }

    }

    [DataContract]
    public class Coord
    {
        [DataMember]
        public double lon { get; set; }
        [DataMember]

        public double lat { get; set; }
    }

    [DataContract]
    public class Weather
    {
        [DataMember]

        public int id { get; set; }
        [DataMember]

        public string main { get; set; }
        [DataMember]

        public string description { get; set; }
        [DataMember]

        public string icon { get; set; }
    }

    [DataContract]

    public class Main
    {
        [DataMember]
        public double temp { get; set; }

        [DataMember]
        public int pressure { get; set; }

        [DataMember]
        public int humidity { get; set; }

        [DataMember]
        public double temp_min { get; set; }

        [DataMember]
        public double temp_max { get; set; }
    }

    [DataContract]
    public class Wind
    {
        [DataMember]
        public double speed { get; set; }

        [DataMember]
        public int deg { get; set; }

        [DataMember]
        public double gust { get; set; }
    }

    [DataContract]
    public class Clouds
    {

        [DataMember]
        public int all { get; set; }
    }

    [DataContract]
    public class Sys
    {
        [DataMember]
        public int type { get; set; }

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public double message { get; set; }

        [DataMember]
        public string country { get; set; }

        [DataMember]
        public int sunrise { get; set; }

        [DataMember]
        public int sunset { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public Coord coord { get; set; }

        [DataMember]
        public List<Weather> weather { get; set; }
        [DataMember]
        public string @base { get; set; }

        [DataMember]
        public Main main { get; set; }

        [DataMember]
        public int visibility { get; set; }

        [DataMember]
        public Wind wind { get; set; }

        [DataMember]
        public Clouds clouds { get; set; }

        [DataMember]
        public int dt { get; set; }

        [DataMember]
        public Sys sys { get; set; }

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public int cod { get; set; }
    }
}
