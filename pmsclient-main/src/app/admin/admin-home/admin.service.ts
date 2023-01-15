import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ToastrService} from "ngx-toastr";

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient, private toaster: ToastrService) {
  }


  addNewDoctor(model: any) {
    return this.http.post('https://localhost:7296/api/Accounts/NewDoctor', model);
  }

}
