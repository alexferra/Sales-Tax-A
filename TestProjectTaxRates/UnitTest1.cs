using Grpc.Core;
using Newtonsoft.Json;
using RestSharp;
using Sales_Tax_A;
using System;
using System.Net.Mime;
using Xunit;

/// <summary>
/// I will use the free and open-source third-party 
/// framework xUnit.net.net.xUnit which was created by the same team that built NUnit but they fixed the
/// mistakes they felt they made previously.xUnit is more extensible and has better community
/// support.
/// 
/// Alejandro Ferragut
/// </summary>
namespace TestProjectTaxRates
{
    public class UnitTest1
    {
        /// <summary>
        /// Will check for the status response of the call. This test is for a successful call
        /// </summary>
        [Fact]
        public void Test1()
        {
            // arrange 
            String zipcode = "33166";
            String country = "US";
            String city = "Miami";

            RestResponse response = new RestResponse() { ContentType = "application/json", ResponseStatus = ResponseStatus.Completed, StatusCode = System.Net.HttpStatusCode.OK };
            Rate taxResponse = new Rate();
            taxResponse.city = "DORAL";
            taxResponse.city_rate = "0.0";
            taxResponse.combined_district_rate = "0.0";
            taxResponse.combined_rate = "0.07";
            taxResponse.country = "US";
            taxResponse.country_rate = "0.0";
            taxResponse.county = "MIAMI-DADE";
            taxResponse.county_rate = "0.01";
            taxResponse.freight_taxable = false;
            taxResponse.state = "FL";
            taxResponse.state_rate = "0.06";
            taxResponse.zip = "33166";

            // act
            SalesTaxResponseLocation actual = SalesTaxService.getTaxRatesForLocation(zipcode, country, city);
            // assert
            Assert.Equal(response.StatusCode, actual.Response.StatusCode);

            var object1Json = JsonConvert.SerializeObject(taxResponse);
            var object2Json = JsonConvert.SerializeObject(actual.TaxResponseEF.rate);
            Assert.Equal(object1Json, object2Json);

        }


        /// <summary>
        /// Tests the tax calculation for Tax orders. 
        /// </summary>
        [Fact]
        public void Test2()
        {
            // arrange 
            String dataForText = "{\"from_country\":\"US\",\"from_zip\":\"07001\",\"from_state\":\"NJ\"" +
                ",\"to_country\":\"US\",\"to_zip\":\"07446\",\"to_state\":\"NJ\"" +
                ",\"amount\":16.50,\"shipping\":1.5,\"line_items\":[{\"quantity\":1,\"unit_price\":15.0,\"product_tax_code\":\"31000\"}]}";

            TaxForOrderEF taxForOrderEF = JsonConvert.DeserializeObject<TaxForOrderEF>(dataForText);

            String responseTest = "{\"tax\":{\"amount_to_collect\":1.09,\"breakdown\":" +
                "{\"city_tax_collectable\":0.0,\"city_tax_rate\":0.0,\"city_taxable_amount\":0.0,\"combined_tax_rate\":0.06625," +
                "\"county_tax_collectable\":0.0,\"county_tax_rate\":0.0,\"county_taxable_amount\":0.0," +
                "\"line_items\":[{\"city_amount\":0.0,\"city_tax_rate\":0.0,\"city_taxable_amount\":0.0," +
                "\"combined_tax_rate\":0.06625,\"county_amount\":0.0,\"county_tax_rate\":0.0," +
                "\"county_taxable_amount\":0.0,\"id\":\"1\",\"special_district_amount\":0.0," +
                "\"special_district_taxable_amount\":0.0,\"special_tax_rate\":0.0,\"state_amount" +
                "\":0.99,\"state_sales_tax_rate\":0.06625,\"state_taxable_amount\":15.0,\"tax_collectable" +
                "\":0.99,\"taxable_amount\":15.0}],\"shipping\":{\"city_amount\":0.0,\"city_tax_rate\":0.0," +
                "\"city_taxable_amount\":0.0,\"combined_tax_rate\":0.06625,\"county_amount\":0.0,\"county_tax_rate" +
                "\":0.0,\"county_taxable_amount\":0.0,\"special_district_amount\":0.0,\"special_tax_rate\":0.0," +
                "\"special_taxable_amount\":0.0,\"state_amount\":0.1,\"state_sales_tax_rate\":0.06625," +
                "\"state_taxable_amount\":1.5,\"tax_collectable\":0.1,\"taxable_amount\":1.5}," +
                "\"special_district_tax_collectable\":0.0,\"special_district_taxable_amount\":0.0," +
                "\"special_tax_rate\":0.0,\"state_tax_collectable\":1.09,\"state_tax_rate\":0.06625," +
                "\"state_taxable_amount\":16.5,\"tax_collectable\":1.09,\"taxable_amount\":16.5}," +
                "\"freight_taxable\":true,\"has_nexus\":true,\"jurisdictions\":" +
                "{\"city\":\"RAMSEY\",\"country\":\"US\",\"county\":\"BERGEN\",\"state\":\"NJ\"}" +
                ",\"order_total_amount\":16.5,\"rate\":0.06625,\"shipping\":1.5,\"tax_source\":" +
                "\"destination\",\"taxable_amount\":16.5}}";

            TaxForOrderResponseEF taxForOrderResponseEF  = JsonConvert.DeserializeObject<TaxForOrderResponseEF>(responseTest);

            // act
            TaxForOrderResponseEF taxForOrderResponseEFResult = SalesTaxService.calculateTaxes(taxForOrderEF);

            // assert
            var object1Json = JsonConvert.SerializeObject(taxForOrderResponseEF);
            var object2Json = JsonConvert.SerializeObject(taxForOrderResponseEFResult);
            Assert.Equal(object1Json, object2Json);

        }



    }
}
