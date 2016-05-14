namespace Net51Zhang.Common.TaskWrapper
{
    using System.Collections.Generic;

    internal interface IScheduleTaskService
    {
        void DeleteTask(ScheduleTask task);
        void InsertTask(ScheduleTask task);
        ScheduleTask GetScheduleTaskById(string id);
        IList<ScheduleTask> GetAllScheduleTasks();
    }
}