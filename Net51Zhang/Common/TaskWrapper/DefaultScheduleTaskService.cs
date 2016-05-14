namespace Net51Zhang.Common.TaskWrapper
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class DefaultScheduleTaskService : Collection<ScheduleTask>, IScheduleTaskService
    {
        public DefaultScheduleTaskService(IList<ScheduleTask> tasks) : base(tasks)
        {
            
        }

        public void DeleteTask(ScheduleTask task)
        {
            this.Remove(task);
        }

        public void InsertTask(ScheduleTask task)
        {
            this.Insert(0, task);
        }

        public ScheduleTask GetScheduleTaskById(string id)
        {
            return this.FirstOrDefault(t => t.Id == id);
        }

        public IList<ScheduleTask> GetAllScheduleTasks()
        {
            return this.Items;
        }
    }
}