﻿var settingOpen = 0;
var patientPageURL = '../Registration/Patients.aspx';
var tokenPageURL = '../Token/Tokens.aspx';
var doctorPageURL = '../Doctor/Doctors.aspx';
var pharmacyPageURL = '../Pharmacy/Pharmacy.aspx';
var stockPageURL = '../Stock/Stock.aspx';
var adminPageURL = '../Admin/Admin.aspx';
var masterPageURL = '../MasterAdd/Categories.aspx';

function selectTile(id, RoleName) {
  //  debugger;
    var tileList = ['patients', 'token', 'doctor', 'pharmacy', 'stock', 'admin','master'];
    var Url = [patientPageURL, tokenPageURL, doctorPageURL, pharmacyPageURL, stockPageURL,adminPageURL,masterPageURL];

     

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

function activateTabSelection(id) {

    var tileList = ['patients', 'token', 'doctor', 'pharmacy', 'stock', 'admin','master'];
    var Url = [patientPageURL, tokenPageURL, doctorPageURL, pharmacyPageURL, stockPageURL, adminPageURL,masterPageURL];



    for (i = 0; i < tileList.length; i++) {
        if (id == tileList[i]) {
            document.getElementById(id).className = 'active'; 


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