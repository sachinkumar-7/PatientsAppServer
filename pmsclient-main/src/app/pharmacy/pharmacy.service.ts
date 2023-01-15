import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ConsultationDetails} from "../patient/Models/Ptientmodel";

@Injectable({
  providedIn: 'root'
})
export class PharmacyService {

  constructor(private http: HttpClient) {
  }

  getPaymentPendingAppointments() {
    return this.http.get<Array<ConsultationDetails>>('https://localhost:7296/api/Pharmacy/GetPaymentPendingAppointments');
  }


  getPrescriptions(appointmentId: string) {
    return this.http.get<any>('https://localhost:7296/api/Pharmacy/GetPaymentPendingAppointments/' + appointmentId);
  }

  completeConsultation(object: any) {
    return this.http.put<any>('https://localhost:7296/api/Pharmacy/CompletePatient', object);
  }

}
