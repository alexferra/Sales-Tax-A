using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Alejandro Ferragut
///  utility class to show API calls.
/// 
/// </summary>>
namespace Sales_Tax_A
{

    public class SalesTaxService
    {

        private static string tokenString = "Token token=\"5da2f821eee4035db4771edab942a4cc\"";
        private static string baseURL = "https://api.taxjar.com/v2/";

        /// <summary>
        /// Will return tax rates for the location provided
        /// </summary>
        /// <param name="zipcode"> needed </param>
        /// <param name="country"> not needed but recommended for accuracy </param>
        /// <param name="city"> can be blank or any value</param>
        /// <returns></returns>
        public static SalesTaxResponseLocation getTaxRatesForLocation(String zipcode, String country = "US", String city ="") {

            var client = new RestClient($"{baseURL}rates/{zipcode}?country={country}&city={city}");            
            var request = new RestRequest("/",Method.Get);
            request.AddHeader("Authorization", tokenString);
            RestResponse response = client.Execute(request);

            TaxResponseEF taxResponseEF = null;
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                taxResponseEF = JsonConvert.DeserializeObject<TaxResponseEF>(response.Content);
            }

            SalesTaxResponseLocation responseReturn = new SalesTaxResponseLocation(taxResponseEF , response);

            client.Dispose();

            return responseReturn;

        }

        /// <summary>
        /// Will return the tax calculation for an order
        /// </summary>
        /// <param name="taxForOrderEF"></param>
        /// <returns></returns>
        public static TaxForOrderResponseEF calculateTaxes(TaxForOrderEF taxForOrderEF)
        {

            var client = new RestClient($"{baseURL}taxes");            
            var request = new RestRequest("/", Method.Post);
            request.AddHeader("Authorization", tokenString);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(taxForOrderEF);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);

            TaxForOrderResponseEF taxForOrderResponseEF = null;
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                taxForOrderResponseEF = JsonConvert.DeserializeObject<TaxForOrderResponseEF>(response.Content);
            }

            client.Dispose();

            return taxForOrderResponseEF;

        }


    }
}
