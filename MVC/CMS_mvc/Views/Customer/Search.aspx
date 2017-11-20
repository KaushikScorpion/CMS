<%@ Page  Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout2.Master" Inherits="System.Web.Mvc.ViewPage<CMS_mvc.Models.retrieve_message>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script>
history.pushState(null, null, document.URL);
window.addEventListener('popstate', function () {
    history.pushState(null, null, document.URL);
});
    </script>
<div style="padding-top:10px;">

    <% using (Html.BeginForm("Search","Customer",FormMethod.Post,new { id = "SearchForm", onsubmit="onsub()" })) { %>
    <%: Html.AntiForgeryToken() %>
    <%: Html.ValidationSummary(true) %>
    <div class="row" >

      <%: Html.HiddenFor(model => model.Start_index) %>
        <%: Html.HiddenFor(model => model.End_index) %>
       <%:Html.Hidden("Searchvaluecopy","") %>

<div class="col-xs-4" style="padding-top:5px; float:right">
                        <a  href="/Customer/Create" type = "button" class = "btn btn-primary" style="float:right;">
                          +Add Customer
                       </a>
            </div>          
<div class="col-sm-3" style="padding-top:5px; float:left">
                    <div class="input-group" >
                        <%: Html.TextBoxFor(model => model.Searchvalue, new {@required="true", @class = "form-control", @onKeyPress="return disableEnterKey(event)"} ) %>
                         <span class="input-group-btn">
                                    <button class="btn btn-danger" type="button" onclick="SubmitSearch()">
                                        <span class=" glyphicon glyphicon-search"></span>
                                    </button>
                          </span>
                         

                    </div>
            </div>
        <div class="col-xs-2" style="padding-top:10px;">
        <span class="badge" id="SearchTextHolder" style="visibility:hidden" onclick="ResetSearch();"> <a><i class="remove glyphicon glyphicon-remove-sign glyphicon-white"></i></a> <span id="SearchText"></span></span>
        </div>
    <%--                 <%: Html.ValidationMessageFor(model => model.Searchvalue) %>--%>
                   
                   
              

    </div>
    
<% } %>
     </div>

    </asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="MainContent2" runat="server">



  <%-- <h2><asp:Label ID="Label1" runat="server" Text="Search Results"></asp:Label>        </h2>--%>
      
    <div id="MainSearchResults">
        

    </div>                   






<div  id="Pager" style="visibility:hidden;">

 <div class=" pull-left"> 
      
       <% int maxpage = 0; maxpage = ViewBag.Count; int page = 0; int maxp = (maxpage) / 10; if (maxpage % 10 != 0) maxp++;%>
       Page: <span id="TotalPages"></span>  || Rows: <span id="TotalRecords"></span>
    
  
  <form class="form-group">
     <input   min="1"  value="1" type="number" id="JumpToPage" />
     <input type="button" id="Go" class="btn btn-sm btn-success" value="Go" onclick="JumpToSpecifiedPage()"/>
 </form>
    </div>
    <ul class="pager pull-right" >
                            <li><input class="btn btn-sm btn-success" id="Prev" type="button" onclick="DecrementIndeces()" value="Prev"/></li>
        
                            <li><input class="btn btn-sm btn-success" id="Next" type="button" onclick="IncrementIndeces()" value="Next"></li>
    </ul>
  

</div>




  
  


    </asp:Content>


