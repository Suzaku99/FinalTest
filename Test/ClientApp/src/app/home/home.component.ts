import { Component } from '@angular/core';
import { Payment } from '../_models/payments.interface';
import { PaymentService } from '../_services/payment.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public fileToUpload: File;
  public filename: string;
  public payments: Payment[];
  public currencyCode: string[];
  public paymentModel: any = {};
  constructor(private paymentService: PaymentService){
    this.filename = "Choose CSV, XML file";

    this.paymentModel.currency = "";
    this.paymentModel.statusCode = "";
    this.paymentModel.dateRange = "";
    this.getPayments();
    this.getCurrency();
  }

  getPayments() {
    this.paymentService.getPayments(this.paymentModel).subscribe((response: any) => {
        this.payments = response;
    }); 
  }

  getCurrency() {
    this.paymentService.getCurrency().subscribe((response: any) => {
        this.currencyCode = response;
    }); 
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    this.filename = this.fileToUpload.name;
  }

  uploadFile()
  {
      this.paymentService.uploadFile(this.fileToUpload).subscribe((response: any) => {
      console.log(response);
    });
  }

  onStatusChange() {
    this.getPayments();
  }

  onCurrencyChange() {
    console.log(this.paymentModel);
    this.getPayments();
  }

  onDateRangeChange() {
    console.log(this.paymentModel);
  }
}
