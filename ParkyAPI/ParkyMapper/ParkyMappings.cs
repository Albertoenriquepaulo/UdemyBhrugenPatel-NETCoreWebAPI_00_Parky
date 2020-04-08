using AutoMapper;
using ParkyAPI.Model;
using ParkyAPI.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkyAPI.ParkyMapper
{
    public class ParkyMappings : Profile
    {
        public ParkyMappings()
        {
            //ReverseMap -> to map both ways
            CreateMap<NationalPark, NationalParkDTO>().ReverseMap();

            CreateMap<Trail, TrailDTO>().ReverseMap();
            CreateMap<Trail, TrailUpsertDTO>().ReverseMap();

            //if we add more models we can add more mapping in here
        }
    }
}

