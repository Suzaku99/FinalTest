import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private url: string = environment.url;
  constructor(private http: HttpClient) { }

  getPayments(model) {
    let params = new HttpParams();
    params = params.append('currency', model.currency);
    params = params.append('statusCode', model.statusCode);
    return this.http.get(this.url + 'payments/GetPayments', { params: params });
  }

  getCurrency() {
    return this.http.get(this.url + 'payments/GetCurrency');
  }

  uploadFile(model){
    const formData: FormData = new FormData();
    formData.append('file', model);
    return this.http.post(this.url + 'payments/uploadfile', formData);
  }
}
