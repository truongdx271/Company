namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Role_Feature
    {
        public int Id { get; set; }

        public int? RoleId { get; set; }

        public int? FeatureId { get; set; }

        public bool? IsTrue { get; set; }

        public bool? Status { get; set; }
    }
}
