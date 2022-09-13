namespace BusinessLayer.Services.AddressServices
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interfaces.AddressInterfaces;
    using Microsoft.Extensions.Configuration;
    using ModelLayer.Models.AddressModels;
    using RepositoryLayer.Interfaces.AddressInterfaces;

    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL addressRL;

        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public bool AddAddress(int UserId, AddressPostModel postModel)
        {
            try
            {
                return addressRL.AddAddress(UserId, postModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AddressResponseModel> GetAllAddress(int UserId)
        {
            try
            {
                return addressRL.GetAllAddress(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteAddressByAddressId(int AddressId, int UserId)
        {
            try
            {
                return addressRL.DeleteAddressByAddressId(AddressId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateAddressbyId(int UserId, AddressPutModel postModel)
        {
            try
            {
                return addressRL.UpdateAddressbyId(UserId, postModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AddressResponseModel GetAddressById(int AddressId, int UserId)
        {
            try
            {
                return addressRL.GetAddressById(AddressId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}