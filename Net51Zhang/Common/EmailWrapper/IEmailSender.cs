namespace Net51Zhang.Common.EmailWrapper
{
    public interface IEmailSender
    {
        void SendEmail(QueuedEmail email);
    }
}