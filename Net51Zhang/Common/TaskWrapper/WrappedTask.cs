using Net51Zhang.Models.DataModel;
using Net51Zhang.Service;

namespace Net51Zhang.Common.TaskWrapper
{
    using System;
    using System.Web.Mvc;

    public class WrappedTask
    {
        private ScheduleTask _scheduleTask;
        public ScheduleTask ScheduleTask
        {
            get { return this._scheduleTask; }
        }

        private ILogService _logger;
        private ITask _task;

        private WrappedTask()
        {
        }

        public WrappedTask(ScheduleTask task)
        {
            this._scheduleTask = task;
            this.Enabled = task.Enabled;

            this._logger = DependencyResolver.Current.GetService<ILogService>();
            this._task = DependencyResolver.Current.GetUnregisterService(this._scheduleTask.Type) as ITask;
        }

        public void Execute()
        {
            try
            {
                if (this.Enabled && this._task != null)
                {
                    this.LastStartUtc = DateTime.UtcNow;
                    this._task.Execute();
                    this.LastEndUtc = this.LastSuccessUtc = DateTime.UtcNow;
                }
            }
            catch (Exception exception)
            {
                this.Enabled = !this._scheduleTask.StopOnError;
                this.LastEndUtc = DateTime.UtcNow;

                this._logger.Insert(new LogEntity()
                {
                    CreateTime = DateTime.Now,
                    Message = string.Format("Task:{0}, Exception:{1}", this._scheduleTask, exception)
                });
            }
        }

        public bool Enabled { get; private set; }

        public DateTime? LastStartUtc { get; private set; }

        public DateTime? LastEndUtc { get; private set; }

        public DateTime? LastSuccessUtc { get; private set; }
    }
}