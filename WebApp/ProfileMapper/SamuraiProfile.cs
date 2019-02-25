using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SamuraiApp.Domain;
using WebApp.Models;

namespace WebApp.ProfileMapper
{
    public class SamuraiProfile : Profile
    {
        public SamuraiProfile()
        {
            this.CreateMap<Samurai, SamuraiModel>().ReverseMap();
            this.CreateMap<Quote,QuoteModel>().ReverseMap();
        }
    }
}
