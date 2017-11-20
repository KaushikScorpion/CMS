<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout4.Master" Inherits="System.Web.Mvc.ViewPage<CMS_mvc.Models.UserDataModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Login
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Login</h2>


<% using (Html.BeginForm(null, null, FormMethod.Post, new { id = "LoginForm", name="LoginForm" }))
   { %>
   
    <%: Html.ValidationSummary(true) %>

   
       
       <div class="form-login ">
       
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Emailid,new { @class = "form-control input-sm chat-input", @placeholder="Email"}) %>
            <%: Html.ValidationMessageFor(model => model.Emailid) %>
        </div>

       
        <div class="editor-field">
            <%: Html.PasswordFor(model => model.Password,new { @class = "form-control input-sm chat-input", @placeholder="Password" ,@onkeypress="LoginUserOnEvent(event)"}) %>
            <%: Html.ValidationMessageFor(model => model.Password) %>
        </div>
        </div>
        
 
        <div class="row" style="padding:10px;"></div>
        <div class="editor-field" >
       
           <button type="button"  class="btn btn-primary" onclick="LoginUser()">
             <span class="glyphicon glyphicon-log-in" ></span> Login
          </button>
      
         </div>
  
<% } %>
        







</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
