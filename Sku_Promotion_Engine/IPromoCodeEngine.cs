﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sku_Promotion_Engine
{
    internal interface IPromoCodeEngine
    {
        float GetTotalOderValue(char[] selectedSkus);
    }
}
