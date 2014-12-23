
function LoginViewModel() {
    var self = this;
    self.username = ko.observable().extend({ required: true, maxLength: 10 });
    self.password = ko.observable().extend({ required: true, maxLength: 10 });

    /*errors*/
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

    self.getEmployeeDetail = function () {
        var employeeObj = {
            Name: self.name(),
            Address: self.address()
        };

        return {
            employee: employeeObj
        };
    };


    /* Actions */
    self.create = function () {
        self.errors.showAllMessages(false);
        if (self.errors().length > 0) {
            self.errors.showAllMessages();
            return;
        }

        var json = JSON.stringify(self.getEmployeeDetail());

        $.ajax({
            url: '/Home/Create',
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
                        message: "Employee is created successfully"
                    });
                    window.setTimeout(function () {
                        bootbox.hideAll();
                    }, 2000);
                    self.reset();
                }
            },
            error: function (xhr) {
                bootbox.error(xhr);
            }
        });
    };

    self.reset = function () {
        self.name('');
        self.address('');
        self.email('');
        self.contact('');

        self.removeErrors();
    };

    self.init = function () {
        self.reset();
    };
}

$(document).ready(function () {
    var vm = new LoginViewModel();
    ko.applyBindingsWithValidation(vm);
    vm.init();
});