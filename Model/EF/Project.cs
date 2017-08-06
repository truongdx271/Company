namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Project")]
    public partial class Project
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImage { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool? IsFeature { get; set; }

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
