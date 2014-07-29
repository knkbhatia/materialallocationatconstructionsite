<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dir_Item.aspx.cs" Inherits="Dir_Item"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <h1>
            <asp:Label ID="lblitemhdr" runat="server" Text="Item Directory" CssClass="myPageHeader"></asp:Label></h1>
        <p>
            <asp:Label ID="lblitemcodesearch" runat="server" Text="Item code: " CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtitemcodesearch" runat="server" MaxLength="20" CssClass="myInput"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblitemdescsearch" runat="server" Text="Item description: " CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtitemdescsearch" runat="server" MaxLength="20" CssClass="myInput"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" />
        </p>
    </div>
    <asp:UpdatePanel ID="upItem" runat="server">
        <ContentTemplate>
            <p>
                <asp:Label ID="lblError" runat="server" ViewStateMode="Disabled" CssClass="myError"></asp:Label>
            </p>
            <asp:GridView ID="gvaudit" runat="server" AutoGenerateColumns="False" GridLines="None"
                AllowPaging="True" CssClass="grid-view" PageSize="15" OnPageIndexChanging="onpageindexchanging"
                CellPadding="4" ForeColor="#333333" OnRowEditing="gvCountDetails_OnRowEditing"
                OnRowDeleting="gvJobActDtl_OnRowDeleting" OnRowUpdating="gvCountDetails_OnRowUpdating"
                OnRowCancelingEdit="gvCountDetails_OnRowCancelingEdit">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowEditButton="True" ItemStyle-Width="150px" />
                    <asp:TemplateField HeaderText="ITEM CODE">
                        <ItemTemplate>
                            <asp:Label ID="lblitemcode" runat="server" Text='<%# bind("ITEM_CODE") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ITEM DESCRIPTION">
                        <ItemTemplate>
                            <asp:Label ID="lblitemdesc" runat="server" Text='<%# bind("ITEM_DESC") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtitemdesc" runat="server" Text='<%# bind("ITEM_DESC") %>' CssClass="myInputEdit">
                            </asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UNIT">
                        <ItemTemplate>
                            <asp:Label ID="lblitemunit" runat="server" Text='<%# bind("UNIT") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtitemunit" runat="server" Text='<%# bind("UNIT") %>'>
                            </asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RATE">
                        <ItemTemplate>
                            <asp:Label ID="lblitemrate" runat="server" Text='<%# bind("RATE") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtitemrate" runat="server" Text='<%# bind("RATE") %>'>
                            </asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Wrap="False" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QUANTITY RECEIVED">
                        <ItemTemplate>
                            <asp:Label ID="lblitemrec" runat="server" Text='<%# bind("QTY_REC") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Wrap="False" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QUANTITY REQUESTED">
                        <ItemTemplate>
                            <asp:Label ID="lblitemreq" runat="server" Text='<%# bind("QTY_REQ") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Wrap="False" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QUANTITY ALLOCATED">
                        <ItemTemplate>
                            <asp:Label ID="lblitemalloc" runat="server" Text='<%# bind("QTY_ALLOC") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Wrap="False" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QUANTITY ISSUED">
                        <ItemTemplate>
                            <asp:Label ID="lblitemiss" runat="server" Text='<%# bind("QTY_ISS") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Wrap="False" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="STOCK">
                        <ItemTemplate>
                            <asp:Label ID="lblitemstock" runat="server" Text='<%# bind("STOCK") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Wrap="False" Width="100px" />
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
                                <asp:Label ID="lblSItemCode" runat="server" Text="Enter Item Code :" CssClass="myLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSItemCode" runat="server" MaxLength="20" CssClass="myInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblitemdesc" runat="server" Text="Enter Item Description :" CssClass="myLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtitemdes" runat="server" MaxLength="20" CssClass="myInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblitemunit" runat="server" Text="Enter Item Unit :" CssClass="myLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtitemunit" runat="server" MaxLength="20" CssClass="myInput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblitemrate" runat="server" Text="Enter Item Rate :" CssClass="myLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtitemrate" runat="server" MaxLength="20" CssClass="myInput"></asp:TextBox>
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
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="false" CssClass="myLabel"></asp:Label>
    
    <center>
    <asp:Button ID="Button2" runat="server" Text="Add new item" OnClick="Button2_Click" />
    </center>
</asp:Content>
