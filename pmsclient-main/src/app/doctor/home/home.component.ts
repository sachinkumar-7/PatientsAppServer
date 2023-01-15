import {Component, OnInit} from '@angular/core';
import {DoctorService} from "./doctor.service";
import {ConsultationDetails, Patient} from "../../patient/Models/Ptientmodel";
import {PatientService} from "../../patient/patient.service";
import {NgForm} from '@angular/forms';
import {AuthServiceService} from "../../auth/auth-service.service";
import {Router} from "@angular/router";


@Component({
  selector: 'doctor-admin-home',
  templateUrl: './home.component.html',
  // styleUrls: ['./admin-admin-home.component.css']
})
export class HomeComponent implements OnInit {

  page: number = 1;
  items = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
  pendingAppointments: Array<ConsultationDetails> = [];
  parkedPatients: Array<ConsultationDetails> = [];
  completedConsultations: Array<ConsultationDetails> = [];
  patientConsultationDetails: Array<ConsultationDetails> = [];
  isClickedConsultButton: boolean = false;
  patient: Patient = new Patient();
  patientage!: number;
  elements: Array<number> = [1];
  appointmentId!: string;


// -----------------------------------------------------------------------------------------------------------------------------------------------

  constructor(private doctorService: DoctorService, private patientService: PatientService, private auth: AuthServiceService, private route: Router) {
  }

// -----------------------------------------------------------------------------------------------------------------------------------------------


  ngOnInit() {

    this.parkedPatients = [];
    this.getCompletedBookings();
    this.getPendingBookings();
    this.getParkedPatients();

  }

  // -----------------------------------------------------------------------------------------------------------------------------------------------


  private getParkedPatients() {
    this.doctorService.getParkedPatientsOfDoctor().subscribe(data => {
      for (const datum of data) {
        this.parkedPatients.push(JSON.parse(JSON.stringify(datum)));
      }
      console.log(this.parkedPatients);
    });
  }

// -----------------------------------------------------------------------------------------------------------------------------------------------


  getPendingBookings() {
    this.pendingAppointments = [];
    this.doctorService.getPendingBookingsOfDoctor(this.auth.getUserinfo().UUID).subscribe(data => {
      for (let datum of data) {
        let ob: ConsultationDetails = JSON.parse(JSON.stringify(datum));
        this.pendingAppointments.push(ob)
      }
      console.log(this.pendingAppointments);
    });
  }


// -----------------------------------------------------------------------------------------------------------------------------------------------


  getCompletedBookings() {
    this.completedConsultations = [];

    this.doctorService.getCompletedConsultationsOfDoctor(this.auth.getUserinfo().UUID).subscribe(data => {
      for (const datum of data) {
        this.completedConsultations.push(JSON.parse(JSON.stringify(datum)));
      }
      console.log(this.completedConsultations);
    });
  }

// -----------------------------------------------------------------------------------------------------------------------------------------------


  handleConsultation(appointmentId: string, doctorId: string, patientId: string) {

    this.appointmentId = appointmentId;
    this.isClickedConsultButton = true;
    this.patientConsultationDetails = [];
    this.patientService.getCompletedConsultations(patientId).subscribe(data => {
      for (const datum of data) {
        this.patientConsultationDetails.push(JSON.parse(JSON.stringify(datum)))
      }
      console.log(this.patientConsultationDetails);
    });

    this.patientService.getPatientById(patientId).subscribe(data => {
      this.patient = JSON.parse(JSON.stringify(data));
      console.log(this.patient);
    });

  }

// -----------------------------------------------------------------------------------------------------------------------------------------------

  handleInvoiceSubmit(form: NgForm) {

    let medicines: Array<string> = [];
    let frequency: Array<string> = [];
    let number: Array<string> = [];
    for (const i in form.value) {
      if (i.startsWith('m')) {
        medicines.push(form.value[i]);
      } else if (i.startsWith('f')) {
        frequency.push(form.value[i])
      } else if (i.startsWith('n')) {
        number.push(form.value[i]);
      }
    }

    let medication: string = "";
    for (let i = 0; i < medicines.length; i++) {
      medication += `${medicines[i]}:${number[i]}:${frequency[i]};`
    }

    let ob = {
      "AppointmentId": this.appointmentId,
      "Diagnosis": form.value['email'],
      "Medications": medication,
      "Radiology": "",
      "LabTest": " ",
      "Remarks": form.value['remarks'],
      "Status": "ok",
      "Session": "ok",
      "ConsultationMode": "ok"
    };

    console.log(ob);
    this.doctorService.putDiagnostics(ob).subscribe(data => {
      console.log(data);
      this.getPendingBookings();
      this.getCompletedBookings();
      this.isClickedConsultButton = false;

    });
  }


  // -----------------------------------------------------------------------------------------------------------------------------------------------

  getDate(dob: string) {
    this.patientage = new Date().getFullYear() - parseInt(dob.split('T')[0].split('-')[0]);
    console.log(this.patientage);
  }

  add() {
    this.elements.push(1);
  }

  remove() {
    this.elements.pop();
  }


  logout() {
    localStorage.clear();
    this.route.navigate(['/']);
  }

  // -----------------------------------------------------------------------------------------------------------------------------------------------


}
