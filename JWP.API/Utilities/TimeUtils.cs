using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWP.API.Utilities
{
    public class TimeUtils
    {

        #region Get Unix Time
        /// <summary>
        /// Get current timestamp in Unix format
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUnixTimeStamp()
        {
            return ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds).ToString();
        }
        #endregion

        #region Convert Unix TimeStamp To DateTime
        /// <summary>
        /// convert unix timestampt to datetime
        /// </summary>
        /// <param name="unixTimeStamp">unix timetamp</param>
        /// <returns>datetime conversion</returns>
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        #endregion
    }

    
}
