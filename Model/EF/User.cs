namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long Id { get; set; }

        [StringLength(50)]
        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [StringLength(100)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [StringLength(100)]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }

        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Ngày sinh")]
        public DateTime? Birthday { get; set; }

        [StringLength(250)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [StringLength(100)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [StringLength(250)]
        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; }

        public int? Role { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ModifiedDate { get; set; }

        public long? ModifiedBy { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
