using API.DTOs;
using Core.Entities;

namespace API.Extensions
{
    public static class AddressExtensions
    {
        public static Address ToAddress(this AddressDto addressDto)
        {
            if (addressDto == null)
            {
                throw new ArgumentNullException(nameof(addressDto));
            }

            var Address = new Address
            {
                Line1 = addressDto.Line1,
                Line2 = addressDto.Line2,
                City = addressDto.City,
                Country = addressDto.Country,
                PostalCode = addressDto.PostalCode,
                State = addressDto.State,
            };

            return Address;
        }

        public static AddressDto? ToDto(this Address? address)
        {
            if (address == null)
            {
                return null;
            }

            var AddressDto = new AddressDto
            {
                Line1 = address.Line1,
                Line2 = address.Line2,
                City = address.City,
                Country = address.Country,
                PostalCode = address.PostalCode,
                State = address.State,
            };

            return AddressDto;
        }

        public static void UpdateFromDto(this Address address,AddressDto addressDto)
        {
            if(address==null)
                throw new ArgumentNullException(nameof(address));


            if (addressDto == null)
            {
                throw new ArgumentNullException(nameof(addressDto));
            }

            address.Line1 = addressDto.Line1;
            address.Line2 = addressDto.Line2;
            address.City = addressDto.City;
            address.Country = addressDto.Country;
            address.PostalCode = addressDto.PostalCode;
            address.State = addressDto.State;
        }
    }
}
