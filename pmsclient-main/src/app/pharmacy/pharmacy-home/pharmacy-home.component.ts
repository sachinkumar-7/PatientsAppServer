import {Component, OnInit} from '@angular/core';
import {PharmacyService} from "../pharmacy.service";
import {ConsultationDetails} from "../../patient/Models/Ptientmodel";

@Component({
  selector: 'app-pharmacy-admin-home',
  templateUrl: './pharmacy-home.component.html',
})
export class PharmacyHomeComponent implements OnInit {


  paymentPendingAppointments!: Array<ConsultationDetails>;
  medicines: string = "";
  newMedicines: string = "";
  total: number = 0;
  currentUser: ConsultationDetails = new ConsultationDetails();
  hider: boolean = true;
  billhider: number = 0;

  getPrescriptions(consult: ConsultationDetails) {

    this.billhider = 1;
    this.currentUser = consult;
    this.medicines = consult.medications;
    console.log(consult.medications);

  }

  constructor(private pharmacy: PharmacyService) {
  }

  getPaymentPendingAppointments() {

    this.paymentPendingAppointments = [];
    this.pharmacy.getPaymentPendingAppointments().subscribe(data => {
      this.paymentPendingAppointments = data;
      console.log(data);
    })

  }

  ngOnInit() {

    this.getPaymentPendingAppointments();
  }


  billing(form: any) {

    this.hider = false;
    let med = this.medicines.split(';').slice(0, this.medicines.split(';').length - 1);
    console.log(med);
    this.total = 0;
    this.newMedicines = "";
    for (let i = 0; i < med.length; i++) {
      med[i] = med[i] + ":" + form.value['mb' + i] + ';';
      this.total += parseInt(form.value['mb' + i]);
      this.newMedicines += med[i]
    }
    this.newMedicines = this.newMedicines.trim();
    console.log(this.newMedicines);


  }

  completeConsultation() {

    let ob = {
      "AppointmentId": this.currentUser.appointmentId,
      "Status": " ",
      "Session": this.currentUser.session,
      "ConsultationMode": this.currentUser.consultationMode,
      "Medications": this.newMedicines
    };
    console.log(ob);
    this.pharmacy.completeConsultation(ob).subscribe(data => {
      console.log(data);
      this.getPaymentPendingAppointments();
      this.hider = true;
      this.billhider = 0;
    });


  }


}
