/*
 * Created by Ranorex
 * User: Tom Millichamp - Edgewords
 * Date: 18/06/2020
 * Time: 14:07
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net; // for the HttpStatusCode check
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using WinForms = System.Windows.Forms;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using Newtonsoft.Json;

namespace Magento_RESTSharp
{
    /// <summary>
    /// Description of MagentoAPI.
    /// </summary>
    [TestModule("7211C018-2162-4007-A12C-38A577858125", ModuleType.UserCode, 1)]
    public class MagentoAPI : ITestModule
    {
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public MagentoAPI()
        {
            // Do not delete - a parameterless constructor is required!
        }

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            
            //See API docs at: https://devdocs.magento.com/guides/v2.4/rest/bk-rest.html
            //Also if you have set it up in Magento, you can see APi docs at http://<your_magento_url>/swagger
            
            //For restSharp, see : http://restsharp.org/usage/serialization.html as you can use different serialization/de-serialization options
            //The default JSON serializer uses the forked version of SimpleJson, but you can use:#
            //NewtonsoftJson (aka Json.Net) - examples in this code are from : https://www.newtonsoft.com/json/help/html/Samples.htm#!
            //Utf8Json
            //System.Text.Json
            
            String token = GetToken();
            RestClient client = new RestClient();
            client.Timeout = -1;
            client.BaseUrl = new Uri("http://127.0.0.1/"); //Base URL to Magento
            
	 		//or you could use an explicit integration token set up in Magento
	        //token="ayi6rlsmypq596wgf7xskf6cg3cw1044"; //Integration Token from Magento admin


	        
	        /*******************************
	        * Get the categories from magento  
			********************************/                
	        RestRequest request = new RestRequest("rest/default/V1/categories", Method.GET);
	        request.AddHeader("Authorization", token); //supply our authorization token
	        
	        IRestResponse response = client.Execute(request);
	        
	        int status = (int)response.StatusCode;
	        Report.Info("Response Code: " + status.ToString());
	        Report.Info(response.Content);
	        
	        
	        /*****************************
	         * Get a customer record
	         * ***************************/
			request = new RestRequest("rest/default/V1/customers/1", Method.GET);
			//request.AddHeader("Authorization", "Bearer ayi6rlsmypq596wgf7xskf6cg3cw1044");
			request.AddHeader("Authorization", token); //using dynamic token, but could use hard-coded as line above
			request.AddHeader("Content-Type","application/json;charset=utf-8");
			response = client.Execute(request);

	        status = (int)response.StatusCode;
	        Report.Info("Response Code: " + status.ToString());
	        Report.Info(response.Content);
	        if (response.StatusCode == HttpStatusCode.OK && response.Content.Contains("roni_cost@example.com")){
				Report.Success("200 OK returned correct customer");
			}
	        
	        
	        /*****************************
	         * Get all products in a category
	         * ***************************/
			request = new RestRequest("rest/default/V1/categories/2/products",Method.GET);
			request.AddHeader("Authorization", "Bearer ayi6rlsmypq596wgf7xskf6cg3cw1044");
			request.AddHeader("Content-Type","application/json;charset=utf-8");
			
			IRestResponse<Product> prod_response = client.Execute<Product>(request);

			Report.Info(prod_response.Content);
	        if (prod_response.StatusCode == HttpStatusCode.OK){
				Report.Success("200 OK returned");
				//Let's use NewtonsoftJson to de-serialize the JSON into an array of products
				List<Product> product_list = JsonConvert.DeserializeObject<List<Product>>(prod_response.Content);
				Report.Info(product_list[0].Sku); //report the first product in the list with its SKU
			}
	        
	        
	        
			/**********************************************************
	        //Create a customer (can only use the same email once)
	        * ********************************************************/
	        Customer myCustomer = new Customer
		    {
		        _customer_details = new CustomerDetails 
		        	{     	
				    Email = "testr@myplace.co.uk",
					Firstname = "testy",
					Lastname = "Jones",
					Website_id = 1
		        	}
		    };
    
	        //Using NewtonsoftJson to serialize the object into JSON
	        string body = JsonConvert.SerializeObject(myCustomer);
	        
	        request = new RestRequest("rest/default/V1/customers", Method.POST);
			request.AddHeader("Authorization", "Bearer ayi6rlsmypq596wgf7xskf6cg3cw1044");
			request.AddHeader("Content-Type","application/json;charset=utf-8");
			request.AddJsonBody(body); // add the json customer details
			response = client.Execute(request);
	        
			Report.Info(response.StatusCode.ToString());
	        Report.Info(response.Content);

        	if (response.StatusCode == HttpStatusCode.OK){
				Report.Success("200 OK returned");
			} 
	        
	        
	        
	        /**********************************************************
	        //Create a customer using a string of json, so no serialization
	        //Shows why using Objects to store values is easier!
	        * ********************************************************/
	        String custJSON = @"{""customer"": {""email"": ""joeb@test.com"",""firstname"": ""tom"",""lastname"": ""jones"",""website_id"": 1}}";
			request = new RestRequest("rest/default/V1/customers", Method.POST);
			request.AddHeader("Authorization", "Bearer ayi6rlsmypq596wgf7xskf6cg3cw1044");
	        request.AddJsonBody(custJSON); // add the json customer details
			response = client.Execute(request);
			Report.Info(response.StatusCode.ToString());
	        Report.Info(response.Content);
	        
	        
	        
	        /*****************************
	         * Negative test for unauthorized
	         * ***************************/
			request = new RestRequest("rest/default/V1/customers/1", Method.GET);
			request.AddHeader("cache-control", "no-cache");
			response = client.Execute(request);
			//check we got a 401 which is a pass
	        if (response.StatusCode == HttpStatusCode.Unauthorized){
				Report.Success("returned unauthorized 401 correctly");
			}
	        
	        
        } // end of run method
    
        
		/************************************
         * Example of obtaining a user Bearer token
         * From a username & password
         ***********************************/	
        public String GetToken (){

            String host = "http://127.0.0.1";
            
            //User Token which is temporary (4 hours be default)
            //and may have restricted access to data
            String endpoint = "/rest/default/V1/integration/admin/token";

            RestClient _restClient = new RestClient(host);
            var request = new RestRequest(endpoint, Method.POST);

            //Initialize Credentials Property
            var userRequest = new Credentials{username="user",password="bitnami1"};
            var inputJson = JsonConvert.SerializeObject(userRequest); //again, serialized using Newtonsoft

            //Request Header
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            //Request Body
            request.AddParameter("application/json", inputJson, ParameterType.RequestBody);
			//execute request
            var response = _restClient.Execute(request);

            var token=response.Content; 
            token=token.Replace("\"","");
            token = token.Trim(' ','\r','\n');
            token = "Bearer " + token;
            Report.Info("Token is : " + token);
            
            
            return token;
        }
    }
}
