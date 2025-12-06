using Microsoft.AspNetCore.Mvc;

[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private static List<Product> products = new List<Product>();

    [HttpGet]
    public ActionResult<List<Product>> GetAll() => products;
    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = products.FirstOrDefault( p => p.Id == id);
        return product != null? Ok(product) : NotFound();
    }
    [HttpPost]
    public ActionResult<Product> Create(Product newProduct)
    {
        products.Add(newProduct);
        return CreatedAtAction(nameof(GetById), new { id = newProduct.Id}, newProduct);
    }

    [HttpPut("{id}")]
    public ActionResult Update( int id, Product updatedProduct)
    {
        var product = products.FirstOrDefault( p => p.Id == id);
        if(product == null) return NotFound();
        product.Name = updatedProduct.Name;
        product.Description = updatedProduct.Description;
        product.Price = updatedProduct.Price;
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete ( int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if(product == null) return NotFound();
        products.Remove(product);
        return NoContent();
    }

}