using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OracleDataAccess;
using System.Configuration;
using System.Text;

public partial class Allocation_Report : System.Web.UI.Page
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
        myquery = "select MAALLO_HDR.ALLO_NO,MAALLO_HDR.ALLO_DATE,MAALLO_HDR.CONT_REQ_NO,MACONT_REQ_HDR.CONT_CODE, MAALLO_HDR.REMARKS from MAALLO_HDR left JOIN MACONT_REQ_HDR ON MAALLO_HDR.CONT_REQ_NO = MACONT_REQ_HDR.CONT_REQ_NO where ALLO_NO is not null ";
        if (txtallonosearch.Text.Length != 0)
        {
            myquery += " and ALLO_NO like '%" + txtallonosearch.Text + "%' ";
        }
        if (txtcontreqnosearch.Text.Length != 0)
        {
            myquery += " and MAALLO_HDR.CONT_REQ_NO like '%" + txtcontreqnosearch.Text + "%' ";
        }
        myquery += " order by ALLO_NO";

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
            ALLONO.Value = ((Label)gvaudit.Rows[i].FindControl("lblallono")).Text;
            bindfvJobActEdit();
        }
        else if (e.CommandName == "Print")
        {
            Int32 i = 0;
            i = Convert.ToInt32(e.CommandArgument);
            string strAllocNo = ((Label)gvaudit.Rows[i].FindControl("lblallono")).Text;

            string[] TextArray = { "pAllocNo", "" + strAllocNo.ToString() + "" };
            GenerateReport.print("Allocation", "pdf", TextArray);
        }
    }
    protected void gvaudit_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void bindfvJobActEdit()
    {
        string allono = ALLONO.Value;
        string myquery;
        myquery = "select MAALLO_ITEM.ITEM_CODE,MAITEM_DIR.ITEM_DESC,MAALLO_ITEM.QTY_REQ,MAALLO_ITEM.QTY_ALLOC from MAALLO_ITEM left JOIN MAITEM_DIR ON MAALLO_ITEM.ITEM_CODE = MAITEM_DIR.ITEM_CODE WHERE ALLO_NO='" + allono + "'";
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
            string strAllocNo = ((Label)gvaudit.SelectedRow.FindControl("lblallono")).Text;

            string[] TextArray = { "pAllocNo", "" + strAllocNo.ToString() + "" };
            GenerateReport.print("Allocation", "pdf", TextArray);
        }
        else
        {
            Alert.Show("Please select any Allocation No.");
        }
    }
}