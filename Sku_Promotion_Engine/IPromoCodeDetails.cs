﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sku_Promotion_Engine
{
    internal interface IPromoCodeDetails
    {
        IDictionary<char[], float> GetListOfPromoCodes();
    }
}