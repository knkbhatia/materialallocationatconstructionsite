using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;

/// <summary>
/// Summary description for Confirm
/// </summary>
public class Confirm
{
    //public Confirm()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}
    public static  string Show (string error)
    {
        string strResult = "";
        Page page = HttpContext.Current.Handler as Page;
        if (page != null)
        {
            error = error.Replace("'", "\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "var i=confirm('" + error + "');if(i==1){strResult='Y'}else{strResult='N'};", true);
        }
        return strResult;
        //for redirecting the page to some other page use 
        //ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');window.location='newpage.aspx';", true);

    }
}