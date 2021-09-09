using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolioMonitoringSystem.Helpers
{
    public static class CommonMethods
    {
        public static (IList<T>, long, long) GetPagedData<T>(IQueryable<T> data, int pageSize = 0, int curretPage = 0) where T : class
        {
            try
            {
                pageSize = pageSize == 0 ? 50 : pageSize;
                curretPage = curretPage > 0 ? curretPage - 1 : 0;
                var startIndex = pageSize * curretPage;
                var pagedData = data.ToList();

                var selectedData = pagedData
                    .Skip(startIndex)
                    .Take(pageSize)
                    .ToList();

                var pagesCount = Math.Ceiling(Convert.ToDecimal(pagedData.Count)) / Convert.ToDecimal(pageSize);

                return (selectedData, (long)pagesCount, (long)pagedData.Count);
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                throw ex;
            }
        }
    }
}
