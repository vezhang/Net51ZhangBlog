using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Net51Zhang.Models.Manager
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "必填")]
        [Display(Name = "用户名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "密码必填")]
        [Display(Name = "密码")]
        [MinLength(4, ErrorMessage = "少于4个字符")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "确认密码必填")]
        [Display(Name = "确认密码")]
        [MinLength(4, ErrorMessage = "少于4个字符")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}