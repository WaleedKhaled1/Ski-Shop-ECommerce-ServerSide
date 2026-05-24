using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Specification
{
    public class ProductSpecification : BaseSpecification<Product>
    {
       public ProductSpecification(string? brand, string? type,string ?sort) : base(x =>
        (String.IsNullOrEmpty(brand) || x.Brand == brand) &&
        (String.IsNullOrEmpty(type) || x.Type == type))
        {
          switch(sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }

    }
}
