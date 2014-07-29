using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OracleDataAccess;
using System.Configuration;
using System.Text;

public partial class Dir_Contractor : System.Web.UI.Page
{
    //OleDbConnection cnn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conn"]);

    public CreateOracleDataAccessObjects dataObj;

    protected void Page_Load(object sender, EventArgs e)
    {

        dataObj = new CreateOracleDataAccessObjects();

        if (!IsPostBack)
        {
            displaygrid();
        }
    }
    private void displaygrid()
    {

        string myquery;
        myquery = "select CONT_CODE,CONT_NAME from MACONT_DIR where CONT_CODE is not null";
        if (txtcontcodesearch.Text.Length != 0)
        {
            myquery += " and CONT_CODE like '%" + txtcontcodesearch.Text.ToUpper() + "%' ";
        }
        if (txtcontnamesearch.Text.Length != 0)
        {
            myquery += " and CONT_NAME like '%" + txtcontnamesearch.Text + "%' ";
        }
        myquery += " order by CONT_CODE";
        dataObj.BindGridView(contgrid, myquery);

    }
    protected void onpageindexchanging(object sender, GridViewPageEventArgs e)
    {
        int NewPage = contgrid.PageIndex + 1;
        if (NewPage <= contgrid.PageCount)
        {
            contgrid.PageIndex = e.NewPageIndex;
        }
        contgrid.DataBind();
        displaygrid();
    }
    protected void gvCountDetails_OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        contgrid.EditIndex = e.NewEditIndex;
        displaygrid();
    }
    protected void gvCountDetails_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        contgrid.EditIndex = -1;
        displaygrid();

    }
    protected void gvCountDetails_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string contcode = ((Label)contgrid.Rows[e.RowIndex].FindControl("lblcontcode")).Text;
        string contname = ((TextBox)contgrid.Rows[e.RowIndex].FindControl("txtcontdesc")).Text;
        StringBuilder strSql1 = new StringBuilder();
        strSql1.Append("update MACONT_DIR set CONT_NAME='");
        strSql1.Append(contname);
        strSql1.Append("' where CONT_CODE='");
        strSql1.Append(contcode.ToString());
        strSql1.Append("'");
        dataObj.execute_sql(strSql1.ToString());
        if (dataObj.err_flag)
        {
            lblError.Text = dataObj.ErrorStr;
            return;
        }
        else
        {
            contgrid.EditIndex = -1;
            displaygrid();
        }
        
    }
    protected void gvJobActDtl_OnRowDeleting(Object sender, GridViewDeleteEventArgs e)
    {
        string contcode = ((Label)contgrid.Rows[e.RowIndex].FindControl("lblcontcode")).Text;
        StringBuilder strSql = new StringBuilder();
        strSql.Append("delete from MACONT_DIR where CONT_CODE='");
        strSql.Append(contcode.ToString());
        strSql.Append("'");
        dataObj.execute_sql(strSql.ToString());
        if (dataObj.err_flag)
        {
            lblError.Text = dataObj.ErrorStr;
            return;
        }
        else
        {
            displaygrid();
        }
        

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        displaygrid();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        txtSItemCode.Text = null;
        TextBox3.Text = null;
        ModalPopupExtender1.Show();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string contcode = txtSItemCode.Text;
        string contname = TextBox3.Text;
        StringBuilder str = new StringBuilder();
        str.Append("insert into MACONT_DIR values ('");
        str.Append(contcode.ToString());
        str.Append("','");
        str.Append(contname.ToString());
        str.Append("')");
        dataObj.execute_sql(str.ToString());
        if (dataObj.err_flag)
        {
            lblError.Text = dataObj.ErrorStr;
            return;
        }
        else
        {
            displaygrid();
        }
        
    }
    protected void btncancel_Click(object sender, ImageClickEventArgs e)
    {

    }

}