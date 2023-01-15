import {Component, OnInit} from '@angular/core';
import {PatientService} from "../patient.service";
import {Doctors} from "../Models/Ptientmodel";
import {ColDef} from "ag-grid-community";

@Component({
  selector: 'app-search-doctor',
  templateUrl: './search-doctor.component.html',
  styleUrls: ['./search-doctor.component.css']
})
export class SearchDoctorComponent implements OnInit {

  public columnDefs: ColDef[] = [
    {headerName: "First Name", field: "firstName"},
    {headerName: "Last Name", field: "lastName"},
    {headerName: "Phone Number", field: "doctorPhoneNumber"},
    {headerName: "Gender", field: "gender"},
    {headerName: "Experience ", field: "experience"},
    {headerName: "Specialization ", field: "specilaization"},
    {headerName: "Qualification", field: "qualification"},

  ];


  public rowData: Array<Doctors> = []


  constructor(private patient: PatientService) {
  }

  ngOnInit() {
    this.patient.getDoctors().subscribe(data => {
      this.rowData = data;
      console.log(this.rowData);
    });
  }


}
