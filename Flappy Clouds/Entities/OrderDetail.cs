using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Flappy_Clouds.Entities;

public partial class OrderDetail
{
    [Key]
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderDetails")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderDetails")]
    public virtual Product Product { get; set; } = null!;
}
