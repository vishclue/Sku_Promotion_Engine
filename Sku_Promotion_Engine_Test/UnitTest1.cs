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

        [TestMethod]
        public void Given_PromoCodeDetails_When_Call_Add_Extra_Promo_Codes_Then_Reflects_Extra_Promo_Codes_Added()
        {
            IPromoCodeDetails promoCodeDetails = new PromoCodeDetails();
            promoCodeDetails.AddExtraPromoCodes(new char[] {'A', 'B'}, 60);
            int promoCodes = promoCodeDetails.PromoCodeToPriceDictionary.Count;
            Assert.IsTrue(promoCodes == 3, "");
        }


    }
}
