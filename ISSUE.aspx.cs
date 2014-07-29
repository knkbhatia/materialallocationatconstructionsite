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

public partial class ISSUE : System.Web.UI.Page
{
    public CreateOracleDataAccessObjects dataObj;
    protected void Page_Load(object sender, EventArgs e)
    {
        dataObj = new CreateOracleDataAccessObjects();
        if (!IsPostBack)
        {
            displaylist();

        }

    }
    private void displaylist()
    {

        string myquery;
        myquery = "select CONT_REQ_NO,ALLO_NO from MAALLO_HDR R WHERE R.ALLO_NO NOT IN (SELECT ALLO_NO FROM MAISSUE_HDR)";
        dataObj.populate_list(ddlallocno, myquery, "CONT_REQ_NO", "ALLO_NO", "", "--Select Allocation Number--");


    }
    protected void ddlAllono_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbldisplayallocno.Text = "";

        if (ddlallocno.SelectedItem.Value.Length != 0)
        {
            lbldisplaycontreqno.Text = ddlallocno.SelectedItem.Value.ToString();
            lbldisplayallocno.Text = ddlallocno.SelectedItem.Text.ToString();
            displaycontdet();
        }
    }
    private void displaygrid()
    {

        string myquery;
        string allono = lbldisplayallocno.Text;
        myquery = "SELECT maallo_item.item_code, maitem_dir.item_desc, maitem_dir.unit,maitem_dir.QTY_REC, maallo_item.qty_alloc FROM maallo_item  left JOIN maitem_dir ON maallo_item.item_code = maitem_dir.item_code where allo_no='" + allono + "'";
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
        displaygrid();
    }
    private void displaycontdet()
    {
        string myquery;
        string contreqno = lbldisplaycontreqno.Text;
        myquery = "select CONT_CODE from MACONT_REQ_HDR where CONT_REQ_NO='" + contreqno + "'";
        //dataObj.execute_sql(myquery);
        OracleDataReader r,s;
        r=dataObj.DataReader(myquery);
        r.Read();
        String contcode = r.GetString(0);
        lbldisplaycontcode.Text = contcode.ToString();
        myquery = "select CONT_NAME from MACONT_DIR where CONT_CODE='" + contcode + "'";
        s = dataObj.DataReader(myquery);
        s.Read();
        String contname = s.GetString(0);
        lbldisplaycontname.Text = contname.ToString();
        GridView1.Visible = true;
        displaygrid();

    }
    private Boolean checkvalidity()
    {
        Int32 i = 0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            String itemcode = ((Label)GridView1.Rows[i].FindControl("lblcontcodegrid")).Text.ToString();
            Int32 qtyrec = dataObj.ExecuteStatementCount("select  nvl(qty_rec,0) from MAITEM_DIR WHERE ITEM_CODE='" + itemcode + "'");
            Int32 qtyalloc = Convert.ToInt32(((Label)GridView1.Rows[i].FindControl("lblqtyallocgrid")).Text.ToString());
            Int32 qtyiss = Convert.ToInt32(((TextBox)GridView1.Rows[i].FindControl("txtquantissgrid")).Text.ToString());
            if (qtyiss > qtyalloc)
            {
                Alert.Show("Error! Quantity issued cannot be greater than quantity allocated");

                return false;

            }
            else if (Convert.ToInt32(qtyiss) > qtyrec)
            {
                Alert.Show("Error! Quantity issued cannot be greater than quantity received");

                return false;
            }


        }
        return true;

    }
   
    protected void save_Click(object sender, EventArgs e)
    {
        if (checkvalidity())
        {
            Int32 issueno = dataObj.ExecuteStatementCount("select  nvl(max(issue_no),0)+1 from MAISSUE_HDR");
            string today = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
            string allocno = lbldisplayallocno.Text.ToString();
            string remarks = lblremarks.Text;
            StringBuilder str = new StringBuilder();
            str.Append("insert into MAISSUE_HDR(ISSUE_NO,ISSUE_DT,ALLO_NO,REMARKS) values ('");
            str.Append(issueno);
            str.Append("','");
            str.Append(today);
            str.Append("','");
            str.Append(allocno);
            str.Append("','");
            str.Append(remarks);
            str.Append("')");
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
                    string qtyiss = ((TextBox)GridView1.Rows[i].FindControl("txtquantissgrid")).Text.ToString();
                    String itemcode = ((Label)GridView1.Rows[i].FindControl("lblcontcodegrid")).Text.ToString();
                    string qtyalloc = ((Label)GridView1.Rows[i].FindControl("lblqtyallocgrid")).Text.ToString();

                    str1.Length = 0;
                    str1.Append("insert into MAISSUE_ITEM(ISSUE_NO,QTY_ISS,QTY_ALLOC,ITEM_CODE) values ('");
                    str1.Append(issueno);
                    str1.Append("','");
                    str1.Append(qtyiss);
                    str1.Append("','");
                    str1.Append(qtyalloc);
                    str1.Append("','");
                    str1.Append(itemcode);
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
                        str1.Append("UPDATE MAITEM_DIR SET QTY_ISS=nvl(QTY_ISS,0)+'");
                        str1.Append(qtyiss);
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
                save.Enabled = false;
                Alert.Show("Issue No " + issueno + " generated.");
            }
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}