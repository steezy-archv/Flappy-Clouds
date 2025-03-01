using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Flappy_Clouds.Entities;

[Table("ShoppingCart")]
public partial class ShoppingCart
{
    [Key]
    public int CartId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AddedAt { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ShoppingCarts")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("ShoppingCarts")]
    public virtual User User { get; set; } = null!;
}
