namespace IWantApp.Domain.Products;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CreateBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string EditeBy { get; set; }
    public DateTime EditeOn { get; set; }
}
