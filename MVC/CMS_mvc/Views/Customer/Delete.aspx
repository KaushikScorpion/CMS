<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout2.Master" Inherits="System.Web.Mvc.ViewPage<CMS_mvc.Models.Result>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Delete</h2>

<fieldset>
    <legend>Result</legend>
</fieldset>
 Status: <%:ViewBag.message %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
