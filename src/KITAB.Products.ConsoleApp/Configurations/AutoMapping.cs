using AutoMapper;
using KITAB.Products.ConsoleApp.ViewModels;
using KITAB.Products.Domain.Models;

namespace KITAB.Products.ConsoleApp.Configurations
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ExportedProduct, ExportedProductViewModel>().ReverseMap();
        }
    }
}
