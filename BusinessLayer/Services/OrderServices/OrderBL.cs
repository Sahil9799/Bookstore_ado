namespace BusinessLayer.Services.OrderServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interfaces.OrderInterfaces;
    using ModelLayer.Models.OrderModels;
    using RepositoryLayer.Interfaces.OrderInterfaces;

    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL orderRL;

        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public bool AddOrder(OrderPostModel postModel)
        {
            try
            {
                return orderRL.AddOrder(postModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OrderResponseModel> GetAllOrders(int UserId)
        {
            try
            {
                return orderRL.GetAllOrders(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}