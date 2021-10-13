using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sku_Promotion_Engine
{
    interface ISkuDetails
    {
        IDictionary<char, float> GetAllSkuPriceDetails();

        void AddAdditionalSkus(char sku, float price);
    }
}
