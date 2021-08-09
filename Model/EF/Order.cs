namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public int Id { get; set; }

        public int? Account_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Create_Date { get; set; }

        public decimal? Total_Price { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }

        public int? Status { get; set; }
    }
}
