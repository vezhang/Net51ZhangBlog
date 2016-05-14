using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Net51Zhang.Common
{
    public static class Constant
    {
        public const string OxfordFaceKey = "b8b1f2e8439747c0b9310e7cec7340fb";

        public const string DataConnectStringName = "ApplicationServices";

        public const string MovieInTheatersApi = "https://api.douban.com/v2/movie/in_theaters";

        public const string MovieComingSoonApi = "https://api.douban.com/v2/movie/coming_soon";

        public const string MovieDetailApi = "https://api.douban.com/v2/movie/subject/{0}";

        public const string MovieSearchApi = "https://api.douban.com/v2/movie/search?q={0}";

        public const string MovieTagApi = "https://api.douban.com/v2/movie/search?tag={0}&count={1}";

        public const string MovieUSBoxApi = "https://api.douban.com/v2/movie/us_box";

        private const string AccountCookieUserKey = "net51zhang_username";

        private const string AccountCookiePsdKey = "userpassword_net51zhang";

        public static readonly string AccountCookieUserKeyHashed = Utils.GetMd5Hash(AccountCookieUserKey);

        public static readonly string AccountCookiePsdKeyHashed = Utils.GetMd5Hash(AccountCookiePsdKey);
    }
}