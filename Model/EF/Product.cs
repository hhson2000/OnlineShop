namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int? Category_Id { get; set; }

        public double? Price { get; set; }

        [StringLength(1200)]
        public string Description { get; set; }

        public int? Quantity { get; set; }

        public int? Status { get; set; }

        [StringLength(200)]
        public string Image_Link { get; set; }

        [StringLength(1000)]
        public string Note { get; set; }
    }
}
