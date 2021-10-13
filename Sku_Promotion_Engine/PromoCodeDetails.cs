using System.Collections.Generic;

namespace Sku_Promotion_Engine
{
    internal class PromoCodeDetails : IPromoCodeDetails
    {
        private IDictionary<char[], float> m_PromoCodeToPriceDictionary;


        public PromoCodeDetails()
        {
            m_PromoCodeToPriceDictionary = new Dictionary<char[], float>();
        }

        public IDictionary<char[], float> PromoCodeToPriceDictionary
        {
            get { return m_PromoCodeToPriceDictionary; }
            set { m_PromoCodeToPriceDictionary = value; }
        }

        IDictionary<char[], float> IPromoCodeDetails.GetListOfPromoCodes()
        {
            m_PromoCodeToPriceDictionary = new Dictionary<char[], float>();
            m_PromoCodeToPriceDictionary.Add(new char[] { 'A', 'A', 'A' }, 130);
            m_PromoCodeToPriceDictionary.Add(new char[] { 'C', 'D' }, 50);
            return m_PromoCodeToPriceDictionary;
        }

    }
}