using System.ComponentModel.DataAnnotations;
using Yurukcu.Web.Entity;

public class ShoppingBag
{
    [Key]
    public int ShoppingBagId { get; set; }

    // Ürünle ilişkili olan kısmı tutuyor
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }
    public decimal Price { get; set; }

    // UserId'yi User ile ilişkilendiriyoruz
    public int UserId { get; set; }

    // User nesnesi ile ilişkiyi tanımlıyoruz
    public User User { get; set; }

    public string? ProductName { get; set; }

}
