using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObj)
        {
            IsSusscessed = true;
            ResultObj = resultObj;
        }

        public ApiSuccessResult()
        {
            IsSusscessed = true;
        }
    }
}