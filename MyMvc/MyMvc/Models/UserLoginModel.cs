using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;//这个命名空间值得研究一下
using System.Linq;
using System.Web;

namespace MyMvc.Models
{
    public class UserLoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名", Description = "4-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "×")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码", Description = "6-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "×")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Display(Name = "验证码", Description = "请输入图片中的验证码。")]
        [Required(ErrorMessage = "×")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "×")]
        public string VerificationCode { get; set; }
    }
}