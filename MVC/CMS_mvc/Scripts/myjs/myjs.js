//function SetActiveTab(TabName) {
   
//    document.getElementById(TabName).className = "active";
//}


function UpdateCustomer() {
    var form = new FormData(document.getElementById("UpdateForm"));
    if ($("#UpdateForm").valid() == false) {
        return false;
    }

   
    CustomerData = {
        'CustomerName': form.get("CustomerName"),
        'Emailid': form.get("Emailid"),
        'Age': form.get("Age"),
        'Gender': form.get("Gender"),
        'Adminid': form.get("Adminid"),
        'Phone': form.get("Phone"),
        'id': form.get("Id"),
        'City': form.get("City"),
        'Address': form.get("Address")
    };

    $.ajax({
        url: "../../Customer/DoUpdate",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(CustomerData),
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
           
                        updatePopupFunction(data);
           

            
        },
        error: function (xhr) {
            alert('error');
        }
    });
}

function updatePopupFunction(data) {
   
    var x = document.getElementById("myModal2text");
    var displaytext = data.Status;
    if (data.Status == "Failure")
    {
        if(data.ExceptionDetails!=null)
            displaytext = displaytext + ".<br>" + data.ExceptionDetails + "";
        else
            displaytext = displaytext + ".<br>" + data.Message + "";
    }
    else
    { displaytext = displaytext + ".<br> Changes Saved! "; }
    x.innerHTML = displaytext;
    $('#myModal2').modal();
}


//delete scetion of js
function ConfirmDelete(customerrow,idtodelete) {
    $(document.getElementById("myModal")).focus();
    //$("myModalSubmit").attr('onclick', 'DeleteCustomer('+customerrow+', '+idtodelete+'\'');
    //$(document.getElementById("myModalSubmit")).on("click", function () { DeleteCustomer(customerrow, idtodelete); });

    //DeleteCustomer(customerrow,idtodelete);
    document.getElementById("ChosenIdToDelete").value = idtodelete;
    document.getElementById("ChosenRowToDelete").value = customerrow;
    
}

function DeleteOnOk() {
    idtodelete=document.getElementById("ChosenIdToDelete").value;
    customerrow = document.getElementById("ChosenRowToDelete").value;
    DeleteCustomer(customerrow, idtodelete);
}



function DeleteCustomer(customerrow,idtodelete) {

    $(document.getElementById("myModalSubmit")).unbind("click");
    var idval = parseInt(idtodelete);
    var jsonobj ={ id : idval };
    $.ajax({

        url: "../../Customer/Delete",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(jsonobj),
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            if (data.Status == "Failure") {
                myPopupFunction(data, idtodelete);

            }
            else {
                $(document.getElementById(customerrow)).hide(1000);
                var maxpage = (parseInt(document.getElementById("maxcount").value));
                var endindex = (parseInt(document.getElementById("End_index").value));
                if (endindex > maxpage)
                    endindex = maxpage;
                endindex = endindex - 1;
                maxpage = maxpage - 1;
                document.getElementById("TotalRecords").innerHTML = endindex + "/" + maxpage;

            }
        },
        error: function (xhr) {
            alert('error');
        }
    });


}

//result popup on delete
function myPopupFunction(data,idtodelete) {
    // var popupid="myPopup"+idtodelete;
    // var popup = document.getElementById(popupid);
    //popup.innerHTML = data;
   // $(document.getElementById("myModal2text")).= "fAILURE sORRY";

  //  $("myModal2text").append$("Very Bad");

    //$(document.getElementById("myModal2")).click(function () { });
    //$(document.getElementById("myModal2text")).html=
    var x = document.getElementById("myModal2text");
    var displaytext = data.Status + "!<br>" + data.ExceptionDetails+"";
    x.innerHTML =displaytext;
    $('#myModal2').modal();
}




//Register form
function RegisterUserOnEvent(e) {
    if (disableEnterKeyOnly(e) == true) {
        return false;
    }
    RegisterUser();
}
function RegisterUser() {
    
    

    if ($("#RegisterForm").valid() == false) {
        return false;
    }
    
    var form = new FormData(document.getElementById("RegisterForm"));
    
    
    CustomerData = {
        'UserName': form.get("UserName"),
        'Emailid': form.get("Emailid"),
        'City': "Unknown",
        'Phone': form.get("Phone"),
        'Password': form.get("Password"),
        'Address': "No address"
    };

    $.ajax({
        url: "../../User/DoRegister",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(CustomerData),
        async: true,
        processData: false,
        cache: false,
        success: function (data) {

            RegisterPopupFunction(data);

        },
        error: function (xhr) {
            alert('error');
        }
    });
}


function RegisterPopupFunction(data) {

    var x = document.getElementById("myModal2text");
    var displaytext = data.Status;
    if (data.Status == "Failure") {
        document.getElementById("myModal2ButtonLogin").style.visibility = "hidden";
        if (data.ExceptionDetails != null)
            displaytext = displaytext + ".<br>" + data.ExceptionDetails + "";
        else
            displaytext = displaytext + ".<br>" + data.Message + "";
    }
    else {
        displaytext = displaytext + ".<br><h5> Registred Successfully! </h5>";
        document.getElementById("myModal2ButtonLogin").style.visibility = "visible";
    }

    x.innerHTML = displaytext;
    $('#myModal2').modal();
}

function Logout() {
    document.cookie = "Adminid=0";
    window.location = "../../User/Login";
}


function disableEnterKey(e) {
    var key;
    if (window.event)
        key = window.event.keyCode; //IE
    else
        key = e.which; //firefox 
    if(key==13)
    SubmitSearch();
    return (key != 13);
}

function disableEnterKeyOnly(e) {
    var key;
    if (window.event)
        key = window.event.keyCode; //IE
    else
        key = e.which; //firefox 
    return (key != 13);
}


function AddCustomer() {
    var form = new FormData(document.getElementById("CreateForm"));
    if ($("#CreateForm").valid() == false) {
        return false;
    }


    CustomerData = {
        'CustomerName': form.get("CustomerName"),
        'Emailid': form.get("Emailid"),
        'Age': form.get("Age"),
        'Gender': form.get("Gender"),
        
        'Phone': form.get("Phone"),
        'City': form.get("City"),
        'Address': form.get("Address")
    };

    $.ajax({
        url: "../../Customer/Create",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(CustomerData),
        async: true,
        processData: false,
        cache: false,
        success: function (data) {

            createPopupFunction(data);



        },
        error: function (xhr) {
            alert('error');
        }
    });
}


function createPopupFunction(data) {

    var x = document.getElementById("myModal2text");
    var displaytext = data.Status;
    if (data.Status == "Failure") {
        if (data.ExceptionDetails != null)
            displaytext = displaytext + ".<br>" + data.ExceptionDetails + "";
        else
            displaytext = displaytext + ".<br>" + data.Message + "";
    }
    else { displaytext = displaytext + ".<br> Customer Added! "; }
    x.innerHTML = displaytext;
    $('#myModal2').modal();
}