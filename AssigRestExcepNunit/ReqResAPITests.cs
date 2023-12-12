using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssigRestExcepNunit
{
    [TestFixture]
    internal class ReqResAPITests
    {
        private RestClient client;
        private string baseUrl = "https://jsonplaceholder.typicode.com/";
        [SetUp]
        public void SetUp()
        {
            client = new RestClient(baseUrl);
        }
        [Test]
        [Order(1)]
        public void GetSingleUser()
        {

            var req = new RestRequest("todos/2", Method.Get);
            var response = client.Execute(req);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            var user = JsonConvert.DeserializeObject<UserData>(response.Content);
            Assert.NotNull(user);
            Assert.That(user.Id, Is.EqualTo(2));
            Assert.IsNotEmpty(user.Title);
            Console.WriteLine("Get title:"+user.Title);

        }
        [Test]
        [Order(2)]
        public void GetAllUser()
        {

            var getUserreq = new RestRequest("todos", Method.Get);
            var getUserresponse = client.Execute(getUserreq);
            Assert.That(getUserresponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));

            List<UserData> users= JsonConvert.DeserializeObject<List<UserData>>(getUserresponse.Content);
         
            Assert.NotNull(users);

            Console.WriteLine("get" + getUserresponse.Content);

        }
        [Test, Order(3)]
        public void CreateUser()
        {
            var createUserRequest = new RestRequest("todos", Method.Post);
            createUserRequest.AddHeader("Content-Type", "application/json");
            createUserRequest.AddJsonBody(new { userId = "1", title = "holiday",completed="true" });
            var createUserResponse = client.Execute(createUserRequest);
            Assert.That(createUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
            var user = JsonConvert.DeserializeObject<UserData>(createUserResponse.Content);
            Assert.NotNull(user);
            Console.WriteLine("Created"+createUserResponse.Content);
        }
        [Test, Order(4)]
        public void UpdateUser()
        {
            {
                var updateuserrequest = new RestRequest("todos/2", Method.Put);
                updateuserrequest.AddHeader("content-type", "application/json");
                updateuserrequest.AddJsonBody(new { userId = "1", title = "holiday", completed = "true" });
                var updateUserResponse = client.Execute(updateuserrequest);
                Assert.That(updateUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                var user = JsonConvert.DeserializeObject<UserData>(updateUserResponse.Content);
                Assert.NotNull(user);
                Console.WriteLine("updated"+updateUserResponse.Content);
            }
        }
        [Test, Order(5)]
        public void DeleteUser()
        {
            {
                var deleteUserRequest = new RestRequest("todos/2", Method.Delete);
                var deleteUserResponse = client.Execute(deleteUserRequest);
                Assert.That(deleteUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            }
        }
        [Test, Order(6)]
        public void GetNonExistingUser()
        {
            var request = new RestRequest("todos/999", Method.Get);
            var response = client.Execute(request);
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }
    }
}