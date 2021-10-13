using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            promoCodeDetails.AddExtraPromoCodes(new char[] {'A', 'B'}.ToString(), 60);
            int promoCodes = promoCodeDetails.PromoCodeToPriceDictionary.Count;
            Assert.IsTrue(promoCodes == 3, "");
        }

        [TestMethod]
        public void Given_SkuDetails_When_Call_GetAllSkuPriceDetails_Then_Reflects_Differrent_Skus()
        {
            ISkuDetails skuDetails = new SkuDetails();
            IDictionary<char, float> skuToPriceDetails = skuDetails.GetAllSkuPriceDetails();

            Assert.IsTrue(skuToPriceDetails.Count == 4, "");
        }


        [TestMethod]
        public void Given_SkuDetails_When_Call_AddAdditionalSkus_Then_Reflects_newly_added_Sku()
        {
            ISkuDetails skuDetails = new SkuDetails();
            
            skuDetails.AddAdditionalSkus('E',90);

            IDictionary<char, float> skuToPriceDetails = skuDetails.GetAllSkuPriceDetails();

            Assert.IsTrue(skuToPriceDetails.Count == 5, "");

            Assert.IsTrue(skuToPriceDetails.Keys.Contains('E'), "");
        }

        [TestMethod]
        public void Given_PromoCodeProcessor_When_Call_IsPromoCodeApplicable_With_SelectedSkus_Containing_PromoCode_Then_Returns_True()
        {
            IPromoCodeDetails promoCodeDetails = new PromoCodeDetails();
            IPromoCodeProcessor promoCodeProcessor = new PromoCodeProcessor(promoCodeDetails);
            bool isPromoCodeApplicable = promoCodeProcessor.IsPromoCodeApplicable(new []{'A','B','C','A','A','D'},new []{'A','A','A'});
            Assert.IsTrue(isPromoCodeApplicable,"");
        }


        [TestMethod]
        public void Given_PromoCodeProcessor_When_Call_IsPromoCodeApplicable_With_SelectedSkus_Not_Containing_PromoCode_Then_Returns_True()
        {
            IPromoCodeDetails promoCodeDetails = new PromoCodeDetails();
            IPromoCodeProcessor promoCodeProcessor = new PromoCodeProcessor(promoCodeDetails);
            bool isPromoCodeApplicable = promoCodeProcessor.IsPromoCodeApplicable(new[] { 'A', 'B', 'C', 'A', 'A', 'D' }, new[] { 'B','B', 'D' });
            Assert.IsFalse(isPromoCodeApplicable, "");
        }

        [TestMethod]
        public void Given_PromoCodeProcessor_When_Call_ApplyPromoCode_With_SelectedSkus_Containing_PromoCode_Then_Returns_filtered_Skus_And_PromoCodeTotalOrderValue()
        {
            IPromoCodeDetails promoCodeDetails =  new PromoCodeDetails();
            IPromoCodeProcessor promoCodeProcessor = new PromoCodeProcessor(promoCodeDetails);
            char[] filteredSkus = new char[]{};

            char[] promoCode = new[] {'C', 'D'};


            float promoCodeTotalOrderValue = 
                promoCodeProcessor.ApplyPromoCode(new[] {'A', 'B', 'C', 'A', 'A', 'D'}, promoCode, out filteredSkus);

            float promoCodeValue = promoCodeDetails.PromoCodeToPriceDictionary[new string(promoCode)];

            Assert.IsTrue(promoCodeTotalOrderValue == promoCodeValue,"");




        }
    }
}
