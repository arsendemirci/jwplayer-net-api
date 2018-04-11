using System;
using System.Collections.Generic;
using JWP.API.Utilities;
using System.Linq;
using System.Text;

namespace JWP.API.Models
{
    #region JwVideo
    [Serializable]
    public class JwVideo
    {
        /// <summary>
        /// Combined size of all conversions created for this video in bytes.
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// Duration of the video in seconds
        /// </summary>
        public float Duration { get; set; }
        /// <summary>
        /// Title of the video
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Video status: created, processing, ready, updating, failed
        /// </summary>
        public VideoStatus Status { get; set; }

        /// <summary>
        /// Video Key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Video update date. Specifies UTC date and time (in Unix timestamp format) when the video was updated for the last time.
        /// </summary>
        public long? Updated { get; set; }
        /// <summary>
        /// Video Update Date in datetime format when the video was updated for the last time.
        /// </summary>
        public DateTime? UpdatedDate
        {
            get
            {
                if (Updated.HasValue)
                {
                    return TimeUtils.UnixTimeStampToDateTime(Updated.Value);
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// Video publish expiration date. Specifies UTC date and time (in Unix timestamp format) when the video will stop being available for streaming.
        /// </summary>
        public long? Expires_Date { get; set; }
        /// <summary>
        /// Video publish expiration date. when the video will stop being available for streaming.
        /// </summary>
        public DateTime? ExpireDate
        {
            get
            {
                if (Expires_Date.HasValue)
                {
                    return TimeUtils.UnixTimeStampToDateTime(Expires_Date.Value);
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// Description of the video.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Video source format: mp4, webm, flv, aac, mp3, vorbis, m3u8,smil
        /// </summary>
        public string SourceFormat { get; set; }
        /// <summary>
        /// Tags of the video
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// Content media type: unknown,audio,video
        /// </summary>
        public MediaType MediaType { get; set; }
        /// <summary>
        /// Resumable upload session ID.
        /// </summary>
        public string Upload_Session_ID { get; set; }
        public string SourceUrl { get; set; }
        /// <summary>
        /// The URL of the web page where this video is published.
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Author of the video
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Not empty if processing of the initial or re-uploaded original file or their default conversion has failed.
        /// </summary>
        public ResponseError Error { get; set; }
        /// <summary>
        /// Video publish date. Specifies UTC date and time (in Unix timestamp format) when the video will be available for streaming.
        /// </summary>
        public long? Date { get; set; }

        /// <summary>
        /// Video publish expiration date. when the video will stop being available for streaming.
        /// </summary>
        public DateTime? PublishDate
        {
            get
            {
                if (Date.HasValue)
                {
                    return TimeUtils.UnixTimeStampToDateTime(Date.Value);
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// MD5 hash of the original file.
        /// </summary>
        public string MD5 { get; set; }
        public string SourceType { get; set; }
        public VideoConversionListResponse VideoDetails { get; set; }

    }
    #endregion

    #region JwVideo Details
    [Serializable]
    public class VideoConversionListResponse
    {
        /// <summary>
        /// list of video conversions
        /// </summary>
        public List<VideoConversion> Conversions { get; set; }
        /// <summary>
        /// Total number of conversions this video has.
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// Call execution status. Set to ok if call executed successfully.
        /// </summary>
        public CallExecutionStatus Status { get; set; }
        /// <summary>
        /// Maximum number of video conversions that should be return. As specified in the result_limit request parameter.
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// Number of video conversions that should be skipped at the beginning of the result set. As specified in the result_offset request parameter.
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// generated link of the largest playable video source
        /// </summary>
        public string VideoLink { get; set; }
        /// <summary>
        /// generated video Thumbnail link
        /// </summary>
        public string ImageLink { get; set; }
        /// <summary>
        /// Height of the video
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Width Of The video
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Error message is call execution fails
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    #endregion

    #region Video Conversions
    [Serializable]
    public class VideoConversion
    {
        public string Key { get; set; }
        /// <summary>
        /// Queued -> Conversion is being transcoded.
        /// Ready -> Conversion is ready and can be streamed.
        /// Failed -> Failed to encode or upload the conversion.
        /// </summary>
        public ConversionStatus Status { get; set; }
        /// <summary>
        /// audio,video
        /// </summary>
        public MediaType MediaType { get; set; }
        /// <summary>
        /// Conversion height. Set to -1 if conversion height in unknown
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Conversion width. Set to -1 if conversion width in unknown.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Size of the conversion in bytes. Set to -1 if conversion file size in unknown.
        /// </summary>
        public long FileSize { get; set; }
        /// <summary>
        /// Duration of the conversion in seconds. Set to -1 if conversion duration in unknown.
        /// </summary>
        public float Duration { get; set; }
        /// <summary>
        /// Conversion error. Included in response only when conversion status is Failed.
        /// </summary>
        public ResponseError Error { get; set; }
        public ConversionTemplate Template { get; set; }
        /// <summary>
        /// Conversion download link split into 3 parts: protocol, address and path. Included in response only when conversion status is Ready.
        /// </summary>
        public ConversionLink Link { get; set; }



    }

    #endregion

    #region Video Info
    /// <summary>
    /// Video Show response model
    /// </summary>
    public class JwVideoInfo
    {
        /// <summary>
        /// video key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Author of the video
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// Publish date unix timestamp
        /// </summary>
        public long Date { get; set; }
        /// <summary>
        /// Publish date
        /// </summary>
        public DateTime CreatedDate { get { return TimeUtils.UnixTimeStampToDateTime(Date); } }
        /// <summary>
        /// Video description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Duration of the video
        /// </summary>
        public float Duration { get; set; }
        /// <summary>
        /// Video expire date unix timestamp
        /// </summary>
        public long? Expires_Date { get; set; }
        /// <summary>
        /// Video Expire Date
        /// </summary>
        public DateTime? ExpireDate
        {
            get
            {
                if (Expires_Date.HasValue)
                {
                    return TimeUtils.UnixTimeStampToDateTime(Expires_Date.Value);
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// Response error
        /// </summary>
        public ResponseError Error { get; set; }
        /// <summary>
        /// video host link
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// md5 hashed string
        /// </summary>
        public string MD5 { get; set; }
        /// <summary>
        /// media type
        /// </summary>
        public MediaType MediaType { get; set; }
        /// <summary>
        /// file size in bytes
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// source format of the media
        /// </summary>
        public string SourceFormat { get; set; }
        /// <summary>
        /// source type, url or file
        /// </summary>
        public string SourceType { get; set; }
        /// <summary>
        /// source url, set if source type is url
        /// </summary>
        public string SourceUrl { get; set; }
        /// <summary>
        /// video status
        /// </summary>
        public VideoShowStatus Status { get; set; }
        /// <summary>
        /// tags of the video
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// title of the video
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Video update date unic timestamp
        /// </summary>
        public long? Updated { get; set; }
        /// <summary>
        /// Video Updated date
        /// </summary>
        public DateTime? UpdatedDate
        {
            get
            {
                if (Updated.HasValue)
                {
                    return TimeUtils.UnixTimeStampToDateTime(Updated.Value);
                }
                else
                    return null;
            }
        }
        /// <summary>
        /// upload session ID
        /// </summary>
        public string Upload_Session_ID { get; set; }
        /// <summary>
        /// video view count
        /// </summary>
        public int Views { get; set; }

        public string ErrorMessage { get; set; }
    }
    #endregion

    #region Video Info Response
    /// <summary>
    /// /videos/show api call response
    /// </summary>
    public class VideoInfoResponse
    {
        /// <summary>
        /// call execution status
        /// </summary>
        public CallExecutionStatus Status { get; set; }
        /// <summary>
        /// request limit class
        /// </summary>
        public RequestLimit Rate_Limit { get; set; }
        /// <summary>
        /// Video info
        /// </summary>
        public JwVideoInfo Video { get; set; }
        /// <summary>
        /// error message is set when status is not ok
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    #endregion

    #region Conversion Template
    [Serializable]
    public class ConversionTemplate
    {
        /// <summary>
        /// True if conversion template is a required template, False otherwise.
        /// </summary>
        public bool Required { get; set; }
        /// <summary>
        /// template format
        /// </summary>
        public TemplateFormat Format { get; set; }
        public int ID { get; set; }
        /// <summary>
        /// Key of the conversion template.
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Name of the conversion template.
        /// </summary>
        public string Name { get; set; }
    }


    #endregion

    #region Template Format
    [Serializable]
    public class TemplateFormat
    {
        /// <summary>
        /// Name of the video format used for this template.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Key of the video format used for this template:, mp4,aac,mp3,passthtough,original
        /// </summary>
        public FormatKey Key { get; set; }
    }
    #endregion

    #region Conversion Link
    [Serializable]
    public class ConversionLink
    {
        /// <summary>
        /// Path part of the conversion download link.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Protocol part of the conversion download link.
        /// </summary>
        public string Protocol { get; set; }
        /// <summary>
        /// Address part of the conversion download link.
        /// </summary>
        public string Address { get; set; }
    }
    #endregion

    #region Response Error
    [Serializable]
    public class ResponseError
    {
        /// <summary>
        /// Error Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Response error title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// error code
        /// </summary>
        public string Code { get; set; }
    }
    #endregion

    #region Upload Query
    internal class UploadQuery
    {
        /// <summary>
        /// Upload token query parameter.
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Upload key query parameter.
        /// </summary>
        public string Key { get; set; }
    }
    #endregion

    #region Video Thumbnail
    /// <summary>
    /// Video Thumnail Image
    /// </summary>
    [Serializable]
    public class VideoThumbnail
    {
        /// <summary>
        /// image key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// file status
        /// </summary>
        public ThumbnailStatus Status { get; set; }
        /// <summary>
        /// strip status
        /// </summary>
        public ThumbnailStatus Strip_Status { get; set; }
        /// <summary>
        /// Image Error
        /// </summary>
        public ResponseError Error { get; set; }
        /// <summary>
        /// strip error
        /// </summary>
        public ResponseError Strip_Error { get; set; }
    }
    #endregion

    #region Upload Link
    internal class UploadLink
    {
        /// <summary>
        /// Path part of the upload URL.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// upload query
        /// </summary>
        public UploadQuery Query { get; set; }
        /// <summary>
        /// Protocol part of the upload URL.
        /// </summary>
        public string Protocol { get; set; }
        /// <summary>
        /// Address part of the upload URL.
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Request Limit
        /// </summary>
        public RequestLimit Rate_Limit { get; set; }
    }
    #endregion

    #region Rate Limit
    public class RequestLimit
    {
        /// <summary>
        /// ?? maybe unix timestamp date limit will be reset
        /// </summary>
        public long Reset { get; set; }
        /// <summary>
        /// Reset Date
        /// </summary>
        public DateTime ResetDate
        {
            get
            {
                return TimeUtils.UnixTimeStampToDateTime(Reset);
            }
        }

        /// <summary>
        /// request limit
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// remaining limit
        /// </summary>
        public int Remaining { get; set; }
    }
    #endregion

    #region JWMedia
    public class JWMedia
    {
        /// <summary>
        /// Media type
        /// </summary>
        public MediaType Type { get; set; }
        /// <summary>
        /// media key
        /// </summary>
        public string Key { get; set; }
    }
    #endregion

    #region Upload Url
    /// <summary>
    /// Generated upload url
    /// </summary>
    internal class UploadUrl
    {
        /// <summary>
        /// Call execution status
        /// </summary>
        public CallExecutionStatus Status { get; set; }
        /// <summary>
        /// upload url media 
        /// </summary>
        public JWMedia Media { get; set; }
        /// <summary>
        /// upload link
        /// </summary>
        public UploadLink Link { get; set; }
        /// <summary>
        /// Upload limit
        /// </summary>
        public RequestLimit Rate_Limit { get; set; }
        /// <summary>
        /// error message if execution fails
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    #endregion

    #region Video Upload Response
    /// <summary>
    /// JW PLayer video upload response model
    /// </summary>
    public class VideoUploadResponse
    {
        /// <summary>
        /// Call execution status
        /// </summary>
        public CallExecutionStatus Status { get; set; }
        /// <summary>
        /// Uploaded media type and key
        /// </summary>
        public JWMedia Media { get; set; }
        /// <summary>
        /// uploaded file properties
        /// </summary>
        public JWFile File { get; set; }
        /// <summary>
        /// response redirect link
        /// </summary>
        public ResponseRedirect Redirect_Link { get; set; }
        /// <summary>
        /// api call result object
        /// </summary>
        public string ErrorMessage { get; set; }

    }
    #endregion

    #region Video List Response
    /// <summary>
    /// video list api call response model
    /// </summary>
    public class VideoListResponse
    {
        /// <summary>
        /// Call Execution Status
        /// </summary>
        public CallExecutionStatus Status { get; set; }
        /// <summary>
        /// list of videos
        /// </summary>
        public List<JwVideo> Videos { get; set; }
        /// <summary>
        /// Request limits
        /// </summary>
        public RequestLimit Rate_Limit { get; set; }
        /// <summary>
        /// video list count limit
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// Total video count in the list
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// error message if call execution status fails
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    #endregion

    #region JW File
    /// <summary>
    /// JWPlayer uploaded file model
    /// </summary>
    public class JWFile
    {
        /// <summary>
        /// md5 hash string
        /// </summary>
        public string MD5 { get; set; }
        /// <summary>
        /// File Size in bytes
        /// </summary>
        public long Size { get; set; }
    }
    #endregion

    #region Response Redirect
    /// <summary>
    /// Response redirect model
    /// </summary>
    public class ResponseRedirect
    {
        /// <summary>
        /// redirect address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// redirect query parameters
        /// </summary>
        public RedirectQuery Query { get; set; }

    }
    #endregion

    #region Redirect Query
    /// <summary>
    /// Redirect query parameters
    /// </summary>
    public class RedirectQuery
    {
        /// <summary>
        /// query string parameter 1
        /// </summary>
        public string Parameter1 { get; set; }
        /// <summary>
        /// query string parameter 2
        /// </summary>
        public string Parameter2 { get; set; }

    }
    #endregion

    #region Result
    /// <summary>
    /// api call result object
    /// </summary>
    public class ApiCallResult
    {
        /// <summary>
        /// Call result status OK, or Error
        /// </summary>
        public CallExecutionStatus Status { get; set; }
        /// <summary>
        /// iff status is Error, error message is set
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    #endregion
}
