using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;

namespace FCM.Net
{
    public class ResponseContent
    {
        public HttpStatusCode StatusCode { get; }
        public string ReasonPhrase { get; }
        public MessageResponse MessageResponse { get; }

        public ResponseContent(HttpStatusCode statusCode, string reasonPhrase, MessageResponse messageResponse)
        {
            this.StatusCode = statusCode;
            this.ReasonPhrase = reasonPhrase;
            this.MessageResponse = messageResponse;
        }
    }

    /// <summary>
    /// Response Message Content
    /// </summary>
    public class MessageResponse
    {
        /// <summary>
        /// Internal Error
        /// </summary>
        public string InternalError { get; set; }

        /// <summary>
        /// Response Content
        /// </summary>
        public string ResponseContent { get; set; }

        /// <summary>
        /// Unique ID (number) identifying the multicast message.
        /// </summary>
        [JsonProperty("multicast_id")]
        public string MulticastId { get; set; }

        /// <summary>
        /// Number of messages that were processed without an error.
        /// </summary>
        [JsonProperty("success")]
        public int Success { get; set; }

        /// <summary>
        /// Number of messages that could not be processed.
        /// </summary>
        [JsonProperty("failure")]
        public int Failure { get; set; }

        /// <summary>
        /// Number of results that contain a canonical registration token. A canonical registration ID is the registration token of the last registration requested by the client app. This is the ID that the server should use when sending messages to the device.
        /// </summary>
        [JsonProperty("canonical_ids")]
        public string CanonicalIds { get; set; }

        /// <summary>
        /// Array of objects representing the status of the messages processed. The objects are listed in the same order as the request (i.e., for each registration ID in the request, its result is listed in the same index in the response).
        /// </summary>
        [JsonProperty("results")]
        public List<Result> Results { get; set; }
    }

    /// <summary>
    /// Result Item
    /// </summary>
    public class Result
    {
        /// <summary>
        /// String specifying a unique ID for each successfully processed message.
        /// </summary>
        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        /// <summary>
        /// Optional string specifying the canonical registration token for the client app that the message was processed and sent to. Sender should use this value as the registration token for future requests. Otherwise, the messages might be rejected.
        /// </summary>
        [JsonProperty("registration_id")]
        public string RegistrationId { get; set; }

        /// <summary>
        /// String specifying the error that occurred when processing the message for the recipient. The possible values can be found in https://firebase.google.com/docs/cloud-messaging/http-server-ref#table9
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
    /// <summary>
    /// Keys for notification messages
    /// </summary>
    public class Notification
    {
        /// <summary>
        /// (i0S, Android, Web) Indicates notification title.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// (i0S, Android, Web) Indicates notification body text.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// <para/>(iOS) N/A
        /// <para/>(Android) Indicates notification icon. Sets value to myicon for drawable resource myicon. If you don't send this key in the request, FCM displays the launcher icon specified in your app manifest.
        /// <para/>(Web) The URL for a notification icon.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// <para/>(iOS) Indicates the action associated with a user click on the notification. Corresponds to category in the APNs payload.
        /// <para/>(Android) Indicates the action associated with a user click on the notification. When this is set, an activity with a matching intent filter is launched when user clicks the notification.
        /// <para/>(Web) Indicates the action associated with a user click on the notification. For all URL values, secure HTTPS is required.
        /// </summary>
        [JsonProperty("click_action")]
        public string ClickAction { get; set; }

        /// <summary>
        /// <para/>(iOS) Indicates a sound to play when the device receives a notification.Sound files can be in the main bundle of the client app or in the Library/Sounds folder of the app's data container. See the iOS Developer Library for more information.
        /// <para/>(Android) Indicates a sound to play when the device receives a notification. Supports default or the filename of a sound resource bundled in the app. Sound files must reside in /res/raw/.
        /// <para/>(Web) N/A
        /// </summary>
        [JsonProperty("sound")]
        public string Sound { get; set; }

        /// <summary>
        /// <para/>(iOS)Indicates the badge on the client app home icon.
        /// <para/>(Android) N/A
        /// <para/>(Web) N/A
        /// </summary>
        [JsonProperty("badge")]
        public string Badge { get; set; }

        /// <summary>
        /// <para/>(iOS) N/A
        /// <para/>(Android) Indicates color of the icon, expressed in #rrggbb format
        /// <para/>(Web) N/A
        /// </summary>
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// <para/>(iOS) N/A
        /// <para/>(Android) Indicates whether each notification results in a new entry in the notification drawer on Android. 
        /// <para/> - If not set, each request creates a new notification.
        /// <para/> - If set, and a notification with the same tag is already being shown, the new notification replaces the existing one in the notification drawer.
        /// <para/>(Web) N/A
        /// </summary>
        [JsonProperty("tag")]
        public string Tag { get; set; }

        /// <summary>
        /// <para/>(iOS) Indicates the key to the body string for localization.Corresponds to "loc-key" in the APNs payload.
        /// <para/>(Android) ndicates the key to the body string for localization. Use the key in the app's string resources when populating this value.
        /// <para/>(Web) N/A
        /// </summary>
        [JsonProperty("body_loc_key")]
        public string BodyLocalizationKey { get; set; }

        /// <summary>
        /// <para/>(iOS) Indicates the string value to replace format specifiers in the body string for localization.Corresponds to "loc-args" in the APNs payload.
        /// <para/>(Android) Indicates the string value to replace format specifiers in the body string for localization. 
        /// <para/>(Web) N/A
        /// </summary>
        [JsonProperty("body_loc_args")]
        public List<string> BodyLocalizationArguments { get; set; }

        /// <summary>
        /// <para/>(iOS)Indicates the key to the title string for localization.Corresponds to "title-loc-key" in the APNs payload.
        /// <para/>(Android) Indicates the key to the title string for localization. Use the key in the app's string resources when populating this value.
        /// <para/>(Web) N/A
        /// </summary>
        [JsonProperty("title_loc_key")]
        public string TitleLocalizationKey { get; set; }

        /// <summary>
        /// <para/>(iOS)Indicates the string value to replace format specifiers in the title string for localization. Corresponds to "title-loc-args" in the APNs payload.
        /// <para/>(Android) Indicates the string value to replace format specifiers in the title string for localization. 
        /// <para/>(Web) N/A
        /// </summary>
        public List<string> TitleLocalizationArguments { get; set; }
    }



    /// <summary>
    /// Priority of the message
    /// </summary>
    public enum Priority
    {
        [JsonProperty("normal")]
        Normal,
        [JsonProperty("high")]
        High
    }
    /// <summary>
    /// Keys for messages
    /// </summary>
    public class Message
    {
        /// <summary>
        /// This parameter specifies the recipient of a message.
        /// <para/>The value must be a registration token, notification key, or topic. Do not set this field when sending to multiple topics. 
        ///<para/>See <seealso cref="FCM.Net.Message.Condition"/>
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// This parameter specifies a list of devices (registration tokens, or IDs) receiving a multicast message. It must contain at least 1 and at most 1000 registration tokens.
        /// <para/>Use this parameter only for multicast messaging, not for single recipients. Multicast messages (sending to more than 1 registration tokens) are allowed using HTTP JSON format only. 
        /// </summary>
        [JsonProperty("registration_ids")]
        public List<string> RegistrationIds { get; set; }

        /// <summary>
        /// This parameter specifies a logical expression of conditions that determine the message target.
        /// <para/>Supported condition: Topic, formatted as "'yourTopic' in topics". This value is case-insensitive.
        /// <para/>Supported operators: &amp;&amp;, ||. Maximum two operators per topic message supported.
        /// </summary>
        [JsonProperty("condition")]
        public string Condition { get; set; }

        /// <summary>
        /// Sets the priority of the message. Valid values are "normal" and "high." On iOS, these correspond to APNs priorities 5 and 10.
        /// <para/>By default, messages are sent with normal priority.Normal priority optimizes the client app's battery consumption and should be used unless immediate delivery is required. 
        /// <para/>For messages with normal priority, the app may receive the message with unspecified delay.
        /// <para/>When a message is sent with high priority, it is sent immediately, and the app can wake a sleeping device and open a network connection to your server.
        /// </summary>
        [JsonProperty("collapse_key")]
        public string CollapseKey { get; set; }

        /// <summary>
        /// Sets the priority of the message. Valid values are "normal" and "high." On iOS, these correspond to APNs priorities 5 and 10.
        /// <para/>By default, messages are sent with normal priority.Normal priority optimizes the client app's battery consumption and should be used unless immediate delivery is required. For messages with normal priority, the app may receive the message with unspecified delay.
        /// <para/>When a message is sent with high priority, it is sent immediately, and the app can wake a sleeping device and open a network connection to your server.
        /// </summary>
        [JsonProperty("priority")]
        public Priority Priority { get; set; }

        /// <summary>
        /// On iOS, use this field to represent content-available in the APNs payload. When a notification or message is sent and this is set to true, an inactive client app is awoken. On Android, data messages wake the app by default. On Chrome, currently not supported.
        /// </summary>
        [JsonProperty("content_available")]
        public bool? ContentAvailable { get; set; }

        /// <summary>
        /// This parameter specifies how long (in seconds) the message should be kept in FCM storage if the device is offline. The maximum time to live supported is 4 weeks, and the default value is 4 weeks. 
        /// </summary>
        [JsonProperty("time_to_live")]
        public byte? TimeToLive { get; set; }

        /// <summary>
        /// This parameter specifies the package name of the application where the registration tokens must match in order to receive the message.
        /// </summary>
        [JsonProperty("restricted_package_name")]
        public string RestrictedPackageName { get; set; }

        /// <summary>
        /// This parameter, when set to true, allows developers to test a request without actually sending a message.
        /// <para/>The default value is false.
        /// </summary>
        [JsonProperty("dry_run")]
        public bool? DryRun { get; set; }

        /// <summary>
        /// This parameter specifies the custom key-value pairs of the message's payload.
        /// <para/>For example, with data:{"score":"3x1"}:
        /// <para/>On iOS, if the message is sent via APNS, it represents the custom data fields.If it is sent via FCM connection server, it would be represented as key value dictionary in AppDelegate application:didReceiveRemoteNotification:.
        /// <para/>On Android, this would result in an intent extra named score with the string value 3x1.
        /// <para/>The key should not be a reserved word ("from" or any word starting with "google" or "gcm"). Do not use any of the words defined in this table(such as collapse_key).
        /// <para/>Values in string types are recommended.You have to convert values in objects or other non-string data types (e.g., integers or booleans) to string.
        /// </summary>
        [JsonProperty("data")]
        public object Data { get; set; }

        /// <summary>
        /// This parameter specifies the predefined, user-visible key-value pairs of the notification payload. See Notification payload support for detail. For more information about notification message and data message options, see Payload.
        /// </summary>
        [JsonProperty("notification")]
        public Notification Notification { get; set; }
    }
    /// <summary>
    /// Send messages from your app server to client apps via Firebase Cloud Messaging
    /// </summary>
    public class Sender : IDisposable
    {
        private readonly string _endpoint = "https://fcm.googleapis.com/fcm/send";
        private readonly string _contentType = "application/json";

        private static HttpClient _client = new HttpClient();

        /// <summary>
        /// Initialize the Message Sender
        /// </summary>
        /// <param name="serverKey">Server Key. To access this information, go to https://console.firebase.google.com/project/<<MY_PROJECT>>/settings/cloudmessaging </param>
        public Sender(string serverKey)
        {
            if (string.IsNullOrWhiteSpace(serverKey))
                throw new ArgumentNullException(nameof(serverKey));

            _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={serverKey}");
        }

        /// <summary>
        /// Dispose the HttpClient
        /// </summary>
        public void Dispose() => _client.Dispose();

        /// <summary>
        /// Send a message Async
        /// </summary>
        /// <param name="json">Json Message</param>
        /// <returns>Response Content</returns>
        public async Task<ResponseContent> SendAsync(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentNullException(nameof(json));

            var requestContent = this.GetRequestContent(json);
            return await this.SendAsync(requestContent);
        }

        /// <summary>
        /// Send a message Async
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Response Content</returns>
        public async Task<ResponseContent> SendAsync(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var requestContent = this.GetRequestContent(message);
            return await this.SendAsync(requestContent);
        }

        private async Task<ResponseContent> SendAsync(HttpContent content)
        {
            var response = await _client.PostAsync(this._endpoint, content);

            var responseContent = await GetResponseContentAsync(response); ;
            var result = new ResponseContent(response.StatusCode, response.ReasonPhrase, responseContent);

            return result;
        }

        private HttpContent GetRequestContent(string json)
        {
            var content = new StringContent(json, Encoding.UTF8, this._contentType);
            return content;
        }

        private HttpContent GetRequestContent(Message message)
        {
            string json = JsonConvert.SerializeObject(message, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                            });

            return this.GetRequestContent(json);
        }

        private async Task<MessageResponse> GetResponseContentAsync(HttpResponseMessage response)
        {
            string json = string.Empty;
            try
            {
                json = await response.Content.ReadAsStringAsync();
                var messageResponse = JsonConvert.DeserializeObject<MessageResponse>(json);
                return messageResponse;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new MessageResponse { InternalError = ex.Message, ResponseContent = json };
            }
        }
    }
}