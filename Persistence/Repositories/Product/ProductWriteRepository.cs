using CRUDOperation.Application.Repositories;
using CRUDOPeration.Domain.Entities;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(CRUDOperationsDbContext context) : base(context)
        {
        }
    }
}
