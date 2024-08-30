using PSW24.API.Controllers;
using PSW24.API.DTOs;
using PSW24.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
namespace PSW24_BackEnd.Controllers
{
    [Route("api/carts")]

    public class CartController : BaseApiController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public ActionResult<CartDto> Create([FromBody]CartDto dto)
        {
            var result = _cartService.Create(dto);
            return CreateResponse(result);
        }

        [HttpDelete("{cartId}")]
        public ActionResult<CartDto> Delete(long cartId)
        {
            var result = _cartService.Delete(cartId);
            return CreateResponse(result);
        }


    }
}
