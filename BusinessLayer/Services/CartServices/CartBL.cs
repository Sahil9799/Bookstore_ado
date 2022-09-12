namespace BusinessLayer.Services.CartServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interfaces.CartInterfaces;
    using ModelLayer.Models.CartModels;
    using RepositoryLayer.Interfaces.CartInterfaces;

    public class CartBL : ICartBL
    {
        private readonly ICartRL cartRL;

        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public bool AddBookTOCart(int UserId, CartPostModel postModel)
        {
            try
            {
                return this.cartRL.AddBookTOCart(UserId, postModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CartResponseModel> GetAllBooksInCart(int UserId)
        {
            try
            {
                return this.cartRL.GetAllBooksInCart(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateCartItem(int UserId, CartUpdateModel cartUpdateModel)
        {
            try
            {
                return this.cartRL.UpdateCartItem(UserId, cartUpdateModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteCartItembyBookId(int UserId, int CartId)
        {
            try
            {
                return this.cartRL.DeleteCartItembyBookId(UserId, CartId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CartResponseModel GetCartItemByCartId(int CartId, int UserId)
        {
            try
            {
                return this.cartRL.GetCartItemByCartId(CartId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}