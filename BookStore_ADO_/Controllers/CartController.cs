namespace BookStore_ADO_DatabaseFirst.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using BusinessLayer.Interfaces.CartInterfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ModelLayer.Models.CartModels;

    [Route("[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartBL cartBL;

        public CartController(ICartBL cartBL)
        {
           this.cartBL = cartBL;
        }

        [Authorize(Roles =Role.Users)]
        [HttpPost("AddBookTOCart")]
        public IActionResult AddBookTOCart(CartPostModel postModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = this.cartBL.AddBookTOCart(UserId,postModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Check if Book is availbale OR it is already in Cart!!  BookId : {postModel.BookId} to the cart!!" });
                }

                return this.Ok(new { success = true, Message = $"BookId : {postModel.BookId} Added to cart Sucessfull..."});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
