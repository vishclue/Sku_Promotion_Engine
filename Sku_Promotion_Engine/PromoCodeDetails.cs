using System.Collections.Generic;

namespace Sku_Promotion_Engine
{
    internal class PromoCodeDetails : IPromoCodeDetails
    {
        private IDictionary<string, float> m_PromoCodeToPriceDictionary;


        public PromoCodeDetails()
        {
            m_PromoCodeToPriceDictionary = new Dictionary<string, float>();
            m_PromoCodeToPriceDictionary.Add("AAA", 130);
            m_PromoCodeToPriceDictionary.Add("CD", 50);
        }

        IDictionary<string, float> IPromoCodeDetails.PromoCodeToPriceDictionary
        {
            get { return m_PromoCodeToPriceDictionary; }
            set { m_PromoCodeToPriceDictionary = value; }
        }

        IDictionary<string, float> IPromoCodeDetails.GetListOfPromoCodes()
        {
            return m_PromoCodeToPriceDictionary;
        }

        void IPromoCodeDetails.AddExtraPromoCodes(string promoCode, float promoCodeDiscount)
        {
            m_PromoCodeToPriceDictionary.Add(promoCode, promoCodeDiscount);
        }

    }
}