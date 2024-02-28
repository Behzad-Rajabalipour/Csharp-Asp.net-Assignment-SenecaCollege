// AJAX for Teacher Add can go in here!
// This file is connected to the project via Shared/_Layout.cshtml

function AddTeacher() {

	//goal: send a request which looks like this:
	//POST : http://localhost:47091/api/TeacherData/AddTeacher
	//with POST data of Teachername, bio, email, etc.

	var URL = "http://localhost:47091/api/TeacherData/AddTeacher/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var EmployeeNumber = document.getElementById('EmployeeNumber').value;
	var HireDate = document.getElementById('HireDate').value;
	var Salary = document.getElementById('Salary').value;

	var TeacherData = {
		"TeacherId": 100,
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"EmployeeNumber": EmployeeNumber,
		"HireDate": HireDate,
		"Salary": Salary
	};

	console.log(TeacherData);

	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	console.log("hi");
	console.log(JSON.stringify(TeacherData));
	rq.send(JSON.stringify(TeacherData));
	//rq.send(TeacherData);
}