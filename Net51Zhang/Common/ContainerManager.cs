using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using iBoxDB.LocalServer;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using Net51Zhang.DBRepository;
using Net51Zhang.Models.DataModel;
using Net51Zhang.Service;
using Net51Zhang.Common.Cache;
using Net51Zhang.Common.Encryption;
using Net51Zhang.Common.TaskWrapper;
using Net51Zhang.Models.Diary.EntityModel;
using Net51Zhang.Models.Manager;
using Net51Zhang.Models.Movie.EntityModel;

namespace Net51Zhang.Common
{
    public static class ContainerManager
    {
        private static readonly UnityContainer _container = new UnityContainer();

        public static void RegisterComponent()
        {
            _container.RegisterInstance<IDbContext>(new NetDBContext(Constant.DataConnectStringName), new ContainerControlledLifetimeManager());
            
            _container.RegisterType<IRepository<LogEntity>, EfRepository<LogEntity>>();
            _container.RegisterType<ILogService, SqlLogService>();

            _container.RegisterType<IRepository<AccountEntity>, EfRepository<AccountEntity>>();
            _container.RegisterType<IAccountService, SqlAccountService>();

            _container.RegisterType<IRepository<DiaryEntity>, EfRepository<DiaryEntity>>();
            _container.RegisterType<IDiaryService, SqlDiaryService>();

            _container.RegisterType<IRepository<DiaryCommentEntity>, EfRepository<DiaryCommentEntity>>();
            _container.RegisterType<IDiaryCommentService, SqlDiaryCommentService>();

            _container.RegisterType<IRepository<MovieCommentEntity>, EfRepository<MovieCommentEntity>>();
            _container.RegisterType<IMovieCommentService, SqlMovieCommentService>();

            _container.RegisterInstance<IEncryptionProvider>(new EOREncryptionProvider("net51zhang_vector"), new ContainerControlledLifetimeManager());
            _container.RegisterInstance<ICache>(new RuntimeCache(), new ContainerControlledLifetimeManager());
           // _container.RegisterInstance<DB.AutoBox>(InitAndCreateAutoBox(), new ContainerControlledLifetimeManager());

            _container.RegisterType<IServiceManager, DefaultServiceManager>();

            _container.RegisterInstance<IScheduleTaskService>(new DefaultScheduleTaskService(new List<ScheduleTask>()
            {
                new ScheduleTask(){Id = Guid.NewGuid().ToString(), Type = typeof(KeepAliveTask), Enabled = true, Seconds = 1800}
            }), new ContainerControlledLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(_container));
        }

        public static object GetUnregisterService(this IDependencyResolver resolver, Type type)
        {
            var constructors = type.GetConstructors();
            foreach (ConstructorInfo constructorInfo in constructors)
            {
                var parameters = constructorInfo.GetParameters();
                var parameterInstances = new List<object>(parameters.Length);
                foreach (ParameterInfo parameterInfo in parameters)
                {
                    var paramService = resolver.GetService(parameterInfo.ParameterType);
                    if (paramService == null)
                        throw new ArgumentException(String.Format("Unknow dependence for parameter type:[{0}]", parameterInfo.ParameterType));

                    parameterInstances.Add(paramService);
                }

                return Activator.CreateInstance(type, parameterInstances.ToArray());
            }

            throw new ArgumentException(string.Format("No constructor was aviliable for type:[{0}]", type));
        }

        public static DB.AutoBox InitAndCreateAutoBox()
        {
            //var dbPath = Path.Combine(Utils.AppFolder(), "App_Data", "ibox");
            //if (!Directory.Exists(dbPath))
            //{
            //    Directory.CreateDirectory(dbPath);
            //}

            //var server = new DB(dbPath);
            //var config = server.GetConfig();
            //config.EnsureTable<MovieComment>(Constant.iBoxDBTable.MovieComment, "Id");
            //config.EnsureTable<Account>(Constant.iBoxDBTable.AccountTable, "Name");

            //return server.Open();

            return null;
        }
    }
}