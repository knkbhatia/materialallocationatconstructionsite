<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dir_Contractor.aspx.cs" Inherits="Dir_Contractor"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <h1>
            <asp:Label ID="Label12" runat="server" Text="Contractor directory" CssClass="myPageHeader"></asp:Label></h1>
        <p>
            <asp:Label ID="Label13" runat="server" Text="Contractor code: " CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtcontcodesearch" runat="server" MaxLength="20" CssClass="myInput"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label14" runat="server" Text="Contractor name: " CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtcontnamesearch" runat="server" MaxLength="20" CssClass="myInput"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
        </p>
    </div>
    <asp:UpdatePanel ID="upItem" runat="server">
        <ContentTemplate>
            <p>
                <asp:Label ID="lblError" runat="server" ViewStateMode="Disabled" CssClass="myError"></asp:Label>
            </p>
            <asp:GridView ID="contgrid" runat="server" AutoGenerateColumns="False" GridLines="None"
                AllowPaging="True" CssClass="grid-view" PageSize="15" OnPageIndexChanging="onpageindexchanging"
                CellPadding="4" ForeColor="#333333" OnRowEditing="gvCountDetails_OnRowEditing"
                AllowSorting="true" OnRowUpdating="gvCountDetails_OnRowUpdating" OnRowDeleting="gvJobActDtl_OnRowDeleting"
                OnRowCancelingEdit="gvCountDetails_OnRowCancelingEdit">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:TemplateField HeaderText="CONTRACTOR CODE">
                        <ItemTemplate>
                            <asp:Label ID="lblcontcode" runat="server" Text='<%# bind("CONT_CODE") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CONTRACTOR NAME">
                        <ItemTemplate>
                            <asp:Label ID="lblitemdesc" runat="server" Text='<%# bind("CONT_NAME") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtcontdesc" runat="server" Text='<%# bind("CONT_NAME") %>'>
                            </asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <span onclick="return confirm('Are you sure to delete the record')" style="text-align: center;">
                                <asp:LinkButton ID="lnkbDelete" runat="Server" Text="Delete" CommandName="Delete"
                                    ForeColor="blue"></asp:LinkButton>
                            </span>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" Width="50px" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <center>
    <br />
        <asp:Button ID="Button2" runat="server" Text="Add new contractor" OnClick="Button2_Click" />
    </center>
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
                                    <asp:Label ID="Label16" CssClass="myPageHeader" Text="View / Edit" runat="Server" />
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
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblSItemCode" runat="server" Text="Enter Contractor Code :" CssClass="myLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSItemCode" runat="server" MaxLength="20" CssClass="myInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Enter Contractor Name :" CssClass="myLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox3" runat="server" MaxLength="20" CssClass="myInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
</asp:Content>
