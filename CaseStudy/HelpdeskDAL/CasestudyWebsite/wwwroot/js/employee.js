$(function () {

    // Asynchrous function that gets all the employees from the database, then prints them in list form
    //  on the HTML website
    const getAll = async (msg) => {
        try {
            $("#employeeList").html("<h3>Finding Employee Information..., please wait... </h3>");
            let response = await fetch(`api/employee`);
            if (!response.ok) // or check for response. status
                throw new Error(`Status - ${response.status}, Problem server side, see server console`);
            let data = await response.json(); // this returns a promise, so we await it
            buildEmployeeList(data);
            (msg === "") ? // are we appending to an existing message
                $("#status").text("Employee Loaded") : $("#status").text(`${msg} - Employee Loaded`);
        } catch (error) {
            $("#status").text(error.message);
        }
    };// getAll

    // Setup const function that gets the information about student that is wanting to be
    //  updated, then put the information inside the field
    const setupForUpdate = (id, data) => {
        // Changes the button function name
        $("#actionbutton").val("update");
        // Changes the heading
        $("div.modaltitle").html("<h4>update employee</h4>");
        // Give option to delete, not like add function
        $("#deletebutton").show();

        // Clear field incase of previous update the box was fillled in
        clearModalFields();
        data.map(employee => {
            if (employee.id === parseInt(id)) {
                // Gets the information and fil them inthe field
                $("#TextBoxTitle").val(employee.title);
                $("#TextBoxFirstname").val(employee.firstname);
                $("#TextBoxLastname").val(employee.lastname);
                $("#TextBoxPhone").val(employee.phoneno);
                $("#TextBoxEmail").val(employee.email);
                $("ddlDepts").val(employee.departmentId);
                sessionStorage.setItem("Id", employee.id);
                sessionStorage.setItem("Timer", employee.timer);
                $("modalstatus").text("update data");
                $("#theModal").modal("toggle");
            }
        });
    };

    // Setup const function that sets up for addition of employee. Just changes the title
    const setupForAdd = () => {
        $("#actionbutton").val("add");
        // changes the heading
        $("div.modaltitle").html("<h4>add employee</h4>");
        $("#theModal").modal("toggle");
        // Changes the button function name
        $("#modalstatus").text("add new employee");
        $("#deletebutton").hide();
        $("ddlDepts").val(-1);
        clearModalFields();
    };

    // Clears all the centent of the field
    const clearModalFields = () => {
        $("#TextBoxTitle").val("");
        $("#TextBoxFirstname").val("");
        $("#TextBoxLastname").val("");
        $("#TextBoxPhone").val("");
        $("#TextBoxEmail").val("");
        sessionStorage.removeItem("Id");
        sessionStorage.removeItem("DepartmentId");
        sessionStorage.removeItem("Timer");
    }; 

    // Builds the list by using the inputted data, by mapping them by each employees
    const buildEmployeeList = (data) => {
        // Clear the list when this function gets called again
        $("#employeeList").empty();
        // build the headings
        div = $(`<div class="list-group-item text-white bg-secondary row d-flex" id="status">Employee Info</div>
                    <div class= "list-group-item row d-flex text-center" id="heading">
                    <div class="col-4 h4">Title</div>
                    <div class="col-4 h4">First</div>
                    <div class="col-4 h4">Last</div>
                </div>`);
        // Append the heading to employee list
        div.appendTo($("#employeeList"));
        // Get the data from JSON to string
        sessionStorage.setItem("allemployees", JSON.stringify(data));
        // Append Add button to the list
        btn = $(`<button class="list-group-item row d-flex" id="0"><div class="col-12 text-left">...click to add employee</div></button>`);
        btn.appendTo($("#employeeList"));
        // Get the employee from the employee list and put them into list
        data.map(emp => {
            btn = $(`<button class="list-group-item row d-flex" id="${emp.id}">`);
            btn.html(`<div class="col-4" id="employeetitle${emp.id}">${emp.title}</div>
                        <div class="col-4" id="employeefname${emp.id}">${emp.firstname}</div>
                        <div class="col-4" id="employeelastname${emp.id}">${emp.lastname}</div>`
            );
            btn.appendTo($("#employeeList"));
        }); // map 
    };

    // Updates the current existing employee information
    const update = async () => {

        try {
            //set up a new client side instance of Employee
            emp = new Object();
            // populate the properties
            emp.Title = $("#TextBoxTitle").val();
            emp.Firstname = $("#TextBoxFirstname").val();
            emp.Lastname = $("#TextBoxLastname").val();
            emp.Phoneno = $("#TextBoxPhone").val();
            emp.Email = $("#TextBoxEmail").val();
            // we stored these 3 earlier 
            emp.Id = sessionStorage.getItem("Id");
            emp.DepartmentId = $(`#ddlDepts`).val();
            emp.Timer = sessionStorage.getItem("Timer");
            emp.Picture64 = null;
            // send the updated back to the server asynchronously using PUT 
            let response = await fetch("api/employee", {
                method: "PUT",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(emp)
            });

            if (response.ok) // or check for response. status
            {
                let data = await response.json();
                getAll(data.msg);
            }
            else {
                $("#status").text(`${response}, Text - ${response.statusText}`);
            } // else
            $("#theModal").modal("toggle");
        } catch (error) {
            $("#status").text(error.message);
        }// try/catch

    };

    // Add a new employee to the database
    const add = async () => {

        try {
            //set up a new client side instance of Employee
            emp = new Object();
            // populate the properties
            emp.Title = $("#TextBoxTitle").val();
            emp.Firstname = $("#TextBoxFirstname").val();
            emp.Lastname = $("#TextBoxLastname").val();
            emp.Phoneno = $("#TextBoxPhone").val();
            emp.Email = $("#TextBoxEmail").val();
            emp.DepartmentId = $(`#ddlDepts`).val();
            emp.Id = -1;
            emp.Timer = null;
            emp.Picture64 = null;
            // send the new employee to the server asynchronously using POST 
            let response = await fetch("api/employee", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                body: JSON.stringify(emp)
            });
            if (response.ok) { // or check for response. status
                let data = await response.json();
                getAll(data.msg);
            } else {
                $("#status").text(`${response}, Text - ${response.statusText}`);
            } // else
            $("#theModal").modal("toggle");
        } catch (error) {
            $("#status").text(error.message);
        }// try/catch
    };

    // Delete an employee from the list
    const _delete = async () => {
        try {
            // call new fetch to remove employee from database
            let response = await fetch(`api/employee/${sessionStorage.getItem('Id')}`, {
                method: 'DELETE',
                headers: { 'Content-Type': 'application/json; charset=utf-8' }
            });
            if (response.ok) {
                let data = await response.json();
                getAll(data.msg);
            }
            else {
                $("#status").text(`Status - ${response.status}, Problem on delete server side, see server console`);
            } // else
            $('#theModal').modal('toggle');
        } catch (error) {
            ('#status').text(error.message);
        } // try/catch
    }

    // calls a function based on what button have been clicked
    $("#actionbutton").click(() => {
        $("#actionbutton").val() === "update" ? update() : add();
    });

    // event handler for confirmation click, passes JSON object to click event and the object
    //  contains the user's choice. If the choice was yes, the delete button click is executed
    //  If it is no, then delete click button is ignored
    $('[data-toggle=confirmation]').confirmation({ rootSelector: '[data-toggle=confirmation]' });

    $('#deletebutton').click(() => _delete()); // if yes was chosen

    // Employee click event handler
    $("#employeeList").click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === "employeeList" || Id === "") {
            Id = e.target.id;
        }

        // If the id of employee matches what employees id is in database, get the information
        // from local storage
        if (Id !== "status" && Id !== "heading") {
            let data = JSON.parse(sessionStorage.getItem("allemployees"));
            Id === "0" ? setupForAdd() : setupForUpdate(Id, data);
        }
        else {
            return false;
        }
    });

    // Loads the dropdown menu for departments
    let loadDepartmentDDL = async () => {
        // set all divisions to local storage
        response = await fetch('api/department');
        if (!response.ok)
            throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
        let divs = await response.json();

        // declare html(string) value
        html = '';
        // clear the dropdown menu
        $('#ddlDepts').empty();
        // add element to the html value
        divs.map(div =>
            html += `<option value="${div.id}">${div.departmentName}</option>`
        );
        // append the html value to the dropdown menu
        $('#ddlDepts').append(html);
    };

    // Call the functions
    loadDepartmentDDL();
    getAll("");
});