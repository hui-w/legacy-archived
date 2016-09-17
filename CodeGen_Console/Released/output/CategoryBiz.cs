using System;
using System.Web;
using System.Web.Caching;
using System.Collections.Generic;

namespace QLike.SiteLite
{
    /// <summary>
    /// Business class of category
    /// Template: Business.tpl (ver 20090611)
    /// Please never modify this file manually
    /// </summary>
    public partial class CategoryBiz
    {
		#region CountAll()
        /// <summary>
        /// Get the count of categorys
        /// </summary>
        /// <returns>int</returns>
        public static int CountAll()
        {
            return CategoryDA.CountAll();
        }
        
        /// <summary>
        /// Get the count of categorys
        /// </summary>
        /// <returns></returns>
        public static int CountAll(bool enableCache)
        {
            if (enableCache)
            {
                //Cache key
                string cacheKey = "CategoryBiz.CountAll";

                if (HttpContext.Current.Cache.Get(cacheKey) != null)
                {
                    //Loaded from cache
                    return Convert.ToInt32(HttpContext.Current.Cache.Get(cacheKey));
                }
                else
                {
                    //Load from DA
                    int recordCount = CategoryDA.CountAll();

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
                return CategoryDA.CountAll();
            }
        }
        #endregion
        
        #region SumAll
        /// <summary>
        /// Get the sum of categorys
        /// </summary>
        /// <returns>double</returns>
        public static double SumAll(CategoryField sumField)
        {
            return CategoryDA.SumAll(sumField);
        }
        #endregion
        
        #region CountBy(CategoryField byField, object value)
        /// <summary>
        /// Get the count of categorys
        /// </summary>
        /// <returns>int</returns>
        public static int CountBy(CategoryField byField, object value)
        {
            return CategoryDA.CountBy(byField, value);
        }
        #endregion
        
        #region SumBy(CategoryField byField, object value, CategoryField sumField)
        /// <summary>
        /// Get the count of categorys
        /// </summary>
        /// <returns>double</returns>
        public static double SumBy(CategoryField byField, object value, CategoryField sumField)
        {
            return CategoryDA.SumBy(byField, value, sumField);
        }
        #endregion
        
        #region GetBy(CategoryField byField, object value)
        /// <summary>
        /// Get the specified category entity
        /// </summary>
        /// <returns>category eneity</returns>
        public static CategoryInfo GetBy(CategoryField byField, object value)
        {
			return CategoryBiz.GetBy(byField, value, false);
        }

        /// <summary>
        /// Get the specified category entity
        /// </summary>
        /// <returns>category eneity</returns>
        public static CategoryInfo GetBy(CategoryField byField, object value, bool enableCache)
        {
            //Check the value input
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return null;
            }

            if (enableCache)
            {
                //Cache key
                string cacheKey = string.Format("CategoryBiz.GetBy_{0}_{1}", byField.ToString(), value.ToString());

                CategoryInfo category = HttpContext.Current.Cache.Get(cacheKey) as CategoryInfo;
                if (category != null)
                {
                    //Loaded from cache
                    return category;
                }
                else
                {
                    //Load from DA
                    category = CategoryDA.GetBy(byField, value);

                    if (category != null)
                    {
                        //Insert into cache
                        HttpContext.Current.Cache.Insert(
                            cacheKey,
                            category,
                            null,
                            DateTime.Now.AddSeconds(Consts.CacheDuration),
                            Cache.NoSlidingExpiration
                            );

                        return category;
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
                return CategoryDA.GetBy(byField, value);
            }
        }
        #endregion
        
		#region SelectAll(CategoryField sortField, bool isDesc)
        /// <summary>
        /// Get the list of categorys
        /// </summary>
        /// <returns>list of categorys</returns>
        public static IList<CategoryInfo> SelectAll(CategoryField sortField, bool isDesc)
        {
            return CategoryBiz.SelectAll(sortField, isDesc, false);
        }
        
        /// <summary>
        /// Get the list of categorys
        /// </summary>
        /// <returns>list of categorys</returns>
        public static IList<CategoryInfo> SelectAll(CategoryField sortField, bool isDesc, bool enableCache)
        {
            if (enableCache)
            {
                //Cache key
                string cacheKey = string.Format("CategoryBiz.SelectAll_{0}_{1}", sortField.ToString(), isDesc.ToString());
                
                IList<CategoryInfo> categorys = HttpContext.Current.Cache.Get(cacheKey) as IList<CategoryInfo>;
                if (categorys != null)
                {
                    //Loaded from cache
                    return categorys;
                }
                else
                {
                    //Load from DA
                    categorys = CategoryDA.SelectAll(sortField, isDesc);

                    //Insert into cache
                    HttpContext.Current.Cache.Insert(
                        cacheKey, 
                        categorys, 
                        null, 
                        DateTime.Now.AddSeconds(Consts.CacheDuration), 
                        Cache.NoSlidingExpiration
                        );

                    return categorys;
                }
            }
            else
            {
                //Cache disabled
                return CategoryDA.SelectAll(sortField, isDesc);
            }
        }
        #endregion
        
        #region SelectAll(CategoryField sortField, bool isDesc, int pageSize, int pageIndex)
        /// <summary>
        /// Get the list of categorys
        /// </summary>
        /// <returns>list of categorys</returns>
        public static IList<CategoryInfo> SelectAll(CategoryField sortField, bool isDesc, int pageSize, int pageIndex)
        {
            return CategoryBiz.SelectAll(sortField, isDesc, pageSize, pageIndex, false);
        }
        
        /// <summary>
        /// Get the list of categorys
        /// </summary>
        /// <returns>list of categorys</returns>
        public static IList<CategoryInfo> SelectAll(CategoryField sortField, bool isDesc, int pageSize, int pageIndex, bool enableCache)
        {
            if (enableCache)
            {
                //Cache key
                string cacheKey = string.Format("CategoryBiz.SelectAll_{0}_{1}_{2}_{3}", 
                    sortField.ToString(), 
                    isDesc.ToString(),
                    pageSize.ToString(),
                    pageIndex.ToString()
                    );

                IList<CategoryInfo> categorys = HttpContext.Current.Cache.Get(cacheKey) as IList<CategoryInfo>;
                if (categorys != null)
                {
                    //Loaded from cache
                    return categorys;
                }
                else
                {
                    //Load from DA
                    categorys = CategoryDA.SelectAll(sortField, isDesc);

                    //Insert into cache
                    HttpContext.Current.Cache.Insert(
                        cacheKey,
                        categorys,
                        null,
                        DateTime.Now.AddSeconds(Consts.CacheDuration),
                        Cache.NoSlidingExpiration
                        );

                    return categorys;
                }
            }
            else
            {
                //Cache disabled
                return CategoryDA.SelectAll(sortField, isDesc, pageSize, pageIndex);
            }
        }
        #endregion

		#region SelectBy(CategoryField sortField, bool isDesc, CategoryField byField, object value)
        /// <summary>
        /// Get the specified category entity
        /// </summary>
        /// <returns>category eneity</returns>
        public static IList<CategoryInfo> SelectBy(CategoryField sortField, bool isDesc, CategoryField byField, object value)
        {
            return CategoryDA.SelectBy(sortField, isDesc, byField, value);
        }
        #endregion
        
        #region SelectBy(CategoryField sortField, bool isDesc, CategoryField byField, object value, int pageSize, int pageIndex);
        /// <summary>
        /// Get the specified category entity
        /// </summary>
        /// <returns>category eneity</returns>
        public static IList<CategoryInfo> SelectBy(CategoryField sortField, bool isDesc, CategoryField byField, object value, int pageSize, int pageIndex)
        {
            return CategoryDA.SelectBy(sortField, isDesc, byField, value, pageSize, pageIndex);
        }
        #endregion
        
        #region Insert
        /// <summary>
        /// Create an entity, and insert into database
        /// </summary>
        /// <returns>if inserted</returns>
        public static bool Insert(String categoryId, String alias, String displayName, Int32 displayOrder, Int32 contentCount, String levelCode, CategoryStatusEnum status, String parentId, String channelId)
        {
            CategoryInfo category = new CategoryInfo();
            category.CategoryId = categoryId;
            category.Alias = alias;
            category.DisplayName = displayName;
            category.DisplayOrder = displayOrder;
            category.ContentCount = contentCount;
            category.LevelCode = levelCode;
            category.Status = status;
            category.ParentId = parentId;
            category.ChannelId = channelId;

            return CategoryDA.Insert(category);
        }
        
        /// <summary>
        /// Insert an entity into database
        /// </summary>
        /// <param name="category">category entity</param>
        /// <returns></returns>
        public static bool Insert(CategoryInfo category)
        {
            return CategoryDA.Insert(category);
        }
        #endregion
        
        #region Update
        /// <summary>
        /// Update an entity in the database
        /// </summary>
        /// <returns>if updated</returns>
        public static bool Update(String categoryId, String alias, String displayName, Int32 displayOrder, Int32 contentCount, String levelCode, CategoryStatusEnum status, String parentId, String channelId)
        {
            CategoryInfo category = new CategoryInfo();
            category.CategoryId = categoryId;
            category.Alias = alias;
            category.DisplayName = displayName;
            category.DisplayOrder = displayOrder;
            category.ContentCount = contentCount;
            category.LevelCode = levelCode;
            category.Status = status;
            category.ParentId = parentId;
            category.ChannelId = channelId;

            return CategoryDA.Update(category);
        }
        
        /// <summary>
        /// Update an entity in the database
        /// </summary>
        /// <param name="category">category entity</param>
        /// <returns></returns>
        public static bool Update(CategoryInfo category)
        {
            return CategoryDA.Update(category);
        }
        #endregion
        
        #region Delete
        /// <summary>
        /// Delete an entity from database
        /// </summary>
        /// <returns>if deleted</returns>
        public static bool Delete(String categoryId)
        {
            return CategoryDA.Delete(categoryId);
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
        public static IList<CategoryInfo> Search(SortStrategy strategy, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        {
            return CategoryDA.Search(strategy, pageSize, pageIndex,conditions);
        }
        #endregion
        
        #region Search(CategoryField sortField, bool isDesc, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="sortField">Sort field</param>
        /// <param name="isDesc">Desc or Asc</param>
        /// <param name="conditions">Search conditions</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>Item list</returns>
        public static IList<CategoryInfo> Search(CategoryField sortField, bool isDesc, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        {
            return CategoryDA.Search(new SortStrategy(new SortItem(sortField.ToString(), isDesc)), pageSize, pageIndex,conditions);
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
        public static IList<CategoryInfo> Search(SortStrategy strategy, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return CategoryDA.Search(strategy, pageSize, pageIndex, condigitonList);
        }
        #endregion

		#region Search(CategoryField sortField, bool isDesc, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="sortField">Sort field</param>
        /// <param name="isDesc">Desc or Asc</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="conditions">Search conditions</param>
        /// <returns>Item list</returns>
        public static IList<CategoryInfo> Search(CategoryField sortField, bool isDesc, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return CategoryDA.Search(new SortStrategy(new SortItem(sortField.ToString(), isDesc)), pageSize, pageIndex, condigitonList);
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
            return CategoryDA.CountForSearch(conditions);
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
            return CategoryDA.CountForSearch(condigitonList);
        }
        #endregion
        
        #region SumForSearch(CategoryField sumField, List<ISearchCondition> conditions)
        /// <summary>
        /// Sum for search
        /// </summary>
        /// <returns>Sum</returns>
        public static double SumForSearch(CategoryField sumField, List<ISearchCondition> conditions)
        {
            return CategoryDA.SumForSearch(sumField, conditions);
        }

        /// <summary>
        /// Sum for search
        /// </summary>
        /// <returns>Sum</returns>
        public static double SumForSearch(CategoryField sumField, params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return CategoryDA.SumForSearch(sumField, condigitonList);
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
        public static bool IncreaseField(CategoryField byField, object byValue, CategoryField increaseField, object increaseValue)
        {
            return CategoryDA.IncreaseField(byField, byValue, increaseField, increaseValue);
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
        public static bool UpdateField(CategoryField byField, object byValue, CategoryField updateField, object newValue)
        {
            return CategoryDA.UpdateField(byField, byValue, updateField, newValue);
        }
        #endregion
    }//end of class
}
