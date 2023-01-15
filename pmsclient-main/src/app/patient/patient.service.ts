import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {Observable} from "rxjs";
import {ConsultationDetails, Doctors, Patient} from "./Models/Ptientmodel";

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  constructor(private http: HttpClient) {

  }

  postBooking(data: any) {
    return this.http.post("https://localhost:7296/api/PatientService/BookAppointment", data);
  }


  getBookings(patientId: string) {

    return this.http.get<Array<object>>('https://localhost:7296/api/PatientService/GetPendingAppointments/' + patientId);

  }

  getCompletedConsultations(patientId: string) {
    return this.http.get<Array<object>>('https://localhost:7296/api/PatientService/GetConsultationDetails/' + patientId);

  }

  getLinkedAccounts(sid: string) {
    console.log(sid);
    return this.http.get<Array<object>>('https://localhost:7296/api/PatientService/GetLinkedAccounts/' + sid);
  }

  getLinkedAccountDetails() {

  }

  getDoctors() {
    return this.http.get<Array<Doctors>>('https://localhost:7296/api/Doctor');
  }

  getDoctorById(doctorId: string) {
    return this.http.get<Array<object>>('https://localhost:7296/api/Doctor/' + doctorId);
  }

  getPatientById(patientId: string) {
    return this.http.get<Array<object>>('https://localhost:7296/api/Patient/' + patientId);
  }


  getAllPatients() {
    return this.http.get<Array<Patient>>('https://localhost:7296/api/Patient');
  }

  getAllConsultationDetails() {
    return this.http.get<Array<ConsultationDetails>>('https://localhost:7296/api/Admin/GetAllAppointments');
  }

}
