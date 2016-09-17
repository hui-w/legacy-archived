using System;

namespace QLike.SiteLite
{
    /// <summary>
    /// Entity of category
    /// Template: Entity.tpl (ver 20090611)
    /// </summary>
    public partial class CategoryInfo
    {
		#region Consts
        /// <summary>
        /// Name of the table
        /// </summary>
		public static readonly string TableName = "Category";
		#endregion
		
		#region MaxLength
        /// <summary>
        /// Max length of categoryId
        /// </summary>
        public static readonly int CategoryId_MaxLength = 32;
        
        /// <summary>
        /// Max length of alias
        /// </summary>
        public static readonly int Alias_MaxLength = 30;
        
        /// <summary>
        /// Max length of displayName
        /// </summary>
        public static readonly int DisplayName_MaxLength = 50;
        
        /// <summary>
        /// Max length of displayOrder
        /// </summary>
        public static readonly int DisplayOrder_MaxLength = 0;
        
        /// <summary>
        /// Max length of contentCount
        /// </summary>
        public static readonly int ContentCount_MaxLength = 0;
        
        /// <summary>
        /// Max length of levelCode
        /// </summary>
        public static readonly int LevelCode_MaxLength = 50;
        
        /// <summary>
        /// Max length of status
        /// </summary>
        public static readonly int Status_MaxLength = 0;
        
        /// <summary>
        /// Max length of parentId
        /// </summary>
        public static readonly int ParentId_MaxLength = 32;
        
        /// <summary>
        /// Max length of channelId
        /// </summary>
        public static readonly int ChannelId_MaxLength = 32;
        #endregion


		#region field & property of CategoryId
        private String categoryId;

        /// <summary>
        /// Property of CategoryId
        /// </summary>
        public String CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }
		#endregion

		#region field & property of Alias
        private String alias;

        /// <summary>
        /// Property of Alias
        /// </summary>
        public String Alias
        {
            get { return alias; }
            set { alias = value; }
        }
		#endregion

		#region field & property of DisplayName
        private String displayName;

        /// <summary>
        /// Property of DisplayName
        /// </summary>
        public String DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }
		#endregion

		#region field & property of DisplayOrder
        private Int32 displayOrder;

        /// <summary>
        /// Property of DisplayOrder
        /// </summary>
        public Int32 DisplayOrder
        {
            get { return displayOrder; }
            set { displayOrder = value; }
        }
		#endregion

		#region field & property of ContentCount
        private Int32 contentCount;

        /// <summary>
        /// Property of ContentCount
        /// </summary>
        public Int32 ContentCount
        {
            get { return contentCount; }
            set { contentCount = value; }
        }
		#endregion

		#region field & property of LevelCode
        private String levelCode;

        /// <summary>
        /// Property of LevelCode
        /// </summary>
        public String LevelCode
        {
            get { return levelCode; }
            set { levelCode = value; }
        }
		#endregion

		#region field & property of Status
        private CategoryStatusEnum status;

        /// <summary>
        /// Property of Status
        /// </summary>
        public CategoryStatusEnum Status
        {
            get { return status; }
            set { status = value; }
        }
		#endregion

		#region field & property of ParentId
        private String parentId;

        /// <summary>
        /// Property of ParentId
        /// </summary>
        public String ParentId
        {
            get { return parentId; }
            set { parentId = value; }
        }
		#endregion

		#region field & property of ChannelId
        private String channelId;

        /// <summary>
        /// Property of ChannelId
        /// </summary>
        public String ChannelId
        {
            get { return channelId; }
            set { channelId = value; }
        }
		#endregion


		#region constructor
        /// <summary>
        /// Constructor, init the entity
        /// </summary>
        public CategoryInfo()
        {
			this.categoryId = string.Empty;
			this.alias = string.Empty;
			this.displayName = string.Empty;
			this.displayOrder = 0;
			this.contentCount = 0;
			this.levelCode = string.Empty;
			this.status = CategoryStatusEnum.Private;
			this.parentId = string.Empty;
			this.channelId = string.Empty;
        }
        #endregion
    }//end of class
    
    /// <summary>
    /// Fields enum of Category
    /// </summary>
    public enum CategoryField
    {
    
        /// <summary>
        /// Field of categoryId
        /// </summary>
		CategoryId,
    
        /// <summary>
        /// Field of alias
        /// </summary>
		Alias,
    
        /// <summary>
        /// Field of displayName
        /// </summary>
		DisplayName,
    
        /// <summary>
        /// Field of displayOrder
        /// </summary>
		DisplayOrder,
    
        /// <summary>
        /// Field of contentCount
        /// </summary>
		ContentCount,
    
        /// <summary>
        /// Field of levelCode
        /// </summary>
		LevelCode,
    
        /// <summary>
        /// Field of status
        /// </summary>
		Status,
    
        /// <summary>
        /// Field of parentId
        /// </summary>
		ParentId,
    
        /// <summary>
        /// Field of channelId
        /// </summary>
		ChannelId
    }//end of enum
}
