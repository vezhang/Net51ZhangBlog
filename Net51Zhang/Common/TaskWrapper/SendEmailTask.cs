using Net51Zhang.Common.EmailWrapper;
using Net51Zhang.Models.DataModel;
using Net51Zhang.Service;

namespace Net51Zhang.Common.TaskWrapper
{
    using System;

    public class SendEmailTask : ITask
    {
        private ILogService _logger;
        private IEmailSender _emailSender;
        private IQueuedEmailService _emailService;

        public SendEmailTask(ILogService logger, IEmailSender emailSender, IQueuedEmailService emailService)
        {
            this._logger = logger;
            this._emailSender = emailSender;
            this._emailService = emailService;
        }

        public virtual void Execute()
        {
            QueuedEmail targetEmail = null;
            try
            {
                targetEmail = this._emailService.DequeueEmail();
                if (targetEmail != null)
                {
                    this._emailSender.SendEmail(targetEmail);
                }
            }
            catch (Exception exception)
            {
                this._logger.Insert(new LogEntity()
                {
                    CreateTime = DateTime.Now,
                    Message = string.Format("SendEmailTask Failed with: {0}", exception)
                });

                if (targetEmail != null)
                {
                    targetEmail.SentTries ++;
                    this._emailService.InsertEmail(targetEmail);
                }

                throw;
            }
        }
    }
}