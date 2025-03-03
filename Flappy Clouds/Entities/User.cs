using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Flappy_Clouds.Entities;

[Index("Email", Name = "UQ__Users__A9D1053438470288", IsUnique = true)]
public partial class User
{
    internal readonly bool IsAdmin;

    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Firstname is required")]
    [MaxLength(50, ErrorMessage = "Max 50 characters is allowed  ")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Lastname is required")]
    [MaxLength(50, ErrorMessage = "Max 50 characters is allowed  ")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [MaxLength(100, ErrorMessage = "Max 100 characters is allowed  ")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [MaxLength(255)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(15)]
    public string? PhoneNumber { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Role is required")]
    [StringLength(20)]
    public string Role { get; set; } = "Customer";

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("User")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("User")]
    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
}
