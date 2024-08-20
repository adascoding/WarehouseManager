using AutoMapper;
using WarehouseManager.API.DTOs;
using WarehouseManager.API.Models;

namespace WarehouseManager.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Warehouse, WarehouseDTO>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<CreateWarehouseDTO, Warehouse>();
        CreateMap<UpdateWarehouseDTO, Warehouse>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.WarehouseId, opt => opt.MapFrom(src => src.WarehouseId));

        CreateMap<CreateProductDTO, Product>();
        CreateMap<UpdateProductDTO, Product>();

        CreateMap(typeof(PagedList<>), typeof(PagedList<>));
    }
}