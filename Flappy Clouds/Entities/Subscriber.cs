using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Flappy_Clouds.Entities;

[Index("Email", Name = "UQ__Subscrib__A9D105341EBFABCC", IsUnique = true)]
public partial class Subscriber
{
    [Key]
    public int SubscriberId { get; set; }

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? SubscribedAt { get; set; }
}
