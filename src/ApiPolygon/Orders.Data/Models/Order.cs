using System.ComponentModel.DataAnnotations;
using Core.Data.Models;

namespace Orders.Data.Models;

public class Order: BaseEntity
{
    [StringLength(255)]
    public string Name { get; set; }
    public decimal Price { get; set; }
}