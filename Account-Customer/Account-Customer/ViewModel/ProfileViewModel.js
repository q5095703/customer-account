
function addressItem(address) {
    debugger;
    this.ID = ko.observable(address.id);
    this.Name = ko.observable(address.name);
    this.Postcode = ko.observable(address.postcode);
    this.Country = ko.observable(address.country);
    this.AddressLine1 = ko.observable(address.addressLine1);
    this.AddressLine2 = ko.observable(address.addressLine2);
    this.Town = ko.observable(address.town);
    this.County = ko.observable(address.county);
    this.ProfileID = ko.observable(address.profileID);
}


var viewModel = function () {

    var self = this;
    self.load = ko.observable(false);
    self.model = {};
    self.model.Profile = ko.observable();
    self.model.Addresses = ko.observableArray();
    self.CanEditProfile = ko.observable(false);
    self.CanEditAddress = ko.observable(false);
    self.currentSelected = ko.observable(new addressItem({
        "id":-1,
        "name": "",
        "postcode": "",
        "country": "",
        "addressLine1": "",
        "addressLine2": "",
        "town": "",
        "county": ""
    }));

    self.addAddress = function () {
        debugger;

        self.CanEditAddress(true);

        self.currentSelected(new addressItem({
            "id":-1,
            "name":"",
            "postcode":"",
            "country": "",
            "addressLine1": "",
            "addressLine2": "",
            "town": "",
            "county": "",
            "profileID": self.model.Profile().ID()
        }))
        debugger;
        //window.location.href = '/Home/UpdateAddress'
    }
    self.newAddress = function () {
        self.CanEditAddress(false);
    }
    self.editAddress = function (item) {
        debugger;
        self.currentSelected(item);
        self.CanEditAddress(true);
       // window.location.href = '/Home/UpdateAddress?ID=' + item.id;
    }
    self.editProfile = function () {
        self.CanEditProfile(true);
    }
    self.UpdateProfile = function () {
        debugger;
        var uncomplete = false;
        if (self.model.Profile().name() === "" || self.model.Profile().name() === null) {
            uncomplete = true;
            $("#profile-name").addClass("input--red");
        }
        if (self.model.Profile().email === "" || self.model.Profile().email === null) {
            uncomplete = true;
            $("#profile-email").addClass("input--red");
        }
        if (self.model.Profile().phoneNumber === "" || self.model.Profile().phoneNumber === null ) {
            uncomplete = true;
            $("#profile-number").addClass("input--red");
        }
        if (self.model.Profile().password === "" || self.model.Profile().password === null ) {
            uncomplete = true;
            $("#profile-password").addClass("input--red");
        }


        if (uncomplete === false) {
            $.ajax({
                type: "post",
                url: "/Profiles/Edit",
                data: { "profile": self.model.Profile() },
                dataType: "json",
                sucess: function (data) {
                    self.CanEditAddress(false);
                }
            })
        }
    }
    self.deleteProfile = function () {
        $.ajax({
            type: "post",
            url: "/Profiles/Delete",
            data: { "ID": self.model.Profile().ID },
            dataType: "json",
            sucess: function (data) {
                self.CanEditAddress(false);
            }
        })
    }
    self.deleteAddress = function (item) {
        $.ajax({
            type: "post",
            url: "/Addresses/Delete",
            data: { "ID": item.ID },
            dataType: "json",
            sucess: function (data) {
                self.CanEditAddress(false);
            }
        })
    }
    self.UpdateAddress = function (item) {
        debugger;
        var uncomplete = false;
        if (item.Name() === "") {
            uncomplete = true;
            $("#address-name").addClass("input--red");
        }
        if (item.Postcode() === "") {
            uncomplete = true;
            $("#address-postcode").addClass("input--red");
        }
        if (item.Country() === "") {
            uncomplete = true;
            $("#address-country").addClass("input--red");
        }
        if (item.AddressLine1() === "") {
            uncomplete = true;
            $("#address-line1").addClass("input--red");
        }
        if (item.AddressLine2() === "") {
            uncomplete = true;
            $("#address-line2").addClass("input--red");
        }
        if (item.Town() === "") {
            uncomplete = true;
            $("#address-town").addClass("input--red");
        }
        if (item.County() === "") {
            uncomplete = true;
            $("#address-county").addClass("input--red");
        }
        if (uncomplete === false) {
            if (self.model.Addresses.indexOf(item) >= 0) {
                $.ajax({
                    type: "post",
                    url: "/Addresses/Edit",
                    data: { "address": item },
                    dataType: "json",
                    sucess: function (data) {
                        self.CanEditAddress(false);
                    }
                })
            } else {
                $.ajax({
                        type: "post",
                        url: "/Addresses/Create",
                        data: { "address": item },
                        dataType: "json",
                        sucess: function (data) {
                            self.model.Addresses.push(item);
                            self.CanEditAddress(false);
                        }
                    });
                }
            
        }
             else {

        }
    }

}




$(function () {
    var vm = new viewModel();

    function profile(profile) {
        debugger;
        this.ID = ko.observable(profile.id);
        this.name = ko.observable(profile.name);
        this.email = ko.observable(profile.email);
        this.phoneNumber = ko.observable(profile.phoneNumber);
        this.password = ko.observable(profile.password);
        this.UpdateProfile = function () {
            debugger;
            vm.CanEditProfile(false);
        }
    }

   

    $.getJSON("/Home/Profile", function (data) {
        vm.model.Profile(new profile(data[0]))
        
        getAddresses();
    });

    function getAddresses() {

        $.getJSON("/Home/Addresses", function (addresses) {
            vm.model.Addresses.removeAll();
            if (addresses.length !== 0) {
                $.each(addresses, function (index, item) {
                    debugger;
                    vm.model.Addresses.push(new addressItem(item));
                });
            }
            tryApply();
        })

    }

    function tryApply() {
        debugger;
        vm.load(true);
        ko.applyBindings(vm);

    }


})

