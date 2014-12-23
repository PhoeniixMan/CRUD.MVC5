
function EmployeeListViewModel() {
    var self = this;
    self.employees = ko.observableArray([]);

    self.getEmployees = function() {
        self.employees([]);

        $.blockUI();
        $.ajax({
            url: '/Home/GetAll',
            dataType: "json",
            contentType: 'application/json',
            type: "GET",
            success: function (data) {
                
                var arr = new Array();
                $.each(data, function(index) {
                    var anEmployee = data[index];
                    arr.push({
                        id: anEmployee.Id,
                        name: anEmployee.Name,
                        address: anEmployee.Address,
                        email: anEmployee.Email,
                        contact: anEmployee.Contact
                    });
                });
                self.employees(arr);
                $.unblockUI();
            },
            error: function (xhr) {
                bootbox.alert(xhr);
            }
        });


        self.showToEdit = function(item) {
            window.location = '/Home/Update/' + item.id;
        };

        self.delete = function (item) {          
            var json = JSON.stringify({employee: item});
            
            $.ajax({
                url: '/Home/Delete',
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
                            message: "Employee is deleted successfully"
                        });
                        window.setTimeout(function () {
                            bootbox.hideAll();
                        }, 2000);
                        self.getEmployees();
                    }
                },
                error: function (xhr) {
                    bootbox.error(xhr);
                }
            });
        };

        self.confirmToDelete = function(item) {
            bootbox.confirm("Are you sure, you want to delete this Employee ?", function(result) {
                if (result === true) {
                    self.delete(item);
                }
            });
        };
    };

    self.init = function () {
        self.getEmployees();
    };
}


$(document).ready(function () {
    var vm = new EmployeeListViewModel();
    ko.applyBindings(vm);
    vm.init();
})