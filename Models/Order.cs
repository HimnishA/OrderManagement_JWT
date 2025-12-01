using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace OrderManagementApi.Models;


public class Order
{
    [Key]
    public int Id { get; set; }


    [Required]
    public string ProductName { get; set; } = null!;


    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }


    [Range(0.0, double.MaxValue)]
    public decimal UnitPrice { get; set; }


    [Range(0.0, double.MaxValue)]
    public decimal TotalAmount { get; set; }


    // Ownership
    [Required]
    public string UserId { get; set; } = null!;


    [ForeignKey("UserId")]
    public ApplicationUser? User { get; set; }
}