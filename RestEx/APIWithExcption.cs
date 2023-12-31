﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestEx
{
    //internal
    public class APIWithExcption
    {
        String baseurl = "https://reqres.in/api/";
        //        public void GetSingleUser()
        //        {
        //            var client = new RestClient(baseurl);
        //            var req=new RestRequest("users/43",Method.Get);
        //            var response=client.Execute(req);
        //            //With err
        //            if (!response.IsSuccessful)
        //            {
        //              
        //                    try
        //                    {
        //                        var errorDetails = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
        //                        if (errorDetails != null)
        //                        {
        //                            Console.WriteLine($"API Error:{errorDetails.Error}");
        //                        }
        //                    }
        //                    catch (JsonException)
        //                    {
        //                        Console.WriteLine("Failed to deserialize error response");
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("Successfull Response");
        //                Console.WriteLine(response.Content);
        //            }
        //          
        //        }                                       
        //    }
        //}
        //Json content check for null data
        public void GetSingleUser()
        {
            var client = new RestClient(baseurl);
            var req = new RestRequest("users/55", Method.Get);
            var response = client.Execute(req);
            //With err
            if (!response.IsSuccessful)
            {
                if (IsJson(response.Content))
                {
                    try
                    {
                        
                        var errorDetails = JsonConvert.DeserializeObject<ErrorResponse>(response.Content);
                        if (errorDetails != null)
                        {
                            Console.WriteLine($"API Error:{errorDetails.Error}");
                        }
                    }
                    catch (JsonException)
                    {
                        Console.WriteLine("Failed to deserialize error response");
                    }
                }

                else
                {

                    Console.WriteLine($"Non-JSON error response:{response.Content}");
                }
            }
            static bool IsJson(string content)
            {
                try
                {
                    JToken.Parse(content);
                    return true;
                }
                catch (ArgumentException)
                {
                    return false;
                }
            }
        }
    }
}
