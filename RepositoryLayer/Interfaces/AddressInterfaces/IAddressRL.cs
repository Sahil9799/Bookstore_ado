namespace RepositoryLayer.Interfaces.AddressInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ModelLayer.Models.AddressModels;

    public interface IAddressRL
    {
        public bool AddAddress(int UserId, AddressPostModel postModel);

        public List<AddressResponseModel> GetAllAddress(int UserId);

        public bool DeleteAddressByAddressId(int AddressId, int UserId);

        public bool UpdateAddressbyId(int UserId, AddressPutModel postModel);

        public AddressResponseModel GetAddressById(int AddressId, int UserId);
    }
}