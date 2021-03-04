var count = 0;
var url = "";

$(document).ready(function () {

    var basicSave = localStorage.getItem("basicSave");
    if (basicSave == null || basicSave == "null") {
        basicSave = 0;
    } else {
        basicSave = parseInt(basicSave);
    }

    var eduSave = localStorage.getItem("eduSave");
    if (eduSave == null || eduSave == "null") {
        eduSave = 0;
    } else {
        eduSave = parseInt(eduSave);
    }

    var uploadSave = localStorage.getItem("uploadSave");
    if (uploadSave == null || uploadSave == "null") {
        uploadSave = 0;
    } else {
        uploadSave = parseInt(uploadSave);
    }

    var signSave = localStorage.getItem("signSave");
    if (signSave == null || signSave == "null") {
        signSave = 0;
    } else {
        signSave = parseInt(signSave);
    }

    $("#basicSave").click(function () {
        basicSave++;
        localStorage.setItem("basicSave", basicSave);
    });
    $("#eduSave").click(function () {
        eduSave++;
        localStorage.setItem("eduSave", eduSave);
    });
    $("#uploadSave").click(function () {
        uploadSave++;
        localStorage.setItem("uploadSave", uploadSave);
    });
    $("#signSave").click(function () {
        signSave++;
        localStorage.setItem("signSave", signSave);
    });

    if (localStorage.getItem("basicSave") > 0) {
        console.log("basicsave")
        $("#bt1").removeClass("active");
        $("#bt1").addClass("after1");
        $("#bar1").addClass("after2");
    }
    if (localStorage.getItem("eduSave") > 0) {
        $("#bt1").removeClass("active");
        $("#bt1").addClass("after1");
        $("#bar1").addClass("after2");
        $("#bt2").removeClass("active");
        $("#bt2").addClass("after1");
        $("#bar2").addClass("after2");
        $("#bt2").addClass("active");
    }
    if (localStorage.getItem("uploadSave") > 0) {
        $("#bt1").removeClass("active");
        $("#bt1").addClass("after1");
        $("#bar1").addClass("after2");
        $("#bt2").removeClass("active");
        $("#bt2").addClass("after1");
        $("#bar2").addClass("after2");
        $("#bt2").addClass("active");
        $("#bt3").removeClass("active");
        $("#bt3").addClass("after1");
        $("#bar3").addClass("after2");
    }
    if (localStorage.getItem("signSave") > 0) {
        $("#bt1").removeClass("active");
        $("#bt1").addClass("after1");
        $("#bar1").addClass("after2");
        $("#bt2").removeClass("active");
        $("#bt2").addClass("after1");
        $("#bar2").addClass("after2");
        $("#bt2").addClass("active");
        $("#bt3").removeClass("active");
        $("#bt3").addClass("after1");
        $("#bar3").addClass("after2");
        $("#bt4").removeClass("active");
        $("#bt4").addClass("after1");
        $("#bar4").addClass("after2");
    }
});



var str = '<td><input type="checkbox" class="form-check-input checkbox" id="exampleCheck1" name="checkbox1"  style="margin-left:auto;margin-right:auto"/></td>';
$(document).ready(function () {


    $('#save').click(function () {
        if ($("#dependentTable tbody").length == 0) {
            $("#dependentTable").append("<tbody></tbody>");
        }


        $("#dependentTable tbody").append("<tr>" + str +
            "<td>" + $("#name2").val() + "</td>" +
            "<td>" + $("#relationship").val() + "</td>" +
            "<td>" + $("#gender").val() + "</td>" +
            "<td>" + $("#dob2").val() + "</td>" +
            "</tr>");
        submitForm("DependentDetails", "Save");
        formClear1();

    });
    function formClear1() {
        $("#name2").val("");
        $("#relationship").val("");
        $("#gender").val("");
        $("#dob2").val("");
    }
    var values = new Array();
    $("#edit").click(function () {


        $.each($("input[type=checkbox]:checked").closest("td").siblings("td"),
            function () {
                values.push($(this).text().trim());
            });
        $("#myModal2").ready(function () {
            $("#editname2").val(values[0]);
            $("#editrelationship").val(values[1]);
            $("#editgender").val(values[2]);
            $("#editdob2").val(values[3]);
        });
        console.log(values);

    });
    $('#savechanges').click(function () {

        if ($("#dependentTable tbody").length == 0) {
            $("#dependentTable").append("<tbody></tbody>");
        }
       // var date = new Date(Date.parse($("#editdob2").val())).format("dd/MM/yyyy");
        var parts = $("#editdob2").val().split('-');
        var date = parts[2] + '-' + parts[1] + '-' + parts[0];
        $("#dependentTable tbody").append("<tr>" + str +
            "<td>" + $("#editname2").val() + "</td>" +
            "<td>" + $("#editrelationship").val() + "</td>" +
            "<td>" + $("#editgender").val() + "</td>" +
            "<td>" + date + "</td>" +
            "</tr>");

        $("input[type=checkbox]:checked").closest("td").siblings("td").remove();

        $("input[type=checkbox]:checked").closest("tr").remove();
        submitForm("DependentDetails", "Edit");
        formClear2();

    });
    function formClear2() {
        $("#editname2").val("");
        $("#editrelationship").val("");
        $("#editgender").val("");
        $("#editdob2").val("");
    }
    function submitForm(detailsType, action) {
        console.log(values);
        console.log("Hi")
        var updatedDetails = "";
        var updatedDetailsJson = "{";
        var previousDetailsJson = "{";
        var previousDetails = "";
        var form = document.getElementById(detailsType + action);
        var inp = form.getElementsByTagName("*");

        var j = 0;
        for (i = 0; i < inp.length; i++) {
            if (typeof inp[i].name == 'string' && inp[i].name > '' && typeof inp[i].value == 'string') {
                updatedDetails += (inp[i].name + '=' + escape(inp[i].value) + '&');
                updatedDetailsJson += '"' + inp[i].name + '":"' + inp[i].value + '",';
                previousDetailsJson += '"' + inp[i].name + '":"' + values[j] + '",';
                previousDetails += (inp[i].name + '=' + escape(values[j]) + '&')
                j++;
            }

        }
        updatedDetails += "token=" + token;

        // updatedDetails = updatedDetails.substr(0, updatedDetails.length - 1);
        // previousDetails = previousDetails.substr(0, previousDetails.length - 1);
        updatedDetailsJson = updatedDetailsJson.substr(0, updatedDetailsJson.length - 1);
        updatedDetailsJson += "}";

        previousDetailsJson = previousDetailsJson.substr(0, previousDetailsJson.length - 1);
        previousDetailsJson += "}";

        var xhttp = new XMLHttpRequest();
        var data = [];

        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                formClear();
                // document.getElementById(1).onmouseover
            }
        }
        var method;
        console.log(JSON.stringify(updatedDetails));
        if (action == "Edit") {
            data.push(JSON.parse(previousDetailsJson || {}));
            data.push(JSON.parse(updatedDetailsJson || {}));


            $.ajax({
                type: "PUT",
                url: "/ProspectiveEmployee/" + detailsType,
                data: { dependent: data, token: token },
                dataType: "application/json",
            });
        }
        else if (action == "Save") {
            method = "POST";
            xhttp.open(method, "/ProspectiveEmployee/" + detailsType, true);
            xhttp.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
            xhttp.send(updatedDetails);
        }

        console.log(updatedDetails);
        console.log(previousDetails);
    }

    var boxcounter = 0
    $('input[type=checkbox]').each(function () {

        if ($(this).is(":checked")) {

            boxcounter++;
            if (boxcounter > 1) {
                $("#edit").attr("disabled", true);
                $("#add").attr("disabled", true);
            }
        }
    });

    function getSelectedRows() {
        var selectedRows = []
        $('input[type=checkbox]').each(function () {
            if ($(this).is(":checked")) {
                selectedRows.push($(this));
            }
        });
        return selectedRows;
    }
    function deleteRows() {
        var selectedRows = getSelectedRows();

        for (var i = 0; i < selectedRows.length; i++) {
            $(selectedRows[i]).parent().parent().remove();
        }
    }
    $('#delete').click(function () {
        deleteRows();
    });
});


$(document).ready(function () {
    function previewFile(input) {
        var file = $("input[type=file]").get(0).files[0];

        if (file) {
            var reader = new FileReader();

            reader.onload = function () {
                $("#blah").attr("src", reader.result);
            }

            reader.readAsDataURL(file);
        }
    }
});