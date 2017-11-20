<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div id="SearchResults" >
<%if(ViewBag.isfirst==false){%>     
<fieldset id="tablefieldset">
<div class="row"></div>
   <%if(ViewBag.jsonlist.Count==0) {
         
         %>
        <div id="NoResults">
        <table id="NoResultsEmptytable" class="table table-bordered table-hover" >
             <tr bgcolor="#eaf2f7"><th>
              <%: Html.DisplayName("Name") %>
            </th>
               
            <th>
              <%: Html.DisplayName("Gender") %>
           </th>
            
            <th>
              <%: Html.DisplayName("Age") %>
            </th>

           <th>
              <%: Html.DisplayName("Phone") %>         
           </th>

        <%--    <th>
              <%: Html.DisplayName("Address") %>
           </th>--%>
            
            <th>
              <%: Html.DisplayName("City") %>
           </th>

           <th>
              <%: Html.DisplayName("Emailid") %>
            </th>

           <%-- <th>
                 <%: Html.DisplayName("Adminid") %>
            </th>--%>
             
             <th>
                 <%: Html.DisplayName("Actions") %>
            </th>
          
                 </tr>
         <tr><td colspan="7">No (matching) Customer Records.</td></tr>

        </table>
        <script>NoMatchingRecords();</script>
        </div>
            <%}else{ %>
         <%int ListCount = 5; ListCount = ViewBag.count; %>
         <%: Html.Hidden("maxcount", ListCount) %>

        <div class="table-responsive" style="overflow-x:inherit;">
        <table id="mytable" class="tablesorter table table-bordered table-hover "><!--table table-bordered table-hover-->
            <thead> <tr><th>
              <%: Html.DisplayName("Name") %>
            </th>
               
            <th>
              <%: Html.DisplayName("Gender") %>
           </th>
            
            <th>
              <%: Html.DisplayName("Age") %>
            </th>

           <th>
              <%: Html.DisplayName("Phone") %>         
           </th>

        <%--    <th>
              <%: Html.DisplayName("Address") %>
           </th>--%>
            
            <th>
              <%: Html.DisplayName("City") %>
           </th>

           <th>
              <%: Html.DisplayName("Emailid") %>
            </th>

           <%-- <th>
                 <%: Html.DisplayName("Adminid") %>
            </th>--%>
             
             <th id="Actions">
                 <%: Html.DisplayName("Actions") %>
            </th>
          
                 </tr>
    </thead>
    <%int count = 0; %>
            <tbody>
    
    <% foreach(CMS_mvc.Models.CustomerDataModel item in ViewBag.jsonlist){ %>

       <%if (count == 10) break; %>
            
          <tr id=<%:"myrow"+count++%>>
            
          <td>        <%: item.CustomerName %> </>
          <td>        <%=item.Gender %>
          <td>        <%: item.Age.ToString() %>
          <td>        <%: item.Phone.ToString() %></td>
          <%--<td>        <%: item.Address %>--%>
          <td>        <%: item.City %>
          <td>        <%: item.Emailid %></td>
          <td>        <%using (Html.BeginForm("Update", "Customer"))
                       {%>
               <%: Html.Hidden("CustomerId", item.Id) %>
<div class="btn-group">
  <input class="btn-sm btn-info" type="submit" value="Update" />
    
              
              <%}%>
                
                   <div class="popup">
                      <%string popupid = "myPopup" + item.Id.ToString(); %>
                      <span class="popuptext" id='<%:popupid %>'>Delete Failed!<br> You are not Authorized</span>

                      <%using (Html.BeginForm())
                       {%>
               <%: Html.Hidden("idtodelete", item.Id) %>
  <%string rowname = "ConfirmDelete(" + "'myrow" + (count - 1) + "'" + "," + item.Id.ToString() + ")"; %>
<input class="btn-sm btn-danger"  id="myModalButton" data-toggle="modal" data-target="#myModal" type="button" value="Delete" onclick='<%:rowname%>'/>
                       </div>
   <%--<div class="dropdown">
                      <button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown">Actions
                         <span class="caret"></span></button>
                            <ul class="dropdown-menu" style="position:relative;">
                         <li style= "padding-left:10px"><button class=" btn-sm btn-primary" onclick="SubmitUpdate()">Update</button></li>
                         <li><input class="btn-sm btn-primary"  id="myModalButton" data-toggle="modal" data-target="#myModal" type="button" value="Delete" onclick='<%:rowname%>'/></li>
                        </ul>
   </div>--%>


  

                      
              <%}%>

                      </div>
              </td>
            
              
          </tr>
                  <%} %>
           
            
</tbody>
   </table>
            <input type="hidden" id="ChosenIdToDelete" /> <input type="hidden" id="ChosenRowToDelete" />
  </div>
</fieldset>

    </div>
<script>
        OnSearchResultsLoad();
</script>
<%} %>

     
<%} %>
