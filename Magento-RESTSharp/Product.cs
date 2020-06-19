/*
 * Created by Ranorex
 * User: tom
 * Date: 19/06/2020
 * Time: 10:36
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;

namespace Magento_RESTSharp
{
	/// <summary>
	/// Used to represent each product as stored in Magento
	/// </summary>
	public class Product
	{
		public String Sku { get; set; }
        public String Position { get; set; }
        public String Category_id { get; set; }
	};
}
