using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestEx;
using RestSharp;


//reqres. in API
String baseurl = "https://reqres.in/api/";
var client=new RestClient(baseurl);

//var getUserRequest = new RestRequest("users/2", Method.Get);
//var getUserResponse=client.Execute(getUserRequest);
//Console.WriteLine(getUserResponse.Content);


////POST or create
//var createUserRequest = new RestRequest("users", Method.Post);
//createUserRequest.AddParameter("name", "John Wick");
//createUserRequest.AddParameter("job", "Software Developer");

//var createUserResponse = client.Execute(createUserRequest);
//Console.WriteLine("POST Create User response");
//Console.WriteLine(createUserResponse.Content);

////PUT OR PATCHor update
//var updateUserRequest = new RestRequest("users/2", Method.Put);
//updateUserRequest.AddParameter("name", "Updated John Wick");
//updateUserRequest.AddParameter("job", "Software Developer");

//var updateUserResponse = client.Execute(updateUserRequest);
//Console.WriteLine("PUT update User response");
//Console.WriteLine(updateUserResponse.Content);

////DELETE
//var deleteUserRequest = new RestRequest("users/2", Method.Delete);
//updateUserRequest.AddParameter("name", "Updated John Wick");

//var deleteUserResponse = client.Execute(deleteUserRequest);
//Console.WriteLine("DELETE User response");
//Console.WriteLine(deleteUserResponse.Content);

////Get all
//var getUserRequest = new RestRequest("users", Method.Get);
//getUserRequest.AddQueryParameter("page","1");//Adding query parameter
//var getUserResponse = client.Execute(getUserRequest);
//Console.WriteLine(getUserResponse.Content);

////POST or create
//var createUserRequest = new RestRequest("users", Method.Post);
//createUserRequest.AddHeader("Content-Type", "application/json");
//createUserRequest.AddJsonBody(new { name = "John wick", job = "Software Developer" });
//var createUserResponse = client.Execute(createUserRequest);
//Console.WriteLine("POST Create User response");
//Console.WriteLine(createUserResponse.Content);

////PUT OR PATCH or update
//var updateUserRequest = new RestRequest("users/2", Method.Put);
//updateUserRequest.AddQueryParameter("data.id", "2");//Adding query parameter
//updateUserRequest.AddHeader("Content-Type", "application/json");
//updateUserRequest.AddJsonBody(new { name = "John wick", job = "Software Developer" });
//var updateUserResponse = client.Execute(updateUserRequest);
//Console.WriteLine("PUT update User response"+updateUserResponse.Content);

////DELETE
//var deleteUserRequest = new RestRequest("users/2", Method.Delete);
//var deleteUserResponse = client.Execute(deleteUserRequest);
//Console.WriteLine("DELETE User response");
//Console.WriteLine(deleteUserResponse.Content);


//GetAllUsers(client);
//GetUser(client);
//CreateUser(client);
//UpdateUser(client);
//DeleteUser(client);
////Get all
//static void GetAllUsers(RestClient client)
//{
//    var getUserRequest = new RestRequest("users", Method.Get);

//    var getUserResponse = client.Execute(getUserRequest);
//    Console.WriteLine(getUserResponse.Content);
//}
////Get User
//static void GetUser(RestClient client)
//{
//    var getUserRequest = new RestRequest("users", Method.Get);
//    getUserRequest.AddQueryParameter("page", "1");//Adding query parameter
//    var getUserResponse = client.Execute(getUserRequest);
//    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
//    { //Parse JSON response content
//        JObject? userJson = JObject.Parse(getUserResponse?.Content);
//        string? pageno = userJson["page"]?.ToString();

//        string? userName = userJson["data"]?["first_name"]?.ToString();
//        string? userLastName = userJson["data"]?["last_name"]?.ToString();
//        Console.WriteLine($"User Name:{pageno}{userName}{userLastName}");
//    }
//    else
//    {
//        Console.WriteLine($"Error:{getUserResponse.ErrorMessage}");
//    }


//}
// //POST or create
// static void CreateUser(RestClient client)
//{
//    var createUserRequest = new RestRequest("users", Method.Post);
//    createUserRequest.AddHeader("Content-Type", "application/json");
//    createUserRequest.AddJsonBody(new { name = "John wick", job = "Software Developer" });
//    var createUserResponse = client.Execute(createUserRequest);
//    Console.WriteLine("POST Create User response");
//    Console.WriteLine(createUserResponse.Content);
//}

////PUT OR PATCH or update
//static void UpdateUser (RestClient client)
//{ 
//var updateUserRequest = new RestRequest("users/2", Method.Put);
//updateUserRequest.AddQueryParameter("data.id", "2");//Adding query parameter
//updateUserRequest.AddHeader("Content-Type", "application/json");
//updateUserRequest.AddJsonBody(new { name = "John wick", job = "Software Developer" });
//var updateUserResponse = client.Execute(updateUserRequest);
//Console.WriteLine("PUT update User response" + updateUserResponse.Content);
//}

////DELETE
//static void DeleteUser(RestClient client)
//{
//    var deleteUserRequest = new RestRequest("users/2", Method.Delete);
//    var deleteUserResponse = client.Execute(deleteUserRequest);
//    Console.WriteLine("DELETE User response");
//    Console.WriteLine(deleteUserResponse.Content);

//}
//GetSingleUser(client);
//static void GetSingleUser(RestClient client)
//{
//    var getUserRequest = new RestRequest("users/2", Method.Get);
//    var getUserResponse = client.Execute(getUserRequest);

//    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
//    { 
//        //deserialize JSON reponse content into C# object
//        var response=JsonConvert.DeserializeObject<UserDataResponse>(getUserResponse.Content);
        
//        UserData? user=response?.Data;

//        //access properties of the deserilized object
//        Console.WriteLine($"User ID:{user?.Id}");
//        Console.WriteLine($"User Email:{user?.Email}");
//        Console.WriteLine($"User Name:{user?.FirstName}{user?.LastName}");
//        Console.WriteLine($"User Avatar:{user?.Avatar}");

//    }
//    else
//    {
//        Console.WriteLine($"Error:{getUserResponse.ErrorMessage}");
//    }


//}
APIWithExcption apt=new APIWithExcption();
apt.GetSingleUser();



