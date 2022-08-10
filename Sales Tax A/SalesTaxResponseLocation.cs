using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales_Tax_A
{
    public class SalesTaxResponseLocation
    {

        private TaxResponseEF taxResponseEF;
        private RestResponse response;

        public SalesTaxResponseLocation(TaxResponseEF taxResponseEF, RestResponse response)
        {
            this.taxResponseEF = taxResponseEF;
            this.response = response;
        }

        public TaxResponseEF TaxResponseEF { get => taxResponseEF; set => taxResponseEF = value; }
        public RestResponse Response { get => response; set => response = value; }
    }
}
