using System.ComponentModel.DataAnnotations;

namespace Yurukcu.Web.Entity
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public bool IsDiscounted { get; set; }

        public decimal? WithoutDiscountPrice { get; set; }
    }
}
