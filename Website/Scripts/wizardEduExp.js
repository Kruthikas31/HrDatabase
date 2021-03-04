var count = 0;

var values = new Array();
//progress bar
$(document).ready(function () {


    $(".next").click(function () {
        str_count = localStorage.getItem("count");
        if (str_count == null || str_count == "null") {
            count = 0;
        } else {
            count = parseInt(str_count);
        }

        count++;
        count = count % 4;
        localStorage.setItem("count", count);
    });

    count = parseInt(localStorage.getItem("count"));

    switch (count) {
        case 1:
            {
                $("#bt1").removeClass("active");
                $("#bt1").addClass("after1");
                $("#bar1").addClass("after2");
            }
            break;
        case 2:
            {
                $("#bt1").removeClass("active");
                $("#bt1").addClass("after1");
                $("#bar1").addClass("after2");
                $("#bt2").removeClass("active");
                $("#bt2").addClass("after1");
                $("#bar2").addClass("after2");
            }
            break;
        case 3:
            {
                $("#bt1").removeClass("active");
                $("#bt1").addClass("after1");
                $("#bar1").addClass("after2");
                $("#bt2").removeClass("active");
                $("#bt2").addClass("after1");
                $("#bar2").addClass("after2");
                $("#bt3").removeClass("active");
                $("#bt3").addClass("after1");
                $("#bar3").addClass("after2");
            }
            break;
    }
});

//education and experience details pop up-form
var str = '<td><input type="checkbox" class="form-check-input check" id="exampleCheck1" name="checkbox1" style="margin-left:auto;margin-right:auto"/></td>';
var action;
$(document).ready(function () {

    function submitForm(detailsType) {

        console.log("HiIn")
        var updatedDetails = "";
        var updatedDetailsJson = "{";
        var previousDetailsJson = "{";
        var previousDetails = "";
        var form = document.getElementById(detailsType);
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


        previousDetails = previousDetails.substr(0, previousDetails.length - 1);
        updatedDetailsJson = updatedDetailsJson.substr(0, updatedDetailsJson.length - 1);
        updatedDetailsJson += "}";
        previousDetailsJson = previousDetailsJson.substr(0, previousDetailsJson.length - 1);
        previousDetailsJson += "}";

        var xhttp = new XMLHttpRequest();
        var data = [];

        xhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {

                // document.getElementById(1).onmouseover
            }
        }
        var method;
        console.log(JSON.stringify(updatedDetails));
        if (action == "Edit") {
            if (detailsType == "EducationDetails") {
                data.push(JSON.parse(previousDetailsJson || {}));
                data.push(JSON.parse(updatedDetailsJson || {}));


                $.ajax({
                    type: "PUT",
                    url: "/ProspectiveEmployee/" + detailsType,
                    data: { education: data, token: token },
                    dataType: "application/json",
                });
            }
            else if (detailsType == "ExperienceDetails") {

                data.push(JSON.parse(previousDetailsJson || {}));
                data.push(JSON.parse(updatedDetailsJson || {}));
                console.log(data[0]);
                console.log(data[1]);
                $.ajax({
                    type: "PUT",
                    url: "/ProspectiveEmployee/" + detailsType,
                    data: { experience: data, token: token },
                    dataType: "application/json",
                    error: function () {
                        alert("Errors arose.");
                    },
                    success: function () {
                        alert("success");
                    },
                    cache: false
                });

            }

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
    $('#eduadd').click(function () {
        action = "Save";
    });
    $('#edusave').click(function () {
        if ($("#educationTable tbody").length == 0) {
            $("#educationTable").append("<tbody></tbody>");
        }

        if ($("input[type=checkbox]").is(":checked")) {
            var row = $("input[type=checkbox]:checked").closest("tr");
            $(row).find("td:eq(1)").text($("#degree").val());
            $(row).find("td:eq(2)").text($("#specialization").val());
            $(row).find("td:eq(3)").text($("#institute").val());
            $(row).find("td:eq(4)").text($("#year").val());

        } else {
            $("#educationTable tbody").append("<tr>" + str +
                "<td>" + $("#degree").val() + "</td>" +
                "<td>" + $("#specialization").val() + "</td>" +
                "<td>" + $("#institute").val() + "</td>" +
                "<td>" + $("#year").val() + "</td>" +
                "</tr>");
        }
        submitForm("EducationDetails");
        formClearEdu();
    });

    function formClearEdu() {
        $("#degree").val("");
        $("#specialization").val("");
        $("#institute").val("");
        $("#year").val("");
    }

    $("#eduedit").click(function () {
        action = "Edit"
        $("#educationTable input[type=checkbox]").change(function () {
            var len = $("#educationTable input:checked").length;
            $("#eduedit").attr("disabled", len >= 2)
        });
        values = [];

        $.each($("input[type=checkbox]:checked").closest("td").siblings("td"),
            function () {
                values.push($(this).text().trim());
            });
        $("#myModaledu").ready(function () {
            $("#degree").val(values[0]);
            $("#specialization").val(values[1]);
            $("#institute").val(values[2]);
            $("#year").val(values[3]);
        });

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
    $('#edudelete').click(function () {
        deleteRows();
    });
    $('#expdelete').click(function () {
        deleteRows();
    });

    $('#expsave').click(function () {
        if ($("#experienceTable tbody").length == 0) {
            $("#experienceTable").append("<tbody></tbody>");
        }
        var parts = $("#fromdate").val().split('-');
        var fromdate = parts[2] + '-' + parts[1] + '-' + parts[0];
        var parts = $("#todate").val().split('-');
        var todate = parts[2] + '-' + parts[1] + '-' + parts[0];
        if ($("input[type=checkbox]").is(":checked")) {
            var row = $("input[type=checkbox]:checked").closest("tr");
            $(row).find("td:eq(1)").text($("#company").val());
            $(row).find("td:eq(2)").text($("#jobrole").val());
            $(row).find("td:eq(3)").text($("#fromdate").val());
            $(row).find("td:eq(4)").text($("#todate").val());
            $(row).find("td:eq(5)").text($("#expyears").val());
            $(row).find("td:eq(6)").text($("#ctc").val());

        } else {

            $("#experienceTable tbody").append("<tr>" + str +
                "<td>" + $("#company").val() + "</td>" +
                "<td>" + $("#jobrole").val() + "</td>" +
                "<td>" + fromdate + "</td>" +
                "<td>" + todate + "</td>" +
                "<td>" + $("#expyears").val() + "</td>" +
                "<td>" + $("#ctc").val() + "</td>" +
                "</tr>");
        }
        submitForm("ExperienceDetails");
        formClearexp();
    });

    function formClearexp() {
        $("#company").val("");
        $("#jobrole").val("");
        $("#fromdate").val("");
        $("#todate").val("");
        $("#expyears").val("");
        $("#ctc").val("");
    }
    $("#expadd").click(function () {
        action = "Save";
    })
    $("#expedit").click(function () {
        $("#experienceTable input[type=checkbox]").change(function () {
            var len = $("#experienceTable input:checked").length;
            $("#expedit").attr("disabled", len >= 2)
        });
        action = "Edit";
        values = [];

        $.each($("input[type=checkbox]:checked").closest("td").siblings("td"),
            function () {
                values.push($(this).text().trim());
            });
        $("#myModalexp").ready(function () {

            $("#company").val(values[0]);
            $("#jobrole").val(values[1]);
            $("#fromdate").val(values[2]);
            $("#todate").val(values[3]);
            $("#expyears").val(values[4]);
            $("#ctc").val(values[5]);
        });
    });
});