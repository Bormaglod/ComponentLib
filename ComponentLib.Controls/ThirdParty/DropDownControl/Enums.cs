///-----------------------------------------------------------------------
/// <copyright file="?.cs" company="Richard Blythe">
///     Copyright (c) 2010 Richard Blythe. All rights reserved.
/// </copyright>
/// <author>Richard Blythe</author>
/// <license>
///     Code Project Open License (CPOL)
///     See <http://www.codeproject.com/info/cpol10.aspx>.
/// </license>
/// <date>20.07.2010</date>
/// <summary>Defines the ? class.</summary>
///-----------------------------------------------------------------------

namespace ComponentLib.Controls.ThirdParty.DropDownControl
{
    #region Using directives
    
    using System;
    
    #endregion
    
    public enum DockSide
    {
        Left,
        
        Right
    }

    public enum DropState
    {
        Closed,
        
        Closing,
        
        Dropping,
        
        Dropped
    }
}
