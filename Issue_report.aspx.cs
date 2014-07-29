using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OracleDataAccess;
using System.Configuration;
using System.Text;
public partial class Issue_report : System.Web.UI.Page
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        displaygrid();
    }
    private void displaygrid()
    {

        string myquery;
        myquery = "select MAISSUE_HDR.ISSUE_NO,MAISSUE_HDR.ISSUE_DT,MAISSUE_HDR.ALLO_NO,MAALLO_HDR.CONT_REQ_NO, MAISSUE_HDR.REMARKS from MAISSUE_HDR left JOIN MAALLO_HDR ON MAISSUE_HDR.ALLO_NO = MAALLO_HDR.ALLO_NO where ISSUE_NO is not null ";
        if (txtissuenosearch.Text.Length != 0)
        {
            myquery += " and ISSUE_NO like '%" + txtissuenosearch.Text + "%' ";
        }
        if (txtallonosearch.Text.Length != 0)
        {
            myquery += " and MAISSUE_HDR.ALLO_NO like '%" + txtallonosearch.Text + "%' ";
        }
        myquery += " order by ISSUE_NO";

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


    protected void gvJobActDtl_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditJobAct")
        {
            Int32 i = 0;
            i = Convert.ToInt32(e.CommandArgument);
            ISSUENO.Value = ((Label)gvaudit.Rows[i].FindControl("lblallono")).Text;
            bindfvJobActEdit();


        }
    }
    protected void gvaudit_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void bindfvJobActEdit()
    {
        string ISSUEno = ISSUENO.Value;
        string myquery;
        myquery = "select MAISSUE_ITEM.ITEM_CODE,MAITEM_DIR.ITEM_DESC,MAISSUE_ITEM.QTY_ALLOC,MAISSUE_ITEM.QTY_ISS from MAISSUE_ITEM left JOIN MAITEM_DIR ON MAISSUE_ITEM.ITEM_CODE = MAITEM_DIR.ITEM_CODE WHERE ISSUE_NO='" + ISSUEno + "'";
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
            string strAllocNo = ((Label)gvaudit.SelectedRow.FindControl("lblissueno")).Text;

            string[] TextArray = { "pIssueNo", "" + strAllocNo.ToString() + "" };
            GenerateReport.print("Issue", "pdf", TextArray);
        }
        else
        {
            Alert.Show("Please select any Allocation No.");
        }
    }
}