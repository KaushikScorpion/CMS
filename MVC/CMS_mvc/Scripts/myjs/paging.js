
function IncrementIndeces() {
    if (document.getElementById("Start_index").value==0)
        document.getElementById("Start_index").value = parseInt(document.getElementById("Start_index").value) + 10 + 1;
    else
        document.getElementById("Start_index").value = parseInt(document.getElementById("Start_index").value) + 10;

    document.getElementById("End_index").value = parseInt(document.getElementById("End_index").value) + 10;

    $("#Searchvalue").removeAttr("required");
    // $("#SearchForm").submit();
    $('#MainSearchResults').load("/Customer/PartialSearchResults", $("#SearchForm").serialize());

}


function DecrementIndeces() {
    if (document.getElementById("Start_index").value == 11)
        document.getElementById("Start_index").value = parseInt(document.getElementById("Start_index").value) - 11;
    else
        document.getElementById("Start_index").value = parseInt(document.getElementById("Start_index").value) - 10;

    document.getElementById("End_index").value = parseInt(document.getElementById("End_index").value) - 10;
    $("#Searchvalue").removeAttr("required");
    //$("#SearchForm").submit();
    $('#MainSearchResults').load("/Customer/PartialSearchResults", $("#SearchForm").serialize());

}


function OnSearchResultsLoad() {

   

    //if (document.getElementById("Searchvalue").value != "")
    //    document.getElementById("id").value = parseInt(document.getElementById("id").value) + 1;
    $("#mytable").tablesorter();
    document.getElementById("Actions").className = "ff";

    document.getElementById("Searchvaluecopy").value = document.getElementById("Searchvalue").value;
    
    document.getElementById("SearchText").innerHTML = document.getElementById("Searchvalue").value;
    if (document.getElementById("Searchvalue").value!="")
        $("#SearchTextHolder").css('visibility','visible');

    if (document.getElementById("tablefieldset") != null & document.getElementById("NoResults") == null)
        $("#Pager").css("visibility", "visible");

    if (parseInt(document.getElementById("Start_index").value) == 0)
        $("#Prev").attr('disabled', 'disabled');
    else
        $("#Prev").removeAttr('disabled');

    if ((parseInt(document.getElementById("End_index").value)) >= (parseInt(document.getElementById("maxcount").value)))
        $("#Next").attr('disabled', 'disabled');
    else
        $("#Next").removeAttr('disabled');

    var endindex = (parseInt(document.getElementById("End_index").value));


    var maxpage = (parseInt(document.getElementById("maxcount").value));
    maxp = Math.floor((maxpage) / 10);
    if (maxpage % 10 != 0) maxp++;
    document.getElementById("TotalPages").innerHTML = Math.ceil((endindex / 10)) + "/" + maxp;
    document.getElementById("JumpToPage").max = maxp;
    document.getElementById("JumpToPage").value = Math.ceil((endindex / 10));

    if (endindex > maxpage)
        endindex = maxpage;
    document.getElementById("TotalRecords").innerHTML = endindex + "/" + maxpage;
}


function onsub() {


    if (document.getElementById("Searchvaluecopy").value != document.getElementById("Searchvalue").value) {
        document.getElementById("Start_index").value = 0;
        document.getElementById("End_index").value = 10;
        $("#SearchTextHolder").css('visibility', 'hidden');
    }
}

function ResetSearch() {
    $("#SearchTextHolder").css('visibility', 'hidden');
    document.getElementById("Searchvalue").value = "";
    document.getElementById("Start_index").value = 0;
    document.getElementById("End_index").value = 10;
    $('#MainSearchResults').load("/Customer/PartialSearchResults", $("#SearchForm").serialize());

}
function SubmitSearch() {
    onsub();
    $('#MainSearchResults').load("/Customer/PartialSearchResults", $("#SearchForm").serialize());
}

function LoadMainSearchResults() {
   
    $('#MainSearchResults').load("/Customer/PartialSearchResults", $("#SearchForm").serialize());
}

function NoMatchingRecords() {
    $("#Pager").css("visibility", "hidden");
    document.getElementById("Start_index").value = 0;
    document.getElementById("End_index").value = 10;
    document.getElementById("SearchText").innerHTML = document.getElementById("Searchvalue").value;
    $("#SearchTextHolder").css("visibility", "visible");
    if (document.getElementById("Searchvalue").value == "")
        $("#SearchTextHolder").css("visibility", "hidden");
}

$(document).ready(function () {
    LoadMainSearchResults();
});

//function JumpToSpecifiedPage() {
//    var jumpto = parseInt(document.getElementById("JumpToPage").value);
//    document.getElementById("Start_index").value = (1 + (jumpto) * 10) - 10;
//    document.getElementById("End_index").value = jumpto * 10;
//    SubmitSearch();

//}


function JumpToSpecifiedPage() {
    var jumpto = parseInt(document.getElementById("JumpToPage").value);
    var maxpage = (parseInt(document.getElementById("maxcount").value));
    //var endindex = (parseInt(document.getElementById("End_index").value));
    maxp = Math.floor((maxpage) / 10);
    if (maxpage % 10 != 0) maxp++;
    if ((jumpto > maxp || jumpto <= 0)) {

    }
    else {


        var jumpto = parseInt(document.getElementById("JumpToPage").value);
        document.getElementById("Start_index").value = ( 1+(jumpto) * 10) - 10;
        document.getElementById("End_index").value = jumpto * 10;
        SubmitSearch();

    }

}