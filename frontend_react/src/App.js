import "./App.css";
import { Button } from "@mui/material";
import Grid from "@mui/material/Grid";
import { useState, useEffect } from "react";
import EmployeeCard from "./components/employeeCard/EmployeeCard";
import CreateEmployeeForm from "./components/createEmployeeForm/CreateEmployeeForm";
import Constants from "./config/apiEndpoints";
import checkForDiscountEmployee from "./businessLogic/checkForDiscountEmployee";
import checkForDiscountDependents from "./businessLogic/checkForDiscountDependents";

function App() {
  useEffect(() => {
    getDependents();
    getEmployees();
  }, []);

  const [employees, setEmployees] = useState([]);
  const [dependents, setDependents] = useState([]);
  const [showingCreateNewEmployee, setShowingCreateNewEmployee] =
    useState(false);

  function onEmployeeDeleted(employeeId) {
    const url = `${Constants.API_URL_DELETE_EMPLOYEE_BY_ID}/${employeeId}`;

    fetch(url, {
      method: "DELETE",
    })
      .then((response) => response.json())
      .then((responseFromServer) => {
        console.log(responseFromServer);
        getEmployees();
      })
      .catch((error) => {
        console.log(error);
        alert("Delete Employee Failed");
      });

    getEmployees();
  }

  function onEmployeeCreated(createdEmployee) {
    setShowingCreateNewEmployee(false);

    if (createdEmployee === null) {
      return;
    } else {
      alert(`Employee successfully created`);
    }

    getEmployees();
    getDependents();
    window.location.reload();
  }

  function getEmployees() {
    const url = Constants.API_URL_GET_ALL_EMPLOYEES;

    fetch(url, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((employees) => {
        setEmployees(employees);
      })
      .catch((error) => {
        //Log the error
        console.log(error);
      });
  }

  function getDependents() {
    const url = Constants.API_URL_GET_ALL_DEPENDENTS;

    fetch(url, {
      method: "GET",
    })
      .then((response) => response.json())
      .then((dependents) => {
        setDependents(dependents);
      })
      .catch((error) => {
        //Log the error
        console.log(error);
      });
  }

  function onDelete(employeeId) {
    const url = `${Constants.API_URL_DELETE_EMPLOYEE_BY_ID}/${employeeId}`;

    fetch(url, {
      method: "DELETE",
    })
      .then((response) => response.json())
      .then((responseFromServer) => {
        console.log(responseFromServer);
        getEmployees();
      })
      .catch((error) => {
        console.log(error);
        alert("Delete Employee Failed");
      });
  }

  //This business logic should not be in the Client. I meant to put it in the backend to keep
  //  the view as "dumb" as possible and maintain seperation of concerns between the model,
  //  view and controller but I am short on time so it is here instead.
  const adjust = (employee) => {
    let filteredDeps = dependents.filter(
      (dep) => dep.employeeId === employee.id
    );

    let discountedEmployee = checkForDiscountEmployee(employee.name);
    let discountedDependents = checkForDiscountDependents(filteredDeps);

    //Adjust our net income after taxes. We could create a base class in the backend for Taxes and override methods
    // based off of different states but again we are short on time. We will assume $4k is deducted from taxes from NY
    let adjustedSalary = 52000 - 4000;

    //Calculate our benefits package cost per week
    let employeeBenefitCost = discountedEmployee === 1 ? 900 : 1000;

    let dependentBenefitsCost =
      450 * discountedDependents +
      500 * (filteredDeps.length - discountedDependents);
    let paycheckBenefitsDeductions = (
      (employeeBenefitCost + dependentBenefitsCost) /
      26
    ).toFixed(2);

    console.log(employeeBenefitCost + dependentBenefitsCost);
    console.log(employee);

    let finalEmployee = {
      id: employee.id,
      name: employee.name,
      salaryRaw: employee.salaryRaw,
      benefitsPackage: employee.benefitsPackage,
      paycheckBenefitsDeductions: paycheckBenefitsDeductions,
      adjustedSalary: adjustedSalary,
    };

    return finalEmployee;
  };

  return (
    <div>
      <div
        id="titleHeader"
        style={{
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          margin: 50,
        }}
      >
        <h2>Paylocity Benefits Portal</h2>
      </div>
      {showingCreateNewEmployee == false && (
        <div id="mainScreen">
          <div id="createEmployeeButton" style={{ textAlign: "center" }}>
            <Button
              variant="outlined"
              size="large"
              color="primary"
              onClick={() => {
                setShowingCreateNewEmployee(true);
              }}
            >
              Create Employee
            </Button>
          </div>
          <Grid
            container
            direction="column"
            justifyContent="space-between"
            flexDirection="column"
            alignItems="center"
          >
            <div id="employeeCards">
              {employees !== null &&
                employees
                  .map((emp) => {
                    return adjust(emp);
                  })
                  .map((emp) => {
                    return (
                      <EmployeeCard
                        employee={emp}
                        onEmployeeDeleted={onEmployeeDeleted}
                        dependents={dependents}
                      />
                    );
                  })}
            </div>
          </Grid>
        </div>
      )}
      {showingCreateNewEmployee && (
        <div>
          <CreateEmployeeForm
            onEmployeeCreated={onEmployeeCreated}
            employeeProps={employees}
          />
        </div>
      )}
    </div>
  );
}

export default App;
