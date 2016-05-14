namespace Net51Zhang.Common.EmailWrapper
{
    public interface IQueuedEmailService
    {
        void InsertEmail(QueuedEmail email);
        QueuedEmail DequeueEmail();
    }
}
