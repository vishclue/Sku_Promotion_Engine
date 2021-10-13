using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sku_Promotion_Engine;

namespace Sku_Promotion_Engine_Test
{
    [TestClass]
    public class SkuPromotionEngineTests
    {
        [TestMethod]
        public void Given_PromoCodeDetails_When_Call_List_Of_Promo_Codes_Then_Returns_List_Of_Promo_Codes()
        {
            IPromoCodeDetails promoCodeDetails = new PromoCodeDetails();
            int promoCodes = promoCodeDetails.GetListOfPromoCodes().Count;
            Assert.IsTrue(promoCodes == 2, "");
        }
    }
}
