using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entityes;
using Talabat.Core.I_Repository;

namespace Talabat.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _BasketRepository;

        public BasketController(IBasketRepository BasketRepository)
        {
            _BasketRepository = BasketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Baskets>> GetBasket(string id)
        {
              var Basket=   await _BasketRepository.GetBasketAcync(id);
            if (Basket == null)
                return BadRequest(ModelState);
            else
                return Ok(Basket);
        }
        [HttpPost]
        public async Task<ActionResult<Baskets>> UpdatOrCreate(Baskets basket)
        {
            var newbasket = await _BasketRepository.CreateOrUpdateAcync(basket);
            if (newbasket == null)
                return BadRequest();
            else
                return Ok(basket);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DleteBasket(string id)
        {
            return await _BasketRepository.DeleteBasketAcync(id);
        }


    }
}
