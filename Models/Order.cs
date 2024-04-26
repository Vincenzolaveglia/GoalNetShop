namespace GoalNetShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int OrderId { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime OrderDate { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }

        public decimal UnitPrice { get; set; }

        public int UserId { get; set; }

        public virtual User Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
