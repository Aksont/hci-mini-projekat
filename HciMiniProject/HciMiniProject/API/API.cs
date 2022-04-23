using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace HciMiniProject.API
{
    class API
    {
        public static List<DataDateValue> GetTreasuryYieldData(string interval, string maturity)
        {
            //string QUERY_URL = "https://www.alphavantage.co/query?function=" + functionName + "&interval=" + interval + "& maturity=" + maturity  + "& apikey=demo";
            string QUERY_URL = "https://www.alphavantage.co/query?function=TREASURY_YIELD&interval=" + interval + "&maturity=" + maturity + "&apikey=demo";
            Uri queryUri = new Uri(QUERY_URL);

            List<DataDateValue> data = new List<DataDateValue>();

            using (WebClient client = new WebClient())
            {
                // -------------------------------------------------------------------------
                // if using .NET Framework (System.Web.Script.Serialization)

                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic json_data = js.Deserialize(client.DownloadString(queryUri), typeof(object));
                foreach (Dictionary<string, object> d in json_data["data"])
                {
                    data.Add(new DataDateValue(d["date"].ToString(), Convert.ToDouble(d["value"])));
                }
            }
            Console.WriteLine("Uspesno dobavljeni podaci");
            return data;
        }


        // mozda moze bez unit
        public static List<DataDateValue> GetRealGDPData(string interval, string unit)
        {
            if (interval != "")
            {
                interval = "&interval=" + interval;
            }
            else { interval = ""; }
            if (unit != "")
            {
                unit = "&unit=" + unit;
            }
            else { unit = ""; }
            string QUERY_URL = "https://www.alphavantage.co/query?function=REAL_GDP" + interval + "&apikey=demo";
            Uri queryUri = new Uri(QUERY_URL);

            List<DataDateValue> data = new List<DataDateValue>();

            using (WebClient client = new WebClient())
            {
                // -------------------------------------------------------------------------
                // if using .NET Framework (System.Web.Script.Serialization)

                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic json_data = js.Deserialize(client.DownloadString(queryUri), typeof(object));
                foreach (Dictionary<string, object> d in json_data["data"])
                {
                    data.Add(new DataDateValue(d["date"].ToString(), Convert.ToDouble(d["value"])));
                }
                Console.WriteLine("Uspesno dobavljeni podaci");
            }

            return data;
        }

    }
}
