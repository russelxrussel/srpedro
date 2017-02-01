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
    static string _gusername;

    static string _gBsNum;
    static string _gSsNum;


    public static string G_USERCODE
    {
        get {
            return _gusercode;
        }
        set {
            _gusercode = value;
        }
    
    }
   
    public static string G_USERNAME
    {
        get
        {
            return _gusername;
        }
        set
        {
            _gusername = value;
        }

    }


    public static string G_BSNUM
    {
        get
        {
            return _gBsNum;
        }
        set
        {
            _gBsNum = value;
        }
    }

    public static string G_SSNUM
    {
        get
        {
            return _gSsNum;
        }
        set
        {
            _gSsNum = value;
        }
    }
}