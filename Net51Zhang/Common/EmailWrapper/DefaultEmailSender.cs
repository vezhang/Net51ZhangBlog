namespace Net51Zhang.Common.EmailWrapper
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;

    public class DefaultEmailSender : IEmailSender
    {
        public virtual void SendEmail(QueuedEmail email)
        {
            var message = new MailMessage();
            message.From = new MailAddress(email.From, email.FromName);
            message.To.Add(new MailAddress(email.To, email.ToName));

            if (!string.IsNullOrEmpty(email.ReplyTo))
            {
                message.ReplyToList.Add(new MailAddress(email.ReplyTo, email.ReplyToName));
            }

            if (!string.IsNullOrEmpty(email.Bcc))
            {
                foreach (var address in email.Bcc.Split(new string[] {";"}, StringSplitOptions.RemoveEmptyEntries))
                {
                    message.Bcc.Add(address.Trim());
                }
            }

            if (!string.IsNullOrEmpty(email.CC))
            {
                foreach (var address in email.CC.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    message.CC.Add(address.Trim());
                }
            }

            message.Body = email.Body;
            message.IsBodyHtml = true;
            message.Subject = email.Subject;

            if (email.AttachmentFiles != null && email.AttachmentFiles.Any())
            {
                foreach (Tuple<string, string> tuple in email.AttachmentFiles)
                {
                    if (File.Exists(tuple.Item1))
                    {
                        var attachment = new Attachment(tuple.Item1);
                        attachment.ContentDisposition.CreationDate = File.GetCreationTime(tuple.Item1);
                        attachment.ContentDisposition.ModificationDate = File.GetLastWriteTime(tuple.Item1);
                        attachment.ContentDisposition.ReadDate = File.GetLastAccessTime(tuple.Item1);
                        if (!string.IsNullOrEmpty(tuple.Item2))
                        {
                            attachment.Name = tuple.Item2;
                        }

                        message.Attachments.Add(attachment);
                    }
                }
            }

           /* using (var smtpClient = new SmtpClient(SatoriPortal.Common.Constants.MicrosoftSmptHost))
            {
                smtpClient.UseDefaultCredentials = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                smtpClient.Send(message);
            }*/
        }
    }
}