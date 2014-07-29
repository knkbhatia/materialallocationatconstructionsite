using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OracleDataAccess;
using System.Configuration;
using System.Text;
using System.Data;
using System.IO;


public partial class LoginPage : System.Web.UI.Page
{
    public CreateOracleDataAccessObjects dataObj;

    protected void Page_Load(object sender, EventArgs e)
    {
        dataObj = new CreateOracleDataAccessObjects();
        error.Text = "";
    }
    protected void butlogin_Click(object sender, EventArgs e)
    {
        if (txtusername.Text.Length == 0)
        {
            error.Text = "Please enter userID";
            return;
        }
        if (txtpassword.Text.Length == 0)
        {
            error.Text = "Please enter password";
            return;
        }

        string myquery = "SELECT PASSWORD FROM MAUSER_DIR where USERID='" + txtusername.Text.ToString().ToUpper() + "'";

        string password = dataObj.ExecuteStatementString(myquery.ToString());
        if (password == txtpassword.Text)
        {
            Session["sUserId"] = txtusername.Text.ToString().ToUpper();

            DeleteTempFiles();

            Response.Redirect("Home.aspx");
        }
        else
        {
            Alert.Show("Invalid User ID and password");
        }
    }
    private void DeleteTempFiles()
    {
        string path = MapPath("") + "\\Reports\\" + Session["sUserId"];
        try
        {
            if (Directory.Exists(path) == true)
            {
                string[] dwgFiles = Directory.GetFiles(path, "*.*");
                int i = 0;
                for (i = 0; i <= dwgFiles.Length - 1; i++)
                {
                    File.Delete(dwgFiles[i]);
                }
            }
            else
            {
                CreateUserDirectory();
            }
        }
        catch (Exception ex)
        {
            //lblError.Text = ex.ToString();
        }
    }
    private void CreateUserDirectory()
    {
        string path = MapPath("") + "\\Reports\\" + Session["sUserId"];
        try
        {
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
        }
        catch (Exception ex)
        {
            //lblError.Text = ex.ToString();
        }
    }
}