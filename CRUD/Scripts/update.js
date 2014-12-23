
function EmployeeUpdateViewModel() {
    var self = this;
    self.id = ko.observable('').extend({ required: true });
    self.name = ko.observable('').extend({ required: true, maxLength: 50 });
    self.address = ko.observable('').extend({ maxLength: 100 });
    self.email = ko.observable('').extend({ required: true });
    self.contact = ko.observable().extend({ required: true });

    /* errors */
    self.errors = ko.validation.group(self);
    self.hasErrors = function () {
        return (self.errors().length > 0) ? true : false;
    };
    self.showErrors = function () {
        self.errors.showAllMessages();
    };
    self.removeErrors = function () {
        self.errors.showAllMessages(false);
    };

    self.getEmployeeObj = function () {
        return {
            Id: self.id(),
            Name: self.name(),
            Address: self.address(),
            Email: self.email(),
            Contact: self.contact()
        };
    };


    /* Actions */
    self.load = function () {
        $.blockUI();
        var json = JSON.stringify({ id: self.id() });
        $.ajax({
            url: '/Home/Get',
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: json,
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                self.name(data.Name);
                self.address(data.Address);
                self.email(data.Email);
                self.contact(data.Contact);
                $.unblockUI();
            },
            error: function (xhr) {
                bootbox.error(xhr);
            }
        });
    };

    self.update = function () {
        if (self.hasErrors()) {
            self.showErrors();
            return;
        }
        
        $.blockUI();
        var json = JSON.stringify(self.getEmployeeObj());
        
        $.ajax({
            url: '/Home/Update',
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: json,
            async: true,
            processData: false,
            cache: false,
            success: function (data) {
                if (data === 'true') {
                    bootbox.dialog({
                        message: "Employee is updated successfully"
                    });
                    window.setTimeout(function () {
                        bootbox.hideAll();
                    }, 2000);
                    self.reset();
                }
                $.unblockUI();
            },
            error: function (xhr) {
                bootbox.error(xhr);
            }
        });
    };

    self.reset = function () {
        self.load();
    };

    self.init = function () {
        self.id($('#txtEmployeeId').val());
        self.load();
    };
}

$(document).ready(function () {
    var vm = new EmployeeUpdateViewModel();
    ko.applyBindingsWithValidation(vm);
    vm.init();
});