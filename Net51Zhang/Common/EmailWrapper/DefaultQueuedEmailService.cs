namespace Net51Zhang.Common.EmailWrapper
{
    using System.Collections.Concurrent;

    /// <summary>
    /// TODO vezhang need optimization
    /// </summary>
    public class DefaultQueuedEmailService:IQueuedEmailService
    {
        private ConcurrentQueue<QueuedEmail> _queuedEmails = new ConcurrentQueue<QueuedEmail>();

        public void InsertEmail(QueuedEmail email)
        {
            this._queuedEmails.Enqueue(email);
        }

        public QueuedEmail DequeueEmail()
        {
            QueuedEmail email = null;
            return this._queuedEmails.TryDequeue(out email) ? email : null;
        }
    }
}