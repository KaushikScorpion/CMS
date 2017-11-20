//login user
function LoginUser2(e) {

    e.preventDefault();
    var form = new FormData(document.getElementById("LoginForm"));

    UserData = {

        'Emailid': document.getElementById("Emailid").value,

        'Password': document.getElementById("Password").value

    };


    $.post("/User/Authenticate", UserData, function (json) {
        document.alert("Hi");
    }, "application/json").error(function (json) {
        document.alert("Hi");
    });
}

function LoginUserOnEvent(e) {
    if (disableEnterKeyOnly(e) == true) {
        return false;
    }
    else
    LoginUser();

}

function LoginUser() {
    $(document.getElementById("LoginButton")).off('click');
    if ($("#LoginForm").valid() == false) {
        return false;
    }

   
    //if ($("#LoginForm").valid() == false) {
    //    return false;
    //}

    var form = new FormData(document.getElementById("LoginForm"));

    UserData = {

        'Emailid': document.getElementById("Emailid").value,

        'Password': document.getElementById("Password").value

    };

    $.ajax({

        url: "../../User/Authenticate/",
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(UserData),
        async: true,
        crossDomain: true,
        processData: false,
        cache: false,
        success: function (data) {

            //LoginPopupFunction(data);
               
                LoginPopupFunction(data);
            


        },
        error: function (data) {

            //LoginPopupFunction(data);
            alert(data.Status);


        }
       
    });

}

function LoginPopupFunction(data) {

    var x = document.getElementById("myModal2text");
    var displaytext = data.Status;
    if (data.Status == "Failure") {
        document.getElementById("myModal2ButtonLogin").style.visibility = "hidden";
        displaytext = displaytext + ".<br>" + data.Message + "";
        x.innerHTML = displaytext;
        $('#myModal2').modal();
        // alert("Wrong Creds");
    }
    else {
        //displaytext = displaytext + ".<br><h5>" + data.Message + "</h5>";
        //x.innerHTML = displaytext;
        //$('#myModal2').modal();
        window.location = "../../";
       // alert("hi");
    }


}