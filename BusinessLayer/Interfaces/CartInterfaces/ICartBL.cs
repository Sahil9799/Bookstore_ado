namespace BusinessLayer.Interfaces.CartInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ModelLayer.Models.CartModels;

    public interface ICartBL
    {
        public bool AddBookTOCart(int UserId, CartPostModel postModel);

        public List<CartResponseModel> GetAllBooksInCart(int UserId);

        public bool UpdateCartItem(int UserId, CartUpdateModel cartUpdateModel);

        public bool DeleteCartItembyBookId(int UserId, int CartId);

        public CartResponseModel GetCartItemByCartId(int CartId, int UserId);
    }
}
