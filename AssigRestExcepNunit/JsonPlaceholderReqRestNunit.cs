using AssigRestExcepNunit.Utilities;
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

namespace AssigRestExcepNunit
{
    [TestFixture]

    public class JsonPlaceholderReqRestNunit : CoreCodes
    {
        [Test, Order(1), TestCase(2)]
        public void GetSingleUser(int uid)
        {
            test = extent.CreateTest("Get single user");
            Log.Information("GetSimgleUser Test Started");
            var req = new RestRequest("todos/" + uid, Method.Get);
            var response = client.Execute(req);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + response.Content);
                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User Returned");
                Assert.That(user.Id, Is.EqualTo(uid));
                Log.Information("UserId matches with the fetch");
                Assert.IsNotEmpty(user.Title);
                Log.Information("Title is not empty");
                Console.WriteLine("Get title:" + user.Title);

                Log.Information("Get single user test passed all Asserts.");
                test.Pass("GetSingleUser passed all Asserts.");

            }
            catch (AssertionException)
            {
                test.Fail("GetSingleUser test failed");
            }
        }
        [Test]
        [Order(2)]
        public void GetAllUser()
        {
            test = extent.CreateTest("Get All user");
            Log.Information("Get all User Test Started");

            var getUserreq = new RestRequest("todos", Method.Get);
            var getUserresponse = client.Execute(getUserreq);
            try
            {
                Assert.That(getUserresponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + getUserresponse.Content);
                List<UserData> users = JsonConvert.DeserializeObject<List<UserData>>(getUserresponse.Content);

                Assert.NotNull(users);
                Log.Information("User Returned");
                Console.WriteLine("get" + getUserresponse.Content);
                Log.Information("Get all user test passed all Asserts.");
                test.Pass("Get all User passed all Asserts.");

            }
            catch (AssertionException)
            {
                test.Fail("Get all User test failed");
            }

        }
        [Test, Order(3)]
        public void CreateUser()
        {
            test = extent.CreateTest("Create user");
            Log.Information("Create User Test Started");
            var createUserRequest = new RestRequest("todos", Method.Post);
            createUserRequest.AddHeader("Content-Type", "application/json");
            createUserRequest.AddJsonBody(new { userId = "1", title = "holiday", completed = "true" });
            var createUserResponse = client.Execute(createUserRequest);
            try
            {
                Assert.That(createUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information("API Response:" + createUserResponse.Content);
                var user = JsonConvert.DeserializeObject<UserData>(createUserResponse.Content);
                Assert.NotNull(user);
                Log.Information("User Returned");
                Console.WriteLine("Created" + createUserResponse.Content);
                Log.Information("Create user test passed all Asserts.");
                test.Pass("Create User passed all Asserts.");
            }

            catch (AssertionException)
            {
                test.Fail("Create User test failed");

            }
        }
        [Test, Order(4), TestCase(2)]
        public void UpdateUser(int uid)
        {

            test = extent.CreateTest("Update user");
            Log.Information("Update User Test Started");
            var updateuserrequest = new RestRequest("todos/" + uid, Method.Put);
            updateuserrequest.AddHeader("content-type", "application/json");
            updateuserrequest.AddJsonBody(new { userId = "1", title = "holiday", completed = "true" });
            var updateUserResponse = client.Execute(updateuserrequest);
            try
            {
                Assert.That(updateUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + updateUserResponse.Content);
                var user = JsonConvert.DeserializeObject<UserData>(updateUserResponse.Content);
                Assert.NotNull(user);
                Log.Information("User Returned");
                Console.WriteLine("updated" + updateUserResponse.Content);
                Log.Information("Update user test passed.");
                test.Pass("Update User passed.");
            }
            catch (AssertionException)
            {
                test.Fail("Update User test failed");
            }

        }
        [Test, Order(5), TestCase(2)]
        public void DeleteUser(int usruid)
        {

            test = extent.CreateTest("Delete user");
            Log.Information("Delete User Test Started");
            var deleteUserRequest = new RestRequest("todos/" + usruid, Method.Delete);
            var deleteUserResponse = client.Execute(deleteUserRequest);
            try
            {
                Assert.That(deleteUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + deleteUserResponse.Content);
                Log.Information("Delete user test passed .");
                test.Pass("Delete User passed .");
            }
            catch (AssertionException)
            {
                test.Fail("Delete User test failed");

            }
        }
        [Test, Order(6), TestCase(999)]
        public void GetNonExistingUser(int uid)
        {
            test = extent.CreateTest("User found");
            Log.Information("NonExisting User test Started");
            var request = new RestRequest("todos/" + uid, Method.Get);
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





