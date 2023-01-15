import {Component, OnInit} from '@angular/core';
import {NgForm} from '@angular/forms';
import {ActivatedRoute} from "@angular/router";
import {ConsultationDetails, Doctors, Patient} from "../Models/Ptientmodel";
import {PatientService} from "../patient.service";
import {Location} from "@angular/common";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'patient-book-appointment',
  templateUrl: './book-ppointment.component.html',
  styleUrls: ['./book-ppointment.component.css']
})
export class BookappointmentComponent implements OnInit {
  getToday() {
    return new Date().toISOString().split('T')[0]
    }
[x: string]: any;

  constructor(private route: ActivatedRoute, private patientService: PatientService, private location: Location, private tost: ToastrService) {

  }

  doctor!:Doctors;
  patient!:Patient;

  page = 1;
  items = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
  PatientId! : string;
  DoctorID!:string;
  doctors : Array<Doctors> = []
  public appointmentFlag: number=0;

  ngOnInit () {
    this.route.queryParams.subscribe ( ( params => {
      console.log ( params );
      this.PatientId = params[ 'patientId' ]
    } ) )

    this.patientService.getDoctors ().subscribe ( data => {
      this.doctors=[];
      for ( let datum of data ) {
        let ob : Doctors = JSON.parse ( JSON.stringify ( datum ) );
        this.doctors.push ( ob );
      }



    } )
  }

  bookappointment(doctorId: string, patientId: string) {

    this.appointmentFlag=1;
    this.DoctorID=doctorId;
    this.patientService.getPatientById(this.PatientId).subscribe(data=>{
      this.patient =  JSON.parse(JSON.stringify(data));
      console.log(this.patient);
    })
    this.patientService.getDoctorById(doctorId).subscribe(data=>{
      this.doctor =  JSON.parse(JSON.stringify(data));
      console.log(this.doctor)
    })
  }


  handlePayment(form: NgForm) {
    const data = {...form.value, patientId: this.PatientId, doctorId: this.DoctorID, ConsultationFee: 210, Status: ""}
    this.patientService.postBooking(data).subscribe(data => {
      console.log(data);
      this.appointmentFlag = 0;
      this.tost.success("Booking Success");
    });

  }

  filteText: string = ""

  filter() {
    this.filteText = (document.getElementById("filterText") as HTMLInputElement).value;

  }

  goHome() {
    this.location.back();
  }

  goBack() {
    this.appointmentFlag = 0;
  }
}
