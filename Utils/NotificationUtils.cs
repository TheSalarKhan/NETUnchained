using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Application;

namespace Utils
{
	public static class NotificationUtils
	{

		public static string SendNotification(string message, string deviceId = "All")
		{
			string sResponseFromServer = "";

			try
			{
				var applicationID = Constant.FCM_SERVER_KEY;
				var senderId = Constant.FCM_SENDER_KEY;
				var uri = "https://fcm.googleapis.com/fcm/send";

				var data = new
				{
					to = (deviceId == "All")?"/topics/all":deviceId,
					priority = "high",
					notification = new
					{
						body = message,
						title = Constant.FCM_TITLE,
						sound= "default"

                    }
				};

				string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(data);

				using(var client = new HttpClient()) {
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",string.Format("key={0}", applicationID));
					client.DefaultRequestHeaders.TryAddWithoutValidation("Sender",string.Format("id={0}", senderId));

						var response = client.PostAsync(uri,new StringContent(jsonString,Encoding.UTF8,"application/json"));
                    // response.Start();
                    response.Wait();
                    // response.RunSynchronously();

                    var responseMessage = response.Result.Content.ReadAsStringAsync();
					// responseMessage.RunSynchronously();
					responseMessage.Wait();

					sResponseFromServer = responseMessage.Result;

				}

			}

			catch (Exception ex)
			{
				Console.Write(ex.Message);
			}
			return sResponseFromServer;
		}
	}
}
