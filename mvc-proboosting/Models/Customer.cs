using System.Runtime.Serialization;

namespace mvc_proboosting.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CustomerId { get; set; }

        [ForeignKey("Booster")]
        public int BoosterId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }


        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [NotMapped]
        public DateTime? LastLogon { get; set; }

        public virtual Booster Booster { get; set; }
    }
}
