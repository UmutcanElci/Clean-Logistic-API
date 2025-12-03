namespace Logistics.Application.Mappers;

using Logistics.Domain;
using Logistics.Application.DTOs;
using Logistics.Domain.common;

public static class MappingExtensions
{
    public static VehicleDto ToDto(this Vehicle vehicle)
    {
        return new VehicleDto
        {
            Id = vehicle.Id,
            LicensePlate = vehicle.LicensePlate,
            Type = vehicle.Type,
            MaxWeightInKg = vehicle.MaxWeightInKg,
            MaxVolumeInCubicMeters = vehicle.MaxVolumeInCubicMeters,
            CanGoAbroad = vehicle.CanGoAbroad,
            MaxSpeedInKph = vehicle.MaxSpeedInKph,
            CurrentLocation = vehicle.CurrentLocation.ToDto(),
            Status = vehicle.Status
        };
    }

    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            MailAddress = order.MailAddress,
            PhoneNumber = order.PhoneNumber,
            Status = order.Status,
            PickUpLocation = order.PickUpLocation.ToDto(),
            DestinationLocation = order.DestinationLocation.ToDto(),
            OrderItems = order.OrderItems.Select(item => item.ToDto()).ToList()
        };
    }

    public static WarehouseDto ToDto(this Warehouse warehouse)
    {
        return new WarehouseDto
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
            MaxCapacity = warehouse.MaxCapacity,
            Type = warehouse.Type,
            Address = warehouse.Address.ToDto()
        };
    }

    public static LocationDto ToDto(this Location location)
    {
        return new LocationDto
        {
            StreetAddress = location.StreetAddress,
            City = location.City,
            PostalCode = location.PostalCode,
            Country = location.Country
        };
    }

    public static OrderItemDto ToDto(this OrderItem orderItem)
    {
        return new OrderItemDto
        {
            Description = orderItem.Description,
            WeightInKg = orderItem.WeightInKg,
            Quantity = orderItem.Quantity
        };
    }

}
