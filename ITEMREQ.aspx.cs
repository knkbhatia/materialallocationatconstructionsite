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
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Collections;
public partial class ITEMREQ : System.Web.UI.Page
{
    public CreateOracleDataAccessObjects dataObj;

    protected void Page_Load(object sender, EventArgs e)
    {
        dataObj = new CreateOracleDataAccessObjects();
        string myquery;
        myquery = "delete from temp_item";
        dataObj.execute_sql(myquery);
        
        if (!IsPostBack)
        {
            displaylist();
        }
    }
    private void displaylist()
    {
        //OleDbConnection cnn = new OleDbConnection(System.Configuration.ConfigurationManager.AppSettings["Conn"]);
        string myquery;
        myquery = "select * from MACONT_DIR";
        dataObj.populate_list(ddlCont, myquery, "CONT_CODE", "CONT_NAME", "", "--Select Contractor--");

        //OleDbCommand cmd = new OleDbCommand(myquery, cnn);
        //cnn.Open();
        //OleDbDataReader rdr = cmd.ExecuteReader();
        //ddlCont.DataSource = rdr;
        //   ddlCont.DataBind();
        //    cnn.Close();
    }
    protected void ddlCont_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label4.Text = "";

        if (ddlCont.SelectedItem.Value.Length != 0)
        {
            Label4.Text = ddlCont.SelectedItem.Value.ToString();
            Label6.Text = ddlCont.SelectedItem.Text.ToString();
        }
        

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        displaygrid();
        
    }
    private void displaygrid()
    {

        string myquery;
        myquery = "select ITEM_CODE,ITEM_DESC,UNIT from MAITEM_DIR";
        dataObj.BindGridView(gvaudit, myquery);
        ModalPopupExtender1.Show();
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
    private void displaygrid1()
    {

        string myquery;
        myquery = "select ITEM_CODE,ITEM_DESC,UNIT from temp_item";
        dataObj.BindGridView(GridView1, myquery);

    }
    protected void onpageindexchanging1(object sender, GridViewPageEventArgs e)
    {
        int NewPage = GridView1.PageIndex + 1;
        if (NewPage <= GridView1.PageCount)
        {
            GridView1.PageIndex = e.NewPageIndex;
        }
        GridView1.DataBind();
        displaygrid1();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int32 i = 0;
        for (i = 0; i <= gvaudit.Rows.Count - 1; i++)
        {
            CheckBox chk = ((CheckBox)gvaudit.Rows[i].FindControl("chkcontcheck"));
            if (chk.Checked)
            {
                string itemcode = ((Label)gvaudit.Rows[i].FindControl("lblcontcode")).Text.ToString();
                string itemdesc = ((Label)gvaudit.Rows[i].FindControl("lblitemdesc")).Text.ToString();
                string itemunit = ((Label)gvaudit.Rows[i].FindControl("lblitemunit1")).Text.ToString();
                StringBuilder str = new StringBuilder();
                str.Append("insert into TEMP_ITEM(item_code,item_desc,unit) values ('");
                str.Append(itemcode.ToString());
                str.Append("','");
                str.Append(itemdesc.ToString());
                str.Append("','");
                str.Append(itemunit.ToString());
                str.Append("')");
                dataObj.execute_sql(str.ToString());
                if (dataObj.err_flag)
                {
                    lblError.Text = dataObj.ErrorStr;
                    return;
                }
                else
                {
                   
                    displaygrid1();
                }
               

            }
        }
        
        //string contcode = txtSItemCode.Text;
        //string contname = TextBox3.Text;
        //StringBuilder str = new StringBuilder();
        //str.Append("insert into MACONT_DIR values ('");
        //str.Append(contcode.ToString());
        //str.Append("','");
        //str.Append(contname.ToString());
        //str.Append("')");
        //dataObj.execute_sql(str.ToString());
        //displaygrid();
    }
    protected void gvJobActDtl_OnRowDeleting(Object sender, GridViewDeleteEventArgs e)
    {
        string itemcode = ((Label)GridView1.Rows[e.RowIndex].FindControl("lblcontcode1")).Text;
        StringBuilder strSql = new StringBuilder();
        strSql.Append("delete from temp_item where ITEM_CODE='");
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
            
            displaygrid1();
        }
        
    }
    protected void btncancel_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void gvaudit_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void findmax()
    {
        Int32 myquery = dataObj.ExecuteStatementCount("select  nvl(max(cont_req_no),0)+1 from MACONT_REQ_HDR");



    }
  

    protected void save_Click(object sender, EventArgs e)
    {
        Int32 reqno = dataObj.ExecuteStatementCount("select  nvl(max(cont_req_no),0)+1 from MACONT_REQ_HDR");

        string today = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
        string contcode = Label4.Text.ToString();
        string rs = remarks.Text;

        StringBuilder str = new StringBuilder();
        str.Append("insert into MACONT_REQ_HDR(CONT_REQ_NO,CONT_REQ_DATE,CONT_CODE,REMARKS) values ('");
        str.Append(reqno);
        str.Append("','");
        str.Append(today);
        str.Append("','");
        str.Append(contcode);
        str.Append("','");
        str.Append(rs);
        str.Append("')");

        //str.Append("insert into MACONT_REQ_HDR(CONT_REQ_NO,CONT_REQ_DATE,CONT_CODE,REMARKS) values ('");
        //str.Append(reqno);
        //str.Append("', sysdate,'");
        //str.Append(contcode);
        //str.Append("','");
        //str.Append(rs);
        //str.Append("')");

        dataObj.execute_sql(str.ToString());
        if (dataObj.err_flag)
        {
            lblError.Text = dataObj.ErrorStr;
            return;
        }
        else
        {
        Int32 i = 0;
        StringBuilder str1 = new StringBuilder();
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            string itemcode = ((Label)GridView1.Rows[i].FindControl("lblcontcode1")).Text.ToString();
            string qtyrec = ((TextBox)GridView1.Rows[i].FindControl("quantreq")).Text.ToString();
            str1.Length = 0;
            str1.Append("insert into MACONT_REQ_ITEM(CONT_REQ_NO,ITEM_CODE,QTY_REQ) values ('");
            str1.Append(reqno);
            str1.Append("','");
            str1.Append(itemcode);
            str1.Append("','");
            str1.Append(qtyrec);
            str1.Append("')");
            dataObj.execute_sql(str1.ToString());
            if (dataObj.err_flag)
            {
                lblError.Text = dataObj.ErrorStr;
                return;
            }
            else
            {

                str1.Length = 0;
                str1.Append("UPDATE MAITEM_DIR SET QTY_REQ=nvl(QTY_REQ,0)+'");
                str1.Append(qtyrec);
                str1.Append("' WHERE ITEM_CODE='");
                str1.Append(itemcode);
                str1.Append("'");
                dataObj.execute_sql(str1.ToString());
                if (dataObj.err_flag)
                {
                    lblError.Text = dataObj.ErrorStr;
                    return;
                }
            }
        }
        }

        
        string myquery;
        myquery = "delete from temp_item";
        dataObj.execute_sql(myquery);
        if (dataObj.err_flag)
        {
            lblError.Text = dataObj.ErrorStr;
            return;
        }
        else
        {
            save.Enabled = false;
            Alert.Show("Request No " + reqno + " generated.");
        }
       
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}
