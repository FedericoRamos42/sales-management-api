using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Producto.Features;
using Application.Services.Products.Features;

namespace Application.Services.Producto
{
    public record ProductUseCases(
        CreateProduct CreateProduct,
        UpdateProductStock UpdateProductStock,
        GetProducts GetByPagination,
        GetProduct GetProduct,
        UpdateProductPrice UpdateProductPrice,
        DeleteProduct DeleteProduct
    );
}
