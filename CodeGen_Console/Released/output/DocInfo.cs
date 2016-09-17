using System;

namespace QLike.SiteLite
{
    /// <summary>
    /// Entity of doc
    /// Template: Entity.tpl (ver 20090611)
    /// </summary>
    public partial class DocInfo
    {
		#region Consts
        /// <summary>
        /// Name of the table
        /// </summary>
		public static readonly string TableName = "Doc";
		#endregion
		
		#region MaxLength
        /// <summary>
        /// Max length of docId
        /// </summary>
        public static readonly int DocId_MaxLength = 32;
        
        /// <summary>
        /// Max length of alias
        /// </summary>
        public static readonly int Alias_MaxLength = 30;
        
        /// <summary>
        /// Max length of subject
        /// </summary>
        public static readonly int Subject_MaxLength = 100;
        
        /// <summary>
        /// Max length of fromName
        /// </summary>
        public static readonly int FromName_MaxLength = 50;
        
        /// <summary>
        /// Max length of fromUrl
        /// </summary>
        public static readonly int FromUrl_MaxLength = 100;
        
        /// <summary>
        /// Max length of bodyAbstract
        /// </summary>
        public static readonly int BodyAbstract_MaxLength = 500;
        
        /// <summary>
        /// Max length of body
        /// </summary>
        public static readonly int Body_MaxLength = 2000;
        
        /// <summary>
        /// Max length of categoryId
        /// </summary>
        public static readonly int CategoryId_MaxLength = 32;
        
        /// <summary>
        /// Max length of tag
        /// </summary>
        public static readonly int Tag_MaxLength = 50;
        
        /// <summary>
        /// Max length of postTime
        /// </summary>
        public static readonly int PostTime_MaxLength = 0;
        
        /// <summary>
        /// Max length of updateTime
        /// </summary>
        public static readonly int UpdateTime_MaxLength = 0;
        
        /// <summary>
        /// Max length of readCount
        /// </summary>
        public static readonly int ReadCount_MaxLength = 0;
        
        /// <summary>
        /// Max length of commentCount
        /// </summary>
        public static readonly int CommentCount_MaxLength = 0;
        
        /// <summary>
        /// Max length of attribute
        /// </summary>
        public static readonly int Attribute_MaxLength = 0;
        
        /// <summary>
        /// Max length of priority
        /// </summary>
        public static readonly int Priority_MaxLength = 0;
        
        /// <summary>
        /// Max length of enableUbb
        /// </summary>
        public static readonly int EnableUbb_MaxLength = 0;
        
        /// <summary>
        /// Max length of contentMode
        /// </summary>
        public static readonly int ContentMode_MaxLength = 0;
        
        /// <summary>
        /// Max length of status
        /// </summary>
        public static readonly int Status_MaxLength = 0;
        
        /// <summary>
        /// Max length of channelId
        /// </summary>
        public static readonly int ChannelId_MaxLength = 32;
        #endregion


		#region field & property of DocId
        private String docId;

        /// <summary>
        /// Property of DocId
        /// </summary>
        public String DocId
        {
            get { return docId; }
            set { docId = value; }
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

		#region field & property of Subject
        private String subject;

        /// <summary>
        /// Property of Subject
        /// </summary>
        public String Subject
        {
            get { return subject; }
            set { subject = value; }
        }
		#endregion

		#region field & property of FromName
        private String fromName;

        /// <summary>
        /// Property of FromName
        /// </summary>
        public String FromName
        {
            get { return fromName; }
            set { fromName = value; }
        }
		#endregion

		#region field & property of FromUrl
        private String fromUrl;

        /// <summary>
        /// Property of FromUrl
        /// </summary>
        public String FromUrl
        {
            get { return fromUrl; }
            set { fromUrl = value; }
        }
		#endregion

		#region field & property of BodyAbstract
        private String bodyAbstract;

        /// <summary>
        /// Property of BodyAbstract
        /// </summary>
        public String BodyAbstract
        {
            get { return bodyAbstract; }
            set { bodyAbstract = value; }
        }
		#endregion

		#region field & property of Body
        private String body;

        /// <summary>
        /// Property of Body
        /// </summary>
        public String Body
        {
            get { return body; }
            set { body = value; }
        }
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

		#region field & property of Tag
        private String tag;

        /// <summary>
        /// Property of Tag
        /// </summary>
        public String Tag
        {
            get { return tag; }
            set { tag = value; }
        }
		#endregion

		#region field & property of PostTime
        private Int64 postTime;

        /// <summary>
        /// Property of PostTime
        /// </summary>
        public Int64 PostTime
        {
            get { return postTime; }
            set { postTime = value; }
        }
		#endregion

		#region field & property of UpdateTime
        private Int64 updateTime;

        /// <summary>
        /// Property of UpdateTime
        /// </summary>
        public Int64 UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }
		#endregion

		#region field & property of ReadCount
        private Int32 readCount;

        /// <summary>
        /// Property of ReadCount
        /// </summary>
        public Int32 ReadCount
        {
            get { return readCount; }
            set { readCount = value; }
        }
		#endregion

		#region field & property of CommentCount
        private Int32 commentCount;

        /// <summary>
        /// Property of CommentCount
        /// </summary>
        public Int32 CommentCount
        {
            get { return commentCount; }
            set { commentCount = value; }
        }
		#endregion

		#region field & property of Attribute
        private Int32 attribute;

        /// <summary>
        /// Property of Attribute
        /// </summary>
        public Int32 Attribute
        {
            get { return attribute; }
            set { attribute = value; }
        }
		#endregion

		#region field & property of Priority
        private Int32 priority;

        /// <summary>
        /// Property of Priority
        /// </summary>
        public Int32 Priority
        {
            get { return priority; }
            set { priority = value; }
        }
		#endregion

		#region field & property of EnableUbb
        private Boolean enableUbb;

        /// <summary>
        /// Property of EnableUbb
        /// </summary>
        public Boolean EnableUbb
        {
            get { return enableUbb; }
            set { enableUbb = value; }
        }
		#endregion

		#region field & property of ContentMode
        private ContentModeEnum contentMode;

        /// <summary>
        /// Property of ContentMode
        /// </summary>
        public ContentModeEnum ContentMode
        {
            get { return contentMode; }
            set { contentMode = value; }
        }
		#endregion

		#region field & property of Status
        private DocStatusEnum status;

        /// <summary>
        /// Property of Status
        /// </summary>
        public DocStatusEnum Status
        {
            get { return status; }
            set { status = value; }
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
        public DocInfo()
        {
			this.docId = string.Empty;
			this.alias = string.Empty;
			this.subject = string.Empty;
			this.fromName = string.Empty;
			this.fromUrl = string.Empty;
			this.bodyAbstract = string.Empty;
			this.body = string.Empty;
			this.categoryId = string.Empty;
			this.tag = string.Empty;
			this.postTime = 0;
			this.updateTime = 0;
			this.readCount = 0;
			this.commentCount = 0;
			this.attribute = 0;
			this.priority = 0;
			this.enableUbb = false;
			this.contentMode = ContentModeEnum.Text;
			this.status = DocStatusEnum.Private;
			this.channelId = string.Empty;
        }
        #endregion
    }//end of class
    
    /// <summary>
    /// Fields enum of Doc
    /// </summary>
    public enum DocField
    {
    
        /// <summary>
        /// Field of docId
        /// </summary>
		DocId,
    
        /// <summary>
        /// Field of alias
        /// </summary>
		Alias,
    
        /// <summary>
        /// Field of subject
        /// </summary>
		Subject,
    
        /// <summary>
        /// Field of fromName
        /// </summary>
		FromName,
    
        /// <summary>
        /// Field of fromUrl
        /// </summary>
		FromUrl,
    
        /// <summary>
        /// Field of bodyAbstract
        /// </summary>
		BodyAbstract,
    
        /// <summary>
        /// Field of body
        /// </summary>
		Body,
    
        /// <summary>
        /// Field of categoryId
        /// </summary>
		CategoryId,
    
        /// <summary>
        /// Field of tag
        /// </summary>
		Tag,
    
        /// <summary>
        /// Field of postTime
        /// </summary>
		PostTime,
    
        /// <summary>
        /// Field of updateTime
        /// </summary>
		UpdateTime,
    
        /// <summary>
        /// Field of readCount
        /// </summary>
		ReadCount,
    
        /// <summary>
        /// Field of commentCount
        /// </summary>
		CommentCount,
    
        /// <summary>
        /// Field of attribute
        /// </summary>
		Attribute,
    
        /// <summary>
        /// Field of priority
        /// </summary>
		Priority,
    
        /// <summary>
        /// Field of enableUbb
        /// </summary>
		EnableUbb,
    
        /// <summary>
        /// Field of contentMode
        /// </summary>
		ContentMode,
    
        /// <summary>
        /// Field of status
        /// </summary>
		Status,
    
        /// <summary>
        /// Field of channelId
        /// </summary>
		ChannelId
    }//end of enum
}
