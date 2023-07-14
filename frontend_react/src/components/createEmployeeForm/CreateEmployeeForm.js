import React, { useState } from "react";
import { TextField, Button } from "@mui/material";
import Constants from "../../config/apiEndpoints";

export default function CreateEmployeeForm({
  onEmployeeCreated,
  employeeProps,
}) {
  const [firstName, setFirstName] = useState("");
  const [benefits, setBenefits] = useState("");

  const [newDependent, setNewDependent] = useState("");
  const [newDependents, setNewDependents] = useState([]);

  function addNewDependent() {
    if (!newDependent) {
      alert("Enter a Dependent");
      return;
    }

    const newDep = {
      id: 0,
      employeeId: 0,
      name: newDependent,
    };

    setNewDependents((oldDependents) => [...oldDependents, newDep]);
    setNewDependent("");
  }

  function deleteDependent(name) {
    const newArray = newDependents.filter((dep) => dep.name !== name);
    setNewDependents(newArray);
  }

  function handleSubmit(event) {
    event.preventDefault();

    const employeeToCreate = {
      id: 0,
      name: firstName,
      salaryRaw: 52000,
      benefitsPackage: benefits,
    };

    fetch(Constants.API_URL_CREATE_EMPLOYEE, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(employeeToCreate),
    })
      .then((response) => response.json())
      .then((responseFromServer) => {
        console.log(responseFromServer);

        //Post our new Dependents
        let depsWithEmpIds = [];

        newDependents.forEach(function (item) {
          item.employeeId = responseFromServer.id;
          depsWithEmpIds.push(item);
        });

        for (const dep of depsWithEmpIds) {
          fetch(Constants.API_URL_CREATE_DEPENDENT, {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify(dep),
          })
            .then((response) => response.json())
            .then((responseFromServer) => {
              console.log(responseFromServer);
            })
            .catch((error) => {
              console.log(error);
            });
        }
      })
      .catch((error) => {
        alert("Create Employee Failed");
      });

    onEmployeeCreated(employeeToCreate);
  }

  return (
    <React.Fragment>
      <div
        id="createEmployeeForm"
        style={{ paddingLeft: 200, paddingRight: 200 }}
      >
        <h2>Create Employee</h2>
        <form onSubmit={handleSubmit}>
          <TextField
            type="text"
            variant="outlined"
            color="secondary"
            label="Name"
            onChange={(e) => setFirstName(e.target.value)}
            value={firstName}
            fullWidth
            required
            sx={{ mb: 4 }}
          />
          <TextField
            type="text"
            variant="outlined"
            color="secondary"
            label="Benefits Package"
            onChange={(e) => setBenefits(e.target.value)}
            value={benefits}
            fullWidth
            required
            sx={{ mb: 4 }}
          />
          <h2>Dependents</h2>
          <TextField
            type="text"
            variant="outlined"
            color="secondary"
            label="Enter the Full Name of your Dependent (i.e. John Smith)"
            placeholder="Add a Dependent..."
            onChange={(e) => setNewDependent(e.target.value)}
            value={newDependent}
            fullWidth
            sx={{ mb: 4 }}
          />
          <ul>
            {newDependents.map((dep) => {
              return (
                <div>
                  <li>{dep.name}</li>
                  <button
                    onClick={() => {
                      deleteDependent(dep.name);
                    }}
                  >
                    Remove
                  </button>
                </div>
              );
            })}
          </ul>
          <Button
            onClick={() => {
              addNewDependent();
            }}
            variant="outlined"
            color="secondary"
          >
            Add Dependent
          </Button>
          <Button variant="outlined" color="secondary" type="submit">
            Submit
          </Button>
          <Button
            onClick={() => {
              onEmployeeCreated(null);
            }}
            variant="outlined"
            color="secondary"
          >
            Cancel
          </Button>
        </form>
      </div>
    </React.Fragment>
  );
}
