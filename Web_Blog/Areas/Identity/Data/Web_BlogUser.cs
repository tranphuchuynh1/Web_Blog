using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Web_Blog.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Web_BlogUser class
public class Web_BlogUser : IdentityUser
{
    // Mã hóa dữ liệu cá nhân khi lưu trữ hơạc khi truyền dữ liệu về database ( cái này có hay k cũng đc )
    // [PersonalData]
    // Kiểu dữ liệu nvarchar chỉ cho tối đa 200 ký tự
    [Column(TypeName = "nvarchar(200)")]
    // Trường Tên
    public string FirstName { get; set; }
    // Trường Họ
    // [PersonalData]
    [Column(TypeName = "nvarchar(200)")]
    public string LastName { get; set; }

}

