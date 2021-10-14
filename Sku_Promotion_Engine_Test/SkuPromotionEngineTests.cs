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
            Assert.IsTrue(promoCodes == 2, "Default promocode count must be 2");
        }

        [TestMethod]
        public void Given_PromoCodeDetails_When_Call_Add_Extra_Promo_Codes_Then_Reflects_Extra_Promo_Codes_Added()
        {
            IPromoCodeDetails promoCodeDetails = new PromoCodeDetails();
            promoCodeDetails.AddExtraPromoCodes(new char[] {'A', 'B'}.ToString(), 60);
            int promoCodes = promoCodeDetails.PromoCodeToPriceDictionary.Count;
            Assert.IsTrue(promoCodes == 3, "Extra promo code additions is not reflecting properly.");
        }

        [TestMethod]
        public void Given_SkuDetails_When_Call_GetAllSkuPriceDetails_Then_Reflects_Differrent_Skus()
        {
            ISkuDetails skuDetails = new SkuDetails();
            IDictionary<char, float> skuToPriceDetails = skuDetails.GetAllSkuPriceDetails();

            Assert.IsTrue(skuToPriceDetails.Count == 4, "skudetails must give a default skus supported.");
        }


        [TestMethod]
        public void Given_SkuDetails_When_Call_AddAdditionalSkus_Then_Reflects_newly_added_Sku()
        {
            ISkuDetails skuDetails = new SkuDetails();
            
            skuDetails.AddAdditionalSkus('E',90);

            IDictionary<char, float> skuToPriceDetails = skuDetails.GetAllSkuPriceDetails();

            Assert.IsTrue(skuToPriceDetails.Count == 5, "");

            Assert.IsTrue(skuToPriceDetails.Keys.Contains('e'), "Add additional skus must add the extra sku.");
        }

        [TestMethod]
        public void Given_PromoCodeProcessor_When_Call_IsPromoCodeApplicable_With_SelectedSkus_Containing_PromoCode_Then_Returns_True()
        {
            IPromoCodeDetails promoCodeDetails = new PromoCodeDetails();
            IPromoCodeProcessor promoCodeProcessor = new PromoCodeProcessor(promoCodeDetails);
            bool isPromoCodeApplicable = promoCodeProcessor.IsPromoCodeApplicable(new []{'A','B','C','A','A','D'},new []{'A','A','A'});
            Assert.IsTrue(isPromoCodeApplicable,"Promocode must be applicable if selectedSkus contains the respective promocode.");
        }


        [TestMethod]
        public void Given_PromoCodeProcessor_When_Call_IsPromoCodeApplicable_With_SelectedSkus_Not_Containing_PromoCode_Then_Returns_True()
        {
            IPromoCodeDetails promoCodeDetails = new PromoCodeDetails();
            IPromoCodeProcessor promoCodeProcessor = new PromoCodeProcessor(promoCodeDetails);
            bool isPromoCodeApplicable = promoCodeProcessor.IsPromoCodeApplicable(new[] { 'A', 'B', 'C', 'A', 'A', 'D' }, new[] { 'B','B', 'D' });
            Assert.IsFalse(isPromoCodeApplicable, "Promocode is should not be applicable if selected skus does not contain the promocode.");
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

            float promoCodeValue = promoCodeDetails.PromoCodeToPriceDictionary[new string(promoCode).ToLowerInvariant()];

            Assert.IsTrue(promoCodeTotalOrderValue == promoCodeValue,"Expected promocode value is not matching with the actual promo code value");
            
        }
          
        [TestMethod]
        public void Given_PromoCodeEngine_And_SetOfSelected_Skus_When_Call_GetTotalOrderValue_Then_Returns_Correct_Value()
        {
            IPromoCodeDetails promoCodeDetails = new PromoCodeDetails();
            IPromoCodeProcessor promoCodeProcessor = new PromoCodeProcessor(promoCodeDetails);
            ISkuDetails skuDetails = new SkuDetails();

            char[] allSelectedSkus = new char[] { 'A', 'B', 'C', 'A', 'A', 'D' };

            char[] promoCode = new[] { 'C', 'D' };
           
            IPromoCodeEngine promoCodeEngine = new PromoCodeEngine(promoCodeProcessor,promoCodeDetails, skuDetails);
            float totalOrderValue = promoCodeEngine.GetTotalOderValue(allSelectedSkus);

            float expectedValue = 230;

            Assert.IsTrue(totalOrderValue == expectedValue , "Expected value is not matching with actual value.");



        }

        [TestMethod]
        public void Given_PromoCodeEngine_And_SetOfSelected_Skus_In_Small_Case_When_Call_GetTotalOrderValue_Then_Returns_Correct_Value()
        {
            IPromoCodeDetails promoCodeDetails = new PromoCodeDetails();
            IPromoCodeProcessor promoCodeProcessor = new PromoCodeProcessor(promoCodeDetails);
            ISkuDetails skuDetails = new SkuDetails();

            char[] allSelectedSkus = new char[] { 'a', 'b', 'c', 'a', 'a', 'd' };


            IPromoCodeEngine promoCodeEngine = new PromoCodeEngine(promoCodeProcessor, promoCodeDetails, skuDetails);
            float totalOrderValue = promoCodeEngine.GetTotalOderValue(allSelectedSkus);

            float expectedValue = 230;

            Assert.IsTrue(totalOrderValue == expectedValue, "Expected value is not matching with actual value.");



        }

        [TestMethod]
        public void Given_PromoCodeEngine_And_SetOfSelected_Skus_When_Add_Extra_PromoCode_And_then_Call_GetTotalOrderValue_Then_Returns_Correct_Value()
        {
            IPromoCodeDetails promoCodeDetails = new PromoCodeDetails();
            IPromoCodeProcessor promoCodeProcessor = new PromoCodeProcessor(promoCodeDetails);
            ISkuDetails skuDetails = new SkuDetails();
            skuDetails.AddAdditionalSkus('e' , 40);

            char[] allSelectedSkus = new char[] { 'a', 'b', 'c', 'a', 'a','e', 'd' };

            promoCodeDetails.AddExtraPromoCodes("ae",70);

            IPromoCodeEngine promoCodeEngine = new PromoCodeEngine(promoCodeProcessor, promoCodeDetails, skuDetails);
            float totalOrderValue = promoCodeEngine.GetTotalOderValue(allSelectedSkus);

            float expectedValue = 270;

            Assert.IsTrue(totalOrderValue == expectedValue, "Expected value is not matching with actual value.");
        }
    }
}
