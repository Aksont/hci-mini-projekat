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
        private const string API_KEY = "&apikey=Y0F084A8IMS8S5JW";
        public static List<DataDateValue> GetTreasuryYieldData(string interval, string maturity)
        {
            string QUERY_URL = "https://www.alphavantage.co/query?function=TREASURY_YIELD&interval=" + interval + "&maturity=" + maturity + API_KEY;
            Uri queryUri = new Uri(QUERY_URL);

            List<DataDateValue> data = new List<DataDateValue>();

            using (WebClient client = new WebClient())
            {
                // -------------------------------------------------------------------------
                // if using .NET Framework (System.Web.Script.Serialization)

                JavaScriptSerializer js = new JavaScriptSerializer();

                string data_string = "";
                try
                {
                    data_string = client.DownloadString(queryUri);
                }
                catch { }

                if (data_string != "")
                {
                    dynamic json_data = js.Deserialize(data_string, typeof(object));


                    foreach (Dictionary<string, object> d in json_data["data"])
                    {
                        AddData(d, ref data);
                    }
                }
            }
            data.Reverse();
            return data;
        }


        // mozda moze bez unit
        public static List<DataDateValue> GetRealGDPData(string interval)
        {
            if (interval != "")
            {
                interval = "&interval=" + interval;
            }
            else { interval = ""; }
            string QUERY_URL = "https://www.alphavantage.co/query?function=REAL_GDP" + interval + API_KEY;
            Uri queryUri = new Uri(QUERY_URL);

            List<DataDateValue> data = new List<DataDateValue>();

            using (WebClient client = new WebClient())
            {
                // -------------------------------------------------------------------------
                // if using .NET Framework (System.Web.Script.Serialization)

                JavaScriptSerializer js = new JavaScriptSerializer();

                string data_string = "";
                try
                {
                    data_string = client.DownloadString(queryUri);
                }
                catch { }

                if (data_string != "")
                {
                    dynamic json_data = js.Deserialize(data_string, typeof(object));


                    foreach (Dictionary<string, object> d in json_data["data"])
                    {
                        AddData(d, ref data);
                    }
                }
            }
            data.Reverse();
            return data;
        }

        private static void AddData(Dictionary<string, object> d, ref List<DataDateValue> data)
        {
            string value = d["value"].ToString();
            if (value.Equals("."))
            { value = "0.0"; }

            string date = ParseDate(d["date"].ToString());
            data.Add(new DataDateValue(date, Convert.ToDouble(value)));
        }
        private static string ParseDate(string date)
        {
            string[] parameters = date.Split('-');
            return parameters[2] + "." + parameters[1] + "." + parameters[0] + ".";
        }
    }
}
