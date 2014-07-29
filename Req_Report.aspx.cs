using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OracleDataAccess;
using System.Configuration;
using System.Text;


public partial class Req_Report : System.Web.UI.Page
{
    public CreateOracleDataAccessObjects dataObj;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["sUserId"] = "KNKBHATIA";
        dataObj = new CreateOracleDataAccessObjects();

        if (!IsPostBack)
        {
            displaygrid();
        }

    }
    private void displaygrid()
    {

        string myquery;
        myquery = "select MACONT_REQ_HDR.CONT_REQ_NO,MACONT_REQ_HDR.CONT_REQ_DATE,MACONT_REQ_HDR.CONT_CODE,MACONT_DIR.CONT_NAME,MACONT_REQ_HDR.REMARKS from MACONT_REQ_HDR left JOIN MACONT_DIR ON MACONT_REQ_HDR.CONT_CODE = MACONT_DIR.CONT_CODE where CONT_REQ_NO is not null ";
        if (txtcontreqnosearch.Text.Length != 0)
        {
            myquery += " and CONT_REQ_NO like '%" + txtcontreqnosearch.Text + "%' ";
        }
        if (txtcontcodesearch.Text.Length != 0)
        {
            myquery += " and CONT_CODE like '%" + txtcontcodesearch.Text.ToUpper() + "%' ";
        }
        myquery += " order by CONT_REQ_NO";

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

    protected void Button1_Click(object sender, EventArgs e)
    {
        displaygrid();
    }
    protected void gvJobActDtl_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditJobAct")
        {
            Int32 i = 0;
            i = Convert.ToInt32(e.CommandArgument);
            REQNO.Value = ((Label)gvaudit.Rows[i].FindControl("lblcontreqno")).Text;
            //lblfvjobcode.Text = ((Label)gvaudit.Rows[i].FindControl("lbljobno")).Text;
            //lblfvflag.Text = ((HiddenField)gvaudit.Rows[i].FindControl("hfldflag")).Value;
            //lblfvactortype.Text = ((HiddenField)gvaudit.Rows[i].FindControl("hfldactortype")).Value;
            //lblfvactorcode.Text = ((Label)gvaudit.Rows[i].FindControl("lblactorcode")).Text;

            bindfvJobActEdit();
            

        }
    }
    protected void gvaudit_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void bindfvJobActEdit()
    {
        string contreqno=REQNO.Value;
        string myquery;
        myquery = "select MACONT_REQ_ITEM.ITEM_CODE,MAITEM_DIR.ITEM_DESC,MACONT_REQ_ITEM.QTY_REQ from MACONT_REQ_ITEM left JOIN MAITEM_DIR ON MACONT_REQ_ITEM.ITEM_CODE = MAITEM_DIR.ITEM_CODE WHERE CONT_REQ_NO='" + contreqno + "'";
        dataObj.BindGridView(GridView1, myquery);
        if (dataObj.err_flag)
        {
            lblError.Text = dataObj.ErrorStr;
            return;
        }
        ModalPopupExtender1.Show();
       
    }
    protected void btncancel_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (gvaudit.SelectedIndex != -1)
        {
            string strAllocNo = ((Label)gvaudit.SelectedRow.FindControl("lblcontreqno")).Text;

            string[] TextArray = { "pReqNo", "" + strAllocNo.ToString() + "" };
            GenerateReport.print("Request", "pdf", TextArray);
        }
        else
        {
            Alert.Show("Please select any Allocation No.");
        }
    }
}