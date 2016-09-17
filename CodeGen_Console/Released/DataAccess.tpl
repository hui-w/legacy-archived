//#define MSSQL
using System;
using System.Data;
using System.Collections.Generic;

namespace QLike.SiteLite
{
    /// <summary>
    /// DataAccess of [=Object]
    /// Template: DataAccess.tpl (ver 20100226)
    /// Please never modify this file manually
    /// </summary>
    public partial class [=Class]DA
    {
        #region ReadDb()
        /// <summary>
        /// Fill data into the entity
        /// </summary>
        private static void ReadDb(IDataReader reader, [=Class]Info [=Object])
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
[#Loop Type=All]
                    case "[=Property]":
                        if (reader[i] != System.DBNull.Value)
                            [=Object].[=Property] = [=TypeCast](reader[i]);
                        break;[#Loop/]
                    default:
                        break;
                }
            }
        }
        #endregion
        
        #region CountAll()
        /// <summary>
        /// Select count of [=Object]s
        /// </summary>
        /// <returns>int</returns>
        public static int CountAll()
        {
			try
			{
				int recordCount = 0;
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText([=Class]Query.CountAll);
					recordCount = Convert.ToInt32(provider.ExecuteScalar());
				}
				return recordCount;
            }
            catch(Exception ex)
            {
				throw ex;
			}
        }
        #endregion
        
        #region SumAll()
        /// <summary>
        /// Select sum of [=Object]s
        /// </summary>
        /// <returns>double</returns>
        public static double SumAll([=Class]Field sumField)
        {	
			try
			{
				double sum = 0;
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format([=Class]Query.SumAll, sumField.ToString());
					provider.SetCommandText(cmdText);
					sum = Convert.ToDouble(provider.ExecuteScalar());
				}
				
				return sum;
            }
            catch(Exception ex)
            {
				throw ex;
			}
        }
        #endregion
        
        #region CountBy([=Class]Field byField, object value)
        /// <summary>
        /// Count by the specified [=Object] field
        /// </summary>
        /// <returns>int</returns>
        public static int CountBy([=Class]Field byField, object value)
        {
			try
			{
				int recordCount = 0;
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format([=Class]Query.CountBy, byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
					recordCount = Convert.ToInt32(provider.ExecuteScalar());
				}
				return recordCount;
			}
			catch(Exception ex)
            {
				throw ex;
			}
        }
        #endregion
        
        #region SumBy([=Class]Field byField, object value, [=Class]Field sumField)
        /// <summary>
        /// Sum by the specified [=Object] field
        /// </summary>
        /// <returns>double</returns>
        public static double SumBy([=Class]Field byField, object value, [=Class]Field sumField)
        {
			try
			{
				double sum = 0;
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format([=Class]Query.SumBy, byField.ToString(), sumField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
					sum = Convert.ToDouble(provider.ExecuteScalar());
				}
				return sum;
			}
			catch(Exception ex)
            {
				throw ex;
			}
        }
        #endregion
        
        #region GetBy([=Class]Field byField, object value)
        /// <summary>
        /// Get the specified [=Object] entity
        /// </summary>[#Loop Type=Primary]
        /// <param name="[=Field]">[=Property]</param>[#Loop/]
        /// <returns>entity</returns>
        public static [=Class]Info GetBy([=Class]Field byField, object value)
        { 
            try
            {
				[=Class]Info [=Object] = null;
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format([=Class]Query.SelectBy, [=Class]Field.[=Class]Id, "DESC", byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
	                
					if (provider.Reader.Read())
					{
						[=Object] = new [=Class]Info();
						ReadDb(provider.Reader, [=Object]);
					}
				}
				return [=Object];
            }
            catch(Exception ex)
            {
				throw ex;
			}
        }
        #endregion
    
        #region SelectAll([=Class]Field sortField, bool isDesc)
        /// <summary>
        /// Select all [=Object]s
        /// </summary>
        /// <returns>entity list</returns>
        public static IList<[=Class]Info> SelectAll([=Class]Field sortField, bool isDesc)
        {
            try
            {
				IList<[=Class]Info> [=Object]s = new List<[=Class]Info>();
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format([=Class]Query.SelectAll, sortField.ToString(), isDesc ? "DESC" : "ASC");
					provider.SetCommandText(cmdText);

					while (provider.Reader.Read())
					{
						[=Class]Info [=Object] = new [=Class]Info();
						ReadDb(provider.Reader, [=Object]);
						[=Object]s.Add([=Object]);
					}
				}
				return [=Object]s;
            }
            catch(Exception ex)
            {
				throw ex;
			} 
        }
        #endregion
        
        #region SelectAll([=Class]Field sortField, bool isDesc, int pageSize, int pageIndex)
        /// <summary>
        /// Select all [=Object]s
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>entity list</returns>
        public static IList<[=Class]Info> SelectAll([=Class]Field sortField, bool isDesc, int pageSize, int pageIndex)
        {
            try
            {
				IList<[=Class]Info> [=Object]s = new List<[=Class]Info>();
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format([=Class]Query.PaginationAll, sortField.ToString(), isDesc ? "DESC" : "ASC");
					provider.SetCommandText(cmdText);
	                
					provider.AddParameterWithValue("@PageSize", pageSize);
					provider.AddParameterWithValue("@SkipCount", pageSize * pageIndex);

					while (provider.Reader.Read())
					{
						[=Class]Info [=Object] = new [=Class]Info();
						ReadDb(provider.Reader, [=Object]);
						[=Object]s.Add([=Object]);
					}
				}
				return [=Object]s;
            }
            catch(Exception ex)
            {
				throw ex;
			}
        }
        #endregion
        
        #region SelectBy([=Class]Field sortField, bool isDesc, [=Class]Field byField, object value)
        /// <summary>
        /// Get the specified [=Object] list
        /// </summary>
        /// <returns>entity list</returns>
        public static IList<[=Class]Info> SelectBy([=Class]Field sortField, bool isDesc, [=Class]Field byField, object value)
        { 
            try
            {
				IList<[=Class]Info> [=Object]s = new List<[=Class]Info>();
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format([=Class]Query.SelectBy, sortField.ToString(), isDesc ? "DESC" : "ASC", byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
	                
					while (provider.Reader.Read())
					{
						[=Class]Info [=Object] = new [=Class]Info();
						ReadDb(provider.Reader, [=Object]);
						[=Object]s.Add([=Object]);
					}
				}
				return [=Object]s;
			}
			catch(Exception ex)
            {
				throw ex;
			}
        }
        #endregion
       
        #region SelectBy([=Class]Field sortField, bool isDesc, [=Class]Field byField, object value, int pageSize, int pageIndex)
        /// <summary>
        /// Get the specified [=Object] list
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>entity list</returns>
        public static IList<[=Class]Info> SelectBy([=Class]Field sortField, bool isDesc, [=Class]Field byField, object value, int pageSize, int pageIndex)
        {
            try
            {
				IList<[=Class]Info> [=Object]s = new List<[=Class]Info>();
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format([=Class]Query.PaginationBy, sortField.ToString(), isDesc ? "DESC" : "ASC", byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
					provider.AddParameterWithValue("@PageSize", pageSize);
					provider.AddParameterWithValue("@SkipCount", pageSize * pageIndex);
	                
					while (provider.Reader.Read())
					{
						[=Class]Info [=Object] = new [=Class]Info();
						ReadDb(provider.Reader, [=Object]);
						[=Object]s.Add([=Object]);
					}
				}
				return [=Object]s;
            }
            catch(Exception ex)
            {
				throw ex;
			}
        }
        #endregion
        
        #region Insert()
        /// <summary>
        /// Insert a row according to the entity
        /// </summary>
        /// <param name="[=Object]">[=Class] Entity</param>
        /// <returns>if inserted</returns>
        public static bool Insert([=Class]Info [=Object])
        {
			try
			{
				bool inserted = false;
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText([=Class]Query.Insert);
[#Loop]
					provider.AddParameterWithValue("@[=Property]", [=Object].[=Property]);[#Loop/]

					if (provider.ExecuteNonQuery() > 0)
					{
						inserted = true;
					}
				}
				return inserted;
            }
            catch(Exception ex)
            {
				throw ex;
			}
        }
        
        /// <summary>
        /// Insert a row according to the entity
        /// </summary>
        /// <param name="[=Object]">[=Class] Entity</param>
        /// <param name="provider">DB Provider</param>
        /// <returns>if inserted</returns>
        public static bool Insert([=Class]Info [=Object], DbProvider provider)
        {
			try
			{
				bool inserted = false;
				provider.ClearParameters();
				provider.SetCommandText([=Class]Query.Insert);
[#Loop]
				provider.AddParameterWithValue("@[=Property]", [=Object].[=Property]);[#Loop/]

				if (provider.ExecuteNonQueryWithTransaction() > 0)
				{
					inserted = true;
				}
				return inserted;
			}
			catch(Exception ex)
            {
				throw ex;
			}
        }
        #endregion
        
        #region Update()
        /// <summary>
        /// Update a row according to the entity
        /// </summary>
        /// <param name="[=Object]">[=Class] Entity</param>
        /// <returns>if updated</returns>
        public static bool Update([=Class]Info [=Object])
        {
			try
			{
				bool updated = false;
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText([=Class]Query.Update);
[#Loop Type=Normal]
					provider.AddParameterWithValue("@[=Property]", [=Object].[=Property]);[#Loop/]
[#Loop Type=Primary]
					provider.AddParameterWithValue("@[=Property]", [=Object].[=Property]);[#Loop/]

					if (provider.ExecuteNonQuery() > 0)
					{
						updated = true;
					}
				}
				return updated;
			}
			catch(Exception ex)
            {
				throw ex;
			}
        }
        
        /// <summary>
        /// Update a row according to the entity
        /// </summary>
        /// <param name="[=Object]">[=Class] Entity</param>
        /// <param name="provider">DB Provider</param>
        /// <returns>if updated</returns>
        public static bool Update([=Class]Info [=Object], DbProvider provider)
        {
			try
			{
				bool updated = false;
				provider.ClearParameters();
				provider.SetCommandText([=Class]Query.Update);
[#Loop Type=Normal]
				provider.AddParameterWithValue("@[=Property]", [=Object].[=Property]);[#Loop/]
[#Loop Type=Primary]
				provider.AddParameterWithValue("@[=Property]", [=Object].[=Property]);[#Loop/]

				if (provider.ExecuteNonQueryWithTransaction() > 0)
				{
				    updated = true;
				}
				return updated;
			}
			catch(Exception ex)
            {
				throw ex;
			}
        }
        #endregion
        
        #region Delete()
        /// <summary>
        /// Delete the row from database
        /// </summary>[#Loop Type=Primary]
        /// <param name="[=Field]">[=Property]</param>[#Loop/]
        /// <returns>if deleted</returns>
        public static bool Delete([#Loop Type=Primary Separator=, ][=Type] [=Field][#Loop/])
        {
            try
            {
				bool updated = false;
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText([=Class]Query.Delete);
[#Loop Type=Primary]
					provider.AddParameterWithValue("@[=Property]", [=Field]);[#Loop/]
	                
					if (provider.ExecuteNonQuery() > 0)
					{
						updated = true;
					}
				}
				return updated;
			}
			catch(Exception ex)
            {
				throw ex;
			}
        }
        
        /// <summary>
        /// Delete the row from database
        /// </summary>[#Loop Type=Primary]
        /// <param name="[=Field]">[=Property]</param>[#Loop/]
        /// <param name="provider">DB Provider</param>
        /// <returns>if deleted</returns>
        public static bool Delete([#Loop Type=Primary Separator=, ][=Type] [=Field][#Loop/], DbProvider provider)
        {
            try
            {
				bool updated = false;
				provider.ClearParameters();
				provider.SetCommandText([=Class]Query.Delete);
[#Loop Type=Primary]
				provider.AddParameterWithValue("@[=Property]", [=Field]);[#Loop/]
	            
				if (provider.ExecuteNonQueryWithTransaction() > 0)
				{
					updated = true;
				}
				return updated;
			}
			catch(Exception ex)
            {
				throw ex;
			}
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
        public static IList<[=Class]Info> Search(SortStrategy strategy, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        {  
            try
            {
				IList<[=Class]Info> [=Object]s = new List<[=Class]Info>();
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(
						[=Class]Query.Search,
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
						[=Class]Info [=Object] = new [=Class]Info();
						ReadDb(provider.Reader, [=Object]);
						[=Object]s.Add([=Object]);
					}
				}
				return [=Object]s;
			}
			catch(Exception ex)
            {
				throw ex;
			}
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
            try
            {
				int recordCount = 0;
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(
						[=Class]Query.CountForSearch,
						SearchHelper.GetSearchExpression(conditions)
						);
					provider.SetCommandText(cmdText);
					foreach (KeyValuePair<string, object> pair in SearchHelper.GetParamValuePairs(conditions))
					{
						provider.AddParameterWithValue(pair.Key, pair.Value);
					}
					recordCount = Convert.ToInt32(provider.ExecuteScalar());
				}
				return recordCount;
			}
			catch(Exception ex)
            {
				throw ex;
			}
        }
        #endregion
        
        #region SumForSearch([=Class]Field sumField, List<ISearchCondition> conditions)
        /// <summary>
        /// Sum for search
        /// </summary>
        /// <returns>Sum</returns>
        public static double SumForSearch([=Class]Field sumField, List<ISearchCondition> conditions)
        {
            try
            {
				double sum = 0;
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(
						[=Class]Query.SumForSearch,
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
				return sum;
			}
			catch(Exception ex)
            {
				throw ex;
			}
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
            try
            {
				bool updated = false;
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format([=Class]Query.IncreaseField, byField.ToString(), increaseField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), byValue);
					provider.AddParameterWithValue("@IncreaseValue", increaseValue.ToString());

					if (provider.ExecuteNonQuery() > 0)
					{
						updated = true;
					}
				}
				return updated;
            }
            catch(Exception ex)
            {
				throw ex;
			}
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
        public static bool IncreaseField([=Class]Field byField, object byValue, [=Class]Field increaseField, object increaseValue, DbProvider provider)
        {
            try
            {
				bool updated = false;
				string cmdText = string.Format([=Class]Query.IncreaseField, byField.ToString(), increaseField.ToString());
				provider.ClearParameters();
				provider.SetCommandText(cmdText);
				provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), byValue);
				provider.AddParameterWithValue("@IncreaseValue", increaseValue.ToString());

				if (provider.ExecuteNonQueryWithTransaction() > 0)
				{
					updated = true;
				}
				return updated;
            }
            catch(Exception ex)
            {
				throw ex;
			}
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
			try
			{
				bool updated = false;
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format([=Class]Query.UpdateField, byField.ToString(), updateField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), byValue);
					provider.AddParameterWithValue("@NewValue", newValue.ToString());

					if (provider.ExecuteNonQuery() > 0)
					{
						updated = true;
					}
				}
				return updated;
			}
			catch(Exception ex)
            {
				throw ex;
			}
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
        public static bool UpdateField([=Class]Field byField, object byValue, [=Class]Field updateField, object newValue, DbProvider provider)
        {
			try
			{
				bool updated = false;
				string cmdText = string.Format([=Class]Query.UpdateField, byField.ToString(), updateField.ToString());
				provider.SetCommandText(cmdText);
				provider.ClearParameters();
				provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), byValue);
				provider.AddParameterWithValue("@NewValue", newValue.ToString());

				if (provider.ExecuteNonQueryWithTransaction() > 0)
				{
					updated = true;
				}
				
				return updated;
			}
			catch(Exception ex)
            {
				throw ex;
			}
        }
        #endregion
    }//end of class
    
    /// <summary>
    /// SQL Queries of [=Object]
    /// Please never modify this file manually
    /// </summary>
    public partial class [=Class]Query
    {
        #region Insert
        /// <summary>
        /// Insert
        /// </summary>
        internal static readonly string Insert = String.Concat(
            "    INSERT INTO [=Object]  ",
            "        (  ",[#Loop Type=All Separator=,  ",]
            "            [=Property][#Loop/]  ",
            "        )  ",
            "    VALUES  ",
            "        (  ",[#Loop Type=All Separator=,  ",]
            "            @[=Property][#Loop/]  ",
            "        )  ",
            " ");
        #endregion
        
        #region Update
        /// <summary>
        /// Update
        /// </summary>
        internal static readonly string Update = String.Concat(
            "    UPDATE [=Object]  ",
            "    SET  ",[#Loop Type=Normal Separator=,  ",]
            "        [=Property] = @[=Property][#Loop/]  ",
            "    WHERE   ",
            "        [#Loop Type=Primary Separator= AND ][=Property] = @[=Property][#Loop/]  ",
            " ");
        #endregion
        
        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        internal static readonly string Delete = String.Concat(
            "    DELETE FROM  ",
            "        [=Object]  ",
            "    WHERE  ",
            "        [#Loop Type=Primary Separator= AND ][=Property] = @[=Property][#Loop/]  ",
            " ");
        #endregion
        
        #region IncreaseField
        /// <summary>
        /// IncreaseField
        /// </summary>
        internal static readonly string IncreaseField = String.Concat(
            "    UPDATE [=Object]  ",
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
            "    UPDATE [=Object]  ",
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
            "    SELECT  ",[#Loop Type=All Separator=,  ",]
            "        [=Property][#Loop/]  ",
            "    FROM  ",
            "        [=Object]  ",
            "    ORDER BY {0} {1}  ",
            " ");
        #endregion
        
        #region SelectBy
        /// <summary>
        /// SelectBy
        /// </summary>
        internal static readonly string SelectBy = String.Concat(
            "    SELECT  ",[#Loop Type=All Separator=,  ",]
            "        [=Property][#Loop/]  ",
            "    FROM  ",
            "        [=Object]  ",
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
            "        COUNT([#Loop Type=Primary Separator= , ][=Property][#Loop/])  ",
            "    FROM  ",
            "        [=Object]  ",
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
            "        [=Object]  ",
            " ");
        #endregion
        
        #region CountBy
        /// <summary>
        /// CountBy
        /// </summary>
        internal static readonly string CountBy = String.Concat(
            "    SELECT  ",
            "        COUNT([#Loop Type=Primary Separator= , ][=Property][#Loop/])  ",
            "    FROM  ",
            "        [=Object]  ",
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
            "        [=Object]  ",
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
            "    SELECT TOP (@PageSize)  ",[#Loop Type=All Separator=,  ",]
            "        [=Property][#Loop/]  ",
            "    FROM  ",
            "        [=Object]  ",
            "    WHERE [#Loop Type=Primary Separator= , ][=Property][#Loop/] NOT IN  ",
            "        (  ",
            "        SELECT TOP (@SkipCount)  ",
            "            [#Loop Type=Primary Separator= ,  ",][=Property][#Loop/]  ",
            "        FROM  ",
            "            [=Object]  ",
            "        ORDER BY  ",
            "            {0} {1}  ",
            "        )  ",
            "    ORDER BY  ",
            "        {0} {1}  ",
            " ");
#else
        internal static readonly string PaginationAll = String.Concat(
            "    SELECT ",[#Loop Type=All Separator=,  ",]
            "        [=Property][#Loop/]  ",
            "    FROM  ",
            "        [=Object]  ",
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
            "    SELECT TOP (@PageSize)  ",[#Loop Type=All Separator=,  ",]
            "        [=Property][#Loop/]  ",
            "    FROM  ",
            "        [=Object]  ",
            "    WHERE [#Loop Type=Primary Separator= , ][=Property][#Loop/] NOT IN  ",
            "        (  ",
            "        SELECT TOP (@SkipCount)  ",
            "            [#Loop Type=Primary Separator= ,  ",][=Property][#Loop/]  ",
            "        FROM  ",
            "            [=Object]  ",
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
            "    SELECT ",[#Loop Type=All Separator=,  ",]
            "        [=Property][#Loop/]  ",
            "    FROM  ",
            "        [=Object]  ",
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
            "    SELECT TOP (@PageSize)  ",[#Loop Type=All Separator=,  ",]
            "        [=Property][#Loop/]  ",
            "    FROM  ",
            "        [=Object]  ",
            "    WHERE [#Loop Type=Primary Separator= , ][=Property][#Loop/] NOT IN  ",
            "        (  ",
            "        SELECT TOP (@SkipCount)  ",
            "            [#Loop Type=Primary Separator= ,  ",][=Property][#Loop/]  ",
            "        FROM  ",
            "            [=Object]  ",
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
            "    SELECT ",[#Loop Type=All Separator=,  ",]
            "        [=Property][#Loop/]  ",
            "    FROM  ",
            "        [=Object]  ",
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
            "        COUNT([#Loop Type=Primary Separator= , ][=Property][#Loop/])  ",
            "    FROM  ",
            "        [=Object]  ",
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
            "        [=Object]  ",
            "    WHERE  ",
            "        {0} ",
            " ");
        #endregion
    }//end of class
}
