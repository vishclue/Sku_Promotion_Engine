using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sku_Promotion_Engine
{
    internal interface IPromoCodeDetails
    {
        IDictionary<string, float> GetListOfPromoCodes();

        IDictionary<string, float> PromoCodeToPriceDictionary { get; set; }

        void AddExtraPromoCodes(string promoCode, float promoCodeDiscount);
    }
}
