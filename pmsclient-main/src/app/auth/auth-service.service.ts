import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Jwt} from "../patient/Models/Ptientmodel";
import {NgForm} from "@angular/forms";

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

  constructor(private http: HttpClient) {
  }

  login(data: any) {
    return this.http.post("https://localhost:7296/api/Accounts/Login", data);
  }

  rgisterNewUser(data: any) {
    return this.http.post('https://localhost:7296/api/Accounts/NewPatient', data)
  }

  getUserinfo() {
    let a = JSON.parse(localStorage.getItem("data") || "");
    a = a.substring(1, a.length - 1).split(',');
    let ob: Jwt = new Jwt();
    let x: Array<string> = [];
    for (const aElement of a) {
      x.push(aElement.toString().split(':')[1]);
    }
    console.log(x);
    ob.iss = x[0];
    ob.sid = x[1].substring(1, x[1].length - 1);
    ob.email = x[2];
    ob.UUID = x[5].substring(1, x[5].length - 1);
    return ob;
  }

}
