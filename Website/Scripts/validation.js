//Basic Information
$(document).ready(function () {

    $('#save-info').on('click', function () {
        if (this.checked) {
            $('#addr1').val($("#addr").val());
            $('#city1').val($("#city").val());
            $('#state1').val($("#state").val());
            $('#pincode1').val($("#pincode").val());
            $('#country1').val($("#country").val());
        }
        else {
            $('#addr1').val('');
            $('#city1').val('');
            $('#state1').val('');
            $('#pincode1').val('');
            $('#country1').val('');
        }
    });
});

$(document).ready(function () {

    $('.fncheck').hide();
    $('.phonecheck').hide();
    $('.emailcheck').hide();
    $('.valid-feedback-dob').hide();
    $('.emergencyinfocheck').hide();
    $('.aadharcheck').hide();
    $('.pancheck').hide();
    $('.uancheck').hide();
    $('.esicheck').hide();
    $('.passcheck').hide();
    $('.doecheck').hide();
    $('.bankcheck').hide();
    $('.ifsccheck').hide();
    $('.bnamecheck').hide();
    $('.detailscheck').hide();
    $('.nationalitycheck').hide();
    $('.citycheck').hide();
    $('.addresscheck').hide();
    $('.pincodecheck').hide();
    $('.bloodcheck').hide();
    $('.statecheck').hide();
    $('.statecheck1').hide();
    $('.countrycheck').hide();
    $('.countrycheck1').hide();
    $('.gendercheck').hide();
    $('.maritalstatuscheck').hide();

    //Exp Years
 
        $("#todate").change(function () {
            var date1 = new Date($("#fromdate").val());
            var date2 = new Date($("#todate").val());
            var timeDiff = Math.abs(date2.getTime() - date1.getTime());
            var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
            var k = (diffDays / 365).toFixed(0);
            $('#expyears').val(k);
        });
    
    //First Name
    $('#firstname').keyup(function () {
        validateUsername();
    });
    function validateUsername() {

        if ($('#firstname').val().match('^[a-zA-Z]{3,16}$')) {
            $('.fncheck').hide();
            $("#firstname").addClass("green").removeClass("red");
        }
        else if ($('#firstname').val() == '') {
            $('.fncheck').show();
            $('#firstname').addClass("red").removeClass("green");
        }
        else {
            $('.fncheck').html("Name cannot contain numbers and special charcters").addClass("e1").show();
            $('#firstname').addClass("red").removeClass("green");
        }

    }

    //Middle Name
    $('#middlename').keyup(function () {
        validateMiddlename();
    });
    function validateMiddlename() {
        if ($("#middlename").val().match('^[a-zA-Z]{0,20}$')) {
            $("#middlename").addClass("green").removeClass("red");
        } else {
            $('#middlename').addClass("red").removeClass("green");
        }
    }

    //Last Name
    $('#lastname').keyup(function () {
        validatelastname();
    });
    function validatelastname() {
        if ($("#lastname").val().match('^[a-zA-Z]{0,20}$')) {
            $("#lastname").addClass("green").removeClass("red");
        } else {
            $('#lastname').addClass("red").removeClass("green");
        }
    }

    // //DOB
    $('#dob').keyup(function () {
        validatedob();
    });
    function validatedob() {
        var today = new Date();
        var dob = new Date($("#dob").val());
        var age = Math.floor((today - dob) / (365.25 * 24 * 60 * 60 * 1000));
        if (age >= 18) {
            $('.valid-feedback-dob').hide();
            $("#dob").addClass("green").removeClass("red");
        }
        else {
            $('.valid-feedback-dob').show();
            $('#dob').addClass("red").removeClass("green");
        }
    }


    // //Blood Group
    $('#blood').keyup(function () {
        validatebloodgroup();
    });
    function validatebloodgroup() {

        if ($("#blood").val() === "") {
            $('.bloodcheck').show().addClass("e1");
            $('#blood').addClass("red").removeClass("green");
        }
        else {
            $('.bloodcheck').hide();
            $("#blood").addClass("green").removeClass("red");

        }

    }



    // //Phone Number
    $('#phoneNumber').keyup(function () {
        validatephone();
    });
    function validatephone() {
        if ($('#phoneNumber').val().match('^[0-9]{10}$')) {
            $('.phonecheck').hide();
            $('#phoneNumber').addClass("green").removeClass("red");
        }
        else if ($('#phoneNumber').val() == '') {
            $('.phonecheck').show();
            $('#phoneNumber').addClass("red").removeClass("green");
        }
        else {
            $('.phonecheck').html("Please enter valid contact Info").addClass("e1").show();
            $('#phoneNumber').addClass("red").removeClass("green");
        }
    }

    //Email
    $('#pemail').keyup(function () {
        validatemail();
    });
    function validatemail() {
        if ($('#pemail').val().match(/^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/)) {
            $('.emailcheck').hide();
            $("#pemail").addClass("green").removeClass("red");
        }
        else if ($('#pemail').val() == '') {
            $('.emailcheck').show();
            $('#pemail').addClass("red").removeClass("green");
        }
        else {
            $('.emailcheck').html("Please enter valid Email Id").addClass("e1").show();
            $('#pemail').addClass("red").removeClass("green");
        }
    }


    //Emergency Contact Number
    $('#emergencycontact').keyup(function () {
        validatecontact();
    });
    function validatecontact() {
        if ($('#emergencycontact').val().match('^[0-9]{10}$')) {
            $('.emergencyinfocheck').hide();
            $("#emergencycontact").addClass("green").removeClass("red");
        }
        else if ($('#emergencycontact').val() == '') {
            $('.emergencyinfocheck').show();
            $('#emergencycontact').addClass("red").removeClass("green");
        }
        else {
            $('.emergencyinfocheck').html("Please enter valid contact information").addClass("e1").show();
            $('#emergencycontact').addClass("red").removeClass("green");
        }
    }


    //Aadhaar
    $('#aadharno').keyup(function () {
        validateaadhar();
    });

    function validateaadhar() {
        if ($('#aadharno').val().match("^[2-9]{1}[0-9]{3}[0-9]{4}[0-9]{4}$")) {
            $('.aadharcheck').hide();
            $("#aadharno").addClass("green").removeClass("red");
        }
        else if ($('#aadharno').val() == '') {
            $('.aadharcheck').show();
            $('#aadharno').addClass("red").removeClass("green");
        }
        else {
            $('.aadharcheck').html("Invalid aadhar number").addClass("e1").show();
            $('#aadharno').addClass("red").removeClass("green");
        }
    }


    //PAN

    $('#panno').blur(function () {
        validatepan();
    });

    function validatepan() {
        if ($('#panno').val().match(/[A-Z]{5}[0-9]{4}[A-Z]{1}$/)) {
            $('.pancheck').hide();
            $("#panno").addClass("green").removeClass("red");
        }
        else if ($('#panno').val() == '') {
            $('.pancheck').show();
            $('#panno').addClass("red").removeClass("green");
        }

        else {
            $('.pancheck').html("Invalid PAN number").addClass("e1").show();
            $('#panno').addClass("red").removeClass("green");
        }
    }


    //UAN
    $('#uan').blur(function () {
        validateuan();
    });

    function validateuan() {
        if ($('#uan').val().match('^[0-9]{12}$') ) {
            $('.uancheck').hide();
            $("#uan").addClass("green").removeClass("red");
        }
        else if ($('#uan').val() == '') {
            $('.uancheck').hide();
           // $('#uan').addClass("red").removeClass("green");
        }
        else {
            $('.uancheck').html("Invalid UAN number").addClass("e1").show();
            $('#uan').addClass("red").removeClass("green");
        }
    }


    //ESI
    $('#esi').blur(function () {
        validatesi();
    });

    function validatesi() {

        if ($('#esi').val().match('^[0-9]{17}$') ) {
            $('.esicheck').hide();
            $("#esi").addClass("green").removeClass("red");
        }
        else if ($('#esi').val() == '') {
            $('.esicheck').hide();
           // $('#esi').addClass("red").removeClass("green");
        }
        else {
            $('.esicheck').html("Invalid ESI number").addClass("e1").show();
            $('#esi').addClass("red").removeClass("green");
        }
    }

    //Passport
    $('#passport').blur(function () {
        validatepass();
    });

    function validatepass() {
        if ($('#passport').val().match(/[A-Z]{1}[0-9]{7}$/)) {
            $('.passcheck').hide();
            $("#passport").addClass("green").removeClass("red");
        }
        else if ($('#passport').val() == '') {
            $('.passcheck').hide();
           // $('#passport').addClass("red").removeClass("green");
        }
        else {
            $('.passcheck').html("Invalid PASSPORT number").addClass("e1").show();
            $('#passport').addClass("red").removeClass("green");
        }
    }


    //Passport Expiry
    //$('#doe').blur(function () {
    //    validatedoe();
    //});

    //function validatedoe() {

    //    if ($('#doe').val().match(/^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$/)) {
    //        $('.doecheck').hide();
    //        $("#doe").addClass("green").removeClass("red");
    //    }
    //    else if ($('#doe').val() == '') {
    //        $('.doecheck').show();
    //        $('#doe').addClass("red").removeClass("green");

    //    }
    //    else {
    //        $('.doecheck').html("Invalid").addClass("e1").show();
    //        $('#doe').addClass("red").removeClass("green");
    //    }
    //}

    //Nationality
    $('#nationality').blur(function () {
        validatenationality();
    });

    function validatenationality() {

        if ($('#nationality').val() === "") {
            $('.nationalitycheck').show().addClass("e1");
            $('#nationality').addClass("red").removeClass("green");
        }
        else {
            $('.nationalitycheck').hide();
            $("#nationality").addClass("green").removeClass("red");

        }

    }

    //Bank Account number
    $('#banknumber').blur(function () {
        validatebank();
    });

    function validatebank() {

        if ($('#banknumber').val().match("[0-9]{9,18}$")) {
            $('.bankcheck').hide();
            $("#banknumber").addClass("green").removeClass("red");
        }
        else if ($('#banknumber').val() == '') {
            $('.bankcheck').show();
            $('#banknumber').addClass("red").removeClass("green");

        } else {
            $('.bankcheck').html("Please provide valid account number").addClass("e1").show();
            $('#banknumber').addClass("red").removeClass("green");
        }
    }

    //IFSC
    $('#ifsc').blur(function () {
        validateifsc();
    });

    function validateifsc() {
        if ($('#ifsc').val().match("^[A-Za-z]{4}[0-9]{7}$")) {
            $('.ifsccheck').hide();
            $("#ifsc").addClass("green").removeClass("red");
        }
        else if ($().val() == '') {
            $('.ifsccheck').show();
            $('#ifsc').addClass("red").removeClass("green");
        }
        else {
            $('.ifsccheck').html("Please provide valid IFSC number").addClass("e1").show();
            $('#ifsc').addClass("red").removeClass("green");
        }
    }

    //Name
    $('#bname').blur(function () {
        validatebname();
    });

    function validatebname() {

        if ($('#bname').val().match("^[a-zA-Z ]+$")) {
            $('.bnamecheck').hide();
            $("#bname").addClass("green").removeClass("red");
        }
        else if ($('#bname').val() == '') {
            $('.bnamecheck').show();
            $('#bname').addClass("red").removeClass("green");
        }
        else {
            $('.bnamecheck').html("Name cannot have numbers and special characters").addClass("e1").show();
            $('#bname').addClass("red").removeClass("green");
        }
    }

    //BankBranchDetails
    $('#branchdetails').blur(function () {
        validatebranchdetails();
    });

    function validatebranchdetails() {
        if ($('#branchdetails').val().match("[a-zA-Z0-9]+\\.?")) {
            $('.detailscheck').hide();
            $("#branchdetails").addClass("green").removeClass("red");
        }
        else {
            $('.detailscheck').show();
            $('#branchdetails').addClass("red").removeClass("green");
        }
    }

    //Pincode
    $('#pincode').blur(function () {
        validatepincode();
    });

    function validatepincode() {
        if ($('#pincode').val().match("^[1-9][0-9]{5}$")) {
            $('.pincodecheck').hide();
            $("#pincode").addClass("green").removeClass("red");
        }
        else if ($('#pincode').val() == '') {
            $('.pincodecheck').show();
            $('#pincode').addClass("red").removeClass("green");
        }
        else {
            $('.pincodecheck').html("Invalid Pincode").addClass("e1").show();
            $('#pincode').addClass("red").removeClass("green");
        }
    }

    //Address
    $('#addr').blur(function () {
        validateaddr();
    });

    function validateaddr() {

        if ($('#addr').val().match("[a-zA-Z0-9]+\\.?")) {
            $('.addresscheck').hide();
            $("#addr").addClass("green").removeClass("red");
        } else {
            $('.addresscheck').show();
            $('#addr').addClass("red").removeClass("green");
        }
    }

    //City
    $('#city').blur(function () {
        validatecity();
    });

    function validatecity() {

        if ($('#city').val().match("^[a-zA-Z]+(?:[\s-][a-zA-Z]+)*$")) {
            $('.citycheck').hide();
            $("#city").addClass("green").removeClass("red");
        }
        else if ($('#city').val() == '') {
            $('.citycheck').show();
            $('#city').addClass("red").removeClass("green");
        }
        else {
            $('.citycheck').html("Invalid").addClass("e1").show();
            $('#city').addClass("red").removeClass("green");
        }
    }

    //State
    $('#state').blur(function () {
        validatestate();
    });

    function validatestate() {
        if ($('#state').val() === "") {
            $('.statecheck').show().addClass("e1");
            $('#state').addClass("red").removeClass("green");
        }
        else {
            $('.statecheck').hide();
            $("#state").addClass("green").removeClass("red");

        }

    }

    //Country
    $('#country').blur(function () {
        validatecountry();
    });

    function validatecountry() {
        if ($('#country').val() === "") {
            $('.countrycheck').show().addClass("e1");
            $('#country').addClass("red").removeClass("green");
        }
        else {
            $('.countrycheck').hide();
            $("#country").addClass("green").removeClass("red");

        }

    }

    $('#primarybtn').click(function (e) {

        e.preventDefault();
        document.documentElement.scrollTop = 0;
        validateUsername();
        validateMiddlename();
        validatelastname();
        validatedob();
        validatebloodgroup();
        validatephone();
        validatemail();
        validatecontact();
        validateaadhar();
        validatepan();
        validateuan();
        validatesi()
        validatepass();
        validatedoe();
        validatenationality();
        validatebank();
        validateifsc();
        validatebname();
        validateaddr();
        validatecity();
        validatestate();
        validatecountry();

    });
});









