namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(80)]
        public string Password { get; set; }

        public int? Account_Detail_Id { get; set; }

        public int? Role_ID { get; set; }

        public int? Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Create_Date { get; set; }
    }
}
