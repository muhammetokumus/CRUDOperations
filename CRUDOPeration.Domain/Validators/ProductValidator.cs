using CRUDOPeration.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDOPerations.Domain.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        //Fluent Validation
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).Length(3, 22).WithMessage("İsim 3 ve 22 karakter aralığında olmalıdır!");
            RuleFor(x => x.Price).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(1).WithMessage("Fiyat 0'dan büyük olmalıdır!");
        }
    }
}
