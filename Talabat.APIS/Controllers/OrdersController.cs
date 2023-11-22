using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIS.DTOs;
using Talabat.Core;
using Talabat.Core.Entityes.Order_Module;
using Talabat.Core.I_Services;

namespace Talabat.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderServise _orderServise;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderServise orderServise, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._orderServise = orderServise;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] 
        public async Task<ActionResult<OrderToReturnDTO?>> CreateOrder(OrderDTO orderDTO)
        {
            var AddessMapping = _mapper.Map<AddressDTO, AddressOrder>(orderDTO.addressDTO);

            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderServise.CreateOrderAsynk(BuyerEmail, orderDTO.BasketId, orderDTO.DeliveryMethodId, AddessMapping);

            if (order == null) { return BadRequest(); }

            var OrderMapping = _mapper.Map<Order, OrderToReturnDTO>(order);

            return Ok(OrderMapping);
        }

        [HttpGet("OrdersUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<OrderToReturnDTO?>> GetOrdersUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
         var Orders =  await _orderServise.GetOrdersForSpsifcationUserAsynk(Email);
            if (Orders == null)
                return null;

            var OrderMapping = _mapper.Map<IEnumerable< Order>,IEnumerable< OrderToReturnDTO>>(Orders);

            return Ok(OrderMapping);


        }



        [HttpGet("OrderIdUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrdersUser(int OrderID)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Order = await _orderServise.GetOrderByIdSpsifcationUserAsynk(Email , OrderID);
            if (Order == null)
                return null;

            var OrderMapping = _mapper.Map<Order,OrderToReturnDTO>(Order);
           
            return Ok(OrderMapping);


        }

        [HttpGet("DeliveryMethods")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task< ActionResult<DeliveryMethod>> GetDeliveryMethods()
        {
           var deliverymeth = await _unitOfWork.RepositoryUonitOfWork<DeliveryMethod>().GetAllAsync();

            if (deliverymeth == null)
                return null;

            return Ok(deliverymeth);
        }

    }
}
