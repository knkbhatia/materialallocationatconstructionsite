<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ISSUE.aspx.cs" Inherits="ISSUE"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="height: 236px; font: caption;">
        <h1>
            <asp:Label ID="lblissue" runat="server" Text="ISSUE" CssClass="myPageHeader" ></asp:Label></h1>
        <br />
        <h2>
            <asp:Label ID="lblselallocno" runat="server" Text="Select allocation number: " CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:DropDownList ID="ddlallocno" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAllono_SelectedIndexChanged">
            </asp:DropDownList>
            <br />
            <asp:Label ID="lblallocno" runat="server" Text="Allocation number:" CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:Label ID="lbldisplayallocno" runat="server" Text="" CssClass="myLabeldisp"></asp:Label>
            <br />
            <asp:Label ID="lblcontreqno" runat="server" Text="Contractor request number:" CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:Label ID="lbldisplaycontreqno" runat="server" Text="" CssClass="myLabeldisp"></asp:Label>
        </h2>
        <h2>
            <asp:Label ID="lblcontcode" runat="server" Text="Contractor code:" CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:Label ID="lbldisplaycontcode" runat="server" Text="" CssClass="myLabel"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblcontname" runat="server" Text="Contractor name:" CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:Label ID="lbldisplaycontname" runat="server" Text="" CssClass="myLabel"> </asp:Label>
            <br />
        </h2>
        <h2>
             <asp:Label ID="rmrks" runat="server" Text="Enter remarks(if any):" CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:TextBox ID="lblremarks" runat="server" Width="509px" MaxLength="200" CssClass="myInput"></asp:TextBox>
            <br />
        </h2>
         <p>
                <asp:Label ID="lblError" runat="server" ViewStateMode="Disabled" CssClass="myError"></asp:Label>
            </p>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None"
        AllowPaging="True" CssClass="grid-view" PageSize="25" OnPageIndexChanging="onpageindexchanging1"
        CellPadding="4" ForeColor="#333333" Visible="false">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="ITEM CODE">
                <ItemTemplate>
                    <asp:Label ID="lblcontcodegrid" runat="server" Text='<%# bind("ITEM_CODE") %>'>
                    </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ITEM DESCRIPTION">
                <ItemTemplate>
                    <asp:Label ID="lblitemdescgrid" runat="server" Text='<%# bind("ITEM_DESC") %>'>
                    </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ITEM UNIT">
                <ItemTemplate>
                    <asp:Label ID="lblitemunitgrid" runat="server" Text='<%# bind("UNIT") %>'>
                    </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="QUANTITY RECEIVED">
                <ItemTemplate>
                    <asp:Label ID="lblqtyrecgrid" runat="server" Text='<%# bind("QTY_REC") %>'>
                    </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" Wrap="False" Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="QUANTITY ALLOCATED">
                <ItemTemplate>
                    <asp:Label ID="lblqtyallocgrid" runat="server" Text='<%# bind("QTY_ALLOC") %>'>
                    </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" Wrap="False" Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="QUANTITY ISSUED">
                <ItemTemplate>
                    <asp:TextBox ID="txtquantissgrid" runat="server" MaxLength="20" CssClass="myInput"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" Wrap="False" Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:RegularExpressionValidator ID="re1" ControlToValidate="txtquantissgrid" runat="server"
                        ValidationGroup="Save" ValidationExpression="\d+" Display="Static" EnableClientScript="true"
                        ErrorMessage="Please enter numbers only"></asp:RegularExpressionValidator>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" ForeColor="Red" Wrap="False" Width="150px" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="emptyMessage" runat="server" Text="No Records Found" Font-Bold="true"
                ForeColor="red" BorderStyle="none" />
        </EmptyDataTemplate>
        <RowStyle CssClass="myGridTextItem" />
        <EditRowStyle CssClass="myGridEditTextItem" />
        <SelectedRowStyle CssClass="myGridSelectedItemStyle" />
        <HeaderStyle CssClass="myGridHeader" />
        <AlternatingRowStyle CssClass="myGridAlternatingItemStyle" />
        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
            NextPageText="Next" PreviousPageText="Previous" PageButtonCount="5" />
        <PagerStyle CssClass="myGridPager" HorizontalAlign="Center" ForeColor="White" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <center>
        <asp:Button ID="save" Text="SAVE" runat="server" OnClick="save_Click" ValidationGroup="Save" />
        <asp:Button ID="btnReset" Text="Reset" runat="server" OnClick="btnReset_Click" />
    </center>
    </div>
</asp:Content>
