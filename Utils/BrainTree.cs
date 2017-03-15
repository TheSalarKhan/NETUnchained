using Application;
using Braintree;

namespace Utils {
    public class BrainTreeUtils {
        static BraintreeGateway paymentGateway;

        public static BraintreeGateway getPaymentGateway()
        {

            if (paymentGateway == null)
            {

                paymentGateway = new BraintreeGateway
                {
                    Environment = Braintree.Environment.SANDBOX,
                    MerchantId = Constant.BT_MERCHANT_ID,
                    PublicKey = Constant.BT_PUBLIC_KEY,
                    PrivateKey = Constant.BT_PRIVATE_KEY
                };
            }
            return paymentGateway;
        }
    }
}