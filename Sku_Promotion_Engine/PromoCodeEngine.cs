using System.Collections.Generic;

namespace Sku_Promotion_Engine
{
    internal class PromoCodeEngine : IPromoCodeEngine
    {
        private IPromoCodeProcessor m_PromoCodeProcessor;
        private IPromoCodeDetails m_PromoCodeDetails;
        private ISkuDetails m_SkuDetails;

        public PromoCodeEngine(IPromoCodeProcessor promoCodeProcessor, IPromoCodeDetails promoCodeDetails, ISkuDetails skuDetails)
        {
            m_PromoCodeProcessor = promoCodeProcessor;
            m_PromoCodeDetails = promoCodeDetails;
            m_SkuDetails = skuDetails;
        }

        float IPromoCodeEngine.GetTotalOderValue(char[] selectedSkus)
        {
            IDictionary<string, float> promoCodeToPriceDictionary = m_PromoCodeDetails.GetListOfPromoCodes();

            float totalOrderValue = 0;

            char[] modifiedSkuArray = selectedSkus;

            foreach (var key in promoCodeToPriceDictionary.Keys)
            {
                bool isPromoCodeApplicable = m_PromoCodeProcessor.IsPromoCodeApplicable(modifiedSkuArray, key.ToCharArray());

                if (isPromoCodeApplicable)
                {
                    totalOrderValue += m_PromoCodeProcessor.ApplyPromoCode(modifiedSkuArray, key.ToCharArray(), out modifiedSkuArray);
                }
            }

            foreach (char selectedSku in modifiedSkuArray)
            {
                totalOrderValue += m_SkuDetails.GetAllSkuPriceDetails()[selectedSku];
            }

            return totalOrderValue;

        }
    }
}