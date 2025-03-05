using Flappy_Clouds.Entities;

public class ProductListViewModel
{
    public List<Product> Products { get; set; } = new();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
