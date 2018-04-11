using JWP.API.Extensions;
using JWP.API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using JWP.API.Utilities;


namespace JWP.API
{
    public class JWPlayer
    {
        #region Private Properties
        /// <summary>
        /// can be xml,py,php,json default = json
        /// </summary>
        private string APIFormat { get; set; }
        /// <summary>
        /// api base address
        /// </summary>
        private string _apiURL = "http://api.jwplatform.com/v1";
        /// <summary>
        /// api call required arguments
        /// </summary>
        private string _args = "";
        /// <summary>
        /// api call query strings
        /// </summary>
        private NameValueCollection _queryString = null;
        #endregion

        #region Public Propertires
        /// <summary>
        /// jw player api key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// jwplayer api secret
        /// </summary>
        public string Secret { get; set; }


        #endregion

        #region Constructor
        /// <summary>
        /// api constructor
        /// </summary>
        /// <param name="key">jw player account key</param>
        /// <param name="secret">jw player account secret</param>
        public JWPlayer(string key, string secret)
        {
            Key = key;
            Secret = secret;
            APIFormat = "json";
        }

        #endregion

        #region Private Members

        #region Api Call
        /// <summary>
        /// Call the API method with no params beyond the required
        /// </summary>
        /// <param name="apiCall">The path to the API method call (/videos/list)</param>
        /// <returns>The string response from the API call</returns>
        private string Call(string apiCall)
        {
            return Call(apiCall, null);
        }

        #endregion

        #region Api Call Overload 1

        /// <summary>
        /// Call the API method with additional, non-required params
        /// </summary>
        /// <param name="apiCall">The path to the API method call (/videos/list)</param>
        /// <param name="args">Additional, non-required arguments</param>
        /// <returns>The string response from the API call</returns>
        private string Call(string apiCall, NameValueCollection args)
        {

            _queryString = new NameValueCollection();

            //add the non-required args to the required args
            if (args != null)
            {
                foreach (string k in args.Keys)
                {
                    _queryString.Add(k, UrlEncodeToUpper(args.Get(k), Encoding.UTF8));
                }
            }
            BuildArgs();
            WebClient client = CreateWebClient();

            string callUrl = _apiURL + apiCall;

            try
            {
                //XmlDocument xdoc = GetDataFromUrl(callUrl);
                //string jsonResult = JsonConvert.SerializeXmlNode(xdoc);
                //return jsonResult;
                return client.DownloadString(callUrl);
            }
            catch (Exception ex)
            {
                return ex.Message + Environment.NewLine + ex.StackTrace;
            }
        }
        #endregion

        #region Upload 

        /// <summary>
        /// Upload a video to account with optional arguments
        /// </summary>
        /// <param name="uploadUrl">The url returned from /videos/create call</param>
        /// <param name="args">Optional args (video meta data)</param>
        /// <param name="filePath">Path to file to upload</param>
        /// <returns>The string response from the API call</returns>
        private string Upload(string uploadUrl, NameValueCollection args, string filePath)
        {
            _queryString = args; //no required args

            CustomWebClient client = new CustomWebClient();
            ServicePointManager.Expect100Continue = false; //upload will fail w/o 

            client.Timeout = 7200000;
            client.BaseAddress = _apiURL;
            client.QueryString = _queryString;
            client.Encoding = UTF8Encoding.UTF8;

            WebHeaderCollection whd = new WebHeaderCollection();
            whd.Add("Content-Disposition", string.Format("attachment; filename=\"{0}\"", Path.GetFileName(filePath)));

            _queryString["api_format"] = "json";
            ConvertQueryStringsToArgs();

            string callUrl = uploadUrl + "?" + _args;

            byte[] response = client.UploadFile(callUrl, filePath);
            return Encoding.UTF8.GetString(response);
        }
        #endregion

        #region Create Signature
        /// <summary>
        /// Hash the provided arguments
        /// </summary>
        private string CreateSignature()
        {

            ConvertQueryStringsToArgs();

            HashAlgorithm ha = HashAlgorithm.Create("SHA");
            byte[] hashed = ha.ComputeHash(Encoding.UTF8.GetBytes(_args + Secret));
            return BitConverter.ToString(hashed).Replace("-", "").ToLower();
        }
        #endregion

        #region Convert QueryStrings To Args
        /// <summary>
        /// Convert query string collection to ordered arg string
        /// </summary>
        private void ConvertQueryStringsToArgs()
        {

            Array.Sort(_queryString.AllKeys);
            StringBuilder sb = new StringBuilder();

            foreach (string key in _queryString.AllKeys)
            {
                sb.AppendFormat("{0}={1}&", key, _queryString[key]);
            }
            sb.Remove(sb.Length - 1, 1); //remove trailing &

            _args = sb.ToString();

        }
        #endregion

        #region Build Required Args
        /// <summary>
        /// Append required arguments to URL
        /// </summary>
        private void BuildArgs()
        {
            _queryString["api_format"] = APIFormat ?? "json";
            _queryString["api_key"] = Key;
            _queryString["api_kit"] = "dnet-1.0";
            _queryString["api_nonce"] = string.Format("{0:00000000}", new Random().Next(99999999));
            _queryString["api_timestamp"] = TimeUtils.GetCurrentUnixTimeStamp();
            _queryString["api_signature"] = CreateSignature();

            _args = string.Concat(_args, "&api_signature=", _queryString["api_signature"]);
        }
        #endregion

        #region Create WebClient
        /// <summary>
        /// Construct instance of WebClient for request
        /// </summary>
        /// <returns></returns>
        private WebClient CreateWebClient()
        {
            ServicePointManager.Expect100Continue = false; //upload will fail w/o 
            WebClient client = new WebClient();

            client.BaseAddress = _apiURL;
            client.QueryString = _queryString;
            client.Encoding = UTF8Encoding.UTF8;
            return client;
        }
        #endregion



        #region Url Encode To Upper
        /// <summary>
        /// Convert hex chars to uppercase as per OAuth-spec requirement
        /// </summary>
        /// <returns>string</returns>
        private string UrlEncodeToUpper(string data, Encoding enc)
        {
            data = Regex.Replace(HttpUtility.UrlEncode(data), "(%[0-9a-f][0-9a-f])", c => c.Value.ToUpper());
            // API expects spaces as %20
            data = data.Replace("+", "%20");
            // API expects tildes to be passed through directly
            data = data.Replace("%7E", "~");

            return data;
        }
        #endregion

        #endregion

        #region Public Members

        #region Create Video Upload Url
        /// <summary>
        /// create video upload url
        /// </summary>
        /// <returns>UploadUrl <media><key>video key</key></media></returns>
        private UploadUrl CreateVideoUploadUrl()
        {
            UploadUrl response = new UploadUrl();

            string responseJson = Call("/videos/create");

            response = JsonConvert.DeserializeObject<UploadUrl>(responseJson);

            return response;

        }
        #endregion

        #region Upload Video
        /// <summary>
        /// Upload Video api call
        /// </summary>
        /// <param name="url"> upload url created by CreateVideoUploadUrl() method</param>
        /// <param name="filePath"> physical file path of the video</param>
        /// <returns></returns>
        public VideoUploadResponse UploadVideo(string filePath)
        {
            VideoUploadResponse response = new VideoUploadResponse();
            response.Status = CallExecutionStatus.Error;
            try
            {
                //create upload url
                UploadUrl url = CreateVideoUploadUrl();
                //format upload url
                string upload_url = url.Link.Protocol + "://" + url.Link.Address + url.Link.Path;

                string uploadResponse = "";

                //create request query strings parameters
                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("key", url.Link.Query.Key);
                nvc.Add("token", url.Link.Query.Token);
                uploadResponse = Upload(upload_url, nvc, filePath);
                //deserialize response to VideoUploadResponse
                response = JsonConvert.DeserializeObject<VideoUploadResponse>(uploadResponse);
                //set api call result
                response.ErrorMessage = "";
                response.Status = CallExecutionStatus.OK;
            }
            catch (Exception ex)
            {
                response.Status = CallExecutionStatus.Error;
                response.ErrorMessage = ex.Message + Environment.NewLine + ex.StackTrace;
            }

            return response;
        }

        #endregion

        #region Get Video List
        /// <summary>
        /// Get Video list without video info details
        /// </summary>
        /// <returns></returns>
        public VideoListResponse GetVideoList()
        {
            return GetVideoList(false);
        }
        #endregion

        #region Get Video List With Conversions
        /// <summary>
        /// Get List Of Videos with video info details
        /// </summary>
        /// <param name="withConversions"></param>
        /// <returns></returns>
        public VideoListResponse GetVideoList(bool withConversions)
        {
            VideoListResponse response = new VideoListResponse();
            //List<JwVideo> list = new List<JwVideo>();

            try
            {
                string responseJson = Call("/videos/list");
                //JObject job = JObject.Parse(response);
                response = JsonConvert.DeserializeObject<VideoListResponse>(responseJson);

                if (withConversions && response.Videos != null && response.Videos.Count > 0)
                {
                    response.Videos.ForEach(x => x.VideoDetails = GetVideoConversionList(x.Key));
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Status = CallExecutionStatus.Error;
            }

            return response;
        }
        #endregion

        #region Get Video Details
        /// <summary>
        /// Get all video Details including conversions
        /// </summary>
        /// <param name="videoKey"></param>
        /// <returns></returns>
        public VideoConversionListResponse GetVideoConversionList(string videoKey)
        {
            VideoConversionListResponse response = new VideoConversionListResponse();
            try
            {
                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("video_key", videoKey);

                string responseJson = Call("/videos/conversions/list", nvc);

                response = JsonConvert.DeserializeObject<VideoConversionListResponse>(responseJson);


                List<VideoConversion> convList = response.Conversions.Where(c => c.Status == ConversionStatus.Ready
                                                    && c.MediaType == MediaType.Video
                                                    && c.Link != null
                                                    && c.Template != null
                                                    && c.Template.Format != null
                                                    && c.Template.Format.Key == FormatKey.mp4).OrderByDescending(c => c.FileSize).ToList();



                if (convList.Count > 0)
                {
                    VideoConversion source = convList[0];

                    response.VideoLink = source.Link.Protocol + "://" + source.Link.Address + source.Link.Path;
                    response.ImageLink = source.Link.Protocol + "://" + source.Link.Address + "/thumbs/" + videoKey + "-" + source.Width.ToString() + ".jpg";
                    response.Width = source.Width;
                    response.Height = source.Height;

                }

            }
            catch (Exception ex)
            {

                response.ErrorMessage = ex.Message + Environment.NewLine + ex.StackTrace;
                response.Status = CallExecutionStatus.Error;
            }

            return response;
        }
        #endregion

        #region Get Video Info
        /// <summary>
        /// Get Video Info 
        /// </summary>
        /// <param name="videoKey"> video key</param>
        /// <returns></returns>
        public VideoInfoResponse GetVideoInfo(string videoKey)
        {
            VideoInfoResponse vi = new VideoInfoResponse();
            try
            {
                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("video_key", videoKey);
                string response = Call("/videos/show", nvc);

                vi = JsonConvert.DeserializeObject<VideoInfoResponse>(response);
            }
            catch (Exception ex)
            {
                vi.Status = CallExecutionStatus.Error;
                vi.ErrorMessage = ex.Message + Environment.NewLine + ex.StackTrace;
            }

            return vi;
        }
        #endregion

        #endregion

    }
}
