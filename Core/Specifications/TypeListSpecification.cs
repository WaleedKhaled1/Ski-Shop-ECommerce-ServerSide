using Core.Entities;
using Core.Specification;

namespace Core.Specifications
{
    public class TypeListSpecification : BaseSpecification<Product, string>
    {
        public TypeListSpecification()
        {
            AddSelect(p => p.Type);
            ApplyDistinct();
        }
    }
}
