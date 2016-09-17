using System;
using System.Web;
using System.Web.Caching;
using System.Collections.Generic;

namespace QLike.SiteLite
{
    /// <summary>
    /// Business class of channel
    /// Template: Business.tpl (ver 20090611)
    /// Please never modify this file manually
    /// </summary>
    public partial class ChannelBiz
    {
		#region CountAll()
        /// <summary>
        /// Get the count of channels
        /// </summary>
        /// <returns>int</returns>
        public static int CountAll()
        {
            return ChannelDA.CountAll();
        }
        
        /// <summary>
        /// Get the count of channels
        /// </summary>
        /// <returns></returns>
        public static int CountAll(bool enableCache)
        {
            if (enableCache)
            {
                //Cache key
                string cacheKey = "ChannelBiz.CountAll";

                if (HttpContext.Current.Cache.Get(cacheKey) != null)
                {
                    //Loaded from cache
                    return Convert.ToInt32(HttpContext.Current.Cache.Get(cacheKey));
                }
                else
                {
                    //Load from DA
                    int recordCount = ChannelDA.CountAll();

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
                return ChannelDA.CountAll();
            }
        }
        #endregion
        
        #region SumAll
        /// <summary>
        /// Get the sum of channels
        /// </summary>
        /// <returns>double</returns>
        public static double SumAll(ChannelField sumField)
        {
            return ChannelDA.SumAll(sumField);
        }
        #endregion
        
        #region CountBy(ChannelField byField, object value)
        /// <summary>
        /// Get the count of channels
        /// </summary>
        /// <returns>int</returns>
        public static int CountBy(ChannelField byField, object value)
        {
            return ChannelDA.CountBy(byField, value);
        }
        #endregion
        
        #region SumBy(ChannelField byField, object value, ChannelField sumField)
        /// <summary>
        /// Get the count of channels
        /// </summary>
        /// <returns>double</returns>
        public static double SumBy(ChannelField byField, object value, ChannelField sumField)
        {
            return ChannelDA.SumBy(byField, value, sumField);
        }
        #endregion
        
        #region GetBy(ChannelField byField, object value)
        /// <summary>
        /// Get the specified channel entity
        /// </summary>
        /// <returns>channel eneity</returns>
        public static ChannelInfo GetBy(ChannelField byField, object value)
        {
			return ChannelBiz.GetBy(byField, value, false);
        }

        /// <summary>
        /// Get the specified channel entity
        /// </summary>
        /// <returns>channel eneity</returns>
        public static ChannelInfo GetBy(ChannelField byField, object value, bool enableCache)
        {
            //Check the value input
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return null;
            }

            if (enableCache)
            {
                //Cache key
                string cacheKey = string.Format("ChannelBiz.GetBy_{0}_{1}", byField.ToString(), value.ToString());

                ChannelInfo channel = HttpContext.Current.Cache.Get(cacheKey) as ChannelInfo;
                if (channel != null)
                {
                    //Loaded from cache
                    return channel;
                }
                else
                {
                    //Load from DA
                    channel = ChannelDA.GetBy(byField, value);

                    if (channel != null)
                    {
                        //Insert into cache
                        HttpContext.Current.Cache.Insert(
                            cacheKey,
                            channel,
                            null,
                            DateTime.Now.AddSeconds(Consts.CacheDuration),
                            Cache.NoSlidingExpiration
                            );

                        return channel;
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
                return ChannelDA.GetBy(byField, value);
            }
        }
        #endregion
        
		#region SelectAll(ChannelField sortField, bool isDesc)
        /// <summary>
        /// Get the list of channels
        /// </summary>
        /// <returns>list of channels</returns>
        public static IList<ChannelInfo> SelectAll(ChannelField sortField, bool isDesc)
        {
            return ChannelBiz.SelectAll(sortField, isDesc, false);
        }
        
        /// <summary>
        /// Get the list of channels
        /// </summary>
        /// <returns>list of channels</returns>
        public static IList<ChannelInfo> SelectAll(ChannelField sortField, bool isDesc, bool enableCache)
        {
            if (enableCache)
            {
                //Cache key
                string cacheKey = string.Format("ChannelBiz.SelectAll_{0}_{1}", sortField.ToString(), isDesc.ToString());
                
                IList<ChannelInfo> channels = HttpContext.Current.Cache.Get(cacheKey) as IList<ChannelInfo>;
                if (channels != null)
                {
                    //Loaded from cache
                    return channels;
                }
                else
                {
                    //Load from DA
                    channels = ChannelDA.SelectAll(sortField, isDesc);

                    //Insert into cache
                    HttpContext.Current.Cache.Insert(
                        cacheKey, 
                        channels, 
                        null, 
                        DateTime.Now.AddSeconds(Consts.CacheDuration), 
                        Cache.NoSlidingExpiration
                        );

                    return channels;
                }
            }
            else
            {
                //Cache disabled
                return ChannelDA.SelectAll(sortField, isDesc);
            }
        }
        #endregion
        
        #region SelectAll(ChannelField sortField, bool isDesc, int pageSize, int pageIndex)
        /// <summary>
        /// Get the list of channels
        /// </summary>
        /// <returns>list of channels</returns>
        public static IList<ChannelInfo> SelectAll(ChannelField sortField, bool isDesc, int pageSize, int pageIndex)
        {
            return ChannelBiz.SelectAll(sortField, isDesc, pageSize, pageIndex, false);
        }
        
        /// <summary>
        /// Get the list of channels
        /// </summary>
        /// <returns>list of channels</returns>
        public static IList<ChannelInfo> SelectAll(ChannelField sortField, bool isDesc, int pageSize, int pageIndex, bool enableCache)
        {
            if (enableCache)
            {
                //Cache key
                string cacheKey = string.Format("ChannelBiz.SelectAll_{0}_{1}_{2}_{3}", 
                    sortField.ToString(), 
                    isDesc.ToString(),
                    pageSize.ToString(),
                    pageIndex.ToString()
                    );

                IList<ChannelInfo> channels = HttpContext.Current.Cache.Get(cacheKey) as IList<ChannelInfo>;
                if (channels != null)
                {
                    //Loaded from cache
                    return channels;
                }
                else
                {
                    //Load from DA
                    channels = ChannelDA.SelectAll(sortField, isDesc);

                    //Insert into cache
                    HttpContext.Current.Cache.Insert(
                        cacheKey,
                        channels,
                        null,
                        DateTime.Now.AddSeconds(Consts.CacheDuration),
                        Cache.NoSlidingExpiration
                        );

                    return channels;
                }
            }
            else
            {
                //Cache disabled
                return ChannelDA.SelectAll(sortField, isDesc, pageSize, pageIndex);
            }
        }
        #endregion

		#region SelectBy(ChannelField sortField, bool isDesc, ChannelField byField, object value)
        /// <summary>
        /// Get the specified channel entity
        /// </summary>
        /// <returns>channel eneity</returns>
        public static IList<ChannelInfo> SelectBy(ChannelField sortField, bool isDesc, ChannelField byField, object value)
        {
            return ChannelDA.SelectBy(sortField, isDesc, byField, value);
        }
        #endregion
        
        #region SelectBy(ChannelField sortField, bool isDesc, ChannelField byField, object value, int pageSize, int pageIndex);
        /// <summary>
        /// Get the specified channel entity
        /// </summary>
        /// <returns>channel eneity</returns>
        public static IList<ChannelInfo> SelectBy(ChannelField sortField, bool isDesc, ChannelField byField, object value, int pageSize, int pageIndex)
        {
            return ChannelDA.SelectBy(sortField, isDesc, byField, value, pageSize, pageIndex);
        }
        #endregion
        
        #region Insert
        /// <summary>
        /// Create an entity, and insert into database
        /// </summary>
        /// <returns>if inserted</returns>
        public static bool Insert(String channelId, String displayName, String url, String signInName, String password, String description, Int64 visitTime, String visitIp, Int32 visitCount, ChannelStatusEnum status)
        {
            ChannelInfo channel = new ChannelInfo();
            channel.ChannelId = channelId;
            channel.DisplayName = displayName;
            channel.Url = url;
            channel.SignInName = signInName;
            channel.Password = password;
            channel.Description = description;
            channel.VisitTime = visitTime;
            channel.VisitIp = visitIp;
            channel.VisitCount = visitCount;
            channel.Status = status;

            return ChannelDA.Insert(channel);
        }
        
        /// <summary>
        /// Insert an entity into database
        /// </summary>
        /// <param name="channel">channel entity</param>
        /// <returns></returns>
        public static bool Insert(ChannelInfo channel)
        {
            return ChannelDA.Insert(channel);
        }
        #endregion
        
        #region Update
        /// <summary>
        /// Update an entity in the database
        /// </summary>
        /// <returns>if updated</returns>
        public static bool Update(String channelId, String displayName, String url, String signInName, String password, String description, Int64 visitTime, String visitIp, Int32 visitCount, ChannelStatusEnum status)
        {
            ChannelInfo channel = new ChannelInfo();
            channel.ChannelId = channelId;
            channel.DisplayName = displayName;
            channel.Url = url;
            channel.SignInName = signInName;
            channel.Password = password;
            channel.Description = description;
            channel.VisitTime = visitTime;
            channel.VisitIp = visitIp;
            channel.VisitCount = visitCount;
            channel.Status = status;

            return ChannelDA.Update(channel);
        }
        
        /// <summary>
        /// Update an entity in the database
        /// </summary>
        /// <param name="channel">channel entity</param>
        /// <returns></returns>
        public static bool Update(ChannelInfo channel)
        {
            return ChannelDA.Update(channel);
        }
        #endregion
        
        #region Delete
        /// <summary>
        /// Delete an entity from database
        /// </summary>
        /// <returns>if deleted</returns>
        public static bool Delete(String channelId)
        {
            return ChannelDA.Delete(channelId);
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
        public static IList<ChannelInfo> Search(SortStrategy strategy, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        {
            return ChannelDA.Search(strategy, pageSize, pageIndex,conditions);
        }
        #endregion
        
        #region Search(ChannelField sortField, bool isDesc, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="sortField">Sort field</param>
        /// <param name="isDesc">Desc or Asc</param>
        /// <param name="conditions">Search conditions</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>Item list</returns>
        public static IList<ChannelInfo> Search(ChannelField sortField, bool isDesc, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        {
            return ChannelDA.Search(new SortStrategy(new SortItem(sortField.ToString(), isDesc)), pageSize, pageIndex,conditions);
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
        public static IList<ChannelInfo> Search(SortStrategy strategy, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return ChannelDA.Search(strategy, pageSize, pageIndex, condigitonList);
        }
        #endregion

		#region Search(ChannelField sortField, bool isDesc, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="sortField">Sort field</param>
        /// <param name="isDesc">Desc or Asc</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="conditions">Search conditions</param>
        /// <returns>Item list</returns>
        public static IList<ChannelInfo> Search(ChannelField sortField, bool isDesc, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return ChannelDA.Search(new SortStrategy(new SortItem(sortField.ToString(), isDesc)), pageSize, pageIndex, condigitonList);
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
            return ChannelDA.CountForSearch(conditions);
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
            return ChannelDA.CountForSearch(condigitonList);
        }
        #endregion
        
        #region SumForSearch(ChannelField sumField, List<ISearchCondition> conditions)
        /// <summary>
        /// Sum for search
        /// </summary>
        /// <returns>Sum</returns>
        public static double SumForSearch(ChannelField sumField, List<ISearchCondition> conditions)
        {
            return ChannelDA.SumForSearch(sumField, conditions);
        }

        /// <summary>
        /// Sum for search
        /// </summary>
        /// <returns>Sum</returns>
        public static double SumForSearch(ChannelField sumField, params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return ChannelDA.SumForSearch(sumField, condigitonList);
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
        public static bool IncreaseField(ChannelField byField, object byValue, ChannelField increaseField, object increaseValue)
        {
            return ChannelDA.IncreaseField(byField, byValue, increaseField, increaseValue);
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
        public static bool UpdateField(ChannelField byField, object byValue, ChannelField updateField, object newValue)
        {
            return ChannelDA.UpdateField(byField, byValue, updateField, newValue);
        }
        #endregion
    }//end of class
}
