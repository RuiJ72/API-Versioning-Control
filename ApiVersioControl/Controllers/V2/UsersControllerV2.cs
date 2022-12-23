using ApiVersioControl.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiVersioControl.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private const string ApiTestURL = "https://dummyapi.io/data/v1/user?limit=30";
        private const string AppID = "63a5086c487a7f21524f3b03";
        private readonly HttpClient _httpClient;

        public UsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [MapToApiVersion("2.0")]
        [HttpGet(Name = "GetUsersData")]
        public async Task<IActionResult> GetUsersDataAsync()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("app-id", AppID);

            var response = await _httpClient.GetStreamAsync(ApiTestURL);

            var usersData = await JsonSerializer.DeserializeAsync<UsersResponsData>(response);

            var users = usersData?.data;

            return Ok(users);
        }
    }
}
