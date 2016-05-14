using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iBoxDB.LocalServer;
using Net51Zhang.Common.Cache;
using Net51Zhang.Common.Encryption;
using Net51Zhang.Service;

namespace Net51Zhang.Common
{
    public interface IServiceManager
    {
        ICache Cache { get; }

        DB.AutoBox AutoBox { get; }

        ILogService LogService { get; }

        IDiaryService DiaryService { get; }

        IDiaryCommentService DiaryCommentService { get; }

        IMovieCommentService MovieCommentService { get; }

        IEncryptionProvider EncryptionProvider { get; }

        IAccountService AccountService { get; }
    }
}