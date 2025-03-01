using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Flappy_Clouds.Entities;

public partial class Order
{
    [Key]
    public int OrderId { get; set; }

    public int UserId { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal TotalAmount { get; set; }

    [StringLength(20)]
    public string? OrderStatus { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? OrderDate { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [ForeignKey("UserId")]
    [InverseProperty("Orders")]
    public virtual User User { get; set; } = null!;
}
