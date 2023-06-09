﻿using AutoMapper;
using MovieApi.Data.Dtos;
using MovieApi.Models;

namespace MovieApi.Configuration.Profiles;

public class MapperProfile : Profile
{

    public MapperProfile() 
    {
        CreateMap<CreateMovieDto, Movie>();
        CreateMap<UpdateMovieDto, Movie>();
        CreateMap<Movie, UpdateMovieDto>();
        CreateMap<Movie, ReadMovieDto>();
    }

}
