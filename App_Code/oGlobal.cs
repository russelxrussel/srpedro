using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for oGlobal
/// </summary>
public static class oGlobal
{
    static string _gusercode;

    public static string G_USERCODE
    {
        get {
            return _gusercode;
        }
        set {
            _gusercode = value;
        }
    
    }
}