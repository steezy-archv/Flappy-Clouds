using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Flappy_Clouds.Entities;

[Index("Name", Name = "UQ__Categori__737584F65B9FFFBB", IsUnique = true)]
public partial class Category
{
    [Key]
    public int CategoryId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
