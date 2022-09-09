namespace BusinessLayer.Interfaces.CartInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ModelLayer.Models.CartModels;

    public interface ICartBL
    {
        public bool AddBookTOCart(int UserId, CartPostModel postModel);
    }
}
