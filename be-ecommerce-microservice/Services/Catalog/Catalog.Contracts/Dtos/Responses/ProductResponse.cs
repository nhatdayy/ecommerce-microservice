namespace Catalog.Contracts.Dtos.Responses;
public class ProductResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public BrandResponse? Brand { get; set; }
    public ProductTypeResponse? Type { get; set; }

}
