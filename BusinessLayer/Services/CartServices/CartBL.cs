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
    }
}
