namespace Dreamy.Domain.Shared.Types
{
    public enum BillStatus
    {
        Unfullfilled = 1,
        Preparing = 2,
        Collected = 3,
        Shipping = 4,
        Shipped = 5,
        Arrived = 6,
        Returning = 7,
        Returned = 8
    }
}
