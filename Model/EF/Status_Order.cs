namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Status_Order
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Status { get; set; }
    }
}
