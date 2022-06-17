using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json; // Nuget Package
using System.Net.Mime;
using System;

namespace Simulation
{
    internal class AppleSimulation
    {
        readonly string apikey = "7bc20aa8bef9454f837b81c8874cf5dd";
        private DateTime[] dates;
        private double[] datesUnix;
        private double[] stddevs;

        public class jsonReturn
        {
            public dateStddev[] values { get; set; }

            public String status { get; set; }
        }

        public class dateStddev
        {
            public String datetime { get; set; }
            public String stddev { get; set; }
        }

        public void breakDateStddev(dateStddev[] arr)
        {
            DateTime[] tempDates = new DateTime[arr.Length];
            double[] tempDatesUnix = new double[arr.Length];
            double[] tempStddevs = new double[arr.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                tempDates[i] = DateTime.Parse(arr[i].datetime);
                tempDatesUnix[i] = ((DateTimeOffset) tempDates[i]).ToUnixTimeSeconds();
                tempStddevs[i] = double.Parse(arr[i].stddev, System.Globalization.CultureInfo.InvariantCulture);
            }

            dates = tempDates;
            stddevs = tempStddevs;
            datesUnix = tempDatesUnix;
        }

        public string constructUrl(String symbol, String interval, String sd, String outputsize)
        {
            return "https://api.twelvedata.com/stddev?symbol=" + symbol + "&interval=" + interval + "&sd=" + sd + "&outputsize=" + outputsize + "&apikey=" + apikey;
        }

        public async Task<jsonReturn> PostCallAPI(string url)
        {
            if (url == null)
            {
                Console.WriteLine("ERROR: url is null");
                return null;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    if (response != null)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        jsonReturn ret = JsonConvert.DeserializeObject<jsonReturn>(jsonString); ;
                        breakDateStddev(ret.values);
                        return null;
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("ERROR: request failed");
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public DateTime[] getDates()
        {
            if (dates == null)
            {
                Console.WriteLine("ERROR: dates is null!");
                return null;
            }

            return dates;
        }

        public double[] getStddev()
        {
            if (stddevs == null)
            {
                Console.WriteLine("ERROR: dates is null!");
                return null;
            }

            return stddevs;
        }

        public void graphStddev(String[] dates, double[] stddevs)
        {
            
        }

        static void Main(String[] args)
        {
            AppleSimulation sim = new AppleSimulation();

            String url = sim.constructUrl("AAPL", "1day", "1", "10");

            _ = sim.PostCallAPI(url).Result;

            foreach (DateTime date in sim.getDates())
            {
                Console.WriteLine(date);
            }

            foreach (double stddev in sim.getStddev())
            {
                Console.WriteLine(stddev);
            }
        }
    }
}