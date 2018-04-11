using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWP.API.Models
{
    #region Conversion Status
    public enum ConversionStatus
    {
        /// <summary>
        /// Conversion is being transcoded.
        /// </summary>
        Queued,
        /// <summary>
        /// Conversion is ready and can be streamed.
        /// </summary>
        Ready,
        /// <summary>
        /// Failed to encode or upload the conversion.
        /// </summary>
        Failed
    }
    #endregion

    #region Video Status
    public enum VideoStatus
    {
        /// <summary>
        /// Video is created. Waiting for the original file upload being complete.
        /// </summary>
        Created,
        /// <summary>
        /// Original file or default conversion is being processed.
        /// </summary>
        Processing,
        /// <summary>
        /// Video ready for streaming (original and default conversion are ready).
        /// </summary>
        Ready,
        /// <summary>
        /// New (re-uploaded) original file is being processed. Previous original file and default conversion are ready for streaming.
        /// </summary>
        Updating,

        /// <summary>
        /// Processing of the original file or default conversion has failed.
        /// </summary>
        Failed,

    }
    #endregion

    #region Video Show Status
    /// <summary>
    /// video/show api call video status
    /// </summary>
    public enum VideoShowStatus
    {

        /// <summary>
        /// Video is created. Waiting for the original file upload being complete.
        /// </summary>
        Created,

        /// <summary>
        /// Original file or default conversion is being processed.
        /// </summary>
        Processing,
        /// <summary>
        /// Video ready for streaming (original and default conversion are ready).
        /// </summary>
        Ready,

        /// <summary>
        /// New (re-uploaded) original file is being processed. Previous original file and default conversion are ready for streaming.
        /// </summary>
        Updating,

        /// <summary>
        /// Processing of the original file or default conversion has failed.
        /// </summary>
        Failed

    }
    #endregion

    #region Media Types
    public enum MediaType
    {
        /// <summary>
        /// Unknownn media type
        /// </summary>
        Unknown,
        /// <summary>
        /// media type is audio
        /// </summary>
        Audio,
        /// <summary>
        /// Media Type is Video
        /// </summary>
        Video
    }
    #endregion

    #region Call Execution Status
    public enum CallExecutionStatus
    {
        /// <summary>
        /// call executed succesfully
        /// </summary>
        OK,
        /// <summary>
        /// api call failed
        /// </summary>
        Error
    }
    #endregion

    #region Format Keys
    /// <summary>
    ///  Key of the video format used for its template:
    /// </summary>
    public enum FormatKey
    {

        /// <summary>
        /// H.264 video and AAC audio in MP4 container.
        /// </summary>
        mp4,

        /// <summary>
        /// AAC audio in MP4 container.
        /// </summary>
        aac,

        /// <summary>
        /// MP3 audio.
        /// </summary>
        mp3,

        /// <summary>
        /// Passthrough format.
        /// </summary>
        passthrough,

        /// <summary>
        /// Original unmodified video or audio.
        /// </summary>
        original,

    }
    #endregion


    #region Thumbnail Status
    /// <summary>
    /// Thumbnail creation status
    /// </summary>
    public enum ThumbnailStatus
    {

        /// <summary>
        /// Thumbnail images not build.
        /// </summary>
        Not_Build,

        /// <summary>
        /// Creating thumbnail images.
        /// </summary>
        Creating,

        /// <summary>
        /// All thumbnail images are created.
        /// </summary>
        Ready,

        /// <summary>
        /// Failed to create thumbnail images.
        /// </summary>
        Failed

    }
    #endregion
}
