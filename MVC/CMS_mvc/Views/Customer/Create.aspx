<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout2.Master" Inherits="System.Web.Mvc.ViewPage<CMS_mvc.Models.CustomerDataModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add Customer
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="padding-top:10px;" class="col-lg-5">
<% using (Html.BeginForm(null, null, FormMethod.Post, new { id = "CreateForm" }))
   { %>
    <%: Html.AntiForgeryToken() %>
    <%: Html.ValidationSummary(true) %>
     <ul class = "nav nav-tabs">
             <li class = "active"><a href = "#">Add Customer</a></li>
      </ul>
    <div  style="padding-top:20px;">
    <fieldset >
        <div class="editor-label required">
            <%: Html.Label("Name") %>
        </div>
        <div class="editor-field ">
            <%: Html.TextBoxFor(model => model.CustomerName,new { @class = "form-control" }) %>
            <%: Html.ValidationMessageFor(model => model.CustomerName) %>
        </div>

        <div class="editor-label required">
            <%: Html.LabelFor(model => model.Gender) %>
        </div>
        <div class="editor-field">
           <%-- <%: Html.TextBoxFor(model => model.Gender,new { @class = "form-control" }) %>--%>

             <%: 
     Html.DropDownListFor(
           model => model.Gender, 
           new SelectList(
                  new List<Object>{ 
                       new { value =  "male"  , text = "Male"  },
                       new { value = "female" , text = "Female" }
                       
                    },
                  "value",
                  "text"
                 
        ), new { @class = "form-control" }
)
                        %>

            <%: Html.ValidationMessageFor(model => model.Gender) %>
        </div>
        <div class="editor-label required">
            <%: Html.LabelFor(model => model.Age) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Age,new { @class = "form-control" }) %>
            <%: Html.ValidationMessageFor(model => model.Age) %>
        </div>

        <div class="editor-label required">
            <%: Html.LabelFor(model => model.Phone) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Phone,new { @class = "form-control" }) %>
            <%: Html.ValidationMessageFor(model => model.Phone) %>
        </div>

        <div class="editor-label required">
            <%: Html.LabelFor(model => model.Address) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Address,new { @class = "form-control" }) %>
            <%: Html.ValidationMessageFor(model => model.Address) %>
        </div>

        <div class="editor-label required">
            <%: Html.LabelFor(model => model.City) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.City,new { @class = "form-control" }) %>
            <%: Html.ValidationMessageFor(model => model.City) %>
        </div>

        <div class="editor-label required">
            <%: Html.LabelFor(model => model.Emailid) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Emailid,new { @class = "form-control" }) %>
            <%: Html.ValidationMessageFor(model => model.Emailid) %>
           
        </div>

        <%--<div class="editor-label required">
            <%: Html.LabelFor(model => model.Adminid) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.Adminid,new { @class = "form-control" }) %>
            <%: Html.ValidationMessageFor(model => model.Adminid) %>
        </div>--%>
        <br />
        <p>
        <button class="btn btn-success" type="button"  onclick="AddCustomer()"><span class="glyphicon glyphicon-ok"></span>&nbsp;Add</button>
        
        <a href="../../" type = "button" class = "btn btn-primary pull-right" >Home </a>
        <input  class="btn btn-danger" type="button" value="Back" onclick="window.history.back()" />
        </p>
        <p>
            <%if(ViewBag.message!="empty"&&ViewBag.message!=null){ %>
            <%:ViewBag.message %>
            <% } %>

        </p>
        
    </fieldset>
    </div>


   
<% } %>


</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent2" runat="server">
</asp:Content>
