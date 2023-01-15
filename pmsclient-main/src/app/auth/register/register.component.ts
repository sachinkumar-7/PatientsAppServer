import {Component} from '@angular/core';
import {NgForm} from "@angular/forms";
import {AuthServiceService} from "../auth-service.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  getToday() {
    return new Date().toISOString().split('T')[0]
    }

  constructor(private auth: AuthServiceService) {

  }


  handleRegister(form: NgForm) {

    const data = {...form.value, Relation: "self"}
    console.log(data);
    this.auth.rgisterNewUser(data).subscribe(data => {
      console.log(data);
    });

  }
}


