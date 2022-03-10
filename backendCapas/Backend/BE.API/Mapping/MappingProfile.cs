using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using data = BE.DAL.DO.Objetos;

namespace BE.API.Mapping
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<data.Categories, DataModels.Categories>().ReverseMap();
            CreateMap<data.Products, DataModels.Products>().ReverseMap();
        }
    }
}
