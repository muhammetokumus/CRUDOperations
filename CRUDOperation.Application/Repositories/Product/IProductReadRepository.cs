using CRUDOPeration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDOperation.Application.Repositories
{
    public interface IProductReadRepository : IReadRepository<Product>
    {
    }
}
