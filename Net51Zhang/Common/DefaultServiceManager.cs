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
    public class DefaultServiceManager : IServiceManager
    {
        private ICache _cache;
        public ICache Cache
        {
            get { return this._cache; }
        }

        private DB.AutoBox _autoBox;
        public DB.AutoBox AutoBox
        {
            get { return this._autoBox; }
        }

        private ILogService _logService;
        public ILogService LogService
        {
            get { return this._logService; }
        }

        private IEncryptionProvider _encryptionProvider;
        public IEncryptionProvider EncryptionProvider
        {
            get { return this._encryptionProvider; }
        }

        private IDiaryService _diaryService;
        public IDiaryService DiaryService
        {
            get { return this._diaryService; }
        }

        private IDiaryCommentService _diaryCommentService;
        public IDiaryCommentService DiaryCommentService
        {
            get { return this._diaryCommentService; }
        }

        private IMovieCommentService _movieCommentService;
        public IMovieCommentService MovieCommentService
        {
            get { return this._movieCommentService; }
        }

        private IAccountService _accountService;

        public IAccountService AccountService
        {
            get { return this._accountService; }
        }

        public DefaultServiceManager(ICache cache, ILogService logService, 
            IEncryptionProvider encryptionProvider,
            IDiaryService diaryService, 
            IDiaryCommentService diaryCommentService,
            IMovieCommentService movieCommentService,
            IAccountService accountService)
        {
            this._cache = cache;
            this._autoBox = null;
            this._logService = logService;
            this._encryptionProvider = encryptionProvider;
            this._diaryService = diaryService;
            this._diaryCommentService = diaryCommentService;
            this._movieCommentService = movieCommentService;
            this._accountService = accountService;
        }
    }
}