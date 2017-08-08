using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Company.Areas.Admin.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }

        [StringLength(250)]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Nội dung")]
        public string Content { get; set; }

        [Display(Name = "Loại bài viết")]
        public string menuName { get; set; }

        [Display(Name = "Người viết")]
        public string usrCreate { get; set; }

        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Ngày viết")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Người sửa")]
        public string usrEdit { get; set; }

        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Ngày sửa")]
        public DateTime? ModifiedDate { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}