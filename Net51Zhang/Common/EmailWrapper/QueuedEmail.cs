namespace Net51Zhang.Common.EmailWrapper
{
    using System;
    using System.Collections.Generic;

    public class QueuedEmail
    {
        /// <summary>
        /// From (Email Address)
        /// </summary>
        public string From { get; set; }

        public string FromName { get; set; }

        /// <summary>
        /// To (Email Address)
        /// </summary>
        public string To { get; set; }

        public string ToName { get; set; }

        public string ReplyTo { get; set; }

        public string ReplyToName { get; set; }

        public string CC { get; set; }

        public string Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        /// <summary>
        /// Item1 => AttachmentFilePath, Item2 => AttachmentFileName 
        /// </summary>
        public List<Tuple<string, string>> AttachmentFiles { get; set; }

        public DateTime? CreateDateTimeUtc { get; set; }

        public int SentTries { get; set; }
    }
}