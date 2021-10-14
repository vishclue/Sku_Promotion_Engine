using System;
using System.Collections;

namespace Sku_Promotion_Engine
{
    class PromoCodeProcessor : IPromoCodeProcessor
    {
        private IPromoCodeDetails m_PromoCodeDetails;
        public PromoCodeProcessor(IPromoCodeDetails promoCodeDetails)
        {  
            m_PromoCodeDetails = promoCodeDetails;
        }
        bool IPromoCodeProcessor.IsPromoCodeApplicable(char[] allSelectedSkus, char[] promoCode)
        {
            if(allSelectedSkus.Length == 0)
                throw new ArgumentException(nameof(allSelectedSkus));

            char[] modifiedSkus = allSelectedSkus;

            string modifiedSkuString = new string(modifiedSkus).ToLowerInvariant();

            bool found = false;

            char[] promoCodeLowerCase = new string(promoCode).ToLowerInvariant().ToCharArray();

            for (int i = 0; i < promoCodeLowerCase.Length; i++)
            {
                if (modifiedSkuString.Contains(promoCodeLowerCase[i].ToString()))
                {
                    found = true;
                    
                    int index = modifiedSkuString.IndexOf(promoCodeLowerCase[i]);
                    modifiedSkuString = modifiedSkuString.Remove(index, 1);
                    //modifiedSkus = str.ToCharArray();
                }
                else
                {
                    found = false;
                    break;
                }
            }

            return found;
        }

        float IPromoCodeProcessor.ApplyPromoCode(char[] allSelectedSkus, char[] promoCode, out char[] modifiedSkus)
        {  

            allSelectedSkus= new string(allSelectedSkus).ToLowerInvariant().ToCharArray();
            promoCode = new string(promoCode).ToLowerInvariant().ToCharArray();

            int numberOfTimesToApplyPromoCode = 0;
            float promoCodeTotalOrderValue = 0;
            modifiedSkus = allSelectedSkus;
            
            while (((IPromoCodeProcessor)this).IsPromoCodeApplicable(modifiedSkus, promoCode))
            {
                numberOfTimesToApplyPromoCode += 1;
                promoCodeTotalOrderValue += m_PromoCodeDetails.GetListOfPromoCodes()[new string(promoCode)];
                modifiedSkus = GetModifiedSkus(modifiedSkus, promoCode);
            }

            return promoCodeTotalOrderValue;
        }

        private static char[] GetModifiedSkus(char[] modifiedSkus, char[] promoCodeCharArr)
        {
            string modifiedSkuString = new string(modifiedSkus);

            for (int i = 0; i < promoCodeCharArr.Length; i++)
            {

                int index = modifiedSkuString.IndexOf(promoCodeCharArr[i]);
                modifiedSkuString = modifiedSkuString.Remove(index, 1);
            }

            char[] modifiedSkuArray = modifiedSkuString.ToCharArray();
            return modifiedSkuArray;
        }

    }
}