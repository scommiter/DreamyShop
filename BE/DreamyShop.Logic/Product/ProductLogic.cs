using AutoMapper;
using AutoMapper.QueryableExtensions;
using DreamyShop.Common.Exceptions;
using DreamyShop.Common.Extensions;
using DreamyShop.Common.Results;
using DreamyShop.Domain;
using DreamyShop.Domain.Shared.Dtos;
using DreamyShop.Domain.Shared.Types;
using DreamyShop.EntityFrameworkCore;
using DreamyShop.Logic.Conditions;
using DreamyShop.Repository.RepositoryWrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace DreamyShop.Logic.Product
{
    public class ProductLogic : IProductLogic
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public ProductLogic(
            IRepositoryWrapper repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}   