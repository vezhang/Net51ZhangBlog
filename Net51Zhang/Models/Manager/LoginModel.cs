using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Net51Zhang.Models.Manager
{
    public class LoginModel
    {
        [Required(ErrorMessage = "必填")]
        [Display(Name = "用户名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "必填")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}