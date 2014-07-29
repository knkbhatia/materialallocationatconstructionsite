using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OracleDataAccess;
using System.Configuration;
using System.Text;

public partial class Dir_Item : System.Web.UI.Page
{
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
        myquery = "select ITEM_CODE,ITEM_DESC,UNIT,RATE,QTY_REC,QTY_REQ,QTY_ALLOC,QTY_ISS,STOCK from MAITEM_DIR where ITEM_CODE is not null ";
        if (txtitemcodesearch.Text.Length != 0)
        {
            myquery += " and ITEM_CODE like '%" + txtitemcodesearch.Text.ToUpper() + "%' ";
        }
        if (txtitemdescsearch.Text.Length != 0)
        {
            myquery += " and ITEM_DESC like '%" + txtitemdescsearch.Text + "%' ";
        }
        myquery += " order by ITEM_CODE";

        dataObj.BindGridView(gvaudit, myquery);
    }
    protected void onpageindexchanging(object sender, GridViewPageEventArgs e)
    {
        int NewPage = gvaudit.PageIndex + 1;
        if (NewPage <= gvaudit.PageCount)
        {
            gvaudit.PageIndex = e.NewPageIndex;
        }
        gvaudit.DataBind();
        displaygrid();
    }
    protected void gvCountDetails_OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        //Set the edit index.
        //gvCountDetails.EditIndex = e.NewEditIndex;
        gvaudit.EditIndex = e.NewEditIndex;
        //Bind data to the GridView control.
        //bindgvCountDetails();
        displaygrid();
    }

    protected void gvCountDetails_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        ////Reset the edit index.
        //gvCountDetails.EditIndex = -1;
        ////Bind data to the GridView control.
        //bindgvCountDetails();
        gvaudit.EditIndex = -1;
        displaygrid();

    }

    protected void gvCountDetails_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //lblError.Text = "";
        //string strCountryCode = ((Label)gvCountDetails.Rows[e.RowIndex].FindControl("lblcountry3")).Text;
        //string strCountryName = ((TextBox)gvCountDetails.Rows[e.RowIndex].FindControl("txtcountryname")).Text;
        //StringBuilder strSql1 = new StringBuilder();
        //strSql1.Append("update country_dir set COUNTRY_NAME='");
        //strSql1.Append(strCountryName.ToString());
        //strSql1.Append("' where COUNTRY_CODE_3='");
        //strSql1.Append(strCountryCode.ToString());
        //strSql1.Append("'");
        //dataObj.execute_sql(strSql1.ToString());
        ////Reset the edit index.
        //gvCountDetails.EditIndex = -1;
        ////Bind data to the GridView control.
        //bindgvCountDetails();
        string itemcode = ((Label)gvaudit.Rows[e.RowIndex].FindControl("lblitemcode")).Text;
        string itemdes = ((TextBox)gvaudit.Rows[e.RowIndex].FindControl("txtitemdesc")).Text;
        string itemunit = ((TextBox)gvaudit.Rows[e.RowIndex].FindControl("txtitemunit")).Text;
        string itemrate = ((TextBox)gvaudit.Rows[e.RowIndex].FindControl("txtitemrate")).Text;
        StringBuilder strSql1 = new StringBuilder();
        strSql1.Length = 0;

        strSql1.Append("update MAITEM_DIR set ITEM_DESC='");
        strSql1.Append(itemdes);
        strSql1.Append("', UNIT='");
        strSql1.Append(itemunit);
        strSql1.Append("', RATE='");
        strSql1.Append(itemrate);
        strSql1.Append("' where ITEM_CODE='");
        strSql1.Append(itemcode.ToString());
        strSql1.Append("'");
        dataObj.execute_sql(strSql1.ToString());
        if (dataObj.err_flag)
        {
            lblError.Text = dataObj.ErrorStr;
            return;
        }
        else
        {
            gvaudit.EditIndex = -1;
            displaygrid();
        }
        
    }



    protected void gvJobActDtl_OnRowDeleting(Object sender, GridViewDeleteEventArgs e)
    {
        //string p_jobcode = ((Label)gvJobActDtl.Rows[e.RowIndex].FindControl("lbljobno")).Text;
        //string p_flag = ((HiddenField)gvJobActDtl.Rows[e.RowIndex].FindControl("hfldflag")).Value;
        //string p_actorType = ((HiddenField)gvJobActDtl.Rows[e.RowIndex].FindControl("hfldactortype")).Value;
        //string p_actorCode = ((Label)gvJobActDtl.Rows[e.RowIndex].FindControl("lblactorcode")).Text;
        //StringBuilder strSql = new StringBuilder();
        //strSql.Append("delete from job_actor_dir where JOB_CODE='");
        //strSql.Append(p_jobcode.ToString());
        //strSql.Append("' and  CLIENT_EIL_FLAG='");
        //strSql.Append(p_flag.ToString());
        //strSql.Append("' and ACTOR_TYPE='");
        //strSql.Append(p_actorType.ToString());
        //strSql.Append("' and ACTOR_CODE='");
        //strSql.Append(p_actorCode.ToString());
        //strSql.Append("'");
        //dataObj.execute_sql(strSql.ToString());
        //if (dataObj.err_flag == true)
        //{
        //    lblError.Text = dataObj.ErrorStr;
        //    //blStatus = false;
        //}
        //else
        //{
        //    bindgvJobActDtl();
        //    //blStatus = true;
        //}
        string itemcode = ((Label)gvaudit.Rows[e.RowIndex].FindControl("lblitemcode")).Text;
        StringBuilder strSql = new StringBuilder();
        strSql.Append("delete from MAITEM_DIR where ITEM_CODE='");
        strSql.Append(itemcode.ToString());
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
        txtitemdes.Text = null;
        txtitemunit.Text = null;
        txtitemrate.Text = null;
        ModalPopupExtender1.Show();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string itemcode = txtSItemCode.Text;
        string itemdesc = txtitemdes.Text;
        string itemunit = txtitemunit.Text;
        string itemrate = txtitemrate.Text;
        StringBuilder str = new StringBuilder();
        str.Append("insert into MAITEM_DIR(item_code,item_desc,unit,rate) values ('");
        str.Append(itemcode.ToString());
        str.Append("','");
        str.Append(itemdesc.ToString());
        str.Append("','");
        str.Append(itemunit.ToString());
        str.Append("','");
        str.Append(itemrate.ToString());
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