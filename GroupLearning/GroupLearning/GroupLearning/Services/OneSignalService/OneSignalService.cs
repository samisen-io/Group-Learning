using GroupLearning.Interfaces.OneSignalServices;
using RestSharp;

namespace GroupLearning.Services.OneSignalService;

public class OneSignalService : IOneSignalService
{
  //private readonly HttpClient _httpClient;
  private readonly string _apiKey;
  private readonly string _appId;

  public OneSignalService(/*HttpClient httpClient,*/ string apiKey, string appId)
  {
    //_httpClient = httpClient;
    _apiKey = apiKey;
    _appId = appId;
  }

  public async Task SendSmsAsync(string phoneNumber, string message)
  {
    try
    {
      var options = new RestClientOptions("https://api.onesignal.com/notifications?c=sms");
      var client = new RestClient(options);
      var request = new RestRequest("");
      request.AddHeader("accept", "application/json");

      var jsonBody = new
      {
        app_id = _appId,
        contents = new { en = $"{message}" },
        sms_from = "+919642960930",
        name = "OTP",
        included_segments = new[] { "All" },
        filters = new[]
        {
        new
        {
            field = "tag",
            key = _apiKey,
            relation = "=",
            value = ""
        }
        }
      };

      request.AddJsonBody(jsonBody);

      var response = await client.PostAsync(request);
    }
    catch (Exception e)
    {

      throw new Exception(e.Message);
    }

    //var requestUrl = "https://onesignal.com/api/v1/notifications";

    //var payload = new
    //{
    //  app_id = _appId,
    //  contents = new { en = message },
    //  include_phone_numbers = new string[] { phoneNumber }
    //};

    //var jsonPayload = JsonConvert.SerializeObject(payload);
    //var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

    //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _apiKey);

    //var response = await _httpClient.PostAsync(requestUrl, content);

    //if (!response.IsSuccessStatusCode)
    //{
    //  throw new HttpRequestException($"Failed to send SMS: {response}");
    //}
  }
}

