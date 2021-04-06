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
  public textError: string;
  constructor(private paymentService: PaymentService){
    this.filename = "Choose CSV, XML file";

    this.paymentModel.currency = "";
    this.paymentModel.statusCode = "";
    this.paymentModel.dateRange = "";
    this.paymentModel.start = "";
    this.paymentModel.end = "";
    this.getPayments();
    this.getCurrency();
  }

  getPayments() {
    this.paymentService.getPayments(this.paymentModel).subscribe((response: any) => {
        this.payments = response;
    }, (error: any) => {
      console.log(error);
      this.textError = error;
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
    if(!this.fileToUpload)
    {
      this.textError = "File is empty"
      return;
    }
    
      this.paymentService.uploadFile(this.fileToUpload).subscribe((response: any) => {
        this.textError = response;
        this.getPayments();
    }, (error: any) => {
      console.log(error);
      this.textError = error.error;
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

    if(this.paymentModel.dateRange[0] != undefined)
    {
      this.paymentModel.start = this.paymentModel.dateRange[0].toISOString().slice(0,10); 
      this.paymentModel.end = this.paymentModel.dateRange[1].toISOString().slice(0,10); 

      console.log(this.paymentModel.start);
    }

    this.getPayments();
  }

  onClear() {
    this.paymentModel.currency = "";
    this.paymentModel.statusCode = "";
    this.paymentModel.dateRange = "";
    this.paymentModel.start = "";
    this.paymentModel.end = "";
    this.textError = "";
    this.getPayments();
  }
}
