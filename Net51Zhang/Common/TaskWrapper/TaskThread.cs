namespace Net51Zhang.Common.TaskWrapper
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class TaskThread : IDisposable
    {
        private Timer _timer;
        private bool _disposed;
        private readonly Dictionary<string, WrappedTask> _tasks;

        internal TaskThread()
        {
            this._tasks = new Dictionary<string, WrappedTask>();
            this.Seconds = 60;
        }

        public void InitTimer()
        {
            this._timer = new Timer(new TimerCallback(this.TimeCallTackCore), null, this.Interval, this.Interval);
        }

        public void AddTask(WrappedTask task)
        {
            this._tasks.Add(task.ScheduleTask.Id, task);
        }

        public void Dispose()
        {
            if (!this._disposed && this._timer != null)
            {
                lock (this)
                {
                    this._timer.Dispose();
                    this._timer = null;
                    this._disposed = true;
                }
            }
        }

        private void TimeCallTackCore(object state)
        {
            this._timer.Change(-1, -1);
            this.Run();
            if (this.RunOnlyOnce)
            {
                this.Dispose();
            }
            else
            {
                this._timer.Change(this.Interval, this.Interval);
            }
        }

        private void Run()
        {
            if (this.Seconds <= 0)
                return;

            this.StartUtc = DateTime.UtcNow;
            this.IsRunning = true;

            foreach (var wrapperedTask in _tasks.Values)
            {
                wrapperedTask.Execute();
            }

            this.IsRunning = false;
        }

        public bool RunOnlyOnce { get; set; }

        public int Seconds { get; set; }

        public int Interval
        {
            get { return this.Seconds*1000; }
        }

        public bool IsRunning { get; private set; }

        public DateTime? StartUtc { get; private set; }
    }
}