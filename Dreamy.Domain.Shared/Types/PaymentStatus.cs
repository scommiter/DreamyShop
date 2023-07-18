namespace Dreamy.Domain.Shared.Types
{
    public enum PaymentStatus
    {
        Unpaid = 1,
        Failed = 2,
        Expired = 3,
        Paid = 4,
        Refunding = 5,
        Refunded = 6,
    }
}
