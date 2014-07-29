using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OracleDataAccess;
using System.Configuration;
using System.Text;

public partial class allocation : System.Web.UI.Page
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
        myquery = "select CONT_CODE,CONT_NAME from MACONT_DIR";
        dataObj.populate_list(ddlCont, myquery, "CONT_CODE", "CONT_NAME", "", "--Select Contractor--");

       
    }
    protected void ddlCont_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbldisplaycontcode.Text = "";

        if (ddlCont.SelectedItem.Value.Length != 0)
        {
            lbldisplaycontcode.Text = ddlCont.SelectedItem.Value.ToString();
            lbldisplaycontname.Text = ddlCont.SelectedItem.Text.ToString();
            displaycontreqno();
        }
        

    }
    private void displaycontreqno()
    {
        string myquery;
        string contcode = lbldisplaycontcode.Text;
        myquery = "select CONT_REQ_NO from MACONT_REQ_HDR R where CONT_CODE='" + contcode + "'AND R.CONT_REQ_NO NOT IN (SELECT CONT_REQ_NO FROM MAALLO_HDR)";
        dataObj.populate_list(ddlcontreqno, myquery, "CONT_REQ_NO", "CONT_REQ_NO", "", "--Select Request number--");
    }
    protected void ddlContreqno_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbldisplaycontreqno.Text = "";

        if (ddlCont.SelectedItem.Value.Length != 0)
        {
            lbldisplaycontreqno.Text = ddlcontreqno.SelectedItem.Value.ToString();
            GridView1.Visible = true;
            displaygrid();
        }

    }
  
    private void displaygrid()
    {

        string myquery;
        string contreqno = lbldisplaycontreqno.Text;
        myquery = "SELECT macont_req_item.item_code, maitem_dir.item_desc, maitem_dir.unit, macont_req_item.qty_req FROM macont_req_item  left JOIN maitem_dir ON macont_req_item.item_code = maitem_dir.item_code where cont_req_no='"+contreqno+"'";
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

    private Boolean checkvalidity()
    {
        Int32 i=0;
        for (i = 0; i <= GridView1.Rows.Count - 1; i++)
        {
            Int32 qtyreq = Convert.ToInt32(((Label)GridView1.Rows[i].FindControl("lblqtyreqgrid")).Text.ToString());
            Int32 qtyalloc = Convert.ToInt32(((TextBox)GridView1.Rows[i].FindControl("txtquantallocgrid")).Text.ToString());
            if (qtyalloc > qtyreq)
            {
                Alert.Show("Error! Quantity allocated cannot be greater than quantity requested");
                
                return false;

            }

           
        }
        return true;

    }
   
    protected void save_Click(object sender, EventArgs e)
    {
        if (checkvalidity())
        {
            Int32 allono = dataObj.ExecuteStatementCount("select  nvl(max(allo_no),0)+1 from MAALLO_HDR");
            string today = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
            string contreqno = lbldisplaycontreqno.Text.ToString();
            string remarks = lblremarks.Text;
            StringBuilder str = new StringBuilder();
            str.Append("insert into MAALLO_HDR(ALLO_NO,ALLO_DATE,CONT_REQ_NO,REMARKS) values ('");
            str.Append(allono);
            str.Append("','");
            str.Append(today);
            str.Append("','");
            str.Append(contreqno);
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
                    string qtyreq = ((Label)GridView1.Rows[i].FindControl("lblqtyreqgrid")).Text.ToString();
                    String itemcode = ((Label)GridView1.Rows[i].FindControl("lblcontcodegrid")).Text.ToString();
                    string qtyalloc = ((TextBox)GridView1.Rows[i].FindControl("txtquantallocgrid")).Text.ToString();
                    str1.Length = 0;
                    str1.Append("insert into MAALLO_ITEM(ALLO_NO,QTY_REQ,QTY_ALLOC,ITEM_CODE) values ('");
                    str1.Append(allono);
                    str1.Append("','");
                    str1.Append(qtyreq);
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
                        str1.Append("UPDATE MAITEM_DIR SET QTY_ALLOC=nvl(QTY_ALLOC,0)+'");
                        str1.Append(qtyalloc);
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

                Alert.Show("Request No " + allono + " generated.");
            }
        }

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }


}