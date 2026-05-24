using Core.Entities;
using Core.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class BrandListSpecification: BaseSpecification<Product, string>
    {
        public BrandListSpecification()
        {
            AddSelect(p => p.Brand);
            ApplyDistinct();
        }
    }
}
