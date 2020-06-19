/*
 * Created by Ranorex
 * User: Tom Millichamp - Edgewords
 * Date: 19/06/2020
 * Time: 12:11
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using Newtonsoft.Json;

namespace Magento_RESTSharp
{
	/**********************************
	 * Customer json is nested
	 * so we have to create 2 classes
	 * to cope with each level
	 * *******************************/
	
	/***
	  {
	  "customer": {
			"email": "user@test.com",
			"firstname": "joe",
			"lastname": "bloggs",
			"website_id": 1
			}
		}
	***/
	
	public class CustomerParamater
	{
		// see https://www.newtonsoft.com/json/help/html/JsonPropertyName.htm for explanation
	    [JsonProperty("customer")]
	    public Customer Customer { get; set; }
	}
	
	public class Customer
	{
		public String Email { get; set; }
		public String Firstname { get; set; }
		public String Lastname { get; set; }
		public int Website_id { get; set; }
	}
}
