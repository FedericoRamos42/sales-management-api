using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.Categories;
using Application.Services.Categories.Mappers;
using Application.Services.Categories.Models;
using Application.Utils.Result;
using Domain.Enitites;
using Domain.Interfaces;

namespace Application.Services.Categories.Features
{
    public class GetAllCategories
    {
        private readonly IUnitOfWork _repository;

        public GetAllCategories(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<CategoryDto>>> Execute()
        {
            List<Category> categories = (List<Category>) await _repository.Categories.Search();
            var dto = categories.ToListDto();
            return Result <List<CategoryDto>>.Succes(dto);
        }
    }
}
