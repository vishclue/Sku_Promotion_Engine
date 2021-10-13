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
            char[] modifiedSkus = allSelectedSkus;

            bool found = false;

            for (int i = 0; i < promoCode.Length; i++)
            {
                if (((IList) modifiedSkus).Contains(promoCode[i]))
                {
                    found = true;
                    string str = new string(modifiedSkus);
                    int index = str.IndexOf(promoCode[i]);
                    str = str.Remove(index, 1);
                    modifiedSkus = str.ToCharArray();
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

        private static char[] GetModifiedSkus(char[] originalCartArr, char[] promoCodeCharArr)
        {
            string str = new string(originalCartArr);

            for (int i = 0; i < promoCodeCharArr.Length; i++)
            {

                int index = str.IndexOf(promoCodeCharArr[i]);
                str = str.Remove(index, 1);
            }

            char[] modifiedCartArr = str.ToCharArray();
            return modifiedCartArr;
        }

    }
}