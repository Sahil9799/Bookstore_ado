namespace BusinessLayer.Interfaces.WishListInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ModelLayer.Models.WishListModel;

    public interface IWishListBL
    {
        public bool AddTOWishList(int UserId, WishListPostModel listPostModel);

        public List<WishListResponseModel> GetAllWishList(int UserId);

        public bool DeleteWishListItem(int UserId, int WishListId);

        public WishListResponseModel GetByWishListId(int WishListId, int UserId);
    }
}