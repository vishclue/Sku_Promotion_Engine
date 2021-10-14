using System;
using System.Collections.Generic;

namespace Sku_Promotion_Engine
{
    class SkuDetails : ISkuDetails
    {
        private IDictionary<char, float> m_SkuToPriceDictionary;
        public SkuDetails()
        {
            m_SkuToPriceDictionary = new Dictionary<char, float>();
            m_SkuToPriceDictionary.Add('a',60);
            m_SkuToPriceDictionary.Add('b', 50);
            m_SkuToPriceDictionary.Add('c', 30);
            m_SkuToPriceDictionary.Add('d', 20);
            

        }
        IDictionary<char, float> ISkuDetails.GetAllSkuPriceDetails()
        {
            return m_SkuToPriceDictionary;
        }
        
        void ISkuDetails.AddAdditionalSkus(char sku, float price)
        {
            char skuLowerCase = sku.ToString().ToLowerInvariant().ToCharArray()[0];

            if (m_SkuToPriceDictionary.Keys.Contains(skuLowerCase))
                throw new ArgumentException("Specified sku already exists.");

            m_SkuToPriceDictionary.Add(skuLowerCase, price);
        }
    }
}