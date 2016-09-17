using System;
using System.Web;
using System.Web.Caching;
using System.Collections.Generic;

namespace QLike.SiteLite
{
    /// <summary>
    /// Business class of [=Object]
    /// Template: Business.tpl (ver 20090611)
    /// Please never modify this file manually
    /// </summary>
    public partial class [=Class]Biz
    {
		#region CountAll()
        /// <summary>
        /// Get the count of [=Object]s
        /// </summary>
        /// <returns>int</returns>
        public static int CountAll()
        {
            return [=Class]DA.CountAll();
        }
        
        /// <summary>
        /// Get the count of [=Object]s
        /// </summary>
        /// <returns></returns>
        public static int CountAll(bool enableCache)
        {
            if (enableCache)
            {
                //Cache key
                string cacheKey = "[=Class]Biz.CountAll";

                if (HttpContext.Current.Cache.Get(cacheKey) != null)
                {
                    //Loaded from cache
                    return Convert.ToInt32(HttpContext.Current.Cache.Get(cacheKey));
                }
                else
                {
                    //Load from DA
                    int recordCount = [=Class]DA.CountAll();

                    //Insert into cache
                    HttpContext.Current.Cache.Insert(
                        cacheKey,
                        recordCount,
                        null,
                        DateTime.Now.AddSeconds(Consts.CacheDuration),
                        Cache.NoSlidingExpiration
                        );

                    return recordCount;
                }
            }
            else
            {
                //Cache disabled
                return [=Class]DA.CountAll();
            }
        }
        #endregion
        
        #region SumAll
        /// <summary>
        /// Get the sum of [=Object]s
        /// </summary>
        /// <returns>double</returns>
        public static double SumAll([=Class]Field sumField)
        {
            return [=Class]DA.SumAll(sumField);
        }
        #endregion
        
        #region CountBy([=Class]Field byField, object value)
        /// <summary>
        /// Get the count of [=Object]s
        /// </summary>
        /// <returns>int</returns>
        public static int CountBy([=Class]Field byField, object value)
        {
            return [=Class]DA.CountBy(byField, value);
        }
        #endregion
        
        #region SumBy([=Class]Field byField, object value, [=Class]Field sumField)
        /// <summary>
        /// Get the count of [=Object]s
        /// </summary>
        /// <returns>double</returns>
        public static double SumBy([=Class]Field byField, object value, [=Class]Field sumField)
        {
            return [=Class]DA.SumBy(byField, value, sumField);
        }
        #endregion
        
        #region GetBy([=Class]Field byField, object value)
        /// <summary>
        /// Get the specified [=Object] entity
        /// </summary>
        /// <returns>[=Object] eneity</returns>
        public static [=Class]Info GetBy([=Class]Field byField, object value)
        {
			return [=Class]Biz.GetBy(byField, value, false);
        }

        /// <summary>
        /// Get the specified [=Object] entity
        /// </summary>
        /// <returns>[=Object] eneity</returns>
        public static [=Class]Info GetBy([=Class]Field byField, object value, bool enableCache)
        {
            //Check the value input
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return null;
            }

            if (enableCache)
            {
                //Cache key
                string cacheKey = string.Format("[=Class]Biz.GetBy_{0}_{1}", byField.ToString(), value.ToString());

                [=Class]Info [=Object] = HttpContext.Current.Cache.Get(cacheKey) as [=Class]Info;
                if ([=Object] != null)
                {
                    //Loaded from cache
                    return [=Object];
                }
                else
                {
                    //Load from DA
                    [=Object] = [=Class]DA.GetBy(byField, value);

                    if ([=Object] != null)
                    {
                        //Insert into cache
                        HttpContext.Current.Cache.Insert(
                            cacheKey,
                            [=Object],
                            null,
                            DateTime.Now.AddSeconds(Consts.CacheDuration),
                            Cache.NoSlidingExpiration
                            );

                        return [=Object];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                //Cache disabled
                return [=Class]DA.GetBy(byField, value);
            }
        }
        #endregion
        
		#region SelectAll([=Class]Field sortField, bool isDesc)
        /// <summary>
        /// Get the list of [=Object]s
        /// </summary>
        /// <returns>list of [=Object]s</returns>
        public static IList<[=Class]Info> SelectAll([=Class]Field sortField, bool isDesc)
        {
            return [=Class]Biz.SelectAll(sortField, isDesc, false);
        }
        
        /// <summary>
        /// Get the list of [=Object]s
        /// </summary>
        /// <returns>list of [=Object]s</returns>
        public static IList<[=Class]Info> SelectAll([=Class]Field sortField, bool isDesc, bool enableCache)
        {
            if (enableCache)
            {
                //Cache key
                string cacheKey = string.Format("[=Class]Biz.SelectAll_{0}_{1}", sortField.ToString(), isDesc.ToString());
                
                IList<[=Class]Info> [=Object]s = HttpContext.Current.Cache.Get(cacheKey) as IList<[=Class]Info>;
                if ([=Object]s != null)
                {
                    //Loaded from cache
                    return [=Object]s;
                }
                else
                {
                    //Load from DA
                    [=Object]s = [=Class]DA.SelectAll(sortField, isDesc);

                    //Insert into cache
                    HttpContext.Current.Cache.Insert(
                        cacheKey, 
                        [=Object]s, 
                        null, 
                        DateTime.Now.AddSeconds(Consts.CacheDuration), 
                        Cache.NoSlidingExpiration
                        );

                    return [=Object]s;
                }
            }
            else
            {
                //Cache disabled
                return [=Class]DA.SelectAll(sortField, isDesc);
            }
        }
        #endregion
        
        #region SelectAll([=Class]Field sortField, bool isDesc, int pageSize, int pageIndex)
        /// <summary>
        /// Get the list of [=Object]s
        /// </summary>
        /// <returns>list of [=Object]s</returns>
        public static IList<[=Class]Info> SelectAll([=Class]Field sortField, bool isDesc, int pageSize, int pageIndex)
        {
            return [=Class]Biz.SelectAll(sortField, isDesc, pageSize, pageIndex, false);
        }
        
        /// <summary>
        /// Get the list of [=Object]s
        /// </summary>
        /// <returns>list of [=Object]s</returns>
        public static IList<[=Class]Info> SelectAll([=Class]Field sortField, bool isDesc, int pageSize, int pageIndex, bool enableCache)
        {
            if (enableCache)
            {
                //Cache key
                string cacheKey = string.Format("[=Class]Biz.SelectAll_{0}_{1}_{2}_{3}", 
                    sortField.ToString(), 
                    isDesc.ToString(),
                    pageSize.ToString(),
                    pageIndex.ToString()
                    );

                IList<[=Class]Info> [=Object]s = HttpContext.Current.Cache.Get(cacheKey) as IList<[=Class]Info>;
                if ([=Object]s != null)
                {
                    //Loaded from cache
                    return [=Object]s;
                }
                else
                {
                    //Load from DA
                    [=Object]s = [=Class]DA.SelectAll(sortField, isDesc);

                    //Insert into cache
                    HttpContext.Current.Cache.Insert(
                        cacheKey,
                        [=Object]s,
                        null,
                        DateTime.Now.AddSeconds(Consts.CacheDuration),
                        Cache.NoSlidingExpiration
                        );

                    return [=Object]s;
                }
            }
            else
            {
                //Cache disabled
                return [=Class]DA.SelectAll(sortField, isDesc, pageSize, pageIndex);
            }
        }
        #endregion

		#region SelectBy([=Class]Field sortField, bool isDesc, [=Class]Field byField, object value)
        /// <summary>
        /// Get the specified [=Object] entity
        /// </summary>
        /// <returns>[=Object] eneity</returns>
        public static IList<[=Class]Info> SelectBy([=Class]Field sortField, bool isDesc, [=Class]Field byField, object value)
        {
            return [=Class]DA.SelectBy(sortField, isDesc, byField, value);
        }
        #endregion
        
        #region SelectBy([=Class]Field sortField, bool isDesc, [=Class]Field byField, object value, int pageSize, int pageIndex);
        /// <summary>
        /// Get the specified [=Object] entity
        /// </summary>
        /// <returns>[=Object] eneity</returns>
        public static IList<[=Class]Info> SelectBy([=Class]Field sortField, bool isDesc, [=Class]Field byField, object value, int pageSize, int pageIndex)
        {
            return [=Class]DA.SelectBy(sortField, isDesc, byField, value, pageSize, pageIndex);
        }
        #endregion
        
        #region Insert
        /// <summary>
        /// Create an entity, and insert into database
        /// </summary>
        /// <returns>if inserted</returns>
        public static bool Insert([#Loop Separator=, ][=Type] [=Field][#Loop/])
        {
            [=Class]Info [=Object] = new [=Class]Info();[#Loop]
            [=Object].[=Property] = [=Field];[#Loop/]

            return [=Class]DA.Insert([=Object]);
        }
        
        /// <summary>
        /// Insert an entity into database
        /// </summary>
        /// <param name="[=Object]">[=Object] entity</param>
        /// <returns></returns>
        public static bool Insert([=Class]Info [=Object])
        {
            return [=Class]DA.Insert([=Object]);
        }
        #endregion
        
        #region Update
        /// <summary>
        /// Update an entity in the database
        /// </summary>
        /// <returns>if updated</returns>
        public static bool Update([#Loop Separator=, ][=Type] [=Field][#Loop/])
        {
            [=Class]Info [=Object] = new [=Class]Info();[#Loop]
            [=Object].[=Property] = [=Field];[#Loop/]

            return [=Class]DA.Update([=Object]);
        }
        
        /// <summary>
        /// Update an entity in the database
        /// </summary>
        /// <param name="[=Object]">[=Object] entity</param>
        /// <returns></returns>
        public static bool Update([=Class]Info [=Object])
        {
            return [=Class]DA.Update([=Object]);
        }
        #endregion
        
        #region Delete
        /// <summary>
        /// Delete an entity from database
        /// </summary>
        /// <returns>if deleted</returns>
        public static bool Delete([#Loop Type=Primary Separator=, ][=Type] [=Field][#Loop/])
        {
            return [=Class]DA.Delete([#Loop Type=Primary Separator=, ][=Field][#Loop/]);
        }
        #endregion
        
        #region Search(SortStrategy strategy, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="strategy">Sort strategy</param>
        /// <param name="conditions">Search conditions</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>Item list</returns>
        public static IList<[=Class]Info> Search(SortStrategy strategy, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        {
            return [=Class]DA.Search(strategy, pageSize, pageIndex,conditions);
        }
        #endregion
        
        #region Search([=Class]Field sortField, bool isDesc, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="sortField">Sort field</param>
        /// <param name="isDesc">Desc or Asc</param>
        /// <param name="conditions">Search conditions</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>Item list</returns>
        public static IList<[=Class]Info> Search([=Class]Field sortField, bool isDesc, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        {
            return [=Class]DA.Search(new SortStrategy(new SortItem(sortField.ToString(), isDesc)), pageSize, pageIndex,conditions);
        }
        #endregion
        
        #region Search(SortStrategy strategy, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="strategy">Sort strategy</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="conditions">Search conditions</param>
        /// <returns>Item list</returns>
        public static IList<[=Class]Info> Search(SortStrategy strategy, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return [=Class]DA.Search(strategy, pageSize, pageIndex, condigitonList);
        }
        #endregion

		#region Search([=Class]Field sortField, bool isDesc, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="sortField">Sort field</param>
        /// <param name="isDesc">Desc or Asc</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="conditions">Search conditions</param>
        /// <returns>Item list</returns>
        public static IList<[=Class]Info> Search([=Class]Field sortField, bool isDesc, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return [=Class]DA.Search(new SortStrategy(new SortItem(sortField.ToString(), isDesc)), pageSize, pageIndex, condigitonList);
        }
        #endregion
        
        #region CountForSearch(List<ISearchCondition> conditions)
        /// <summary>
        /// Count for search
        /// </summary>
        /// <param name="conditions">Search conditions</param>
        /// <returns>Record count</returns>
        public static int CountForSearch(List<ISearchCondition> conditions)
        {
            return [=Class]DA.CountForSearch(conditions);
        }

        /// <summary>
        /// Count for search
        /// </summary>
        /// <param name="conditions">Search conditions</param>
        /// <returns>Record count</returns>
        public static int CountForSearch(params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return [=Class]DA.CountForSearch(condigitonList);
        }
        #endregion
        
        #region SumForSearch([=Class]Field sumField, List<ISearchCondition> conditions)
        /// <summary>
        /// Sum for search
        /// </summary>
        /// <returns>Sum</returns>
        public static double SumForSearch([=Class]Field sumField, List<ISearchCondition> conditions)
        {
            return [=Class]DA.SumForSearch(sumField, conditions);
        }

        /// <summary>
        /// Sum for search
        /// </summary>
        /// <returns>Sum</returns>
        public static double SumForSearch([=Class]Field sumField, params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return [=Class]DA.SumForSearch(sumField, condigitonList);
        }
        #endregion
        
		#region IncreaseField()
        /// <summary>
        /// Increase the field
        /// </summary>
        /// <param name="byField"></param>
        /// <param name="byValue"></param>
        /// <param name="increaseField"></param>
        /// <param name="increaseValue"></param>
        /// <returns></returns>
        public static bool IncreaseField([=Class]Field byField, object byValue, [=Class]Field increaseField, object increaseValue)
        {
            return [=Class]DA.IncreaseField(byField, byValue, increaseField, increaseValue);
        }
        #endregion

        #region UpdateField()
        /// <summary>
        /// Update the field
        /// </summary>
        /// <param name="byField"></param>
        /// <param name="byValue"></param>
        /// <param name="updateField"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static bool UpdateField([=Class]Field byField, object byValue, [=Class]Field updateField, object newValue)
        {
            return [=Class]DA.UpdateField(byField, byValue, updateField, newValue);
        }
        #endregion
    }//end of class
}
