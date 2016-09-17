using System;

namespace QLike.SiteLite
{
    /// <summary>
    /// Entity of [=Object]
    /// Template: Entity.tpl (ver 20090611)
    /// </summary>
    public partial class [=Class]Info
    {
		#region Consts
        /// <summary>
        /// Name of the table
        /// </summary>
		public static readonly string TableName = "[=Class]";
		#endregion
		
		#region MaxLength[#Loop Type=All]
        /// <summary>
        /// Max length of [=Field]
        /// </summary>
        public static readonly int [=Property]_MaxLength = [=Size];
        [#Loop/]#endregion

[#Loop Type=All]
		#region field & property of [=Property]
        private [=Type] [=Field];

        /// <summary>
        /// Property of [=Property]
        /// </summary>
        public [=Type] [=Property]
        {
            get { return [=Field]; }
            set { [=Field] = value; }
        }
		#endregion
[#Loop/]

		#region constructor
        /// <summary>
        /// Constructor, init the entity
        /// </summary>
        public [=Class]Info()
        {[#Loop]
			this.[=Field] = [=Value];[#Loop/]
        }
        #endregion
    }//end of class
    
    /// <summary>
    /// Fields enum of [=Class]
    /// </summary>
    public enum [=Class]Field
    {[#Loop Type=All Separator=,]
    
        /// <summary>
        /// Field of [=Field]
        /// </summary>
		[=Property][#Loop/]
    }//end of enum
}
