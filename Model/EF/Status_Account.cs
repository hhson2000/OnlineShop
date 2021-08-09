namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Status_Account
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Status { get; set; }
    }
}
