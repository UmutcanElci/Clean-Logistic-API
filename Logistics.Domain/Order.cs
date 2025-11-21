public class Order
{
    public Guid Id { get; set; }
    public required string CustomerName { get; set; }
    public required string MailAddress { get; set; }
    public required string PhoneNumber { get; set; }

    public StorageType ProductRequestType { get; set; }
    public int AmountOfProductAsKg { get; set; } // this two fields can be change not be sure about it


    // Then this can't be required - also need a mapper for handle what customer gonna input and what not  - we can't handle the functions in here I believe
    public required Location PickUpLocation { get; set; }
    public required Location DestinationLocation { get; set; }
}
