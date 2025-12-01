using System.ComponentModel.DataAnnotations;


namespace OrderManagementApi.DTOs;


public class OrderDto
{
    public int? Id { get; set; }


    [Required]
    public string ProductName { get; set; } = null!;


    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }


    [Range(0.0, double.MaxValue)]
    public decimal UnitPrice { get; set; }
}