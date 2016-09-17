using System;

namespace QLike.SiteLite
{
    /// <summary>
    /// Entity of channel
    /// Template: Entity.tpl (ver 20090611)
    /// </summary>
    public partial class ChannelInfo
    {
		#region Consts
        /// <summary>
        /// Name of the table
        /// </summary>
		public static readonly string TableName = "Channel";
		#endregion
		
		#region MaxLength
        /// <summary>
        /// Max length of channelId
        /// </summary>
        public static readonly int ChannelId_MaxLength = 32;
        
        /// <summary>
        /// Max length of displayName
        /// </summary>
        public static readonly int DisplayName_MaxLength = 50;
        
        /// <summary>
        /// Max length of url
        /// </summary>
        public static readonly int Url_MaxLength = 100;
        
        /// <summary>
        /// Max length of signInName
        /// </summary>
        public static readonly int SignInName_MaxLength = 50;
        
        /// <summary>
        /// Max length of password
        /// </summary>
        public static readonly int Password_MaxLength = 50;
        
        /// <summary>
        /// Max length of description
        /// </summary>
        public static readonly int Description_MaxLength = 100;
        
        /// <summary>
        /// Max length of visitTime
        /// </summary>
        public static readonly int VisitTime_MaxLength = 0;
        
        /// <summary>
        /// Max length of visitIp
        /// </summary>
        public static readonly int VisitIp_MaxLength = 50;
        
        /// <summary>
        /// Max length of visitCount
        /// </summary>
        public static readonly int VisitCount_MaxLength = 0;
        
        /// <summary>
        /// Max length of status
        /// </summary>
        public static readonly int Status_MaxLength = 0;
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

		#region field & property of Url
        private String url;

        /// <summary>
        /// Property of Url
        /// </summary>
        public String Url
        {
            get { return url; }
            set { url = value; }
        }
		#endregion

		#region field & property of SignInName
        private String signInName;

        /// <summary>
        /// Property of SignInName
        /// </summary>
        public String SignInName
        {
            get { return signInName; }
            set { signInName = value; }
        }
		#endregion

		#region field & property of Password
        private String password;

        /// <summary>
        /// Property of Password
        /// </summary>
        public String Password
        {
            get { return password; }
            set { password = value; }
        }
		#endregion

		#region field & property of Description
        private String description;

        /// <summary>
        /// Property of Description
        /// </summary>
        public String Description
        {
            get { return description; }
            set { description = value; }
        }
		#endregion

		#region field & property of VisitTime
        private Int64 visitTime;

        /// <summary>
        /// Property of VisitTime
        /// </summary>
        public Int64 VisitTime
        {
            get { return visitTime; }
            set { visitTime = value; }
        }
		#endregion

		#region field & property of VisitIp
        private String visitIp;

        /// <summary>
        /// Property of VisitIp
        /// </summary>
        public String VisitIp
        {
            get { return visitIp; }
            set { visitIp = value; }
        }
		#endregion

		#region field & property of VisitCount
        private Int32 visitCount;

        /// <summary>
        /// Property of VisitCount
        /// </summary>
        public Int32 VisitCount
        {
            get { return visitCount; }
            set { visitCount = value; }
        }
		#endregion

		#region field & property of Status
        private ChannelStatusEnum status;

        /// <summary>
        /// Property of Status
        /// </summary>
        public ChannelStatusEnum Status
        {
            get { return status; }
            set { status = value; }
        }
		#endregion


		#region constructor
        /// <summary>
        /// Constructor, init the entity
        /// </summary>
        public ChannelInfo()
        {
			this.channelId = string.Empty;
			this.displayName = string.Empty;
			this.url = string.Empty;
			this.signInName = string.Empty;
			this.password = string.Empty;
			this.description = string.Empty;
			this.visitTime = 0;
			this.visitIp = string.Empty;
			this.visitCount = 0;
			this.status = ChannelStatusEnum.Public;
        }
        #endregion
    }//end of class
    
    /// <summary>
    /// Fields enum of Channel
    /// </summary>
    public enum ChannelField
    {
    
        /// <summary>
        /// Field of channelId
        /// </summary>
		ChannelId,
    
        /// <summary>
        /// Field of displayName
        /// </summary>
		DisplayName,
    
        /// <summary>
        /// Field of url
        /// </summary>
		Url,
    
        /// <summary>
        /// Field of signInName
        /// </summary>
		SignInName,
    
        /// <summary>
        /// Field of password
        /// </summary>
		Password,
    
        /// <summary>
        /// Field of description
        /// </summary>
		Description,
    
        /// <summary>
        /// Field of visitTime
        /// </summary>
		VisitTime,
    
        /// <summary>
        /// Field of visitIp
        /// </summary>
		VisitIp,
    
        /// <summary>
        /// Field of visitCount
        /// </summary>
		VisitCount,
    
        /// <summary>
        /// Field of status
        /// </summary>
		Status
    }//end of enum
}
