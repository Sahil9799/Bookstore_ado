namespace RepositoryLayer.Interfaces.CartInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ModelLayer.Models.CartModels;

    public interface ICartRL
    {
        public bool AddBookTOCart(int UserId,CartPostModel postModel);
    }
}
