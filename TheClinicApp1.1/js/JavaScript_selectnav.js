var settingOpen = 0;
function selectTile(id, RoleName) {

    var tileList = ['patients', 'token', 'doctor', 'pharmacy', 'stock'];
    if (RoleName == 'Doctor') {
        var Url = ['../Registration/Patients.aspx', '../Token/Tokens.aspx', '../Doctor/Doctors.aspx', '../Pharmacy/Pharmacy.aspx', '../Stock/Stock.aspx'];

    }
    else if (RoleName == 'Administrator') {
        var Url = ['../Registration/Patients.aspx', '../Token/Tokens.aspx', '../Login/AccessDenied.aspx', '../Pharmacy/Pharmacy.aspx', '../Stock/Stock.aspx'];
    }
    else if (RoleName == 'pharmacist') {
        var Url = ['../Registration/Patients.aspx', '../Token/Tokens.aspx', '../Login/AccessDenied.aspx', '../Pharmacy/Pharmacy.aspx', '../Stock/Stock.aspx'];
    }
    else {
        var Url = ['../Registration/Patients.aspx', '../Token/Tokens.aspx', '../Login/AccessDenied.aspx', '../Login/AccessDenied.aspx', '../Login/AccessDenied.aspx'];
    }
    for (i = 0; i < tileList.length; i++) {
        if (id == tileList[i]) {
            document.getElementById(id).className = 'active'
            window.location.href = Url[i];


        }
        else {
            document.getElementById(tileList[i]).className = tileList[i]

        }

    }

}

function openSettings() {
    if (settingOpen == 0) {
        $("#settings").fadeIn("slow", function () {

        });
        settingOpen = 1;
    }
    else {
        $("#settings").fadeOut("slow", function () {

        });
        settingOpen = 0;
    }

}

function OpenPageOnHyperLinkClick(HyperLinkid) {
    var url = "";
    debugger;
    if (HyperLinkid == "hlkAssignRoles") {
        NavigateUrl = "../Admin/AssignRoles.aspx";
    }

    else if (HyperLinkid == "hlkCreateUser") {
        NavigateUrl = "../Admin/User.aspx";
    }
    else if (HyperLinkid == "hlkInputMasters") {
        NavigateUrl = "../Admin/Masters.aspx";
    }

    document.getElementById('main').src = NavigateUrl;
    document.getElementById('settings').style.display = "none";

}