using Application.Dtos;
using Application.Dtos.Bills;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Bills;

namespace Api.Mapping;
public class MapProfile: Profile
{
  public MapProfile()
  {
    CreateMap<BillDto, Bill>().ReverseMap();
    CreateMap<BillItemDto, BillItem>().ReverseMap();
    CreateMap<CategoryDto, Category>().ReverseMap();
  }
}
