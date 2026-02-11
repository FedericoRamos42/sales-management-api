using Application.Services.Sales.Interfaces;
using Application.Services.Sales.Mappers;
using Application.Services.Sales.Models.Request;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Sales.Features
{
    public class ExportSalesToExcel
    {
        private readonly IUnitOfWork _repository;
        private readonly ISalesExcelExporter _excelExporter;
        public ExportSalesToExcel(IUnitOfWork repository, ISalesExcelExporter excelExporter)
        {
            _repository = repository;
            _excelExporter = excelExporter;
        }

        public async Task<byte[]> Execute()
        {
            var sales = await _repository.Sales.GetAllSales();

            var salesDto = sales.ToListDto();
            return _excelExporter.ExportSalesToExcel(salesDto);
        }
    }
}
