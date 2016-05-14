using System;
using System.ComponentModel.DataAnnotations;
using Net51Zhang.Models.DataModel;

namespace Net51Zhang.Models.Manager
{
    public class AccountEntity : BaseEntity
    {
        public string Name { get; set; }   
        public string HashedPassword { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}