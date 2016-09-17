using System;

namespace QLike.SiteLite
{
    /// <summary>
    /// Entity of comment
    /// Template: Entity.tpl (ver 20090611)
    /// </summary>
    public partial class CommentInfo
    {
		#region Consts
        /// <summary>
        /// Name of the table
        /// </summary>
		public static readonly string TableName = "Comment";
		#endregion
		
		#region MaxLength
        /// <summary>
        /// Max length of commentId
        /// </summary>
        public static readonly int CommentId_MaxLength = 32;
        
        /// <summary>
        /// Max length of docId
        /// </summary>
        public static readonly int DocId_MaxLength = 32;
        
        /// <summary>
        /// Max length of body
        /// </summary>
        public static readonly int Body_MaxLength = 500;
        
        /// <summary>
        /// Max length of status
        /// </summary>
        public static readonly int Status_MaxLength = 0;
        
        /// <summary>
        /// Max length of userName
        /// </summary>
        public static readonly int UserName_MaxLength = 50;
        
        /// <summary>
        /// Max length of userMail
        /// </summary>
        public static readonly int UserMail_MaxLength = 50;
        
        /// <summary>
        /// Max length of userWeb
        /// </summary>
        public static readonly int UserWeb_MaxLength = 50;
        
        /// <summary>
        /// Max length of userIm
        /// </summary>
        public static readonly int UserIm_MaxLength = 50;
        
        /// <summary>
        /// Max length of postIp
        /// </summary>
        public static readonly int PostIp_MaxLength = 50;
        
        /// <summary>
        /// Max length of postTime
        /// </summary>
        public static readonly int PostTime_MaxLength = 0;
        
        /// <summary>
        /// Max length of channelId
        /// </summary>
        public static readonly int ChannelId_MaxLength = 32;
        #endregion


		#region field & property of CommentId
        private String commentId;

        /// <summary>
        /// Property of CommentId
        /// </summary>
        public String CommentId
        {
            get { return commentId; }
            set { commentId = value; }
        }
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

		#region field & property of Status
        private CommentStatusEnum status;

        /// <summary>
        /// Property of Status
        /// </summary>
        public CommentStatusEnum Status
        {
            get { return status; }
            set { status = value; }
        }
		#endregion

		#region field & property of UserName
        private String userName;

        /// <summary>
        /// Property of UserName
        /// </summary>
        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }
		#endregion

		#region field & property of UserMail
        private String userMail;

        /// <summary>
        /// Property of UserMail
        /// </summary>
        public String UserMail
        {
            get { return userMail; }
            set { userMail = value; }
        }
		#endregion

		#region field & property of UserWeb
        private String userWeb;

        /// <summary>
        /// Property of UserWeb
        /// </summary>
        public String UserWeb
        {
            get { return userWeb; }
            set { userWeb = value; }
        }
		#endregion

		#region field & property of UserIm
        private String userIm;

        /// <summary>
        /// Property of UserIm
        /// </summary>
        public String UserIm
        {
            get { return userIm; }
            set { userIm = value; }
        }
		#endregion

		#region field & property of PostIp
        private String postIp;

        /// <summary>
        /// Property of PostIp
        /// </summary>
        public String PostIp
        {
            get { return postIp; }
            set { postIp = value; }
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
        public CommentInfo()
        {
			this.commentId = string.Empty;
			this.docId = string.Empty;
			this.body = string.Empty;
			this.status = CommentStatusEnum.Pending;
			this.userName = string.Empty;
			this.userMail = string.Empty;
			this.userWeb = string.Empty;
			this.userIm = string.Empty;
			this.postIp = string.Empty;
			this.postTime = 0;
			this.channelId = string.Empty;
        }
        #endregion
    }//end of class
    
    /// <summary>
    /// Fields enum of Comment
    /// </summary>
    public enum CommentField
    {
    
        /// <summary>
        /// Field of commentId
        /// </summary>
		CommentId,
    
        /// <summary>
        /// Field of docId
        /// </summary>
		DocId,
    
        /// <summary>
        /// Field of body
        /// </summary>
		Body,
    
        /// <summary>
        /// Field of status
        /// </summary>
		Status,
    
        /// <summary>
        /// Field of userName
        /// </summary>
		UserName,
    
        /// <summary>
        /// Field of userMail
        /// </summary>
		UserMail,
    
        /// <summary>
        /// Field of userWeb
        /// </summary>
		UserWeb,
    
        /// <summary>
        /// Field of userIm
        /// </summary>
		UserIm,
    
        /// <summary>
        /// Field of postIp
        /// </summary>
		PostIp,
    
        /// <summary>
        /// Field of postTime
        /// </summary>
		PostTime,
    
        /// <summary>
        /// Field of channelId
        /// </summary>
		ChannelId
    }//end of enum
}
