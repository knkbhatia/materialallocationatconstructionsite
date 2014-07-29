<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Allocation_Report.aspx.cs"
    Inherits="Allocation_Report" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <h1>
            <asp:Label ID="lblallohdr" runat="server" Text="Allocation Report" CssClass="myPageHeader"></asp:Label></h1>
        <p>
            
            &nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblallonosearch" runat="server" Text="Allocation no: " CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtallonosearch" runat="server" MaxLength="20" CssClass="myInput"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblcontreqnosearch" runat="server" Text="Contractor request number: "
                CssClass="myLabel"></asp:Label>
            &nbsp;
            <asp:TextBox ID="txtcontreqnosearch" runat="server" MaxLength="20" CssClass="myInput"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Search" runat="server" OnClick="Button1_Click" Text="Search" />
            <asp:Button ID="btnPrint" runat="server" Text="Print" 
                onclick="btnPrint_Click" />
        </p>
    </div>
    <%--<asp:UpdatePanel ID="upItem" runat="server">
        <ContentTemplate>--%>
            <p>
                <asp:Label ID="lblError" runat="server" ViewStateMode="Disabled" CssClass="myError"></asp:Label>
            </p>
            <asp:GridView ID="gvaudit" runat="server" AutoGenerateColumns="False" GridLines="None"
                AllowPaging="True" CssClass="grid-view" PageSize="15" OnPageIndexChanging="onpageindexchanging"
                CellPadding="4" ForeColor="#333333" OnRowCommand="gvJobActDtl_OnRowCommand">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowSelectButton="true" />
                    <asp:TemplateField HeaderText="ALLOCATION NUMBER">
                        <ItemTemplate>
                            <asp:Label ID="lblallono" runat="server" Text='<%# bind("ALLO_NO") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ALLOCATION DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblallodate" runat="server" Text='<%# bind("ALLO_DATE") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CONTRACTOR REQUEST NO">
                        <ItemTemplate>
                            <asp:Label ID="lblcontreqno" runat="server" Text='<%# bind("CONT_REQ_NO") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="False" Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CONTRACTOR CODE">
                        <ItemTemplate>
                            <asp:Label ID="lblcontcode" runat="server" Text='<%# bind("CONT_CODE") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="REMARKS">
                        <ItemTemplate>
                            <asp:Label ID="lblremarks" runat="server" Text='<%# bind("REMARKS") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="False" Width="150px" />
                    </asp:TemplateField>
                    <asp:ButtonField Text="Items Allocated" CommandName="EditJobAct">
                        <ItemStyle ForeColor="Blue" HorizontalAlign="Center" Width="100px" />
                    </asp:ButtonField>
                    <asp:ButtonField Text="Print" CommandName="Print">
                        <ItemStyle ForeColor="Blue" HorizontalAlign="Center" Width="100px" />
                    </asp:ButtonField>
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
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:HiddenField ID="ALLONO" runat="server" />
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
                                        <asp:Label ID="Label16" CssClass="myPageHeader" Text="Items allocated" runat="Server" /></h2>
                                </td>
                                <td align="right" style="width: 20px">
                                    <asp:ImageButton runat="server" ID="btncancel" ImageUrl="close.png" ForeColor="White"
                                        Height="20px" ImageAlign="AbsMiddle" Width="20px" ToolTip="Close" OnClick="btncancel_Click" />
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
                    <div style="overflow: auto;">
                        <asp:UpdatePanel ID="upitemDtl" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                    CssClass="grid-view" CellPadding="4" ForeColor="#333333">
                                    <Columns>
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
                                            <ItemStyle HorizontalAlign="Left" Wrap="False" Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QUANTITY REQUESTED">
                                            <ItemTemplate>
                                                <asp:Label ID="lblqtyreq" runat="server" Text='<%# bind("QTY_REQ") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Wrap="False" Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QUANTITY ALLOCATED">
                                            <ItemTemplate>
                                                <asp:Label ID="lblqtyreq" runat="server" Text='<%# bind("QTY_ALLOC") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Wrap="False" Width="150px" />
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
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
