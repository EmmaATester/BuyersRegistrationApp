using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;

namespace BuyersApp
{
    /// <summary>
    /// Simple to query Yahoo Finance REST API to get currency conversions based on three digit ccounty codes
    /// </summary>
    public class YahooFinanceCurrencyConverter
    {
        /// <summary>
        /// Simple csv of name cCurrencyCodeDictionaryFileName in format: 
        ///     country, code
        /// </summary>
        private const string cCurrencyCodeDictionaryFileName = "CountriesAndCurrencyCodes.csv";
        /// <summary>
        /// Dictionary keyed by 3 digit currency codes, with country names as values
        /// </summary>
        public Dictionary<string, string> CurrencyCodeDictionary { get; private set; }

        /// <summary>
        /// Main constructor. Populates CurrencyCodeDictionary from file
        /// </summary>
        public YahooFinanceCurrencyConverter()
        {
            CurrencyCodeDictionary = GetCurrencyCodeDictionary();
        }

        /// <summary>
        /// Converts a value from one currency to the next. 
        /// Requires three digit code of from and to currencies, and value to convert
        /// </summary>
        /// <param name="fromCurrency">three digit code of currency to convert from</param>
        /// <param name="toCurrency">three digit code of currency to convert from to</param>
        /// <param name="valueToConvert">value to convert</param>
        /// <returns>converted currency value</returns>
        public float Convert(string fromCurrency, string toCurrency, float valueToConvert)
        {
            float conversionRate = GetConversion(fromCurrency, toCurrency);
            return valueToConvert * conversionRate;
        }

        /// <summary>
        /// Gets the Dictionary keyed by 3 digit currency codes, with country names as values
        /// From the default file: CountriesAndCurrencyCodes.csv
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetCurrencyCodeDictionary()
        {
            return GetCurrencyCodeDictionaryFromFile(cCurrencyCodeDictionaryFileName); 
        }

        /// <summary>
        /// Gets current conversion rate for passed in currency codes from Yahoo Finance website
        /// </summary>
        /// <param name="fromCurrency">three digit code of currency to convert from</param>
        /// <param name="toCurrency">three digit code of currency to convert from to</param>
        /// <returns>Current conversion rate for passed in currency codes from Yahoo Finance website</returns>
        private float GetConversion(string fromCurrency, string toCurrency)
        {
            string requestUrl = CreateRequest(fromCurrency, toCurrency);
            XmlDocument responseAsXml = MakeRequest(requestUrl);
            if(null == responseAsXml)
                throw new Exception("Failed to get response from Yahoo Finance website");
            return GetConversionAsFloat(responseAsXml);
        }

        /// <summary>
        /// Create REST request for YFAPI
        /// </summary>
        /// <param name="firstCurrency">three digit code of currency to convert from</param>
        /// <param name="secondCurrency">three digit code of currency to convert from to</param>
        /// <returns>url request</returns>
        private static string CreateRequest(string firstCurrency, string secondCurrency)
        {
            string urlRequest = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20csv%20where%20url%3D%22http%3A%2F%2Ffinance.yahoo.com%2Fd%2Fquotes.csv%3Fe%3D.csv%26f%3Dc4l1%26s%3D" +
                                firstCurrency +
                                secondCurrency +
                                "%3Dx%22%3B&diagnostics=true";
            return (urlRequest);
        }

        /// <summary>
        /// Process request to Yahoo Finance REST site and pass back XML
        /// Returns null if fails
        /// </summary>
        /// <param name="requestUrl">Url to request</param>
        /// <returns>XMLDoc returned by request</returns>
        private XmlDocument MakeRequest(string requestUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                if (request == null)
                    throw new Exception("Error creating HttpWebRequest with url:" + requestUrl);

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                if (response == null)
                    throw new Exception("Error getting response HttpWebRequest with url:" + requestUrl);

                XmlDocument xmlDoc = new XmlDocument();
                // ReSharper disable once AssignNullToNotNullAttribute
                xmlDoc.Load(response.GetResponseStream());
                return (xmlDoc);

            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Simple xPath find of currency conversion rate from Xml response
        /// Expects a specific response, with rate @ //results/row/col1
        /// </summary>
        /// <param name="response">XmlDoc from Yahoo Finance with currency conversion</param>
        /// <returns>Float of currency conversion from XmlDoc</returns>
        private float GetConversionAsFloat(XmlDocument response)
        {
            XmlNodeList result = response.SelectNodes("//results/row/col1");
            if (result == null || result.Count == 0)
                throw new Exception("Result not as expected for currency conversion. Full response:" + response.InnerXml);

            string conversionAsString = result[0].InnerText;
            if (String.IsNullOrWhiteSpace(conversionAsString))
                throw new Exception("Result for conversion rate is empty. Full response:" + response.InnerXml);

            float conversion;
            if (!float.TryParse(conversionAsString, out conversion))
                throw new Exception("Result for conversion rate could not be parsed as Float. Full response:" + response.InnerXml);

            return conversion;
        }
        /// <summary>
        /// Creates a dictionary keyed by three digit string of country code, then value country name
        /// Relies on simple csv of name cCurrencyCodeDictionaryFileName in format: 
        ///     country, code
        /// </summary>
        /// <param name="currencyCodeDictionaryFileName">filename of csv to use for dictionary</param>
        /// <returns>dictionary keyed by three digit string of country code, then value country name</returns>
        private static Dictionary<string, string> GetCurrencyCodeDictionaryFromFile(string currencyCodeDictionaryFileName)
        {
            if (!File.Exists(currencyCodeDictionaryFileName))
                throw new FileNotFoundException("Cannot find file: " + currencyCodeDictionaryFileName);
            return File.ReadLines(currencyCodeDictionaryFileName).Select(line => line.Split(',')).ToDictionary(line => line[1], line => line[0]);
        }
    }
}
