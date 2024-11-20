

namespace Cinema.Domain.Entities
{
    public class Discount
    {
        public int Id { get; set; }

        // Basic Details
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal DiscountPercentage { get; set; } // 0-100 range
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        // Promo Code (null for general discounts)
        public string? Code { get; set; }

        // Is the Discount Unlimited
        public bool IsUnlimited { get; set; } = false; // True for general/unlimited discounts
        public bool IsUsed { get; set; } = false; // Tracks if the discount is used (limited discount)

        // Methods
        public bool IsActive(DateTime dateTime)
        {
            return dateTime >= StartDate && dateTime <= EndDate && (!IsUsed || IsUnlimited);
        }

        public bool CanApply(string code = null)
        {
            bool isCodeValid = Code == null || Code.Equals(code, StringComparison.OrdinalIgnoreCase);
            return isCodeValid && IsActive(DateTime.Now);
        }

        public void Apply()
        {
            if (!IsUnlimited)
            {
                if (IsUsed)
                    throw new InvalidOperationException("This discount has already been used.");

                IsUsed = true; // Mark as used
            }
        }
    }
}
