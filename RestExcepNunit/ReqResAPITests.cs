using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestExcepNunit
{
    [TestFixture]
    internal class ReqResAPITests
    {
        private RestClient client;
        private string baseUrl = "https://reqres.in/api/";
        [SetUp]
        public void SetUp()
        {
            client = new RestClient(baseUrl);
        }
        [Test]
        [Order(1)]
        public void GetSingleUser()
        {

            var req = new RestRequest("users/2", Method.Get);
            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var userdata = JsonConvert.DeserializeObject<UserDataResponse>(response.Content);
            UserData? user = userdata?.Data;
            Assert.NotNull(user);
            Assert.That(user.Id, Is.EqualTo(2));
            Assert.IsNotEmpty(user.Email);

        }
        [Test, Order(2)]
        public void CreateUser()
        {
            var createUserRequest = new RestRequest("users", Method.Post);
            createUserRequest.AddHeader("Content-Type", "application/json");
            createUserRequest.AddJsonBody(new { name = "John wick", job = "Software Developer" });
            var createUserResponse = client.Execute(createUserRequest);
            Assert.That(createUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            var user = JsonConvert.DeserializeObject<UserData>(createUserResponse.Content);
            Assert.NotNull(user);
            //Assert.IsNotEmpty(user.Email);
            Console.WriteLine(createUserResponse.Content);
        }
        [Test, Order(3)]
        public void UpdateUser()
        {
            {
                var updateuserrequest = new RestRequest("users/2", Method.Put);
                updateuserrequest.AddHeader("content-type", "application/json");
                updateuserrequest.AddJsonBody(new { name = "john wick", job = "software developer" });
                var updateUserResponse = client.Execute(updateuserrequest);
                Assert.That(updateUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                var user = JsonConvert.DeserializeObject<UserData>(updateUserResponse.Content);
                Assert.NotNull(user);
                Console.WriteLine(updateUserResponse.Content);
            }
        }
        [Test, Order(4)]
        public void DeleteUser()
        {
            {
                var deleteUserRequest = new RestRequest("users/2", Method.Delete);
                var deleteUserResponse = client.Execute(deleteUserRequest);
                Assert.That(deleteUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
            }
        }
        [Test, Order(5)]
        public void GetNonExistingUser()
        {
            var request = new RestRequest("users/999", Method.Get);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }
    }
}
