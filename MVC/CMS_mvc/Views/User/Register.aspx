<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout3.Master" Inherits="System.Web.Mvc.ViewPage<CMS_mvc.Models.UserDataModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Register
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
<h3>Register</h3>

<% using (Html.BeginForm(null, null, FormMethod.Post, new { id = "RegisterForm" }))
   { %>
    <%: Html.AntiForgeryToken() %>
    <%: Html.ValidationSummary(true) %>

    <fieldset>
        <div class="editor-label">
            <%: Html.Label("Name") %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.UserName,new { @class = "col-xs-3 form-control" }) %>
            <%: Html.ValidationMessageFor(model => model.UserName) %>
        </div>
         <div class="editor-label required">
            <%: Html.LabelFor(model => model.Phone) %>
        </div>
        <div class="editor-field ">
            <%: Html.TextBoxFor(model => model.Phone,new { @class = "col-xs-3 form-control" }) %>
            <%: Html.ValidationMessageFor(model => model.Phone) %>
        </div>

        <div class="editor-label required">
            <%: Html.LabelFor(model => model.Emailid) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Emailid,new { @class = "col-xs-3 form-control"}) %>
            <%: Html.ValidationMessageFor(model => model.Emailid) %>
        </div>

        <div class="editor-label required">
            <%: Html.LabelFor(model => model.Password) %>
        </div>
        <div class="editor-field">
            <%: Html.PasswordFor(model => model.Password,new { @class = "col-xs-3 form-control" }) %>
            <%: Html.ValidationMessageFor(model => model.Password) %>
        </div>
        
         <div class="editor-label required">
            <%: Html.LabelFor(model => model.ConfirmPassword) %>
        </div>
        <div class="editor-field">
            <%: Html.PasswordFor(model => model.ConfirmPassword,new { @class = "col-xs-3 form-control" ,@onkeypress="RegisterUserOnEvent(event)"}) %>
            <%: Html.ValidationMessageFor(model => model.ConfirmPassword) %>
        </div>
        
        <div class="row" style="padding:10px;"></div>
        <div class="editor-field" >
       
           <button type="button" onclick="RegisterUser()" class="btn btn-primary">
             <span class="glyphicon glyphicon-log-in"></span> Submit
          </button>
      
         </div>
    </fieldset>
<% } %>


</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>


