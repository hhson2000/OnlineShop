using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBalo.Models
{
    public class RegisterModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Yêu cầu nhập email")]

        public string Email { set; get; }

        [Display(Name = "Mật khẩu")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự.")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu")]
        public string Password { set; get; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        public string ConfirmPassword { set; get; }

        [Display(Name = "Họ vào tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ và tên")]
        public string Name { get; set; }

        [Display(Name = "Điện thoại")]
        [Required]
        [StringLength(50)]
        public string Phone_Number { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { set; get; }

        [Display(Name = "Nam")]
        [Required]
        public bool Gender { get; set; }

    }
}