namespace BusinessLayer.Services.WishListServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interfaces.WishListInterfaces;
    using ModelLayer.Models.WishListModel;
    using RepositoryLayer.Interfaces.WishListInterfaces;

    public class WishListBL : IWishListBL
    {
        private readonly IWishListRL wishListRL;

        public WishListBL(IWishListRL wishListRL)
        {
            this.wishListRL = wishListRL;
        }

        public bool AddTOWishList(int UserId, WishListPostModel listPostModel)
        {
            try
            {
                return this.wishListRL.AddTOWishList(UserId, listPostModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WishListResponseModel> GetAllWishList(int UserId)
        {
            try
            {
                return this.wishListRL.GetAllWishList(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteWishListItem(int UserId, int WishListId)
        {
            try
            {
                return this.wishListRL.DeleteWishListItem(UserId, WishListId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WishListResponseModel GetByWishListId(int WishListId, int UserId)
        {
            try
            {
                return this.wishListRL.GetByWishListId(UserId, WishListId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
