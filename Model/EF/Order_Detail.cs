namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_Detail
    {
        public int Id { get; set; }

        public int? Order_Id { get; set; }

        public int? Product_Id { get; set; }

        [StringLength(200)]
        public string Product_Name { get; set; }

        public int? Product_Quantity { get; set; }

        public decimal? Product_Price { get; set; }
    }
}
