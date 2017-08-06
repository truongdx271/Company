namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Article")]
    public partial class Article
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public int? MenuId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? ModifiedDate { get; set; }

        public long? ModifiedBy { get; set; }

        public bool Status { get; set; }

        public bool Deleted { get; set; }
    }
}
