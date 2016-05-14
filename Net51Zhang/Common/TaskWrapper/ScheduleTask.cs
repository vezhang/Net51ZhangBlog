namespace Net51Zhang.Common.TaskWrapper
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("Name={Name}, Seconds={Seconds}")]
    public class ScheduleTask
    {
        /// <summary>
        /// Task unique Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Task Name
        /// </summary>
        private string _name;
        public string Name
        {
            get { return string.IsNullOrEmpty(this._name) ? this.Type.Name : this._name; }
            set { this._name = value; }
        }

        /// <summary>
        /// Run period in seconds
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// type of appropriate ITask class
        /// </summary>
        public Type Type { get; set; }

        public bool Enabled { get; set; }

        /// <summary>
        /// bool value indicate whether the task will stop on some error
        /// </summary>
        public bool StopOnError { get; set; }

        public override string ToString()
        {
            return string.Format("Id={0},Name={1},Seconds={2},Type={3}, Enabled={4}", this.Id, this.Name, this.Seconds,
                this.Type.FullName, this.Enabled);
        }
    }
}