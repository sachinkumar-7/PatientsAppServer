import {Component, OnInit, ViewChild} from '@angular/core';
import {NgForm} from "@angular/forms";
import {AdminService} from "./admin.service";
import {ToastrService} from 'ngx-toastr';
import {ConsultationDetails, Doctors, Message, Patient} from "../../patient/Models/Ptientmodel";
import {Router} from "@angular/router";
import {ColDef} from "ag-grid-community";
import {PatientService} from "../../patient/patient.service";


@Component({
  selector: 'admin-admin-home',
  templateUrl: './admin-home.component.html',
  styleUrls: ['./admin-home.component.css']
})

export class AdminHomeComponent implements OnInit {
getToday() {
return new Date().toISOString().split('T')[0]
}

  constructor(private service: AdminService, private toastr: ToastrService, private router: Router, private patient: PatientService) {
  }

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
  public defaultColDef: ColDef = {
    sortable: true,
    filter: true,
  };


  public usercolumnDefs: ColDef[] = [
    {headerName: "Patient ID", field: "patientId"},
    {headerName: "First Name", field: "firstName"},
    {headerName: "Last Name", field: "lastName"},
    {headerName: "Gender", field: "gender"},
    {headerName: "Phone Number", field: "patientPhoneNumber"},
    {headerName: "Emergency Contact Number", field: "emergencyContactNumber"},
    {headerName: "Blood Group", field: "bloodGroup"},
    {headerName: "Aadhar Number", field: "aadhar"},
    {headerName: "House Number", field: "houseNo"},
    {headerName: "Place", field: "place"},
    {headerName: "Pincode", field: "pincode"},


  ];

  public userrowData: Array<Patient> = [];


  public concolumnDefs: ColDef[] = [
    {headerName: "Appointment ID", field: "appointmentId"},
    {headerName: "Patient ID", field: "patientId"},
    {headerName: "Doctor Id", field: "doctorId"},
    {headerName: "Date", field: "date"},
    {headerName: "Session", field: "session"},
    {headerName: "Consultation Fee", field: "consultationFee"},
    {headerName: "Status", field: "status"},
    {headerName: "Diagnosis", field: "diagnosis"},
    {headerName: "medications", field: "medications"},
    {headerName: "Radiology", field: "radiology"},
    {headerName: "Lab Test", field: "labTest"},


  ];

  public conrowData: Array<ConsultationDetails> = [];


  ngOnInit() {
    this.patient.getDoctors().subscribe(data => {
      this.rowData = data;
      // console.log(this.rowData);
    });

    this.patient.getAllPatients().subscribe(data => {
      this.userrowData = data;
      // console.log(this.userrowData);
    });

    this.patient.getAllConsultationDetails().subscribe(data => {
      this.conrowData = data;
      // console.log(this.conrowData);
    });

  }


  success() {
    // console.log('hi');
    this.toastr.success('Account Added Successfully');
    this.router.navigate(['admin/home']);
  }

  error() {
    // console.log('heeya');
    this.toastr.error("Some Error Has Occurred While Adding Account");
    this.router.navigate(['admin/home']);
  }

  // message = "na"

  handleRegisterDoctor(form: NgForm) {

    const model = {...form.value, "availability": true}
    console.log(form.value);


    this.service.addNewDoctor(model).subscribe((data) => {

        // console.log('hi');
        this.success();
        form.reset();
      },
      (error) => {
        // console.log(error);
        this.error();
      }
    );

  }
  logout() {
    localStorage.clear();
    this.router.navigate(['/']);
  }

}





