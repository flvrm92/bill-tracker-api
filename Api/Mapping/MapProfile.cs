using Application.Dtos.Bills;
using AutoMapper;
using Domain.Entities.Bills;

namespace Api.Mapping;
public class MapProfile: Profile
{
  public MapProfile()
  {
    CreateMap<BillDto, Bill>().ReverseMap();
    CreateMap<BillItemDto, BillItem>().ReverseMap();
  }
}
