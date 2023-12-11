using Newtonsoft.Json.Linq;
using RestSharp;


//reqres. in API
String baseurl = "https://jsonplaceholder.typicode.com/";
var client = new RestClient(baseurl);

GetAllUsers(client);
GetUser(client);
CreateUser(client);
UpdateUser(client);
DeleteUser(client);
//Get all
static void GetAllUsers(RestClient client)
{
    var getUserRequest = new RestRequest("posts", Method.Get);
    var getUserResponse = client.Execute(getUserRequest);
    Console.WriteLine(getUserResponse.Content);
}
//Get User
static void GetUser(RestClient client)
{
    var getUserRequest = new RestRequest("posts/1", Method.Get);
    var getUserResponse = client.Execute(getUserRequest);
    if (getUserResponse.StatusCode == System.Net.HttpStatusCode.OK)
    { //Parse JSON response content
        JObject? userJson = JObject.Parse(getUserResponse?.Content);
        //Access the"data" array and it's first element

        string? title = userJson["data"]?["title"]?.ToString();
       
        Console.WriteLine($"Get title:"+title);
    }
    else
    {
        Console.WriteLine($"Error:{getUserResponse.ErrorMessage}");
    }


}
//POST or create
static void CreateUser(RestClient client)
{
    var createUserRequest = new RestRequest("posts", Method.Post);
    createUserRequest.AddHeader("Content-Type", "application/json");
    createUserRequest.AddJsonBody(new { userId="13",title = "Holiday", body = "sunday" });
    var createUserResponse = client.Execute(createUserRequest);
    Console.WriteLine("Created"+createUserResponse.Content);
}

//PUT OR PATCH or update
static void UpdateUser(RestClient client)
{
    var updateUserRequest = new RestRequest("posts/1", Method.Put);
    updateUserRequest.AddHeader("Content-Type", "application/json");
    updateUserRequest.AddJsonBody(new { userId = "13", title = "Holiday", body = "monday" });
    var updateUserResponse = client.Execute(updateUserRequest);
    Console.WriteLine("PUT update User response" + updateUserResponse.Content);
}

//DELETE
static void DeleteUser(RestClient client)
{
    var deleteUserRequest = new RestRequest("posts/1", Method.Delete);
    var deleteUserResponse = client.Execute(deleteUserRequest);
    Console.WriteLine("DELETE User response");
    Console.WriteLine(deleteUserResponse.Content);

}
