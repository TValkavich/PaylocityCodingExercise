import React from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import Typography from "@mui/material/Typography";
import { Button, CardActions, List, ListItem, ListItemText } from "@mui/material";
import Grid from "@mui/material/Grid";
import Divider from "@mui/material/Divider";

export default function EmployeeCard({
  employee,
  onEmployeeDeleted,
  dependents,
}) {
  return (
    <div>
      <Grid
        container
        direction="column"
        justifyContent="space-between"
        flexDirection="column"
        alignItems="center"
      >
        <Card sx={{ maxWidth: 1500, margin: 5 }}>
          <CardContent>
            <Grid
              container
              direction="row"
              justifyContent="space-between"
              flexDirection="row"
              alignItems="center"
            >
              <div
                id="employeeName"
                style={{ paddingLeft: 50, paddingRight: 50 }}
              >
                <Typography gutterBottom variant="h5" component="div">
                  <List>
                    <ListItem>
                      <ListItemText
                        primary={"Name"}
                        secondary={employee.name}
                      ></ListItemText>
                    </ListItem>
                  </List>
                </Typography>
              </div>
              <Divider orientation="vertical" flexItem />
              <div
                id="employeeInfo"
                style={{ paddingLeft: 50, paddingRight: 50 }}
              >
                <h2 style={{ textAlign: "center" }}>Employee Info</h2>
                <Typography gutterBottom variant="h5" component="div">
                  <List>
                    <ListItem>
                      <ListItemText
                        primary={"Gross Salary"}
                        secondary={`$${employee.salaryRaw}`}
                      ></ListItemText>
                    </ListItem>
                    <ListItem>
                      <ListItemText
                        primary={"Net Salary"}
                        secondary={`$${employee.adjustedSalary}`}
                      ></ListItemText>
                    </ListItem>
                    <ListItem>
                      <ListItemText
                        primary={"Weekly Benefits Cost"}
                        secondary={`$${employee.paycheckBenefitsDeductions}`}
                      ></ListItemText>
                    </ListItem>
                    <ListItem>
                      <ListItemText
                        primary={"Benefits Package"}
                        secondary={employee.benefitsPackage}
                      ></ListItemText>
                    </ListItem>
                  </List>
                </Typography>
              </div>
              <Divider orientation="vertical" flexItem />
              <div
                id="listDependents"
                style={{ paddingLeft: 50, paddingRight: 50 }}
              >
                <h2 style={{ textAlign: "center" }}>Dependents</h2>
                {dependents
                  .filter((dep) => dep.employeeId === employee.id)
                  .map((dep) => {
                    return (
                      <List>
                        <ListItem>
                          <ListItemText primary={dep.name}></ListItemText>
                        </ListItem>
                      </List>
                    );
                  })}
              </div>
            </Grid>
          </CardContent>
          <CardActions>
            <Button
              variant="outlined"
              size="large"
              color="primary"
              onClick={() => {
                console.log("Update Employee");
              }}
            >
              Update Employee
            </Button>
            <Button
              variant="outlined"
              size="large"
              color="secondary"
              onClick={() => {
                onEmployeeDeleted(employee.id);
              }}
            >
              Delete Employee
            </Button>
          </CardActions>
        </Card>
      </Grid>
    </div>
  );
}
