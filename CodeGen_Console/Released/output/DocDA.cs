//#define MSSQL
using System;
using System.Data;
using System.Collections.Generic;

namespace QLike.SiteLite
{
    /// <summary>
    /// DataAccess of doc
    /// Template: DataAccess.tpl (ver 20100226)
    /// Please never modify this file manually
    /// </summary>
    public partial class DocDA
    {
        #region ReadDb()
        /// <summary>
        /// Fill data into the entity
        /// </summary>
        private static void ReadDb(IDataReader reader, DocInfo doc)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {

                    case "DocId":
                        if (reader[i] != System.DBNull.Value)
                            doc.DocId = Convert.ToString(reader[i]);
                        break;
                    case "Alias":
                        if (reader[i] != System.DBNull.Value)
                            doc.Alias = Convert.ToString(reader[i]);
                        break;
                    case "Subject":
                        if (reader[i] != System.DBNull.Value)
                            doc.Subject = Convert.ToString(reader[i]);
                        break;
                    case "FromName":
                        if (reader[i] != System.DBNull.Value)
                            doc.FromName = Convert.ToString(reader[i]);
                        break;
                    case "FromUrl":
                        if (reader[i] != System.DBNull.Value)
                            doc.FromUrl = Convert.ToString(reader[i]);
                        break;
                    case "BodyAbstract":
                        if (reader[i] != System.DBNull.Value)
                            doc.BodyAbstract = Convert.ToString(reader[i]);
                        break;
                    case "Body":
                        if (reader[i] != System.DBNull.Value)
                            doc.Body = Convert.ToString(reader[i]);
                        break;
                    case "CategoryId":
                        if (reader[i] != System.DBNull.Value)
                            doc.CategoryId = Convert.ToString(reader[i]);
                        break;
                    case "Tag":
                        if (reader[i] != System.DBNull.Value)
                            doc.Tag = Convert.ToString(reader[i]);
                        break;
                    case "PostTime":
                        if (reader[i] != System.DBNull.Value)
                            doc.PostTime = Convert.ToInt64(reader[i]);
                        break;
                    case "UpdateTime":
                        if (reader[i] != System.DBNull.Value)
                            doc.UpdateTime = Convert.ToInt64(reader[i]);
                        break;
                    case "ReadCount":
                        if (reader[i] != System.DBNull.Value)
                            doc.ReadCount = Convert.ToInt32(reader[i]);
                        break;
                    case "CommentCount":
                        if (reader[i] != System.DBNull.Value)
                            doc.CommentCount = Convert.ToInt32(reader[i]);
                        break;
                    case "Attribute":
                        if (reader[i] != System.DBNull.Value)
                            doc.Attribute = Convert.ToInt32(reader[i]);
                        break;
                    case "Priority":
                        if (reader[i] != System.DBNull.Value)
                            doc.Priority = Convert.ToInt32(reader[i]);
                        break;
                    case "EnableUbb":
                        if (reader[i] != System.DBNull.Value)
                            doc.EnableUbb = Convert.ToBoolean(reader[i]);
                        break;
                    case "ContentMode":
                        if (reader[i] != System.DBNull.Value)
                            doc.ContentMode = (ContentModeEnum)Convert.ToInt32(reader[i]);
                        break;
                    case "Status":
                        if (reader[i] != System.DBNull.Value)
                            doc.Status = (DocStatusEnum)Convert.ToInt32(reader[i]);
                        break;
                    case "ChannelId":
                        if (reader[i] != System.DBNull.Value)
                            doc.ChannelId = Convert.ToString(reader[i]);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
        
        #region CountAll()
        /// <summary>
        /// Select count of docs
        /// </summary>
        /// <returns>int</returns>
        public static int CountAll()
        {
			int recordCount = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(DocQuery.CountAll);
					recordCount = Convert.ToInt32(provider.ExecuteScalar());
				}
            }
            catch{ }
            return recordCount;
        }
        #endregion
        
        #region SumAll()
        /// <summary>
        /// Select sum of docs
        /// </summary>
        /// <returns>double</returns>
        public static double SumAll(DocField sumField)
        {
			double sum = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(DocQuery.SumAll, sumField.ToString());
					provider.SetCommandText(cmdText);
					sum = Convert.ToDouble(provider.ExecuteScalar());
				}
            }
            catch{ }
            return sum;
        }
        #endregion
        
        #region CountBy(DocField byField, object value)
        /// <summary>
        /// Count by the specified doc field
        /// </summary>
        /// <returns>int</returns>
        public static int CountBy(DocField byField, object value)
        {
			int recordCount = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(DocQuery.CountBy, byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
					recordCount = Convert.ToInt32(provider.ExecuteScalar());
				}
			}
			catch{ }
            return recordCount;
        }
        #endregion
        
        #region SumBy(DocField byField, object value, DocField sumField)
        /// <summary>
        /// Sum by the specified doc field
        /// </summary>
        /// <returns>double</returns>
        public static double SumBy(DocField byField, object value, DocField sumField)
        {
			double sum = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(DocQuery.SumBy, byField.ToString(), sumField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
					sum = Convert.ToDouble(provider.ExecuteScalar());
				}
			}
			catch{ }
            return sum;
        }
        #endregion
        
        #region GetBy(DocField byField, object value)
        /// <summary>
        /// Get the specified doc entity
        /// </summary>
        /// <param name="docId">DocId</param>
        /// <returns>entity</returns>
        public static DocInfo GetBy(DocField byField, object value)
        {
            DocInfo doc = null;
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(DocQuery.SelectBy, DocField.DocId, "DESC", byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
	                
					if (provider.Reader.Read())
					{
						doc = new DocInfo();
						ReadDb(provider.Reader, doc);
					}
				}
            }
            catch{ }
            return doc;
        }
        #endregion
    
        #region SelectAll(DocField sortField, bool isDesc)
        /// <summary>
        /// Select all docs
        /// </summary>
        /// <returns>entity list</returns>
        public static IList<DocInfo> SelectAll(DocField sortField, bool isDesc)
        {
            IList<DocInfo> docs = new List<DocInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(DocQuery.SelectAll, sortField.ToString(), isDesc ? "DESC" : "ASC");
					provider.SetCommandText(cmdText);

					while (provider.Reader.Read())
					{
						DocInfo doc = new DocInfo();
						ReadDb(provider.Reader, doc);
						docs.Add(doc);
					}
				}
            }
            catch{ }
            return docs;
        }
        #endregion
        
        #region SelectAll(DocField sortField, bool isDesc, int pageSize, int pageIndex)
        /// <summary>
        /// Select all docs
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>entity list</returns>
        public static IList<DocInfo> SelectAll(DocField sortField, bool isDesc, int pageSize, int pageIndex)
        {
            IList<DocInfo> docs = new List<DocInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(DocQuery.PaginationAll, sortField.ToString(), isDesc ? "DESC" : "ASC");
					provider.SetCommandText(cmdText);
	                
					provider.AddParameterWithValue("@PageSize", pageSize);
					provider.AddParameterWithValue("@SkipCount", pageSize * pageIndex);

					while (provider.Reader.Read())
					{
						DocInfo doc = new DocInfo();
						ReadDb(provider.Reader, doc);
						docs.Add(doc);
					}
				}
            }
            catch{ }
            return docs;
        }
        #endregion
        
        #region SelectBy(DocField sortField, bool isDesc, DocField byField, object value)
        /// <summary>
        /// Get the specified doc list
        /// </summary>
        /// <returns>entity list</returns>
        public static IList<DocInfo> SelectBy(DocField sortField, bool isDesc, DocField byField, object value)
        {
            IList<DocInfo> docs = new List<DocInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(DocQuery.SelectBy, sortField.ToString(), isDesc ? "DESC" : "ASC", byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
	                
					while (provider.Reader.Read())
					{
						DocInfo doc = new DocInfo();
						ReadDb(provider.Reader, doc);
						docs.Add(doc);
					}
				}
			}
			catch{ }
            return docs;
        }
        #endregion
       
        #region SelectBy(DocField sortField, bool isDesc, DocField byField, object value, int pageSize, int pageIndex)
        /// <summary>
        /// Get the specified doc list
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>entity list</returns>
        public static IList<DocInfo> SelectBy(DocField sortField, bool isDesc, DocField byField, object value, int pageSize, int pageIndex)
        {
            IList<DocInfo> docs = new List<DocInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(DocQuery.PaginationBy, sortField.ToString(), isDesc ? "DESC" : "ASC", byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
					provider.AddParameterWithValue("@PageSize", pageSize);
					provider.AddParameterWithValue("@SkipCount", pageSize * pageIndex);
	                
					while (provider.Reader.Read())
					{
						DocInfo doc = new DocInfo();
						ReadDb(provider.Reader, doc);
						docs.Add(doc);
					}
				}
            }
            catch{ }
            return docs;
        }
        #endregion
        
        #region Insert()
        /// <summary>
        /// Insert a row according to the entity
        /// </summary>
        /// <param name="doc">Doc Entity</param>
        /// <returns>if inserted</returns>
        public static bool Insert(DocInfo doc)
        {
            bool inserted = false;

			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(DocQuery.Insert);

					provider.AddParameterWithValue("@DocId", doc.DocId);
					provider.AddParameterWithValue("@Alias", doc.Alias);
					provider.AddParameterWithValue("@Subject", doc.Subject);
					provider.AddParameterWithValue("@FromName", doc.FromName);
					provider.AddParameterWithValue("@FromUrl", doc.FromUrl);
					provider.AddParameterWithValue("@BodyAbstract", doc.BodyAbstract);
					provider.AddParameterWithValue("@Body", doc.Body);
					provider.AddParameterWithValue("@CategoryId", doc.CategoryId);
					provider.AddParameterWithValue("@Tag", doc.Tag);
					provider.AddParameterWithValue("@PostTime", doc.PostTime);
					provider.AddParameterWithValue("@UpdateTime", doc.UpdateTime);
					provider.AddParameterWithValue("@ReadCount", doc.ReadCount);
					provider.AddParameterWithValue("@CommentCount", doc.CommentCount);
					provider.AddParameterWithValue("@Attribute", doc.Attribute);
					provider.AddParameterWithValue("@Priority", doc.Priority);
					provider.AddParameterWithValue("@EnableUbb", doc.EnableUbb);
					provider.AddParameterWithValue("@ContentMode", doc.ContentMode);
					provider.AddParameterWithValue("@Status", doc.Status);
					provider.AddParameterWithValue("@ChannelId", doc.ChannelId);

					if (provider.ExecuteNonQuery() > 0)
					{
						inserted = true;
					}
				}
            }
            catch{ }

            return inserted;
        }
        
        /// <summary>
        /// Insert a row according to the entity
        /// </summary>
        /// <param name="doc">Doc Entity</param>
        /// <param name="provider">DB Provider</param>
        /// <returns>if inserted</returns>
        public static bool Insert(DocInfo doc, DbProvider provider)
        {
            bool inserted = false;

			try
			{
				provider.ClearParameters();
				provider.SetCommandText(DocQuery.Insert);

				provider.AddParameterWithValue("@DocId", doc.DocId);
				provider.AddParameterWithValue("@Alias", doc.Alias);
				provider.AddParameterWithValue("@Subject", doc.Subject);
				provider.AddParameterWithValue("@FromName", doc.FromName);
				provider.AddParameterWithValue("@FromUrl", doc.FromUrl);
				provider.AddParameterWithValue("@BodyAbstract", doc.BodyAbstract);
				provider.AddParameterWithValue("@Body", doc.Body);
				provider.AddParameterWithValue("@CategoryId", doc.CategoryId);
				provider.AddParameterWithValue("@Tag", doc.Tag);
				provider.AddParameterWithValue("@PostTime", doc.PostTime);
				provider.AddParameterWithValue("@UpdateTime", doc.UpdateTime);
				provider.AddParameterWithValue("@ReadCount", doc.ReadCount);
				provider.AddParameterWithValue("@CommentCount", doc.CommentCount);
				provider.AddParameterWithValue("@Attribute", doc.Attribute);
				provider.AddParameterWithValue("@Priority", doc.Priority);
				provider.AddParameterWithValue("@EnableUbb", doc.EnableUbb);
				provider.AddParameterWithValue("@ContentMode", doc.ContentMode);
				provider.AddParameterWithValue("@Status", doc.Status);
				provider.AddParameterWithValue("@ChannelId", doc.ChannelId);

				if (provider.ExecuteNonQueryWithTransaction() > 0)
				{
					inserted = true;
				}
			}
			catch{ }

            return inserted;
        }
        #endregion
        
        #region Update()
        /// <summary>
        /// Update a row according to the entity
        /// </summary>
        /// <param name="doc">Doc Entity</param>
        /// <returns>if updated</returns>
        public static bool Update(DocInfo doc)
        {
            bool updated = false;

			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(DocQuery.Update);

					provider.AddParameterWithValue("@Alias", doc.Alias);
					provider.AddParameterWithValue("@Subject", doc.Subject);
					provider.AddParameterWithValue("@FromName", doc.FromName);
					provider.AddParameterWithValue("@FromUrl", doc.FromUrl);
					provider.AddParameterWithValue("@BodyAbstract", doc.BodyAbstract);
					provider.AddParameterWithValue("@Body", doc.Body);
					provider.AddParameterWithValue("@CategoryId", doc.CategoryId);
					provider.AddParameterWithValue("@Tag", doc.Tag);
					provider.AddParameterWithValue("@PostTime", doc.PostTime);
					provider.AddParameterWithValue("@UpdateTime", doc.UpdateTime);
					provider.AddParameterWithValue("@ReadCount", doc.ReadCount);
					provider.AddParameterWithValue("@CommentCount", doc.CommentCount);
					provider.AddParameterWithValue("@Attribute", doc.Attribute);
					provider.AddParameterWithValue("@Priority", doc.Priority);
					provider.AddParameterWithValue("@EnableUbb", doc.EnableUbb);
					provider.AddParameterWithValue("@ContentMode", doc.ContentMode);
					provider.AddParameterWithValue("@Status", doc.Status);
					provider.AddParameterWithValue("@ChannelId", doc.ChannelId);

					provider.AddParameterWithValue("@DocId", doc.DocId);

					if (provider.ExecuteNonQuery() > 0)
					{
						updated = true;
					}
				}
			}
			catch{ }

            return updated;
        }
        
        /// <summary>
        /// Update a row according to the entity
        /// </summary>
        /// <param name="doc">Doc Entity</param>
        /// <param name="provider">DB Provider</param>
        /// <returns>if updated</returns>
        public static bool Update(DocInfo doc, DbProvider provider)
        {
            bool updated = false;

			try
			{
				provider.ClearParameters();
				provider.SetCommandText(DocQuery.Update);

				provider.AddParameterWithValue("@Alias", doc.Alias);
				provider.AddParameterWithValue("@Subject", doc.Subject);
				provider.AddParameterWithValue("@FromName", doc.FromName);
				provider.AddParameterWithValue("@FromUrl", doc.FromUrl);
				provider.AddParameterWithValue("@BodyAbstract", doc.BodyAbstract);
				provider.AddParameterWithValue("@Body", doc.Body);
				provider.AddParameterWithValue("@CategoryId", doc.CategoryId);
				provider.AddParameterWithValue("@Tag", doc.Tag);
				provider.AddParameterWithValue("@PostTime", doc.PostTime);
				provider.AddParameterWithValue("@UpdateTime", doc.UpdateTime);
				provider.AddParameterWithValue("@ReadCount", doc.ReadCount);
				provider.AddParameterWithValue("@CommentCount", doc.CommentCount);
				provider.AddParameterWithValue("@Attribute", doc.Attribute);
				provider.AddParameterWithValue("@Priority", doc.Priority);
				provider.AddParameterWithValue("@EnableUbb", doc.EnableUbb);
				provider.AddParameterWithValue("@ContentMode", doc.ContentMode);
				provider.AddParameterWithValue("@Status", doc.Status);
				provider.AddParameterWithValue("@ChannelId", doc.ChannelId);

				provider.AddParameterWithValue("@DocId", doc.DocId);

				if (provider.ExecuteNonQueryWithTransaction() > 0)
				{
				    updated = true;
				}
			}
			catch{ }
			
            return updated;
        }
        #endregion
        
        #region Delete()
        /// <summary>
        /// Delete the row from database
        /// </summary>
        /// <param name="docId">DocId</param>
        /// <returns>if deleted</returns>
        public static bool Delete(String docId)
        {
            bool updated = false;
            
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(DocQuery.Delete);

					provider.AddParameterWithValue("@DocId", docId);
	                
					if (provider.ExecuteNonQuery() > 0)
					{
						updated = true;
					}
				}
			}
			catch{ }

            return updated;
        }
        
        /// <summary>
        /// Delete the row from database
        /// </summary>
        /// <param name="docId">DocId</param>
        /// <param name="provider">DB Provider</param>
        /// <returns>if deleted</returns>
        public static bool Delete(String docId, DbProvider provider)
        {
            bool updated = false;
            
            try
            {
				provider.ClearParameters();
				provider.SetCommandText(DocQuery.Delete);

				provider.AddParameterWithValue("@DocId", docId);
	            
				if (provider.ExecuteNonQueryWithTransaction() > 0)
				{
					updated = true;
				}
			}
			catch{ }

            return updated;
        }
        #endregion
        
        #region Search(SortStrategy strategy, bool isDesc, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="strategy">Sort strategy</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="conditions">Search conditions</param>
        /// <returns>Item list</returns>
        public static IList<DocInfo> Search(SortStrategy strategy, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        {
            IList<DocInfo> docs = new List<DocInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(
						DocQuery.Search,
						strategy.ToString(),
						SearchHelper.GetSearchExpression(conditions)
						);
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue("@PageSize", pageSize);
					provider.AddParameterWithValue("@SkipCount", pageSize * pageIndex);
					foreach (KeyValuePair<string, object> pair in SearchHelper.GetParamValuePairs(conditions))
					{
						provider.AddParameterWithValue(pair.Key, pair.Value);
					}

					while (provider.Reader.Read())
					{
						DocInfo doc = new DocInfo();
						ReadDb(provider.Reader, doc);
						docs.Add(doc);
					}
				}
			}
			catch{ }
            return docs;
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
            int recordCount = 0;
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(
						DocQuery.CountForSearch,
						SearchHelper.GetSearchExpression(conditions)
						);
					provider.SetCommandText(cmdText);
					foreach (KeyValuePair<string, object> pair in SearchHelper.GetParamValuePairs(conditions))
					{
						provider.AddParameterWithValue(pair.Key, pair.Value);
					}
					recordCount = Convert.ToInt32(provider.ExecuteScalar());
				}
			}
			catch{ }
            return recordCount;
        }
        #endregion
        
        #region SumForSearch(DocField sumField, List<ISearchCondition> conditions)
        /// <summary>
        /// Sum for search
        /// </summary>
        /// <returns>Sum</returns>
        public static double SumForSearch(DocField sumField, List<ISearchCondition> conditions)
        {
            double sum = 0;
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(
						DocQuery.SumForSearch,
						SearchHelper.GetSearchExpression(conditions),
						sumField.ToString()
						);
					provider.SetCommandText(cmdText);
					foreach (KeyValuePair<string, object> pair in SearchHelper.GetParamValuePairs(conditions))
					{
						provider.AddParameterWithValue(pair.Key, pair.Value);
					}
					sum = Convert.ToDouble(provider.ExecuteScalar());
				}
			}
			catch{ }
            return sum;
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
            bool updated = false;
            
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(DocQuery.IncreaseField, byField.ToString(), increaseField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), byValue);
					provider.AddParameterWithValue("@IncreaseValue", increaseValue.ToString());

					if (provider.ExecuteNonQuery() > 0)
					{
						updated = true;
					}
				}
            }
            catch{ }

            return updated;
        }
        
        /// <summary>
        /// Increase the field
        /// </summary>
        /// <param name="byField"></param>
        /// <param name="byValue"></param>
        /// <param name="increaseField"></param>
        /// <param name="increaseValue"></param>
        /// <param name="provider">DB Provider</param>
        /// <returns></returns>
        public static bool IncreaseField(DocField byField, object byValue, DocField increaseField, object increaseValue, DbProvider provider)
        {
            bool updated = false;
            
            try
            {
				string cmdText = string.Format(DocQuery.IncreaseField, byField.ToString(), increaseField.ToString());
				provider.ClearParameters();
				provider.SetCommandText(cmdText);
				provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), byValue);
				provider.AddParameterWithValue("@IncreaseValue", increaseValue.ToString());

				if (provider.ExecuteNonQueryWithTransaction() > 0)
				{
					updated = true;
				}
            }
            catch{ }

            return updated;
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
            bool updated = false;

			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(DocQuery.UpdateField, byField.ToString(), updateField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), byValue);
					provider.AddParameterWithValue("@NewValue", newValue.ToString());

					if (provider.ExecuteNonQuery() > 0)
					{
						updated = true;
					}
				}
			}
			catch{ }

            return updated;
        }
        
        /// <summary>
        /// Update the field
        /// </summary>
        /// <param name="byField"></param>
        /// <param name="byValue"></param>
        /// <param name="updateField"></param>
        /// <param name="newValue"></param>
        /// <param name="provider">DB Provider</param>
        /// <returns></returns>
        public static bool UpdateField(DocField byField, object byValue, DocField updateField, object newValue, DbProvider provider)
        {
            bool updated = false;

			try
			{
				string cmdText = string.Format(DocQuery.UpdateField, byField.ToString(), updateField.ToString());
				provider.SetCommandText(cmdText);
				provider.ClearParameters();
				provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), byValue);
				provider.AddParameterWithValue("@NewValue", newValue.ToString());

				if (provider.ExecuteNonQueryWithTransaction() > 0)
				{
					updated = true;
				}
			}
			catch{ }

            return updated;
        }
        #endregion
    }//end of class
    
    /// <summary>
    /// SQL Queries of doc
    /// Please never modify this file manually
    /// </summary>
    public partial class DocQuery
    {
        #region Insert
        /// <summary>
        /// Insert
        /// </summary>
        internal static readonly string Insert = String.Concat(
            "    INSERT INTO doc  ",
            "        (  ",
            "            DocId,  ",
            "            Alias,  ",
            "            Subject,  ",
            "            FromName,  ",
            "            FromUrl,  ",
            "            BodyAbstract,  ",
            "            Body,  ",
            "            CategoryId,  ",
            "            Tag,  ",
            "            PostTime,  ",
            "            UpdateTime,  ",
            "            ReadCount,  ",
            "            CommentCount,  ",
            "            Attribute,  ",
            "            Priority,  ",
            "            EnableUbb,  ",
            "            ContentMode,  ",
            "            Status,  ",
            "            ChannelId  ",
            "        )  ",
            "    VALUES  ",
            "        (  ",
            "            @DocId,  ",
            "            @Alias,  ",
            "            @Subject,  ",
            "            @FromName,  ",
            "            @FromUrl,  ",
            "            @BodyAbstract,  ",
            "            @Body,  ",
            "            @CategoryId,  ",
            "            @Tag,  ",
            "            @PostTime,  ",
            "            @UpdateTime,  ",
            "            @ReadCount,  ",
            "            @CommentCount,  ",
            "            @Attribute,  ",
            "            @Priority,  ",
            "            @EnableUbb,  ",
            "            @ContentMode,  ",
            "            @Status,  ",
            "            @ChannelId  ",
            "        )  ",
            " ");
        #endregion
        
        #region Update
        /// <summary>
        /// Update
        /// </summary>
        internal static readonly string Update = String.Concat(
            "    UPDATE doc  ",
            "    SET  ",
            "        Alias = @Alias,  ",
            "        Subject = @Subject,  ",
            "        FromName = @FromName,  ",
            "        FromUrl = @FromUrl,  ",
            "        BodyAbstract = @BodyAbstract,  ",
            "        Body = @Body,  ",
            "        CategoryId = @CategoryId,  ",
            "        Tag = @Tag,  ",
            "        PostTime = @PostTime,  ",
            "        UpdateTime = @UpdateTime,  ",
            "        ReadCount = @ReadCount,  ",
            "        CommentCount = @CommentCount,  ",
            "        Attribute = @Attribute,  ",
            "        Priority = @Priority,  ",
            "        EnableUbb = @EnableUbb,  ",
            "        ContentMode = @ContentMode,  ",
            "        Status = @Status,  ",
            "        ChannelId = @ChannelId  ",
            "    WHERE   ",
            "        DocId = @DocId  ",
            " ");
        #endregion
        
        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        internal static readonly string Delete = String.Concat(
            "    DELETE FROM  ",
            "        doc  ",
            "    WHERE  ",
            "        DocId = @DocId  ",
            " ");
        #endregion
        
        #region IncreaseField
        /// <summary>
        /// IncreaseField
        /// </summary>
        internal static readonly string IncreaseField = String.Concat(
            "    UPDATE doc  ",
            "    SET  ",
            "        {1} = {1} + @IncreaseValue  ",
            "    WHERE  ",
            "        {0} = @{0}  ",
            " ");
        #endregion

        #region UpdateField
        /// <summary>
        /// UpdateField
        /// </summary>
        internal static readonly string UpdateField = String.Concat(
            "    UPDATE doc  ",
            "    SET  ",
            "        {1} = @NewValue  ",
            "    WHERE  ",
            "        {0} = @{0}  ",
            " ");
        #endregion
        
        #region SelectAll
        /// <summary>
        /// SelectAll
        /// </summary>
        internal static readonly string SelectAll = String.Concat(
            "    SELECT  ",
            "        DocId,  ",
            "        Alias,  ",
            "        Subject,  ",
            "        FromName,  ",
            "        FromUrl,  ",
            "        BodyAbstract,  ",
            "        Body,  ",
            "        CategoryId,  ",
            "        Tag,  ",
            "        PostTime,  ",
            "        UpdateTime,  ",
            "        ReadCount,  ",
            "        CommentCount,  ",
            "        Attribute,  ",
            "        Priority,  ",
            "        EnableUbb,  ",
            "        ContentMode,  ",
            "        Status,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        doc  ",
            "    ORDER BY {0} {1}  ",
            " ");
        #endregion
        
        #region SelectBy
        /// <summary>
        /// SelectBy
        /// </summary>
        internal static readonly string SelectBy = String.Concat(
            "    SELECT  ",
            "        DocId,  ",
            "        Alias,  ",
            "        Subject,  ",
            "        FromName,  ",
            "        FromUrl,  ",
            "        BodyAbstract,  ",
            "        Body,  ",
            "        CategoryId,  ",
            "        Tag,  ",
            "        PostTime,  ",
            "        UpdateTime,  ",
            "        ReadCount,  ",
            "        CommentCount,  ",
            "        Attribute,  ",
            "        Priority,  ",
            "        EnableUbb,  ",
            "        ContentMode,  ",
            "        Status,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        doc  ",
            "    WHERE  ",
            "        {2} = @{2}  ",
            "    ORDER BY {0} {1}  ",
            " ");
        #endregion
        
        #region CountAll
        /// <summary>
        /// CountAll
        /// </summary>
        internal static readonly string CountAll = String.Concat(
            "    SELECT  ",
            "        COUNT(DocId)  ",
            "    FROM  ",
            "        doc  ",
            " ");
        #endregion
        
        #region SumAll
        /// <summary>
        /// SumAll
        /// </summary>
        internal static readonly string SumAll = String.Concat(
            "    SELECT  ",
            "        SUM({0})  ",
            "    FROM  ",
            "        doc  ",
            " ");
        #endregion
        
        #region CountBy
        /// <summary>
        /// CountBy
        /// </summary>
        internal static readonly string CountBy = String.Concat(
            "    SELECT  ",
            "        COUNT(DocId)  ",
            "    FROM  ",
            "        doc  ",
            "    WHERE  ",
            "        {0} = @{0}  ",
            " ");
        #endregion
        
        #region SumBy
        /// <summary>
        /// SumBy
        /// </summary>
        internal static readonly string SumBy = String.Concat(
            "    SELECT  ",
            "        SUM({1})  ",
            "    FROM  ",
            "        doc  ",
            "    WHERE  ",
            "        {0} = @{0}  ",
            " ");
        #endregion
        
        #region PaginationAll
        /// <summary>
        /// PaginationAll
        /// </summary>
#if MSSQL
        internal static readonly string PaginationAll = String.Concat(
            "    SELECT TOP (@PageSize)  ",
            "        DocId,  ",
            "        Alias,  ",
            "        Subject,  ",
            "        FromName,  ",
            "        FromUrl,  ",
            "        BodyAbstract,  ",
            "        Body,  ",
            "        CategoryId,  ",
            "        Tag,  ",
            "        PostTime,  ",
            "        UpdateTime,  ",
            "        ReadCount,  ",
            "        CommentCount,  ",
            "        Attribute,  ",
            "        Priority,  ",
            "        EnableUbb,  ",
            "        ContentMode,  ",
            "        Status,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        doc  ",
            "    WHERE DocId NOT IN  ",
            "        (  ",
            "        SELECT TOP (@SkipCount)  ",
            "            DocId  ",
            "        FROM  ",
            "            doc  ",
            "        ORDER BY  ",
            "            {0} {1}  ",
            "        )  ",
            "    ORDER BY  ",
            "        {0} {1}  ",
            " ");
#else
        internal static readonly string PaginationAll = String.Concat(
            "    SELECT ",
            "        DocId,  ",
            "        Alias,  ",
            "        Subject,  ",
            "        FromName,  ",
            "        FromUrl,  ",
            "        BodyAbstract,  ",
            "        Body,  ",
            "        CategoryId,  ",
            "        Tag,  ",
            "        PostTime,  ",
            "        UpdateTime,  ",
            "        ReadCount,  ",
            "        CommentCount,  ",
            "        Attribute,  ",
            "        Priority,  ",
            "        EnableUbb,  ",
            "        ContentMode,  ",
            "        Status,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        doc  ",
            "    ORDER BY  ",
            "        {0} {1}  ",
            "    LIMIT @SkipCount, @PageSize  ",
            " ");
#endif
        #endregion
        
        #region PaginationBy
        /// <summary>
        /// PaginationBy
        /// </summary>
#if MSSQL
        internal static readonly string PaginationBy = String.Concat(
            "    SELECT TOP (@PageSize)  ",
            "        DocId,  ",
            "        Alias,  ",
            "        Subject,  ",
            "        FromName,  ",
            "        FromUrl,  ",
            "        BodyAbstract,  ",
            "        Body,  ",
            "        CategoryId,  ",
            "        Tag,  ",
            "        PostTime,  ",
            "        UpdateTime,  ",
            "        ReadCount,  ",
            "        CommentCount,  ",
            "        Attribute,  ",
            "        Priority,  ",
            "        EnableUbb,  ",
            "        ContentMode,  ",
            "        Status,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        doc  ",
            "    WHERE DocId NOT IN  ",
            "        (  ",
            "        SELECT TOP (@SkipCount)  ",
            "            DocId  ",
            "        FROM  ",
            "            doc  ",
            "        WHERE  ",
            "            {2} = @{2}  ",
            "        ORDER BY  ",
            "            {0} {1}  ",
            "        )  ",
            "    AND  ",
            "        {2} = @{2}  ",
            "    ORDER BY  ",
            "        {0} {1}  ",
            " ");
#else
        internal static readonly string PaginationBy = String.Concat(
            "    SELECT ",
            "        DocId,  ",
            "        Alias,  ",
            "        Subject,  ",
            "        FromName,  ",
            "        FromUrl,  ",
            "        BodyAbstract,  ",
            "        Body,  ",
            "        CategoryId,  ",
            "        Tag,  ",
            "        PostTime,  ",
            "        UpdateTime,  ",
            "        ReadCount,  ",
            "        CommentCount,  ",
            "        Attribute,  ",
            "        Priority,  ",
            "        EnableUbb,  ",
            "        ContentMode,  ",
            "        Status,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        doc  ",
            "    WHERE  ",
            "        {2} = @{2}  ",
            "    ORDER BY  ",
            "        {0} {1}  ",
            "    LIMIT @SkipCount, @PageSize  ",
            " ");
#endif

        #endregion
        
        #region Search
        /// <summary>
        /// Search
        /// </summary>
#if MSSQL
        internal static readonly string Search = String.Concat(
            "    SELECT TOP (@PageSize)  ",
            "        DocId,  ",
            "        Alias,  ",
            "        Subject,  ",
            "        FromName,  ",
            "        FromUrl,  ",
            "        BodyAbstract,  ",
            "        Body,  ",
            "        CategoryId,  ",
            "        Tag,  ",
            "        PostTime,  ",
            "        UpdateTime,  ",
            "        ReadCount,  ",
            "        CommentCount,  ",
            "        Attribute,  ",
            "        Priority,  ",
            "        EnableUbb,  ",
            "        ContentMode,  ",
            "        Status,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        doc  ",
            "    WHERE DocId NOT IN  ",
            "        (  ",
            "        SELECT TOP (@SkipCount)  ",
            "            DocId  ",
            "        FROM  ",
            "            doc  ",
            "        WHERE  ",
            "            {1} ",
            "        ORDER BY  ",
            "            {0}  ",
            "        )  ",
            "    AND  ",
            "        {1} ",
            "    ORDER BY  ",
            "        {0}  ",
            " ");
#else
        internal static readonly string Search = String.Concat(
            "    SELECT ",
            "        DocId,  ",
            "        Alias,  ",
            "        Subject,  ",
            "        FromName,  ",
            "        FromUrl,  ",
            "        BodyAbstract,  ",
            "        Body,  ",
            "        CategoryId,  ",
            "        Tag,  ",
            "        PostTime,  ",
            "        UpdateTime,  ",
            "        ReadCount,  ",
            "        CommentCount,  ",
            "        Attribute,  ",
            "        Priority,  ",
            "        EnableUbb,  ",
            "        ContentMode,  ",
            "        Status,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        doc  ",
            "    WHERE  ",
            "        {1} ",
            "    ORDER BY  ",
            "        {0}  ",
            "    LIMIT @SkipCount, @PageSize  ",
            " ");
#endif
        #endregion

        #region CountForSearch
        /// <summary>
        /// CountForSearch
        /// </summary>
        internal static readonly string CountForSearch = String.Concat(
            "    SELECT  ",
            "        COUNT(DocId)  ",
            "    FROM  ",
            "        doc  ",
            "    WHERE  ",
            "        {0} ",
            " ");
        #endregion
        
        #region SumForSearch
        /// <summary>
        /// SumForSearch
        /// </summary>
        internal static readonly string SumForSearch = String.Concat(
            "    SELECT  ",
            "        SUM({1})  ",
            "    FROM  ",
            "        doc  ",
            "    WHERE  ",
            "        {0} ",
            " ");
        #endregion
    }//end of class
}
