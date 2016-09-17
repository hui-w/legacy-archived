//#define MSSQL
using System;
using System.Data;
using System.Collections.Generic;

namespace QLike.SiteLite
{
    /// <summary>
    /// DataAccess of channel
    /// Template: DataAccess.tpl (ver 20100226)
    /// Please never modify this file manually
    /// </summary>
    public partial class ChannelDA
    {
        #region ReadDb()
        /// <summary>
        /// Fill data into the entity
        /// </summary>
        private static void ReadDb(IDataReader reader, ChannelInfo channel)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {

                    case "ChannelId":
                        if (reader[i] != System.DBNull.Value)
                            channel.ChannelId = Convert.ToString(reader[i]);
                        break;
                    case "DisplayName":
                        if (reader[i] != System.DBNull.Value)
                            channel.DisplayName = Convert.ToString(reader[i]);
                        break;
                    case "Url":
                        if (reader[i] != System.DBNull.Value)
                            channel.Url = Convert.ToString(reader[i]);
                        break;
                    case "SignInName":
                        if (reader[i] != System.DBNull.Value)
                            channel.SignInName = Convert.ToString(reader[i]);
                        break;
                    case "Password":
                        if (reader[i] != System.DBNull.Value)
                            channel.Password = Convert.ToString(reader[i]);
                        break;
                    case "Description":
                        if (reader[i] != System.DBNull.Value)
                            channel.Description = Convert.ToString(reader[i]);
                        break;
                    case "VisitTime":
                        if (reader[i] != System.DBNull.Value)
                            channel.VisitTime = Convert.ToInt64(reader[i]);
                        break;
                    case "VisitIp":
                        if (reader[i] != System.DBNull.Value)
                            channel.VisitIp = Convert.ToString(reader[i]);
                        break;
                    case "VisitCount":
                        if (reader[i] != System.DBNull.Value)
                            channel.VisitCount = Convert.ToInt32(reader[i]);
                        break;
                    case "Status":
                        if (reader[i] != System.DBNull.Value)
                            channel.Status = (ChannelStatusEnum)Convert.ToInt32(reader[i]);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
        
        #region CountAll()
        /// <summary>
        /// Select count of channels
        /// </summary>
        /// <returns>int</returns>
        public static int CountAll()
        {
			int recordCount = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(ChannelQuery.CountAll);
					recordCount = Convert.ToInt32(provider.ExecuteScalar());
				}
            }
            catch{ }
            return recordCount;
        }
        #endregion
        
        #region SumAll()
        /// <summary>
        /// Select sum of channels
        /// </summary>
        /// <returns>double</returns>
        public static double SumAll(ChannelField sumField)
        {
			double sum = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(ChannelQuery.SumAll, sumField.ToString());
					provider.SetCommandText(cmdText);
					sum = Convert.ToDouble(provider.ExecuteScalar());
				}
            }
            catch{ }
            return sum;
        }
        #endregion
        
        #region CountBy(ChannelField byField, object value)
        /// <summary>
        /// Count by the specified channel field
        /// </summary>
        /// <returns>int</returns>
        public static int CountBy(ChannelField byField, object value)
        {
			int recordCount = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(ChannelQuery.CountBy, byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
					recordCount = Convert.ToInt32(provider.ExecuteScalar());
				}
			}
			catch{ }
            return recordCount;
        }
        #endregion
        
        #region SumBy(ChannelField byField, object value, ChannelField sumField)
        /// <summary>
        /// Sum by the specified channel field
        /// </summary>
        /// <returns>double</returns>
        public static double SumBy(ChannelField byField, object value, ChannelField sumField)
        {
			double sum = 0;
			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(ChannelQuery.SumBy, byField.ToString(), sumField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
					sum = Convert.ToDouble(provider.ExecuteScalar());
				}
			}
			catch{ }
            return sum;
        }
        #endregion
        
        #region GetBy(ChannelField byField, object value)
        /// <summary>
        /// Get the specified channel entity
        /// </summary>
        /// <param name="channelId">ChannelId</param>
        /// <returns>entity</returns>
        public static ChannelInfo GetBy(ChannelField byField, object value)
        {
            ChannelInfo channel = null;
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(ChannelQuery.SelectBy, ChannelField.ChannelId, "DESC", byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
	                
					if (provider.Reader.Read())
					{
						channel = new ChannelInfo();
						ReadDb(provider.Reader, channel);
					}
				}
            }
            catch{ }
            return channel;
        }
        #endregion
    
        #region SelectAll(ChannelField sortField, bool isDesc)
        /// <summary>
        /// Select all channels
        /// </summary>
        /// <returns>entity list</returns>
        public static IList<ChannelInfo> SelectAll(ChannelField sortField, bool isDesc)
        {
            IList<ChannelInfo> channels = new List<ChannelInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(ChannelQuery.SelectAll, sortField.ToString(), isDesc ? "DESC" : "ASC");
					provider.SetCommandText(cmdText);

					while (provider.Reader.Read())
					{
						ChannelInfo channel = new ChannelInfo();
						ReadDb(provider.Reader, channel);
						channels.Add(channel);
					}
				}
            }
            catch{ }
            return channels;
        }
        #endregion
        
        #region SelectAll(ChannelField sortField, bool isDesc, int pageSize, int pageIndex)
        /// <summary>
        /// Select all channels
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>entity list</returns>
        public static IList<ChannelInfo> SelectAll(ChannelField sortField, bool isDesc, int pageSize, int pageIndex)
        {
            IList<ChannelInfo> channels = new List<ChannelInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(ChannelQuery.PaginationAll, sortField.ToString(), isDesc ? "DESC" : "ASC");
					provider.SetCommandText(cmdText);
	                
					provider.AddParameterWithValue("@PageSize", pageSize);
					provider.AddParameterWithValue("@SkipCount", pageSize * pageIndex);

					while (provider.Reader.Read())
					{
						ChannelInfo channel = new ChannelInfo();
						ReadDb(provider.Reader, channel);
						channels.Add(channel);
					}
				}
            }
            catch{ }
            return channels;
        }
        #endregion
        
        #region SelectBy(ChannelField sortField, bool isDesc, ChannelField byField, object value)
        /// <summary>
        /// Get the specified channel list
        /// </summary>
        /// <returns>entity list</returns>
        public static IList<ChannelInfo> SelectBy(ChannelField sortField, bool isDesc, ChannelField byField, object value)
        {
            IList<ChannelInfo> channels = new List<ChannelInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(ChannelQuery.SelectBy, sortField.ToString(), isDesc ? "DESC" : "ASC", byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
	                
					while (provider.Reader.Read())
					{
						ChannelInfo channel = new ChannelInfo();
						ReadDb(provider.Reader, channel);
						channels.Add(channel);
					}
				}
			}
			catch{ }
            return channels;
        }
        #endregion
       
        #region SelectBy(ChannelField sortField, bool isDesc, ChannelField byField, object value, int pageSize, int pageIndex)
        /// <summary>
        /// Get the specified channel list
        /// </summary>
        /// <param name="pageSize">Page Size</param>
        /// <param name="pageIndex">Page Index</param>
        /// <returns>entity list</returns>
        public static IList<ChannelInfo> SelectBy(ChannelField sortField, bool isDesc, ChannelField byField, object value, int pageSize, int pageIndex)
        {
            IList<ChannelInfo> channels = new List<ChannelInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(ChannelQuery.PaginationBy, sortField.ToString(), isDesc ? "DESC" : "ASC", byField.ToString());
					provider.SetCommandText(cmdText);
					provider.AddParameterWithValue(string.Format("@{0}", byField.ToString()), value);
					provider.AddParameterWithValue("@PageSize", pageSize);
					provider.AddParameterWithValue("@SkipCount", pageSize * pageIndex);
	                
					while (provider.Reader.Read())
					{
						ChannelInfo channel = new ChannelInfo();
						ReadDb(provider.Reader, channel);
						channels.Add(channel);
					}
				}
            }
            catch{ }
            return channels;
        }
        #endregion
        
        #region Insert()
        /// <summary>
        /// Insert a row according to the entity
        /// </summary>
        /// <param name="channel">Channel Entity</param>
        /// <returns>if inserted</returns>
        public static bool Insert(ChannelInfo channel)
        {
            bool inserted = false;

			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(ChannelQuery.Insert);

					provider.AddParameterWithValue("@ChannelId", channel.ChannelId);
					provider.AddParameterWithValue("@DisplayName", channel.DisplayName);
					provider.AddParameterWithValue("@Url", channel.Url);
					provider.AddParameterWithValue("@SignInName", channel.SignInName);
					provider.AddParameterWithValue("@Password", channel.Password);
					provider.AddParameterWithValue("@Description", channel.Description);
					provider.AddParameterWithValue("@VisitTime", channel.VisitTime);
					provider.AddParameterWithValue("@VisitIp", channel.VisitIp);
					provider.AddParameterWithValue("@VisitCount", channel.VisitCount);
					provider.AddParameterWithValue("@Status", channel.Status);

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
        /// <param name="channel">Channel Entity</param>
        /// <param name="provider">DB Provider</param>
        /// <returns>if inserted</returns>
        public static bool Insert(ChannelInfo channel, DbProvider provider)
        {
            bool inserted = false;

			try
			{
				provider.ClearParameters();
				provider.SetCommandText(ChannelQuery.Insert);

				provider.AddParameterWithValue("@ChannelId", channel.ChannelId);
				provider.AddParameterWithValue("@DisplayName", channel.DisplayName);
				provider.AddParameterWithValue("@Url", channel.Url);
				provider.AddParameterWithValue("@SignInName", channel.SignInName);
				provider.AddParameterWithValue("@Password", channel.Password);
				provider.AddParameterWithValue("@Description", channel.Description);
				provider.AddParameterWithValue("@VisitTime", channel.VisitTime);
				provider.AddParameterWithValue("@VisitIp", channel.VisitIp);
				provider.AddParameterWithValue("@VisitCount", channel.VisitCount);
				provider.AddParameterWithValue("@Status", channel.Status);

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
        /// <param name="channel">Channel Entity</param>
        /// <returns>if updated</returns>
        public static bool Update(ChannelInfo channel)
        {
            bool updated = false;

			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(ChannelQuery.Update);

					provider.AddParameterWithValue("@DisplayName", channel.DisplayName);
					provider.AddParameterWithValue("@Url", channel.Url);
					provider.AddParameterWithValue("@SignInName", channel.SignInName);
					provider.AddParameterWithValue("@Password", channel.Password);
					provider.AddParameterWithValue("@Description", channel.Description);
					provider.AddParameterWithValue("@VisitTime", channel.VisitTime);
					provider.AddParameterWithValue("@VisitIp", channel.VisitIp);
					provider.AddParameterWithValue("@VisitCount", channel.VisitCount);
					provider.AddParameterWithValue("@Status", channel.Status);

					provider.AddParameterWithValue("@ChannelId", channel.ChannelId);

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
        /// <param name="channel">Channel Entity</param>
        /// <param name="provider">DB Provider</param>
        /// <returns>if updated</returns>
        public static bool Update(ChannelInfo channel, DbProvider provider)
        {
            bool updated = false;

			try
			{
				provider.ClearParameters();
				provider.SetCommandText(ChannelQuery.Update);

				provider.AddParameterWithValue("@DisplayName", channel.DisplayName);
				provider.AddParameterWithValue("@Url", channel.Url);
				provider.AddParameterWithValue("@SignInName", channel.SignInName);
				provider.AddParameterWithValue("@Password", channel.Password);
				provider.AddParameterWithValue("@Description", channel.Description);
				provider.AddParameterWithValue("@VisitTime", channel.VisitTime);
				provider.AddParameterWithValue("@VisitIp", channel.VisitIp);
				provider.AddParameterWithValue("@VisitCount", channel.VisitCount);
				provider.AddParameterWithValue("@Status", channel.Status);

				provider.AddParameterWithValue("@ChannelId", channel.ChannelId);

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
        /// <param name="channelId">ChannelId</param>
        /// <returns>if deleted</returns>
        public static bool Delete(String channelId)
        {
            bool updated = false;
            
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					provider.SetCommandText(ChannelQuery.Delete);

					provider.AddParameterWithValue("@ChannelId", channelId);
	                
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
        /// <param name="channelId">ChannelId</param>
        /// <param name="provider">DB Provider</param>
        /// <returns>if deleted</returns>
        public static bool Delete(String channelId, DbProvider provider)
        {
            bool updated = false;
            
            try
            {
				provider.ClearParameters();
				provider.SetCommandText(ChannelQuery.Delete);

				provider.AddParameterWithValue("@ChannelId", channelId);
	            
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
        public static IList<ChannelInfo> Search(SortStrategy strategy, int pageSize, int pageIndex, List<ISearchCondition> conditions)
        {
            IList<ChannelInfo> channels = new List<ChannelInfo>();
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(
						ChannelQuery.Search,
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
						ChannelInfo channel = new ChannelInfo();
						ReadDb(provider.Reader, channel);
						channels.Add(channel);
					}
				}
			}
			catch{ }
            return channels;
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
						ChannelQuery.CountForSearch,
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
        
        #region SumForSearch(ChannelField sumField, List<ISearchCondition> conditions)
        /// <summary>
        /// Sum for search
        /// </summary>
        /// <returns>Sum</returns>
        public static double SumForSearch(ChannelField sumField, List<ISearchCondition> conditions)
        {
            double sum = 0;
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(
						ChannelQuery.SumForSearch,
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
        public static bool IncreaseField(ChannelField byField, object byValue, ChannelField increaseField, object increaseValue)
        {
            bool updated = false;
            
            try
            {
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(ChannelQuery.IncreaseField, byField.ToString(), increaseField.ToString());
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
        public static bool IncreaseField(ChannelField byField, object byValue, ChannelField increaseField, object increaseValue, DbProvider provider)
        {
            bool updated = false;
            
            try
            {
				string cmdText = string.Format(ChannelQuery.IncreaseField, byField.ToString(), increaseField.ToString());
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
        public static bool UpdateField(ChannelField byField, object byValue, ChannelField updateField, object newValue)
        {
            bool updated = false;

			try
			{
				using (DbProvider provider = new DbProvider(DbArguments.Instance))
				{
					string cmdText = string.Format(ChannelQuery.UpdateField, byField.ToString(), updateField.ToString());
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
        public static bool UpdateField(ChannelField byField, object byValue, ChannelField updateField, object newValue, DbProvider provider)
        {
            bool updated = false;

			try
			{
				string cmdText = string.Format(ChannelQuery.UpdateField, byField.ToString(), updateField.ToString());
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
    /// SQL Queries of channel
    /// Please never modify this file manually
    /// </summary>
    public partial class ChannelQuery
    {
        #region Insert
        /// <summary>
        /// Insert
        /// </summary>
        internal static readonly string Insert = String.Concat(
            "    INSERT INTO channel  ",
            "        (  ",
            "            ChannelId,  ",
            "            DisplayName,  ",
            "            Url,  ",
            "            SignInName,  ",
            "            Password,  ",
            "            Description,  ",
            "            VisitTime,  ",
            "            VisitIp,  ",
            "            VisitCount,  ",
            "            Status  ",
            "        )  ",
            "    VALUES  ",
            "        (  ",
            "            @ChannelId,  ",
            "            @DisplayName,  ",
            "            @Url,  ",
            "            @SignInName,  ",
            "            @Password,  ",
            "            @Description,  ",
            "            @VisitTime,  ",
            "            @VisitIp,  ",
            "            @VisitCount,  ",
            "            @Status  ",
            "        )  ",
            " ");
        #endregion
        
        #region Update
        /// <summary>
        /// Update
        /// </summary>
        internal static readonly string Update = String.Concat(
            "    UPDATE channel  ",
            "    SET  ",
            "        DisplayName = @DisplayName,  ",
            "        Url = @Url,  ",
            "        SignInName = @SignInName,  ",
            "        Password = @Password,  ",
            "        Description = @Description,  ",
            "        VisitTime = @VisitTime,  ",
            "        VisitIp = @VisitIp,  ",
            "        VisitCount = @VisitCount,  ",
            "        Status = @Status  ",
            "    WHERE   ",
            "        ChannelId = @ChannelId  ",
            " ");
        #endregion
        
        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        internal static readonly string Delete = String.Concat(
            "    DELETE FROM  ",
            "        channel  ",
            "    WHERE  ",
            "        ChannelId = @ChannelId  ",
            " ");
        #endregion
        
        #region IncreaseField
        /// <summary>
        /// IncreaseField
        /// </summary>
        internal static readonly string IncreaseField = String.Concat(
            "    UPDATE channel  ",
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
            "    UPDATE channel  ",
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
            "        ChannelId,  ",
            "        DisplayName,  ",
            "        Url,  ",
            "        SignInName,  ",
            "        Password,  ",
            "        Description,  ",
            "        VisitTime,  ",
            "        VisitIp,  ",
            "        VisitCount,  ",
            "        Status  ",
            "    FROM  ",
            "        channel  ",
            "    ORDER BY {0} {1}  ",
            " ");
        #endregion
        
        #region SelectBy
        /// <summary>
        /// SelectBy
        /// </summary>
        internal static readonly string SelectBy = String.Concat(
            "    SELECT  ",
            "        ChannelId,  ",
            "        DisplayName,  ",
            "        Url,  ",
            "        SignInName,  ",
            "        Password,  ",
            "        Description,  ",
            "        VisitTime,  ",
            "        VisitIp,  ",
            "        VisitCount,  ",
            "        Status  ",
            "    FROM  ",
            "        channel  ",
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
            "        COUNT(ChannelId)  ",
            "    FROM  ",
            "        channel  ",
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
            "        channel  ",
            " ");
        #endregion
        
        #region CountBy
        /// <summary>
        /// CountBy
        /// </summary>
        internal static readonly string CountBy = String.Concat(
            "    SELECT  ",
            "        COUNT(ChannelId)  ",
            "    FROM  ",
            "        channel  ",
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
            "        channel  ",
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
            "        ChannelId,  ",
            "        DisplayName,  ",
            "        Url,  ",
            "        SignInName,  ",
            "        Password,  ",
            "        Description,  ",
            "        VisitTime,  ",
            "        VisitIp,  ",
            "        VisitCount,  ",
            "        Status  ",
            "    FROM  ",
            "        channel  ",
            "    WHERE ChannelId NOT IN  ",
            "        (  ",
            "        SELECT TOP (@SkipCount)  ",
            "            ChannelId  ",
            "        FROM  ",
            "            channel  ",
            "        ORDER BY  ",
            "            {0} {1}  ",
            "        )  ",
            "    ORDER BY  ",
            "        {0} {1}  ",
            " ");
#else
        internal static readonly string PaginationAll = String.Concat(
            "    SELECT ",
            "        ChannelId,  ",
            "        DisplayName,  ",
            "        Url,  ",
            "        SignInName,  ",
            "        Password,  ",
            "        Description,  ",
            "        VisitTime,  ",
            "        VisitIp,  ",
            "        VisitCount,  ",
            "        Status  ",
            "    FROM  ",
            "        channel  ",
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
            "        ChannelId,  ",
            "        DisplayName,  ",
            "        Url,  ",
            "        SignInName,  ",
            "        Password,  ",
            "        Description,  ",
            "        VisitTime,  ",
            "        VisitIp,  ",
            "        VisitCount,  ",
            "        Status  ",
            "    FROM  ",
            "        channel  ",
            "    WHERE ChannelId NOT IN  ",
            "        (  ",
            "        SELECT TOP (@SkipCount)  ",
            "            ChannelId  ",
            "        FROM  ",
            "            channel  ",
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
            "        ChannelId,  ",
            "        DisplayName,  ",
            "        Url,  ",
            "        SignInName,  ",
            "        Password,  ",
            "        Description,  ",
            "        VisitTime,  ",
            "        VisitIp,  ",
            "        VisitCount,  ",
            "        Status  ",
            "    FROM  ",
            "        channel  ",
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
            "        ChannelId,  ",
            "        DisplayName,  ",
            "        Url,  ",
            "        SignInName,  ",
            "        Password,  ",
            "        Description,  ",
            "        VisitTime,  ",
            "        VisitIp,  ",
            "        VisitCount,  ",
            "        Status  ",
            "    FROM  ",
            "        channel  ",
            "    WHERE ChannelId NOT IN  ",
            "        (  ",
            "        SELECT TOP (@SkipCount)  ",
            "            ChannelId  ",
            "        FROM  ",
            "            channel  ",
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
            "        ChannelId,  ",
            "        DisplayName,  ",
            "        Url,  ",
            "        SignInName,  ",
            "        Password,  ",
            "        Description,  ",
            "        VisitTime,  ",
            "        VisitIp,  ",
            "        VisitCount,  ",
            "        Status  ",
            "    FROM  ",
            "        channel  ",
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
            "        COUNT(ChannelId)  ",
            "    FROM  ",
            "        channel  ",
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
            "        channel  ",
            "    WHERE  ",
            "        {0} ",
            " ");
        #endregion
    }//end of class
}
