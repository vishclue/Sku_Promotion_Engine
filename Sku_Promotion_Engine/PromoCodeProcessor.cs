using System.Collections;

namespace Sku_Promotion_Engine
{
    class PromoCodeProcessor : IPromoCodeProcessor
    {
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
    }
}