<%@ Page Language="C#" AutoEventWireup="true" CodeFile="allocation.aspx.cs" Inherits="allocation"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="height: 236px; font: caption;">
        <h1>
            <asp:Label ID="lblalloc" runat="server" Text="ALLOCATION" CssClass="myPageHeader"></asp:Label></h1>
        <br />
        <h2>
            <asp:Label ID="lblconcode" runat="server" Text="Contractor code: " CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:DropDownList ID="ddlCont" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCont_SelectedIndexChanged"
                CssClass="myInput">
            </asp:DropDownList>
            <br />
            <asp:Label ID="lblcontcode" runat="server" Text="Contractor code:" CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:Label ID="lbldisplaycontcode" runat="server" Text="" CssClass="myLabeldisp"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblcontname" runat="server" Text="Contractor name:" CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:Label ID="lbldisplaycontname" runat="server" Text="" CssClass="myLabeldisp"> </asp:Label>
            <br />
            <asp:Label ID="lblselreqno" runat="server" Text="Select request number: " CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:DropDownList ID="ddlcontreqno" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlContreqno_SelectedIndexChanged"
                CssClass="myInput">
            </asp:DropDownList>
            <br />
            <asp:Label ID="lblcontreqno" runat="server" Text="Contractor request number:" CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:Label ID="lbldisplaycontreqno" runat="server" Text="" CssClass="myLabel"></asp:Label>
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
            <asp:TemplateField HeaderText="QUANTITY REQUESTED">
                <ItemTemplate>
                    <asp:Label ID="lblqtyreqgrid" runat="server" Text='<%# bind("QTY_REQ") %>'>
                    </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" Wrap="False" Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="QUANTITY ALLOCATED">
                <ItemTemplate>
                    <asp:TextBox ID="txtquantallocgrid" runat="server" MaxLength="20" CssClass="myInput"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" Wrap="False" Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:RegularExpressionValidator ID="re1" ControlToValidate="txtquantallocgrid" runat="server"
                        ValidationGroup="abc" ValidationExpression="\d+" Display="Static" EnableClientScript="true"
                        ErrorMessage="Please enter numbers only in quantity allocated"></asp:RegularExpressionValidator>
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
        <asp:Button ID="save" Text="SAVE" runat="server" OnClick="save_Click" ValidationGroup="abc" />
        <asp:Button ID="btnReset" Text="Reset" runat="server" OnClick="btnReset_Click" />
    </center>
   
</asp:Content>
