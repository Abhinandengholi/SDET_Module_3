using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.Security.Cryptography.X509Certificates;
using RestExcepNunit.Utilities;

namespace RestExcepNunit
{

    [TestFixture]
    public class ReqResTests : CoreCodes
    {

        [Test]
        [Order(1)]
        [TestCase(2)]
        public void GetSingleUser(int uid)
        {
            test = extent.CreateTest("Get single user");
            Log.Information("GetSimgleUser Test Started");

            var getUserreq = new RestRequest("users/"+uid, Method.Get);
            var getUserresponse = client.Execute(getUserreq);
            try
            {
                Assert.That(getUserresponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + getUserresponse.Content);

                var userdata = JsonConvert.DeserializeObject<UserDataResponse>(getUserresponse.Content);
                UserData? user = userdata?.Data;

                Assert.NotNull(user);
                Log.Information("User Returned");
                Assert.That(user.Id, Is.EqualTo(2));
                Log.Information("UserId matches with the fetch");
                Assert.IsNotEmpty(user.Email);
                Log.Information("Email is not empty");
                Log.Information("Get single user test passed all Asserts.");
                test.Pass("GetSingleUser passed all Asserts.");

            }
            catch (AssertionException)
            {
                test.Fail("GetSingleUser test failed");
            }


        }
        [Test, Order(2)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create user");
            Log.Information("Create User Test Started");

            var createUserRequest = new RestRequest("users", Method.Post);
            createUserRequest.AddHeader("Content-Type", "application/json");
            createUserRequest.AddJsonBody(new { name = "John wick", job = "Software Developer" });
            var createUserResponse = client.Execute(createUserRequest);
            try
            {

                Assert.That(createUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information("API Response:" + createUserResponse.Content);

                var user = JsonConvert.DeserializeObject<UserData>(createUserResponse.Content);
                Console.WriteLine(createUserResponse.Content);
                Assert.NotNull(user);
                Log.Information("User Returned");
                Log.Information("Create user test passed all Asserts.");
                test.Pass("Create User passed all Asserts.");
            }
            catch (AssertionException)
            {
                test.Fail("Create User test failed");

            }
        }
        [Test, Order(3)]
        [TestCase(2)]
        public void UpdateUser(int uid)
        {

            test = extent.CreateTest("Update user");
            Log.Information("Update User Test Started");

            var updateuserrequest = new RestRequest("users/"+uid, Method.Put);
            updateuserrequest.AddHeader("content-type", "application/json");
            updateuserrequest.AddJsonBody(new { name = "john wick", job = "software developer" });
            var updateUserResponse = client.Execute(updateuserrequest);
            try
            {
                Assert.That(updateUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + updateUserResponse.Content);
                var user = JsonConvert.DeserializeObject<UserData>(updateUserResponse.Content);
                Console.WriteLine(updateUserResponse.Content);
                Assert.NotNull(user);
                Log.Information("User Returned");

                Log.Information("Update user test passed.");
                test.Pass("Update User passed.");
            }
            catch (AssertionException)
            {
                test.Fail("Update User test failed");
            }
        }
        [Test, Order(4)]
        [TestCase(2)]
        public void DeleteUser(int usrid)
        {

            test = extent.CreateTest("Delete user");
            Log.Information("Delete User Test Started");
            var deleteUserRequest = new RestRequest("users/"+usrid, Method.Delete);
            var deleteUserResponse = client.Execute(deleteUserRequest);
            try
            {
                Assert.That(deleteUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
                Log.Information("API Response:" + deleteUserResponse.Content);
                Log.Information("Delete user test passed .");
                test.Pass("Delete User passed .");
            }
            catch(AssertionException)
            {
                test.Fail("Delete User test failed");

            }
        }
        [Test, Order(5)]
        [TestCase(999)]
        public void GetNonExistingUser(int uid)
        {

            test = extent.CreateTest("User found");
            Log.Information("NonExisting User test Started");
            var request = new RestRequest("users/"+uid, Method.Get);
            var response = client.Execute(request);
            try
            {

                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
                Log.Information("API Response:" + response.Content);
                Log.Information("User not found test  passed .");
                test.Pass("User not found test passed.");
            }
            catch (AssertionException)
            {
                test.Fail("User not found test failed");

            }
        }
    }
}
            

        
        
    



