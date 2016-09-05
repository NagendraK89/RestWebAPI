using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using BusinessEntities;

namespace BusinessServices
{
    public interface IProductServices
    {
        ProductEntity GetProductById(long productId);
        IEnumerable<ProductEntity> GetAllProducts();
        long CreateProduct(ProductEntity productEntity);
        bool UpdateProduct(long productId, ProductEntity productEntity);
        bool DeleteProduct(long productId);
    }
}
