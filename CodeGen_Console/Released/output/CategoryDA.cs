//#define MSSQL
using System;
using System.Data;
using System.Collections.Generic;

namespace QLike.SiteLite
{
    /// <summary>
    /// DataAccess of category
    /// Template: DataAccess.tpl (ver 20100226)
    /// Please never modify this file manually
    /// </summary>
    public partial class CategoryDA
    {
        #region ReadDb()
        /// <summary>
        /// Fill data into the entity
        /// </summary>
        private static void ReadDb(IDataReader reader, CategoryInfo category)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {

                    case "CategoryId":
                        if (reader[i] != System.DBNull.Value)
                            category.CategoryId = Convert.ToString(reader[i]);
                        break;
                    case "Alias":
                        if (reader[i] != System.DBNull.Value)
                            category.Alias = Convert.ToString(reader[i]);
                        break;
                    case "DisplayName":
                        if (reader[i] != System.DBNull.Value)
                            category.DisplayName = Convert.ToString(reader[i]);
                        break;
                    case "DisplayOrder":
                        if (reader[i] != System.DBNull.Value)
                            category.DisplayOrder = Convert.ToInt32(reader[i]);
                        break;
                    case "ContentCount":
                        if (reader[i] != System.DBNull.Value)
                            category.ContentCount = Convert.ToInt32(reader[i]);
                        break;
                    case "LevelCode":
                        if (reader[i] != System.DBNull.Value)
                            category.LevelCode = Convert.ToString(reader[i]);
                        break;
                    case "Status":
                        if (reader[i] != System.DBNull.Value)
                            category.Status = (CategoryStatusEnum)Convert.ToInt32(reader[i]);
                        break;
                    case "ParentId":
                        if (reader[i] != System.DBNull.Value)
                            category.ParentId = Convert.ToString(reader[i]);
                        break;
                    case "ChannelId":
                        if (reader[i] != System.DBNull.Value)
                            category.ChannelId = Convert.ToString(reader[i]);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
        
        #region CountAll()
        /// <summary>
        /// Select count of categorys
        /// </summary>
        /// <returns>int</returns>
        public static int CountAll()
        {
			int recordCount = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(CategoryQuery.CountAll);
					recordCount = Convert.ToInt32(provider.ExecuteScalar());
				}
            }
            catch{ }
            return recordCount;
        }
        #endregion
        
        #region SumAll()
        /// <summary>
        /// Select sum of categorys
        /// </summary>
        /// <returns>double</returns>
        public static double SumAll(CategoryField sumField)
        {
			double sum = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CategoryQuery.SumAll, sumField.ToString());
					provider.SetCommandText(cmdText);
					sum = Convert.ToDouble(provider.ExecuteScalar());
				}
            }
            catch{ }
            return sum;
        }
        #endregion
        
        #region CountBy(CategoryField byField, object value)
        /// <summary>
        /// Count by the specified category field
        /// </summary>
        /// <returns>int</returns>
        public static int CountBy(CategoryField byField, object value)
        {
			int recordCount = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CategoryQuery.CountBy, byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
					recordCount = Convert.ToInt32(provider.ExecuteScalar());
				}
			}
			catch{ }
            return recordCount;
        }
        #endregion
        
        #region SumBy(CategoryField byField, object value, CategoryField sumField)
        /// <summary>
        /// Sum by the specified category field
        /// </summary>
        /// <returns>double</returns>
        public static double SumBy(CategoryField byField, object value, CategoryField sumField)
        {
			double sum = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CategoryQuery.SumBy, byField.ToString(), sumField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
					sum = Convert.ToDouble(provider.ExecuteScalar());
				}
			}
			catch{ }
            return sum;
        }
        #endregion
        
        #region GetBy(CategoryField byField, object value)
        /// <summary>
        /// Get the specified category entity
        /// </summary>
        /// <param name="categoryId">CategoryId</param>
        /// <returns>entity</returns>
        public static CategoryInfo GetBy(CategoryField byField, object value)
        {
            CategoryInfo category = null;
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CategoryQuery.SelectBy, CategoryField.CategoryId, "DESC", byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
	                
					if (provider.Reader.Read())
					{
						category = new CategoryInfo();
						ReadDb(provider.Reader, category);
					}
				}
            }
            catch{ }
            return category;
        }
        #endregion
    
        #region SelectAll(CategoryField sortField, bool isDesc)
        /// <summary>
        /// Select all categorys
        /// </summary>
        /// <returns>entity list</returns>
        public static IList<CategoryInfo> SelectAll(CategoryField sortField, bool isDesc)
        {
            IList<CategoryInfo> categorys = new List<CategoryInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CategoryQuery.SelectAll, sortField.ToString(), isDesc ? "DESC" : "ASC");
					provider.SetCommandText(cmdText);

					while (provider.Reader.Read())
					{
						CategoryInfo category = new CategoryInfo();
						ReadDb(provider.Reader, category);
						categorys.Add(category);
					}
				}
            }
            catch{ }
            return categorys;
        }
        #endregion
        
        #region SelectAll(CategoryField sortField, bool isDesc, int pageSize, int pageIndex)
        /// <summary>
        /// Select all categorys
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>entity list</returns>
        public static IList<CategoryInfo> SelectAll(CategoryField sortField, bool isDesc, int pageSize, int pageIndex)
        {
            IList<CategoryInfo> categorys = new List<CategoryInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CategoryQuery.PaginationAll, sortField.ToString(), isDesc ? "DESC" : "ASC");
					provider.SetCommandText(cmdText);
	                
					provider.AddParameterWithValue("@PageSize", pageSize);
					provider.AddParameterWithValue("@SkipCount", pageSize * pageIndex);

					while (provider.Reader.Read())
					{
						CategoryInfo category = new CategoryInfo();
						ReadDb(provider.Reader, category);
						categorys.Add(category);
					}
				}
            }
            catch{ }
            return categorys;
        }
        #endregion
        
        #region SelectBy(CategoryField sortField, bool isDesc, CategoryField byField, object value)
        /// <summary>
        /// Get the specified category list
        /// </summary>
        /// <returns>entity list</returns>
        public static IList<CategoryInfo> SelectBy(CategoryField sortField, bool isDesc, CategoryField byField, object value)
        {
            IList<CategoryInfo> categorys = new List<CategoryInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CategoryQuery.SelectBy, sortField.ToString(), isDesc ? "DESC" : "ASC", byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
	                
					while (provider.Reader.Read())
					{
						CategoryInfo category = new CategoryInfo();
						ReadDb(provider.Reader, category);
						categorys.Add(category);
					}
				}
			}
			catch{ }
            return categorys;
        }
        #endregion
       
        #region SelectBy(CategoryField sortField, bool isDesc, CategoryField byField, object value, int pageSize, int pageIndex)
        /// <summary>
        /// Get the specified category list
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>entity list</returns>
        public static IList<CategoryInfo> SelectBy(CategoryField sortField, bool isDesc, CategoryField byField, object value, int pageSize, int pageIndex)
        {
            IList<CategoryInfo> categorys = new List<CategoryInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CategoryQuery.PaginationBy, sortField.ToString(), isDesc ? "DESC" : "ASC", byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
					provider.AddParameterWithValue("@PageSize", pageSize);
					provider.AddParameterWithValue("@SkipCount", pageSize * pageIndex);
	                
					while (provider.Reader.Read())
					{
						CategoryInfo category = new CategoryInfo();
						ReadDb(provider.Reader, category);
						categorys.Add(category);
					}
				}
            }
            catch{ }
            return categorys;
        }
        #endregion
        
        #region Insert()
        /// <summary>
        /// Insert a row according to the entity
        /// </summary>
        /// <param name="category">Category Entity</param>
        /// <returns>if inserted</returns>
        public static bool Insert(CategoryInfo category)
        {
            bool inserted = false;

			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(CategoryQuery.Insert);

					provider.AddParameterWithValue("@CategoryId", category.CategoryId);
					provider.AddParameterWithValue("@Alias", category.Alias);
					provider.AddParameterWithValue("@DisplayName", category.DisplayName);
					provider.AddParameterWithValue("@DisplayOrder", category.DisplayOrder);
					provider.AddParameterWithValue("@ContentCount", category.ContentCount);
					provider.AddParameterWithValue("@LevelCode", category.LevelCode);
					provider.AddParameterWithValue("@Status", category.Status);
					provider.AddParameterWithValue("@ParentId", category.ParentId);
					provider.AddParameterWithValue("@ChannelId", category.ChannelId);

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
        /// <param name="category">Category Entity</param>
        /// <param name="provider">DB Provider</param>
        /// <returns>if inserted</returns>
        public static bool Insert(CategoryInfo category, DbProvider provider)
        {
            bool inserted = false;

			try
			{
				provider.ClearParameters();
				provider.SetCommandText(CategoryQuery.Insert);

				provider.AddParameterWithValue("@CategoryId", category.CategoryId);
				provider.AddParameterWithValue("@Alias", category.Alias);
				provider.AddParameterWithValue("@DisplayName", category.DisplayName);
				provider.AddParameterWithValue("@DisplayOrder", category.DisplayOrder);
				provider.AddParameterWithValue("@ContentCount", category.ContentCount);
				provider.AddParameterWithValue("@LevelCode", category.LevelCode);
				provider.AddParameterWithValue("@Status", category.Status);
				provider.AddParameterWithValue("@ParentId", category.ParentId);
				provider.AddParameterWithValue("@ChannelId", category.ChannelId);

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
        /// <param name="category">Category Entity</param>
        /// <returns>if updated</returns>
        public static bool Update(CategoryInfo category)
        {
            bool updated = false;

			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(CategoryQuery.Update);

					provider.AddParameterWithValue("@Alias", category.Alias);
					provider.AddParameterWithValue("@DisplayName", category.DisplayName);
					provider.AddParameterWithValue("@DisplayOrder", category.DisplayOrder);
					provider.AddParameterWithValue("@ContentCount", category.ContentCount);
					provider.AddParameterWithValue("@LevelCode", category.LevelCode);
					provider.AddParameterWithValue("@Status", category.Status);
					provider.AddParameterWithValue("@ParentId", category.ParentId);
					provider.AddParameterWithValue("@ChannelId", category.ChannelId);

					provider.AddParameterWithValue("@CategoryId", category.CategoryId);

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
        /// <param name="category">Category Entity</param>
        /// <param name="provider">DB Provider</param>
        /// <returns>if updated</returns>
        public static bool Update(CategoryInfo category, DbProvider provider)
        {
            bool updated = false;

			try
			{
				provider.ClearParameters();
				provider.SetCommandText(CategoryQuery.Update);

				provider.AddParameterWithValue("@Alias", category.Alias);
				provider.AddParameterWithValue("@DisplayName", category.DisplayName);
				provider.AddParameterWithValue("@DisplayOrder", category.DisplayOrder);
				provider.AddParameterWithValue("@ContentCount", category.ContentCount);
				provider.AddParameterWithValue("@LevelCode", category.LevelCode);
				provider.AddParameterWithValue("@Status", category.Status);
				provider.AddParameterWithValue("@ParentId", category.ParentId);
				provider.AddParameterWithValue("@ChannelId", category.ChannelId);

				provider.AddParameterWithValue("@CategoryId", category.CategoryId);

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
        /// <param name="categoryId">CategoryId</param>
        /// <returns>if deleted</returns>
        public static bool Delete(String categoryId)
        {
            bool updated = false;
            
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(CategoryQuery.Delete);

					provider.AddParameterWithValue("@CategoryId", categoryId);
	                
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
        /// <param name="categoryId">CategoryId</param>
        /// <param name="provider">DB Provider</param>
        /// <returns>if deleted</returns>
        public static bool Delete(String categoryId, DbProvider provider)
        {
            bool updated = false;
            
            try
            {
				provider.ClearParameters();
				provider.SetCommandText(CategoryQuery.Delete);

				provider.AddParameterWithValue("@CategoryId", categoryId);
	            
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
        public static IList<CategoryInfo> Search(SortStrategy strategy, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        {
            IList<CategoryInfo> categorys = new List<CategoryInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(
						CategoryQuery.Search,
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
						CategoryInfo category = new CategoryInfo();
						ReadDb(provider.Reader, category);
						categorys.Add(category);
					}
				}
			}
			catch{ }
            return categorys;
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
						CategoryQuery.CountForSearch,
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
        
        #region SumForSearch(CategoryField sumField, List<ISearchCondition> conditions)
        /// <summary>
        /// Sum for search
        /// </summary>
        /// <returns>Sum</returns>
        public static double SumForSearch(CategoryField sumField, List<ISearchCondition> conditions)
        {
            double sum = 0;
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(
						CategoryQuery.SumForSearch,
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
        public static bool IncreaseField(CategoryField byField, object byValue, CategoryField increaseField, object increaseValue)
        {
            bool updated = false;
            
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CategoryQuery.IncreaseField, byField.ToString(), increaseField.ToString());
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
        public static bool IncreaseField(CategoryField byField, object byValue, CategoryField increaseField, object increaseValue, DbProvider provider)
        {
            bool updated = false;
            
            try
            {
				string cmdText = string.Format(CategoryQuery.IncreaseField, byField.ToString(), increaseField.ToString());
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
        public static bool UpdateField(CategoryField byField, object byValue, CategoryField updateField, object newValue)
        {
            bool updated = false;

			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(CategoryQuery.UpdateField, byField.ToString(), updateField.ToString());
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
        public static bool UpdateField(CategoryField byField, object byValue, CategoryField updateField, object newValue, DbProvider provider)
        {
            bool updated = false;

			try
			{
				string cmdText = string.Format(CategoryQuery.UpdateField, byField.ToString(), updateField.ToString());
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
    /// SQL Queries of category
    /// Please never modify this file manually
    /// </summary>
    public partial class CategoryQuery
    {
        #region Insert
        /// <summary>
        /// Insert
        /// </summary>
        internal static readonly string Insert = String.Concat(
            "    INSERT INTO category  ",
            "        (  ",
            "            CategoryId,  ",
            "            Alias,  ",
            "            DisplayName,  ",
            "            DisplayOrder,  ",
            "            ContentCount,  ",
            "            LevelCode,  ",
            "            Status,  ",
            "            ParentId,  ",
            "            ChannelId  ",
            "        )  ",
            "    VALUES  ",
            "        (  ",
            "            @CategoryId,  ",
            "            @Alias,  ",
            "            @DisplayName,  ",
            "            @DisplayOrder,  ",
            "            @ContentCount,  ",
            "            @LevelCode,  ",
            "            @Status,  ",
            "            @ParentId,  ",
            "            @ChannelId  ",
            "        )  ",
            " ");
        #endregion
        
        #region Update
        /// <summary>
        /// Update
        /// </summary>
        internal static readonly string Update = String.Concat(
            "    UPDATE category  ",
            "    SET  ",
            "        Alias = @Alias,  ",
            "        DisplayName = @DisplayName,  ",
            "        DisplayOrder = @DisplayOrder,  ",
            "        ContentCount = @ContentCount,  ",
            "        LevelCode = @LevelCode,  ",
            "        Status = @Status,  ",
            "        ParentId = @ParentId,  ",
            "        ChannelId = @ChannelId  ",
            "    WHERE   ",
            "        CategoryId = @CategoryId  ",
            " ");
        #endregion
        
        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        internal static readonly string Delete = String.Concat(
            "    DELETE FROM  ",
            "        category  ",
            "    WHERE  ",
            "        CategoryId = @CategoryId  ",
            " ");
        #endregion
        
        #region IncreaseField
        /// <summary>
        /// IncreaseField
        /// </summary>
        internal static readonly string IncreaseField = String.Concat(
            "    UPDATE category  ",
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
            "    UPDATE category  ",
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
            "        CategoryId,  ",
            "        Alias,  ",
            "        DisplayName,  ",
            "        DisplayOrder,  ",
            "        ContentCount,  ",
            "        LevelCode,  ",
            "        Status,  ",
            "        ParentId,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        category  ",
            "    ORDER BY {0} {1}  ",
            " ");
        #endregion
        
        #region SelectBy
        /// <summary>
        /// SelectBy
        /// </summary>
        internal static readonly string SelectBy = String.Concat(
            "    SELECT  ",
            "        CategoryId,  ",
            "        Alias,  ",
            "        DisplayName,  ",
            "        DisplayOrder,  ",
            "        ContentCount,  ",
            "        LevelCode,  ",
            "        Status,  ",
            "        ParentId,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        category  ",
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
            "        COUNT(CategoryId)  ",
            "    FROM  ",
            "        category  ",
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
            "        category  ",
            " ");
        #endregion
        
        #region CountBy
        /// <summary>
        /// CountBy
        /// </summary>
        internal static readonly string CountBy = String.Concat(
            "    SELECT  ",
            "        COUNT(CategoryId)  ",
            "    FROM  ",
            "        category  ",
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
            "        category  ",
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
            "        CategoryId,  ",
            "        Alias,  ",
            "        DisplayName,  ",
            "        DisplayOrder,  ",
            "        ContentCount,  ",
            "        LevelCode,  ",
            "        Status,  ",
            "        ParentId,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        category  ",
            "    WHERE CategoryId NOT IN  ",
            "        (  ",
            "        SELECT TOP (@SkipCount)  ",
            "            CategoryId  ",
            "        FROM  ",
            "            category  ",
            "        ORDER BY  ",
            "            {0} {1}  ",
            "        )  ",
            "    ORDER BY  ",
            "        {0} {1}  ",
            " ");
#else
        internal static readonly string PaginationAll = String.Concat(
            "    SELECT ",
            "        CategoryId,  ",
            "        Alias,  ",
            "        DisplayName,  ",
            "        DisplayOrder,  ",
            "        ContentCount,  ",
            "        LevelCode,  ",
            "        Status,  ",
            "        ParentId,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        category  ",
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
            "        CategoryId,  ",
            "        Alias,  ",
            "        DisplayName,  ",
            "        DisplayOrder,  ",
            "        ContentCount,  ",
            "        LevelCode,  ",
            "        Status,  ",
            "        ParentId,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        category  ",
            "    WHERE CategoryId NOT IN  ",
            "        (  ",
            "        SELECT TOP (@SkipCount)  ",
            "            CategoryId  ",
            "        FROM  ",
            "            category  ",
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
            "        CategoryId,  ",
            "        Alias,  ",
            "        DisplayName,  ",
            "        DisplayOrder,  ",
            "        ContentCount,  ",
            "        LevelCode,  ",
            "        Status,  ",
            "        ParentId,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        category  ",
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
            "        CategoryId,  ",
            "        Alias,  ",
            "        DisplayName,  ",
            "        DisplayOrder,  ",
            "        ContentCount,  ",
            "        LevelCode,  ",
            "        Status,  ",
            "        ParentId,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        category  ",
            "    WHERE CategoryId NOT IN  ",
            "        (  ",
            "        SELECT TOP (@SkipCount)  ",
            "            CategoryId  ",
            "        FROM  ",
            "            category  ",
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
            "        CategoryId,  ",
            "        Alias,  ",
            "        DisplayName,  ",
            "        DisplayOrder,  ",
            "        ContentCount,  ",
            "        LevelCode,  ",
            "        Status,  ",
            "        ParentId,  ",
            "        ChannelId  ",
            "    FROM  ",
            "        category  ",
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
            "        COUNT(CategoryId)  ",
            "    FROM  ",
            "        category  ",
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
            "        category  ",
            "    WHERE  ",
            "        {0} ",
            " ");
        #endregion
    }//end of class
}
