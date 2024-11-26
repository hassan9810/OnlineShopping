namespace Store.Validations
{
    public class PostOrderCommandValidator : AbstractValidator<PostOrderCommand>
    {
        public PostOrderCommandValidator()
        {
            RuleFor(command => command.DiscountPromoCode)
                .Must((command, discountPromoCode) =>
                    !command.HaveVoucher || (discountPromoCode == "PROMO10" || discountPromoCode == "PROMO20"))
                .WithMessage("Invalid discount promo code.");
            RuleFor(command => command.DiscountPromoCode)

                .Must((command, discountPromoCode) => command.HaveVoucher || string.IsNullOrEmpty(discountPromoCode))
                .WithMessage("Discount promo code should not be provided when the voucher flag is false.");
        }

        private bool HaveValidDiscountPromoCode(PostOrderCommand command, bool haveVoucher)
        {
            return !haveVoucher || !string.IsNullOrWhiteSpace(command.DiscountPromoCode);
        }
        private bool BeValidDiscountPromoCode(string discountPromoCode)
        {
            string[] allowedPromoCodes = new string[] { "PROMO10", "PROMO20" };
            return allowedPromoCodes.Contains(discountPromoCode);
        }
    }
}