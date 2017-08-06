namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Menu_Sub
    {
        public int Id { get; set; }

        public int? MenuId { get; set; }

        public int? SubMenuId { get; set; }

        public bool? Status { get; set; }
    }
}
