//#define MSSQL
using System;
using System.Data;
using System.Collections.Generic;

namespace QLike.SiteLite
{
    /// <summary>
    /// DataAccess of comment
    /// Template: DataAccess.tpl (ver 20100226)
    /// Please never modify this file manually
    /// </summary>
    public partial class CommentDA
    {
        #region ReadDb()
        /// <summary>
        /// Fill data into the entity
        /// </summary>
        private static void ReadDb(IDataReader reader, CommentInfo comment)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {

                    case "CommentId":
                        if (reader[i] != System.DBNull.Value)
                            comment.CommentId = Convert.ToString(reader[i]);
                        break;
                    case "DocId":
                        if (reader[i] != System.DBNull.Value)
                            comment.DocId = Convert.ToString(reader[i]);
                        break;
                    case "Body":
                        if (reader[i] != System.DBNull.Value)
                            comment.Body = Convert.ToString(reader[i]);
                        break;
                    case "Status":
                        if (reader[i] != System.DBNull.Value)
                            comment.Status = (CommentStatusEnum)Convert.ToInt32(reader[i]);
                        break;
                    case "UserName":
                        if (reader[i] != System.DBNull.Value)
                            comment.UserName = Convert.ToString(reader[i]);
                        break;
                    case "UserMail":
                        if (reader[i] != System.DBNull.Value)
                            comment.UserMail = Convert.ToString(reader[i]);
                        break;
                    case "UserWeb":
                        if (reader[i] != System.DBNull.Value)
                            comment.UserWeb = Convert.ToString(reader[i]);
                        break;
                    case "UserIm":
                        if (reader[i] != System.DBNull.Value)
                            comment.UserIm = Convert.ToString(reader[i]);
                        break;
                    case "PostIp":
                        if (reader[i] != System.DBNull.Value)
                            comment.PostIp = Convert.ToString(reader[i]);
                        break;
                    case "PostTime":
                        if (reader[i] != System.DBNull.Value)
                            comment.PostTime = Convert.ToInt64(reader[i]);
                        break;
                    case "ChannelId":
                        if (reader[i] != System.DBNull.Value)
                            comment.ChannelId = Convert.ToString(reader[i]);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
        
        #region CountAll()
        /// <summary>
        /// Select count of comments
        /// </summary>
        /// <returns>int</returns>
        public static int CountAll()
        {
			int recordCount = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(CommentQuery.CountAll);
					recordCount = Convert.ToInt32(provider.ExecuteScalar());
				}
            }
            catch{ }
            return recordCount;
        }
        #endregion
        
        #region SumAll()
        /// <summary>
        /// Select sum of comments
        /// </summary>
        /// <returns>double</returns>
        public static double SumAll(CommentField sumField)
        {
			double sum = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CommentQuery.SumAll, sumField.ToString());
					provider.SetCommandText(cmdText);
					sum = Convert.ToDouble(provider.ExecuteScalar());
				}
            }
            catch{ }
            return sum;
        }
        #endregion
        
        #region CountBy(CommentField byField, object value)
        /// <summary>
        /// Count by the specified comment field
        /// </summary>
        /// <returns>int</returns>
        public static int CountBy(CommentField byField, object value)
        {
			int recordCount = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CommentQuery.CountBy, byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
					recordCount = Convert.ToInt32(provider.ExecuteScalar());
				}
			}
			catch{ }
            return recordCount;
        }
        #endregion
        
        #region SumBy(CommentField byField, object value, CommentField sumField)
        /// <summary>
        /// Sum by the specified comment field
        /// </summary>
        /// <returns>double</returns>
        public static double SumBy(CommentField byField, object value, CommentField sumField)
        {
			double sum = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CommentQuery.SumBy, byField.ToString(), sumField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
					sum = Convert.ToDouble(provider.ExecuteScalar());
				}
			}
			catch{ }
            return sum;
        }
        #endregion
        
        #region GetBy(CommentField byField, object value)
        /// <summary>
        /// Get the specified comment entity
        /// </summary>
        /// <param name="commentId">CommentId</param>
        /// <returns>entity</returns>
        public static CommentInfo GetBy(CommentField byField, object value)
        {
            CommentInfo comment = null;
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CommentQuery.SelectBy, CommentField.CommentId, "DESC", byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
	                
					if (provider.Reader.Read())
					{
						comment = new CommentInfo();
						ReadDb(provider.Reader, comment);
					}
				}
            }
            catch{ }
            return comment;
        }
        #endregion
    
        #region SelectAll(CommentField sortField, bool isDesc)
        /// <summary>
        /// Select all comments
        /// </summary>
        /// <returns>entity list</returns>
        public static IList<CommentInfo> SelectAll(CommentField sortField, bool isDesc)
        {
            IList<CommentInfo> comments = new List<CommentInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CommentQuery.SelectAll, sortField.ToString(), isDesc ? "DESC" : "ASC");
					provider.SetCommandText(cmdText);

					while (provider.Reader.Read())
					{
						CommentInfo comment = new CommentInfo();
						ReadDb(provider.Reader, comment);
						comments.Add(comment);
					}
				}
            }
            catch{ }
            return comments;
        }
        #endregion
        
        #region SelectAll(CommentField sortField, bool isDesc, int pageSize, int pageIndex)
        /// <summary>
        /// Select all comments
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>entity list</returns>
        public static IList<CommentInfo> SelectAll(CommentField sortField, bool isDesc, int pageSize, int pageIndex)
        {
            IList<CommentInfo> comments = new List<CommentInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CommentQuery.PaginationAll, sortField.ToString(), isDesc ? "DESC" : "ASC");
					provider.SetCommandText(cmdText);
	                
					provider.AddParameterWithValue("@PageSize", pageSize);
					provider.AddParameterWithValue("@SkipCount", pageSize * pageIndex);

					while (provider.Reader.Read())
					{
						CommentInfo comment = new CommentInfo();
						ReadDb(provider.Reader, comment);
						comments.Add(comment);
					}
				}
            }
            catch{ }
            return comments;
        }
        #endregion
        
        #region SelectBy(CommentField sortField, bool isDesc, CommentField byField, object value)
        /// <summary>
        /// Get the specified comment list
        /// </summary>
        /// <returns>entity list</returns>
        public static IList<CommentInfo> SelectBy(CommentField sortField, bool isDesc, CommentField byField, object value)
        {
            IList<CommentInfo> comments = new List<CommentInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CommentQuery.SelectBy, sortField.ToString(), isDesc ? "DESC" : "ASC", byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
	                
					while (provider.Reader.Read())
					{
						CommentInfo comment = new CommentInfo();
						ReadDb(provider.Reader, comment);
						comments.Add(comment);
					}
				}
			}
			catch{ }
            return comments;
        }
        #endregion
       
        #region SelectBy(CommentField sortField, bool isDesc, CommentField byField, object value, int pageSize, int pageIndex)
        /// <summary>
        /// Get the specified comment list
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>entity list</returns>
        public static IList<CommentInfo> SelectBy(CommentField sortField, bool isDesc, CommentField byField, object value, int pageSize, int pageIndex)
        {
            IList<CommentInfo> comments = new List<CommentInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CommentQuery.PaginationBy, sortField.ToString(), isDesc ? "DESC" : "ASC", byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
					provider.AddParameterWithValue("@PageSize", pageSize);
					provider.AddParameterWithValue("@SkipCount", pageSize * pageIndex);
	                
					while (provider.Reader.Read())
					{
						CommentInfo comment = new CommentInfo();
						ReadDb(provider.Reader, comment);
						comments.Add(comment);
					}
				}
            }
            catch{ }
            return comments;
        }
        #endregion
        
        #region Insert()
        /// <summary>
        /// Insert a row according to the entity
        /// </summary>
        /// <param name="comment">Comment Entity</param>
        /// <returns>if inserted</returns>
        public static bool Insert(CommentInfo comment)
        {
            bool inserted = false;

			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(CommentQuery.Insert);

					provider.AddParameterWithValue("@CommentId", comment.CommentId);
					provider.AddParameterWithValue("@DocId", comment.DocId);
					provider.AddParameterWithValue("@Body", comment.Body);
					provider.AddParameterWithValue("@Status", comment.Status);
					provider.AddParameterWithValue("@UserName", comment.UserName);
					provider.AddParameterWithValue("@UserMail", comment.UserMail);
					provider.AddParameterWithValue("@UserWeb", comment.UserWeb);
					provider.AddParameterWithValue("@UserIm", comment.UserIm);
					provider.AddParameterWithValue("@PostIp", comment.PostIp);
					provider.AddParameterWithValue("@PostTime", comment.PostTime);
					provider.AddParameterWithValue("@ChannelId", comment.ChannelId);

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
        /// <param name="comment">Comment Entity</param>
        /// <param name="provider">DB Provider</param>
        /// <returns>if inserted</returns>
        public static bool Insert(CommentInfo comment, DbProvider provider)
        {
            bool inserted = false;

			try
			{
				provider.ClearParameters();
				provider.SetCommandText(CommentQuery.Insert);

				provider.AddParameterWithValue("@CommentId", comment.CommentId);
				provider.AddParameterWithValue("@DocId", comment.DocId);
				provider.AddParameterWithValue("@Body", comment.Body);
				provider.AddParameterWithValue("@Status", comment.Status);
				provider.AddParameterWithValue("@UserName", comment.UserName);
				provider.AddParameterWithValue("@UserMail", comment.UserMail);
				provider.AddParameterWithValue("@UserWeb", comment.UserWeb);
				provider.AddParameterWithValue("@UserIm", comment.UserIm);
				provider.AddParameterWithValue("@PostIp", comment.PostIp);
				provider.AddParameterWithValue("@PostTime", comment.PostTime);
				provider.AddParameterWithValue("@ChannelId", comment.ChannelId);

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
        /// <param name="comment">Comment Entity</param>
        /// <returns>if updated</returns>
        public static bool Update(CommentInfo comment)
        {
            bool updated = false;

			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(CommentQuery.Update);

					provider.AddParameterWithValue("@DocId", comment.DocId);
					provider.AddParameterWithValue("@Body", comment.Body);
					provider.AddParameterWithValue("@Status", comment.Status);
					provider.AddParameterWithValue("@UserName", comment.UserName);
					provider.AddParameterWithValue("@UserMail", comment.UserMail);
					provider.AddParameterWithValue("@UserWeb", comment.UserWeb);
					provider.AddParameterWithValue("@UserIm", comment.UserIm);
					provider.AddParameterWithValue("@PostIp", comment.PostIp);
					provider.AddParameterWithValue("@PostTime", comment.PostTime);
					provider.AddParameterWithValue("@ChannelId", comment.ChannelId);

					provider.AddParameterWithValue("@CommentId", comment.CommentId);

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
        /// <param name="comment">Comment Entity</param>
        /// <param name="provider">DB Provider</param>
        /// <returns>if updated</returns>
        public static bool Update(CommentInfo comment, DbProvider provider)
        {
            bool updated = false;

			try
			{
				provider.ClearParameters();
				provider.SetCommandText(CommentQuery.Update);

				provider.AddParameterWithValue("@DocId", comment.DocId);
				provider.AddParameterWithValue("@Body", comment.Body);
				provider.AddParameterWithValue("@Status", comment.Status);
				provider.AddParameterWithValue("@UserName", comment.UserName);
				provider.AddParameterWithValue("@UserMail", comment.UserMail);
				provider.AddParameterWithValue("@UserWeb", comment.UserWeb);
				provider.AddParameterWithValue("@UserIm", comment.UserIm);
				provider.AddParameterWithValue("@PostIp", comment.PostIp);
				provider.AddParameterWithValue("@PostTime", comment.PostTime);
				provider.AddParameterWithValue("@ChannelId", comment.ChannelId);

				provider.AddParameterWithValue("@CommentId", comment.CommentId);

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
        /// <param name="commentId">CommentId</param>
        /// <returns>if deleted</returns>
        public static bool Delete(String commentId)
        {
            bool updated = false;
            
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(CommentQuery.Delete);

					provider.AddParameterWithValue("@CommentId", commentId);
	                
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
        /// <param name="commentId">CommentId</param>
        /// <param name="provider">DB Provider</param>
        /// <returns>if deleted</returns>
        public static bool Delete(String commentId, DbProvider provider)
        {
            bool updated = false;
            
            try
            {
				provider.ClearParameters();
				provider.SetCommandText(CommentQuery.Delete);

				provider.AddParameterWithValue("@CommentId", commentId);
	            
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
        public static IList<CommentInfo> Search(SortStrategy strategy, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        {
            IList<CommentInfo> comments = new List<CommentInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(
						CommentQuery.Search,
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
						CommentInfo comment = new CommentInfo();
						ReadDb(provider.Reader, comment);
						comments.Add(comment);
					}
				}
			}
			catch{ }
            return comments;
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
						CommentQuery.CountForSearch,
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
        
        #region SumForSearch(CommentField sumField, List<ISearchCondition> conditions)
        /// <summary>
        /// Sum for search
        /// </summary>
        /// <returns>Sum</returns>
        public static double SumForSearch(CommentField sumField, List<ISearchCondition> conditions)
        {
            double sum = 0;
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(
						CommentQuery.SumForSearch,
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
        public static bool IncreaseField(CommentField byField, object byValue, CommentField increaseField, object increaseValue)
        {
            bool updated = false;
            
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CommentQuery.IncreaseField, byField.ToString(), increaseField.ToString());
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
        public static bool IncreaseField(CommentField byField, object byValue, CommentField increaseField, object increaseValue, DbProvider provider)
        {
            bool updated = false;
            
            try
            {
				string cmdText = string.Format(CommentQuery.IncreaseField, byField.ToString(), increaseField.ToString());
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
        public static bool UpdateField(CommentField byField, object byValue, CommentField updateField, object newValue)
        {
            bool updated = false;

			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CommentQuery.UpdateField, byField.ToString(), updateField.ToString());
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
        public static bool UpdateField(CommentField byField, object byValue, CommentField updateField, object newValue, DbProvider provider)
        {
            bool updated = false;

			try
			{
				string cmdText = string.Format(CommentQuery.UpdateField, byField.ToString(), updateField.ToString());
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
    /// SQL Queries of comment
    /// Please never modify this file manually
    /// </summary>
    public partial class CommentQuery
    {
        #region Insert
        /// <summary>
        /// Insert
        /// </summary>
        internal static readonly string Insert = String.Concat(
            "    INSERT INTO comment  ",
            "        (  ",
            "            CommentId,  ",
            "            DocId,  ",
            "            Body,  ",
            "            Status,  ",
            "            UserName,  ",
            "            UserMail,  ",
            "            UserWeb,  ",
            "            UserIm,  ",
            "            PostIp,  ",
            "            PostTime,  ",
            "            ChannelId  ",
            "        )  ",
            "    VALUES  ",
            "        (  ",
            "            @CommentId,  ",
            "            @DocId,  ",
            "            @Body,  ",
            "            @Status,  ",
            "            @UserName,  ",
            "            @UserMail,  ",
            "            @UserWeb,  ",
            "            @UserIm,  ",
            "            @PostIp,  ",
            "            @PostTime,  ",
            "            @ChannelId  ",
            "        )  ",
            " ");
        #endregion
        
        #region Update
        /// <summary>
        /// Update
        /// </summary>
        internal static readonly string Update = String.Concat(
            "    UPDATE comment  ",
            "    SET  ",
            "        DocId = @DocId,  ",
            "        Body = @Body,  ",
            "        Status = @Status,  ",
            "        UserName = @UserName,  ",
            "        UserMail = @UserMail,  ",
            "        UserWeb = @UserWeb,  ",
            "        UserIm = @UserIm,  ",
            "        PostIp = @PostIp,  ",
            "        PostTime = @PostTime,  ",
            "        ChannelId = @ChannelId  ",
            "    WHERE   ",
            "        CommentId = @CommentId  ",
            " ");
        #endregion
        
        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        internal static readonly string Delete = String.Concat(
            "    DELETE FROM  ",
            "        comment  ",
            "    WHERE  ",
            "        CommentId = @CommentId  ",
            " ");
        #endregion
        
        #region IncreaseField
        /// <summary>
        /// IncreaseField
        /// </summary>
        internal static readonly string IncreaseField = String.Concat(
            "    UPDATE comment  ",
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
            "    UPDATE comment  ",
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
            "        CommentId,  ",
            "        DocId,  ",
            "        Body,  ",
            "        Status,  ",
            "        UserName,  ",
            "        UserMail,  ",
            "        UserWeb,  ",
            "        UserIm,  ",
            "        PostIp,  ",
            "        PostTime,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        comment  ",
            "    ORDER BY {0} {1}  ",
            " ");
        #endregion
        
        #region SelectBy
        /// <summary>
        /// SelectBy
        /// </summary>
        internal static readonly string SelectBy = String.Concat(
            "    SELECT  ",
            "        CommentId,  ",
            "        DocId,  ",
            "        Body,  ",
            "        Status,  ",
            "        UserName,  ",
            "        UserMail,  ",
            "        UserWeb,  ",
            "        UserIm,  ",
            "        PostIp,  ",
            "        PostTime,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        comment  ",
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
            "        COUNT(CommentId)  ",
            "    FROM  ",
            "        comment  ",
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
            "        comment  ",
            " ");
        #endregion
        
        #region CountBy
        /// <summary>
        /// CountBy
        /// </summary>
        internal static readonly string CountBy = String.Concat(
            "    SELECT  ",
            "        COUNT(CommentId)  ",
            "    FROM  ",
            "        comment  ",
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
            "        comment  ",
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
            "        CommentId,  ",
            "        DocId,  ",
            "        Body,  ",
            "        Status,  ",
            "        UserName,  ",
            "        UserMail,  ",
            "        UserWeb,  ",
            "        UserIm,  ",
            "        PostIp,  ",
            "        PostTime,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        comment  ",
            "    WHERE CommentId NOT IN  ",
            "        (  ",
            "        SELECT TOP (@SkipCount)  ",
            "            CommentId  ",
            "        FROM  ",
            "            comment  ",
            "        ORDER BY  ",
            "            {0} {1}  ",
            "        )  ",
            "    ORDER BY  ",
            "        {0} {1}  ",
            " ");
#else
        internal static readonly string PaginationAll = String.Concat(
            "    SELECT ",
            "        CommentId,  ",
            "        DocId,  ",
            "        Body,  ",
            "        Status,  ",
            "        UserName,  ",
            "        UserMail,  ",
            "        UserWeb,  ",
            "        UserIm,  ",
            "        PostIp,  ",
            "        PostTime,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        comment  ",
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
            "        CommentId,  ",
            "        DocId,  ",
            "        Body,  ",
            "        Status,  ",
            "        UserName,  ",
            "        UserMail,  ",
            "        UserWeb,  ",
            "        UserIm,  ",
            "        PostIp,  ",
            "        PostTime,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        comment  ",
            "    WHERE CommentId NOT IN  ",
            "        (  ",
            "        SELECT TOP (@SkipCount)  ",
            "            CommentId  ",
            "        FROM  ",
            "            comment  ",
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
            "        CommentId,  ",
            "        DocId,  ",
            "        Body,  ",
            "        Status,  ",
            "        UserName,  ",
            "        UserMail,  ",
            "        UserWeb,  ",
            "        UserIm,  ",
            "        PostIp,  ",
            "        PostTime,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        comment  ",
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
            "        CommentId,  ",
            "        DocId,  ",
            "        Body,  ",
            "        Status,  ",
            "        UserName,  ",
            "        UserMail,  ",
            "        UserWeb,  ",
            "        UserIm,  ",
            "        PostIp,  ",
            "        PostTime,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        comment  ",
            "    WHERE CommentId NOT IN  ",
            "        (  ",
            "        SELECT TOP (@SkipCount)  ",
            "            CommentId  ",
            "        FROM  ",
            "            comment  ",
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
            "        CommentId,  ",
            "        DocId,  ",
            "        Body,  ",
            "        Status,  ",
            "        UserName,  ",
            "        UserMail,  ",
            "        UserWeb,  ",
            "        UserIm,  ",
            "        PostIp,  ",
            "        PostTime,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        comment  ",
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
            "        COUNT(CommentId)  ",
            "    FROM  ",
            "        comment  ",
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
            "        comment  ",
            "    WHERE  ",
            "        {0} ",
            " ");
        #endregion
    }//end of class
}
