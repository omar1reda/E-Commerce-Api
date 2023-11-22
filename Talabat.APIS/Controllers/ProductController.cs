using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using Talabat.APIS.DTOs;
using Talabat.APIS.Helper;
using Talabat.Core;
using Talabat.Core.Entityes;
using Talabat.Core.I_Repository;
using Talabat.Core.Specifications;
using Talabat.Repository;

namespace Talabat.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> _ProductRepository;
        private readonly IGenericRepository<ProductBrand> _productBrand;
        private readonly IGenericRepository<ProductType> _typeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IGenericRepository<Product> ProductRepository, IGenericRepository<ProductBrand> productBrand, IGenericRepository<ProductType> TypeRepository, IMapper mapper , IUnitOfWork unitOfWork)
        {
            _ProductRepository = ProductRepository;
            _productBrand = productBrand;
            _typeRepository = TypeRepository;
            _mapper = mapper;
            this._unitOfWork = unitOfWork;
        }



        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllSpecificationAsync([FromQuery] ProductParams Params)
        {
            var Specific = new ProductWithBrandAndTypeSpecification( Params);

            var product = await _unitOfWork.RepositoryUonitOfWork<Product>().GetAllSpecificationAsync(Specific);
            var ProductDTO = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDTO>>(product);
            var PageReturn = new PaginationReturn<ProductToReturnDTO>()
            {
                PageSize = Params.PageSize,
                PageIndex=Params.PageIndex,
                Data = ProductDTO

            };
            return Ok(PageReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetByIdSpecificationAsync(int id)
        {
            var Specific = new ProductWithBrandAndTypeSpecification(id);
            var product = await _unitOfWork.RepositoryUonitOfWork<Product>().GetByIdASpecificationAsync(Specific);

            var ProductDTO = _mapper.Map<Product, ProductToReturnDTO>(product);
            return Ok(ProductDTO);
        }

        [HttpGet("Type")]
        public async Task<ActionResult<ProductType>> GetAllType()
        {
            var ProdType = await _unitOfWork.RepositoryUonitOfWork<ProductType>().GetAllAsync();
            return Ok(ProdType);
        }

        [HttpGet("Brande")]
        public async Task<ActionResult<ProductBrand>>  GetAllProductBrand()
        {
            var ProdctBrand = await _unitOfWork.RepositoryUonitOfWork<ProductBrand>().GetAllAsync();
            return Ok(ProdctBrand);
        }

        [HttpGet("Brande/{id}")]
        public async Task<ActionResult<ProductBrand>> GetByIdProductBrand(int id)
        {
            var ProdctBrand = await _unitOfWork.RepositoryUonitOfWork<ProductBrand>().GetAllAsync();
            return Ok(ProdctBrand);
        }



    }
}
