using System;
using System.Web;
using System.Web.Caching;
using System.Collections.Generic;

namespace QLike.SiteLite
{
    /// <summary>
    /// Business class of doc
    /// Template: Business.tpl (ver 20090611)
    /// Please never modify this file manually
    /// </summary>
    public partial class DocBiz
    {
		#region CountAll()
        /// <summary>
        /// Get the count of docs
        /// </summary>
        /// <returns>int</returns>
        public static int CountAll()
        {
            return DocDA.CountAll();
        }
        
        /// <summary>
        /// Get the count of docs
        /// </summary>
        /// <returns></returns>
        public static int CountAll(bool enableCache)
        {
            if (enableCache)
            {
                //Cache key
                string cacheKey = "DocBiz.CountAll";

                if (HttpContext.Current.Cache.Get(cacheKey) != null)
                {
                    //Loaded from cache
                    return Convert.ToInt32(HttpContext.Current.Cache.Get(cacheKey));
                }
                else
                {
                    //Load from DA
                    int recordCount = DocDA.CountAll();

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
                return DocDA.CountAll();
            }
        }
        #endregion
        
        #region SumAll
        /// <summary>
        /// Get the sum of docs
        /// </summary>
        /// <returns>double</returns>
        public static double SumAll(DocField sumField)
        {
            return DocDA.SumAll(sumField);
        }
        #endregion
        
        #region CountBy(DocField byField, object value)
        /// <summary>
        /// Get the count of docs
        /// </summary>
        /// <returns>int</returns>
        public static int CountBy(DocField byField, object value)
        {
            return DocDA.CountBy(byField, value);
        }
        #endregion
        
        #region SumBy(DocField byField, object value, DocField sumField)
        /// <summary>
        /// Get the count of docs
        /// </summary>
        /// <returns>double</returns>
        public static double SumBy(DocField byField, object value, DocField sumField)
        {
            return DocDA.SumBy(byField, value, sumField);
        }
        #endregion
        
        #region GetBy(DocField byField, object value)
        /// <summary>
        /// Get the specified doc entity
        /// </summary>
        /// <returns>doc eneity</returns>
        public static DocInfo GetBy(DocField byField, object value)
        {
			return DocBiz.GetBy(byField, value, false);
        }

        /// <summary>
        /// Get the specified doc entity
        /// </summary>
        /// <returns>doc eneity</returns>
        public static DocInfo GetBy(DocField byField, object value, bool enableCache)
        {
            //Check the value input
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return null;
            }

            if (enableCache)
            {
                //Cache key
                string cacheKey = string.Format("DocBiz.GetBy_{0}_{1}", byField.ToString(), value.ToString());

                DocInfo doc = HttpContext.Current.Cache.Get(cacheKey) as DocInfo;
                if (doc != null)
                {
                    //Loaded from cache
                    return doc;
                }
                else
                {
                    //Load from DA
                    doc = DocDA.GetBy(byField, value);

                    if (doc != null)
                    {
                        //Insert into cache
                        HttpContext.Current.Cache.Insert(
                            cacheKey,
                            doc,
                            null,
                            DateTime.Now.AddSeconds(Consts.CacheDuration),
                            Cache.NoSlidingExpiration
                            );

                        return doc;
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
                return DocDA.GetBy(byField, value);
            }
        }
        #endregion
        
		#region SelectAll(DocField sortField, bool isDesc)
        /// <summary>
        /// Get the list of docs
        /// </summary>
        /// <returns>list of docs</returns>
        public static IList<DocInfo> SelectAll(DocField sortField, bool isDesc)
        {
            return DocBiz.SelectAll(sortField, isDesc, false);
        }
        
        /// <summary>
        /// Get the list of docs
        /// </summary>
        /// <returns>list of docs</returns>
        public static IList<DocInfo> SelectAll(DocField sortField, bool isDesc, bool enableCache)
        {
            if (enableCache)
            {
                //Cache key
                string cacheKey = string.Format("DocBiz.SelectAll_{0}_{1}", sortField.ToString(), isDesc.ToString());
                
                IList<DocInfo> docs = HttpContext.Current.Cache.Get(cacheKey) as IList<DocInfo>;
                if (docs != null)
                {
                    //Loaded from cache
                    return docs;
                }
                else
                {
                    //Load from DA
                    docs = DocDA.SelectAll(sortField, isDesc);

                    //Insert into cache
                    HttpContext.Current.Cache.Insert(
                        cacheKey, 
                        docs, 
                        null, 
                        DateTime.Now.AddSeconds(Consts.CacheDuration), 
                        Cache.NoSlidingExpiration
                        );

                    return docs;
                }
            }
            else
            {
                //Cache disabled
                return DocDA.SelectAll(sortField, isDesc);
            }
        }
        #endregion
        
        #region SelectAll(DocField sortField, bool isDesc, int pageSize, int pageIndex)
        /// <summary>
        /// Get the list of docs
        /// </summary>
        /// <returns>list of docs</returns>
        public static IList<DocInfo> SelectAll(DocField sortField, bool isDesc, int pageSize, int pageIndex)
        {
            return DocBiz.SelectAll(sortField, isDesc, pageSize, pageIndex, false);
        }
        
        /// <summary>
        /// Get the list of docs
        /// </summary>
        /// <returns>list of docs</returns>
        public static IList<DocInfo> SelectAll(DocField sortField, bool isDesc, int pageSize, int pageIndex, bool enableCache)
        {
            if (enableCache)
            {
                //Cache key
                string cacheKey = string.Format("DocBiz.SelectAll_{0}_{1}_{2}_{3}", 
                    sortField.ToString(), 
                    isDesc.ToString(),
                    pageSize.ToString(),
                    pageIndex.ToString()
                    );

                IList<DocInfo> docs = HttpContext.Current.Cache.Get(cacheKey) as IList<DocInfo>;
                if (docs != null)
                {
                    //Loaded from cache
                    return docs;
                }
                else
                {
                    //Load from DA
                    docs = DocDA.SelectAll(sortField, isDesc);

                    //Insert into cache
                    HttpContext.Current.Cache.Insert(
                        cacheKey,
                        docs,
                        null,
                        DateTime.Now.AddSeconds(Consts.CacheDuration),
                        Cache.NoSlidingExpiration
                        );

                    return docs;
                }
            }
            else
            {
                //Cache disabled
                return DocDA.SelectAll(sortField, isDesc, pageSize, pageIndex);
            }
        }
        #endregion

		#region SelectBy(DocField sortField, bool isDesc, DocField byField, object value)
        /// <summary>
        /// Get the specified doc entity
        /// </summary>
        /// <returns>doc eneity</returns>
        public static IList<DocInfo> SelectBy(DocField sortField, bool isDesc, DocField byField, object value)
        {
            return DocDA.SelectBy(sortField, isDesc, byField, value);
        }
        #endregion
        
        #region SelectBy(DocField sortField, bool isDesc, DocField byField, object value, int pageSize, int pageIndex);
        /// <summary>
        /// Get the specified doc entity
        /// </summary>
        /// <returns>doc eneity</returns>
        public static IList<DocInfo> SelectBy(DocField sortField, bool isDesc, DocField byField, object value, int pageSize, int pageIndex)
        {
            return DocDA.SelectBy(sortField, isDesc, byField, value, pageSize, pageIndex);
        }
        #endregion
        
        #region Insert
        /// <summary>
        /// Create an entity, and insert into database
        /// </summary>
        /// <returns>if inserted</returns>
        public static bool Insert(String docId, String alias, String subject, String fromName, String fromUrl, String bodyAbstract, String body, String categoryId, String tag, Int64 postTime, Int64 updateTime, Int32 readCount, Int32 commentCount, Int32 attribute, Int32 priority, Boolean enableUbb, ContentModeEnum contentMode, DocStatusEnum status, String channelId)
        {
            DocInfo doc = new DocInfo();
            doc.DocId = docId;
            doc.Alias = alias;
            doc.Subject = subject;
            doc.FromName = fromName;
            doc.FromUrl = fromUrl;
            doc.BodyAbstract = bodyAbstract;
            doc.Body = body;
            doc.CategoryId = categoryId;
            doc.Tag = tag;
            doc.PostTime = postTime;
            doc.UpdateTime = updateTime;
            doc.ReadCount = readCount;
            doc.CommentCount = commentCount;
            doc.Attribute = attribute;
            doc.Priority = priority;
            doc.EnableUbb = enableUbb;
            doc.ContentMode = contentMode;
            doc.Status = status;
            doc.ChannelId = channelId;

            return DocDA.Insert(doc);
        }
        
        /// <summary>
        /// Insert an entity into database
        /// </summary>
        /// <param name="doc">doc entity</param>
        /// <returns></returns>
        public static bool Insert(DocInfo doc)
        {
            return DocDA.Insert(doc);
        }
        #endregion
        
        #region Update
        /// <summary>
        /// Update an entity in the database
        /// </summary>
        /// <returns>if updated</returns>
        public static bool Update(String docId, String alias, String subject, String fromName, String fromUrl, String bodyAbstract, String body, String categoryId, String tag, Int64 postTime, Int64 updateTime, Int32 readCount, Int32 commentCount, Int32 attribute, Int32 priority, Boolean enableUbb, ContentModeEnum contentMode, DocStatusEnum status, String channelId)
        {
            DocInfo doc = new DocInfo();
            doc.DocId = docId;
            doc.Alias = alias;
            doc.Subject = subject;
            doc.FromName = fromName;
            doc.FromUrl = fromUrl;
            doc.BodyAbstract = bodyAbstract;
            doc.Body = body;
            doc.CategoryId = categoryId;
            doc.Tag = tag;
            doc.PostTime = postTime;
            doc.UpdateTime = updateTime;
            doc.ReadCount = readCount;
            doc.CommentCount = commentCount;
            doc.Attribute = attribute;
            doc.Priority = priority;
            doc.EnableUbb = enableUbb;
            doc.ContentMode = contentMode;
            doc.Status = status;
            doc.ChannelId = channelId;

            return DocDA.Update(doc);
        }
        
        /// <summary>
        /// Update an entity in the database
        /// </summary>
        /// <param name="doc">doc entity</param>
        /// <returns></returns>
        public static bool Update(DocInfo doc)
        {
            return DocDA.Update(doc);
        }
        #endregion
        
        #region Delete
        /// <summary>
        /// Delete an entity from database
        /// </summary>
        /// <returns>if deleted</returns>
        public static bool Delete(String docId)
        {
            return DocDA.Delete(docId);
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
        public static IList<DocInfo> Search(SortStrategy strategy, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        {
            return DocDA.Search(strategy, pageSize, pageIndex,conditions);
        }
        #endregion
        
        #region Search(DocField sortField, bool isDesc, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="sortField">Sort field</param>
        /// <param name="isDesc">Desc or Asc</param>
        /// <param name="conditions">Search conditions</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <returns>Item list</returns>
        public static IList<DocInfo> Search(DocField sortField, bool isDesc, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        {
            return DocDA.Search(new SortStrategy(new SortItem(sortField.ToString(), isDesc)), pageSize, pageIndex,conditions);
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
        public static IList<DocInfo> Search(SortStrategy strategy, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return DocDA.Search(strategy, pageSize, pageIndex, condigitonList);
        }
        #endregion

		#region Search(DocField sortField, bool isDesc, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="sortField">Sort field</param>
        /// <param name="isDesc">Desc or Asc</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="conditions">Search conditions</param>
        /// <returns>Item list</returns>
        public static IList<DocInfo> Search(DocField sortField, bool isDesc, int pageSize, int pageIndex, params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return DocDA.Search(new SortStrategy(new SortItem(sortField.ToString(), isDesc)), pageSize, pageIndex, condigitonList);
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
            return DocDA.CountForSearch(conditions);
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
            return DocDA.CountForSearch(condigitonList);
        }
        #endregion
        
        #region SumForSearch(DocField sumField, List<ISearchCondition> conditions)
        /// <summary>
        /// Sum for search
        /// </summary>
        /// <returns>Sum</returns>
        public static double SumForSearch(DocField sumField, List<ISearchCondition> conditions)
        {
            return DocDA.SumForSearch(sumField, conditions);
        }

        /// <summary>
        /// Sum for search
        /// </summary>
        /// <returns>Sum</returns>
        public static double SumForSearch(DocField sumField, params ISearchCondition[] conditions)
        {
            List<ISearchCondition> condigitonList = new List<ISearchCondition>();
            foreach (ISearchCondition condition in conditions)
            {
                condigitonList.Add(condition);
            }
            return DocDA.SumForSearch(sumField, condigitonList);
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
        public static bool IncreaseField(DocField byField, object byValue, DocField increaseField, object increaseValue)
        {
            return DocDA.IncreaseField(byField, byValue, increaseField, increaseValue);
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
        public static bool UpdateField(DocField byField, object byValue, DocField updateField, object newValue)
        {
            return DocDA.UpdateField(byField, byValue, updateField, newValue);
        }
        #endregion
    }//end of class
}
