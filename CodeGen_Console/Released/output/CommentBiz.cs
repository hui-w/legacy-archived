using System;
using System.Web;
using System.Web.Caching;
using System.Collections.Generic;

namespace QLike.SiteLite
{
    /// <summary>
    /// Business class of comment
    /// Template: Business.tpl (ver 20090611)
    /// Please never modify this file manually
    /// </summary>
    public partial class CommentBiz
    {
		#region CountAll()
        /// <summary>
        /// Get the count of comments
        /// </summary>
        /// <returns>int</returns>
        public static int CountAll()
        {
            return CommentDA.CountAll();
        }
        
        /// <summary>
        /// Get the count of comments
        /// </summary>
        /// <returns></returns>
        public static int CountAll(bool enableCache)
        {
            if (enableCache)
            {
                //Cache key
                string cacheKey = "CommentBiz.CountAll";

                if (HttpContext.Current.Cache.Get(cacheKey) != null)
                {
                    //Loaded from cache
                    return Convert.ToInt32(HttpContext.Current.Cache.Get(cacheKey));
                }
                else
                {
                    //Load from DA
                    int recordCount = CommentDA.CountAll();

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
                return CommentDA.CountAll();
            }
        }
        #endregion
        
        #region SumAll
        /// <summary>
        /// Get the sum of comments
        /// </summary>
        /// <returns>double</returns>
        public static double SumAll(CommentField sumField)
        {
            return CommentDA.SumAll(sumField);
        }
        #endregion
        
        #region CountBy(CommentField byField, object value)
        /// <summary>
        /// Get the count of comments
        /// </summary>
        /// <returns>int</returns>
        public static int CountBy(CommentField byField, object value)
        {
            return CommentDA.CountBy(byField, value);
        }
        #endregion
        
        #region SumBy(CommentField byField, object value, CommentField sumField)
        /// <summary>
        /// Get the count of comments
        /// </summary>
        /// <returns>double</returns>
        public static double SumBy(CommentField byField, object value, CommentField sumField)
        {
            return CommentDA.SumBy(byField, value, sumField);
        }
        #endregion
        
        #region GetBy(CommentField byField, object value)
        /// <summary>
        /// Get the specified comment entity
        /// </summary>
        /// <returns>comment eneity</returns>
        public static CommentInfo GetBy(CommentField byField, object value)
        {
			return CommentBiz.GetBy(byField, value, false);
        }

        /// <summary>
        /// Get the specified comment entity
        /// </summary>
        /// <returns>comment eneity</returns>
        public static CommentInfo GetBy(CommentField byField, object value, bool enableCache)
        {
            //Check the value input
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return null;
            }

            if (enableCache)
            {
                //Cache key
                string cacheKey = string.Format("CommentBiz.GetBy_{0}_{1}", byField.ToString(), value.ToString());

                CommentInfo comment = HttpContext.Current.Cache.Get(cacheKey) as CommentInfo;
                if (comment != null)
                {
                    //Loaded from cache
                    return comment;
                }
                else
                {
                    //Load from DA
                    comment = CommentDA.GetBy(byField, value);

                    if (comment != null)
                    {
                        //Insert into cache
                        HttpContext.Current.Cache.Insert(
                            cacheKey,
                            comment,
                            null,
                            DateTime.Now.AddSeconds(Consts.CacheDuration),
                            Cache.NoSlidingExpiration
                            );

                        return comment;
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
                return CommentDA.GetBy(byField, value);
            }
        }
        #endregion
        
		#region SelectAll(CommentField sortField, bool isDesc)
        /// <summary>
        /// Get the list of comments
        /// </summary>
        /// <returns>list of comments</returns>
        public static IList<CommentInfo> SelectAll(CommentField sortField, bool isDesc)
        {
            return CommentBiz.SelectAll(sortField, isDesc, false);
        }
        
        /// <summary>
        /// Get the list of comments
        /// </summary>
        /// <returns>list of comments</returns>
        public static IList<CommentInfo> SelectAll(CommentField sortField, bool isDesc, bool enableCache)
        {
            if (enableCache)
            {
                //Cache key
                string cacheKey = string.Format("CommentBiz.SelectAll_{0}_{1}", sortField.ToString(), isDesc.ToString());
                
                IList<CommentInfo> comments = HttpContext.Current.Cache.Get(cacheKey) as IList<CommentInfo>;
                if (comments != null)
                {
                    //Loaded from cache
                    return comments;
                }
                else
                {
                    //Load from DA
                    comments = CommentDA.SelectAll(sortField, isDesc);

                    //Insert into cache
                    HttpContext.Current.Cache.Insert(
                        cacheKey, 
                        comments, 
                        null, 
                        DateTime.Now.AddSeconds(Consts.CacheDuration), 
                        Cache.NoSlidingExpiration
                        );

                    return comments;
                }
            }
            else
            {
                //Cache disabled
                return CommentDA.SelectAll(sortField, isDesc);
            }
        }
        #endregion
        
        #region SelectAll(CommentField sortField, bool isDesc, int pageSize, int pageIndex)
        /// <summary>
        /// Get the list of comments
        /// </summary>
        /// <returns>list of comments</returns>
        public static IList<CommentInfo> SelectAll(CommentField sortField, bool isDesc, int pageSize, int pageIndex)
        {
            return CommentBiz.SelectAll(sortField, isDesc, pageSize, pageIndex, false);
        }
        
        /// <summary>
        /// Get the list of comments
        /// </summary>
        /// <returns>list of comments</returns>
        public static IList<CommentInfo> SelectAll(CommentField sortField, bool isDesc, int pageSize, int pageIndex, bool enableCache)
        {
            if (enableCache)
            {
                //Cache key
                string cacheKey = string.Format("CommentBiz.SelectAll_{0}_{1}_{2}_{3}", 
                    sortField.ToString(), 
                    isDesc.ToString(),
                    pageSize.ToString(),
                    pageIndex.ToString()
                    );

                IList<CommentInfo> comments = HttpContext.Current.Cache.Get(cacheKey) as IList<CommentInfo>;
                if (comments != null)
                {
                    //Loaded from cache
                    return comments;
                }
                else
                {
                    //Load from DA
                    comments = CommentDA.SelectAll(sortField, isDesc);

                    //Insert into cache
                    HttpContext.Current.Cache.Insert(
                        cacheKey,
                        comments,
                        null,
                        DateTime.Now.AddSeconds(Consts.CacheDuration),
                        Cache.NoSlidingExpiration
                        );

                    return comments;
                }
            }
            else
            {
                //Cache disabled
                return CommentDA.SelectAll(sortField, isDesc, pageSize, pageIndex);
            }
        }
        #endregion

		#region SelectBy(CommentField sortField, bool isDesc, CommentField byField, object value)
        /// <summary>
        /// Get the specified comment entity
        /// </summary>
        /// <returns>comment eneity</returns>
        public static IList<CommentInfo> SelectBy(CommentField sortField, bool isDesc, CommentField byField, object value)
        {
            return CommentDA.SelectBy(sortField, isDesc, byField, value);
        }
        #endregion
        
        #region SelectBy(CommentField sortField, bool isDesc, CommentField byField, object value, int pageSize, int pageIndex);
        /// <summary>
        /// Get the specified comment entity
        /// </summary>
        /// <returns>comment eneity</returns>
        public static IList<CommentInfo> SelectBy(CommentField sortField, bool isDesc, CommentField byField, object value, int pageSize, int pageIndex)
        {
            return CommentDA.SelectBy(sortField, isDesc, byField, value, pageSize, pageIndex);
        }
        #endregion
        
        #region Insert
        /// <summary>
        /// Create an entity, and insert into database
        /// </summary>
        /// <returns>if inserted</returns>
        public static bool Insert(String commentId, String docId, String body, CommentStatusEnum status, String userName, String userMail, String userWeb, String userIm, String postIp, Int64 postTime, String channelId)
        {
            CommentInfo comment = new CommentInfo();
            comment.CommentId = commentId;
            comment.DocId = docId;
            comment.Body = body;
            comment.Status = status;
            comment.UserName = userName;
            comment.UserMail = userMail;
            comment.UserWeb = userWeb;
            comment.UserIm = userIm;
            comment.PostIp = postIp;
            comment.PostTime = postTime;
            comment.ChannelId = channelId;

            return CommentDA.Insert(comment);
        }
        
        /// <summary>
        /// Insert an entity into database
        /// </summary>
        /// <param name="comment">comment entity</param>
        /// <returns></returns>
        public static bool Insert(CommentInfo comment)
        {
            return CommentDA.Insert(comment);
        }
        #endregion
        
        #region Update
        /// <summary>
        /// Update an entity in the database
        /// </summary>
        /// <returns>if updated</returns>
        public static bool Update(String commentId, String docId, String body, CommentStatusEnum status, String userName, String userMail, String userWeb, String userIm, String postIp, Int64 postTime, String channelId)
        {
            CommentInfo comment = new CommentInfo();
            comment.CommentId = commentId;
            comment.DocId = docId;
            comment.Body = body;
            comment.Status = status;
            comment.UserName = userName;
            comment.UserMail = userMail;
            comment.UserWeb = userWeb;
            comment.UserIm = userIm;
            comment.PostIp = postIp;
            comment.PostTime = postTime;
            comment.ChannelId = channelId;

            return CommentDA.Update(comment);
        }
        
        /// <summary>
        /// Update an entity in the database
        /// </summary>
        /// <param name="comment">comment entity</param>
        /// <returns></returns>
        public static bool Update(CommentInfo comment)
        {
            return CommentDA.Update(comment);
        }
        #endregion
        
        #region Delete
        /// <summary>
        /// Delete an entity from database
        /// </summary>
        /// <returns>if deleted</returns>
        public static bool Delete(String commentId)
        {
            return CommentDA.Delete(commentId);
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
        public static IList<CommentInfo> Search(SortStrategy strategy, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        {
            return CommentDA.Search(strategy, pageSize, pageIndex,conditions);
        }
        #endregion
        
        #region Search(CommentField sortField, bool isDesc, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="sortField">Sort field</param>
        /// <param name="isDesc">Desc or Asc</param>
        /// <param name="conditions">Search conditions</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>Item list</returns>
        public static IList<CommentInfo> Search(CommentField sortField, bool isDesc, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        {
            return CommentDA.Search(new SortStrategy(new SortItem(sortField.ToString(), isDesc)), pageSize, pageIndex,conditions);
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
        public static IList<CommentInfo> Search(SortStrategy strategy, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return CommentDA.Search(strategy, pageSize, pageIndex, condigitonList);
        }
        #endregion

		#region Search(CommentField sortField, bool isDesc, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="sortField">Sort field</param>
        /// <param name="isDesc">Desc or Asc</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="conditions">Search conditions</param>
        /// <returns>Item list</returns>
        public static IList<CommentInfo> Search(CommentField sortField, bool isDesc, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return CommentDA.Search(new SortStrategy(new SortItem(sortField.ToString(), isDesc)), pageSize, pageIndex, condigitonList);
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
            return CommentDA.CountForSearch(conditions);
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
            return CommentDA.CountForSearch(condigitonList);
        }
        #endregion
        
        #region SumForSearch(CommentField sumField, List<ISearchCondition> conditions)
        /// <summary>
        /// Sum for search
        /// </summary>
        /// <returns>Sum</returns>
        public static double SumForSearch(CommentField sumField, List<ISearchCondition> conditions)
        {
            return CommentDA.SumForSearch(sumField, conditions);
        }

        /// <summary>
        /// Sum for search
        /// </summary>
        /// <returns>Sum</returns>
        public static double SumForSearch(CommentField sumField, params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return CommentDA.SumForSearch(sumField, condigitonList);
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
        public static bool IncreaseField(CommentField byField, object byValue, CommentField increaseField, object increaseValue)
        {
            return CommentDA.IncreaseField(byField, byValue, increaseField, increaseValue);
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
        public static bool UpdateField(CommentField byField, object byValue, CommentField updateField, object newValue)
        {
            return CommentDA.UpdateField(byField, byValue, updateField, newValue);
        }
        #endregion
    }//end of class
}
