<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ITEMREQ.aspx.cs" Inherits="ITEMREQ"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="height: 236px; font: caption;">
        <h1>
            <asp:Label ID="Label2" runat="server" Text="ITEM REQUIREMENT" CssClass="myPageHeader"></asp:Label></h1>
        <br />
        <h2>
            <asp:Label ID="Label" runat="server" Text="Contractor: " CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:DropDownList ID="ddlCont" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCont_SelectedIndexChanged"
                CssClass="myInput">
            </asp:DropDownList>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Contractor number:" CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:Label ID="Label4" runat="server" Text="" CssClass="myLabeldisp"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label5" runat="server" Text="Contractor name:" CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:Label ID="Label6" runat="server" Text="" CssClass="myLabeldisp"> </asp:Label>
            <br />
            <asp:Label ID="Label7" runat="server" Text="Select Items required:" CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Select" />
            <br />
             <asp:Label ID="rmrks" runat="server" Text="Enter remarks(if any):" CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:TextBox ID="remarks" runat="server" Width="509px" MaxLength="200" CssClass="myInput"></asp:TextBox>
            <br />
        </h2>
         <p>
                <asp:Label ID="lblError" runat="server" ViewStateMode="Disabled" CssClass="myError"></asp:Label>
            </p>
    </div>
    <asp:Button runat="server" ID="Button21" Style="display: none" />
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button21"
        PopupControlID="div1" BackgroundCssClass="popUpStyle" PopupDragHandleControlID="panel6"
        DropShadow="true" CancelControlID="btncancel" />
    <div id="div1" style="background-color: White; border-width: thin; border-style: solid;
        border-color: Black;">
        <table id="Table2" runat="server" border="1">
            <tr>
                <td>
                    <asp:Panel runat="Server" ID="panel6" Width="100%">
                        <table id="Table3" runat="server" width="100%" border="1">
                            <tr>
                                <td align="center" class="myPageHeader">
                                    <h2>
                                        <asp:Label ID="Label16" CssClass="myPageHeader" Text="Add new items" runat="Server" /></h2>
                                </td>
                                <td align="right" style="width: 20px">
                                    <asp:ImageButton runat="server" ID="btncancel" ImageUrl="close.png" ForeColor="White"
                                        Height="20px" ImageAlign="AbsMiddle" Width="20px" ToolTip="Close" OnClick="btncancel_Click" />
                                    <asp:Label ID="lblitemselection" runat="server" Visible="false" CssClass="myLabel"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblfvjobcode" runat="server" Visible="false" CssClass="myLabel"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="overflow: auto; height: 300px">
                        <asp:GridView ID="gvaudit" runat="server" AutoGenerateColumns="False" GridLines="None"
                            CssClass="grid-view" CellPadding="4" ForeColor="#333333" OnSelectedIndexChanged="gvaudit_SelectedIndexChanged">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="SELECT"  >
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkcontcheck" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ITEM CODE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontcode" runat="server" Text='<%# bind("ITEM_CODE") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ITEM DESCRIPTION">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemdesc" runat="server" Text='<%# bind("ITEM_DESC") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Wrap="False" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ITEM UNIT">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemunit1" runat="server" Text='<%# bind("UNIT") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px" />
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
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnSave" runat="server" Text="ADD" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None"
        AllowPaging="True" CssClass="grid-view" PageSize="15" OnPageIndexChanging="onpageindexchanging1"
        OnRowDeleting="gvJobActDtl_OnRowDeleting" CellPadding="4" ForeColor="#333333"
        OnSelectedIndexChanged="gvaudit_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="ITEM CODE">
                <ItemTemplate>
                    <asp:Label ID="lblcontcode1" runat="server" Text='<%# bind("ITEM_CODE") %>'>
                    </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ITEM DESCRIPTION">
                <ItemTemplate>
                    <asp:Label ID="lblitemdesc1" runat="server" Text='<%# bind("ITEM_DESC") %>'>
                    </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" Wrap="False" Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ITEM UNIT">
                <ItemTemplate>
                    <asp:Label ID="lblitemunit11" runat="server" Text='<%# bind("UNIT") %>'>
                    </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="QUANTITY REQUIRED">
                <ItemTemplate>
                    <asp:TextBox ID="quantreq" runat="server" MaxLength="20" CssClass="myInput"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" Wrap="False" Width="150px" />
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:RegularExpressionValidator ID="re1" ControlToValidate="quantreq" runat="server"
                        ValidationGroup="abc" ValidationExpression="\d+" Display="Static" EnableClientScript="true"
                        ErrorMessage="Please enter numbers only in quantity required"></asp:RegularExpressionValidator>
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
