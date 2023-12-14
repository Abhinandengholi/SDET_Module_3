using CaseStudy.Utilities;
using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy
{

    [TestFixture]
    public class BookingAPITesting : CoreCodes
    {
        [Test]
        [Order(0)]
        public void CreateToken()
        {
            test = extent.CreateTest("Create token");
            Log.Information("Create Token Test Started");
            var request = new RestRequest("auth", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { username = "admin", password = "password123" });
            var response = client.Execute(request);
            try
            {
                Assert.That(response.Content, Is.Not.Null, "Response is null");
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + response.Content);
                Log.Information("Token created and  Returned");
                test.Pass("Token creation test passed.");
            }
            catch
            {
                test.Fail("Token creation test failed.");
            }
        }
        [Test, Order(1)]
        public void AllBooking()
        {
            test = extent.CreateTest("Get All booking");
            Log.Information("Get all booking Test Started");

            var getallbookingreq = new RestRequest("booking", Method.Get);
            var getallbookingres = client.Execute(getallbookingreq);
            try
            {
                Assert.That(getallbookingres.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + getallbookingres.Content);

                List<BookingID> bookingdata = JsonConvert.DeserializeObject<List<BookingID>>(getallbookingres.Content);

                Assert.NotNull(bookingdata);
                Log.Information("all booking Returned");
                Log.Information("Get booking test passed all asserts");
                test.Pass("Get booking test passed all asserts.");

            }
            catch (AssertionException)
            {
                test.Fail("Get all booking test failed.");
            }


        }
        [Test, Order(2), TestCase(13)]
        public void GetOneBooking(int id)
        {
            test = extent.CreateTest("Get one booking");
            Log.Information("Get one booking Test Started");

            var getonebookingreq = new RestRequest("booking/" + id, Method.Get);
            getonebookingreq.AddHeader("Accept", "application/json");
            var getonebookingres = client.Execute(getonebookingreq);
            try
            {
                Assert.That(getonebookingres.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + getonebookingres.Content);

                var bookingdata = JsonConvert.DeserializeObject<Bookingdetails>(getonebookingres.Content);

                Assert.NotNull(bookingdata);
                Log.Information("booking Returned");
                Assert.IsNotEmpty(bookingdata.FirstName);
                Log.Information("Name is not empty");
                Log.Information("Get one booking test passed all asserts");
                test.Pass("Get one booking test passed all asserts.");

            }
            catch (AssertionException)
            {
                test.Fail("Get one booking test failed.");
            }

        }
        [Test, Order(3)]
        public void CreateBooking()
        {
            test = extent.CreateTest("Create booking");
            Log.Information("Create booking Test Started");
            var createbookingrequest = new RestRequest("booking", Method.Post);
            createbookingrequest.AddHeader("Content-Type", "application/json");
            createbookingrequest.AddHeader("Accept", "application/json");
            createbookingrequest.AddJsonBody(new
            {
                firstname = "Jim",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2018-01-01",
                    checkout = "2019-01-01"
                },
                additionalneeds = "Breakfast"
            });
            var response = client.Execute(createbookingrequest);
            try
            {
                Assert.That(response.Content, Is.Not.Null, "Response is null");
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + response.Content);
                Log.Information("Booking created and  Returned");
                test.Pass("Booking creation test passed.");
            }
            catch
            {
                test.Fail("Booking creation test failed.");
            }
        }

        [Test, Order(4)]
        [TestCase(13)]
        public void UpdateBooking(int uid)
        {

            test = extent.CreateTest("Update booking");
            Log.Information("Update booking Test Started");
            var gettokenrequest = new RestRequest("auth", Method.Post);
            gettokenrequest.AddHeader("Content-Type", "application/json");
            gettokenrequest.AddJsonBody(new { username = "admin", password = "password123" });
            var gettokenresponse = client.Execute(gettokenrequest);
            var token = JsonConvert.DeserializeObject<Bookingdetails>(gettokenresponse.Content);
            var updateuserrequest = new RestRequest("booking/" + uid, Method.Put);
            updateuserrequest.AddHeader("content-type", "application/json");
            updateuserrequest.AddHeader("Accept", "application/json");
            updateuserrequest.AddHeader("Cookie", "token=" + token?.Token);
            updateuserrequest.AddJsonBody(new
            {
                firstname = "James",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates =
                new
                {
                    checkin = "2018-01-01",
                    checkout = "2019-01-01"
                },
                additionalneeds = "Breakfast"
            });
            var updateUserResponse = client.Execute(updateuserrequest);
            try
            {
                Assert.That(updateUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + updateUserResponse.Content);
                var usr = JsonConvert.DeserializeObject<Bookingdetails>(updateUserResponse.Content);

                Console.WriteLine(updateUserResponse.Content);
                Assert.NotNull(usr);
                Log.Information("User updated and Returned");

                Log.Information("Update user test passed.");
                test.Pass("Update User passed.");
            }
            catch (AssertionException)
            {
                test.Fail("Update User test failed");
            }
        }
        [Test, Order(5)]
        [TestCase(1)]
        public void DeleteUser(int usrid)
        {

            test = extent.CreateTest("Delete user");
            Log.Information("Delete User Test Started");
            var gettokenrequest = new RestRequest("auth", Method.Post);
            gettokenrequest.AddHeader("Content-Type", "application/json");
            gettokenrequest.AddJsonBody(new { username = "admin", password = "password123" });
            var gettokenresponse = client.Execute(gettokenrequest);
            var token = JsonConvert.DeserializeObject<Bookingdetails>(gettokenresponse.Content);


            var deleteUserRequest = new RestRequest("booking/" + usrid, Method.Delete);
            deleteUserRequest.AddHeader("Content-Type", "application/json");
            deleteUserRequest.AddHeader("Cookie", "token=" + token?.Token);
            var deleteUserResponse = client.Execute(deleteUserRequest);
            try
            {
                Assert.That(deleteUserResponse.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information("API Response:" + deleteUserResponse.Content);
                Log.Information("Booking deletion test passed .");
                test.Pass("Delete booking test passed .");
            }
            catch (AssertionException)
            {
                test.Fail("Delete booking test failed"); 

            }
        }
    }
}
       









