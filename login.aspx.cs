﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SRPEDRO;


public partial class login : System.Web.UI.Page
{
    //decrpytClass decrypt = new decrpytClass();
    //xSystem oLogin = new xSystem();

    xtra oXtra = new xtra();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Set the focus
            //12-02-2015
      //      ScriptManager.RegisterStartupScript(this, this.GetType(), "focus", "document.getElementById('" + this.txtUserId.ClientID + "').focus();", true);
  
        }

     
    }

   



    protected void btnLogin_Click(object sender, EventArgs e)
    {
  
        if(oXtra.checkUser(txtUserId.Text, txtPassword.Text))
        {
            Response.Redirect("home.aspx");
        }
        else
        {
      
                lblErrorMessage.Text = "User is not valid.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "msg", "<script>$('#msgModal').modal('show');</script>", false);
        }
        
    }


    protected void Timer1_Tick(object sender, EventArgs e)
    {
        lblDT.Text = oXtra.GetServerDate().ToLongDateString() + " " + oXtra.GetServerDate().ToLongTimeString();
    }
}