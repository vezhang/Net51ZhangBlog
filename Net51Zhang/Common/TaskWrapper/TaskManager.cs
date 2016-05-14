namespace Net51Zhang.Common.TaskWrapper
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Web.Mvc;

    public class TaskManager
    {
        private static readonly TaskManager _taskManager = new TaskManager();
        private readonly List<TaskThread> _taskThreads = new List<TaskThread>(); 

        private TaskManager()
        {
        }

        public void Initialize()
        {
            this._taskThreads.Clear();

            var service = DependencyResolver.Current.GetService<IScheduleTaskService>();
            var tasks = service.GetAllScheduleTasks();

            foreach (var taskGroup in tasks.GroupBy(x => x.Seconds))
            {
                var taskThread = new TaskThread() {Seconds = taskGroup.Key};
                foreach (ScheduleTask scheduleTask in taskGroup)
                {
                    taskThread.AddTask(new WrappedTask(scheduleTask));
                }

                this._taskThreads.Add(taskThread);
            }
        }

        public void Start()
        {
            foreach (TaskThread taskThread in _taskThreads)
            {
                taskThread.InitTimer();
            }
        }

        public void Stop()
        {
            foreach (TaskThread taskThread in _taskThreads)
            {
                taskThread.Dispose();
            }
        }

        public static TaskManager Instance
        {
            get{ return _taskManager;}
        }

        public IList<TaskThread> TaskThreads
        {
            get { return new ReadOnlyCollection<TaskThread>(this._taskThreads); }
        } 
    }
}