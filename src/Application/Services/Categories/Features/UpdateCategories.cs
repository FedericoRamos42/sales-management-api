using Application.Services.Categories.Mappers;
using Application.Services.Categories.Models;
using Application.Services.Categories.Models.Request;
using Application.Utils.Result;
using Domain.Enitites;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Categories.Features
{
    public class UpdateCategories
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateCategoryForRequest> _validator;


        public UpdateCategories(IUnitOfWork repository,IValidator<CreateCategoryForRequest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<CategoryDto>> Execute(int id,CreateCategoryForRequest request)
        {
            var validate = await _validator.ValidateAsync(request);
            if (!validate.IsValid)
            {
                var errors = validate.Errors.Select(x => x.ErrorMessage).ToList();
                return Result<CategoryDto>.Failure(errors);
            }

            var category = await _repository.Categories.Get(x=> x.Id == id);

            if(category is null)
            {
                return Result<CategoryDto>.Failure($"category with id {id} does not exist");
            }

            category.Name = request.Name;
            await _repository.SaveChangesAsync();
            return Result<CategoryDto>.Succes(category.ToDto());
        }
    }
}
