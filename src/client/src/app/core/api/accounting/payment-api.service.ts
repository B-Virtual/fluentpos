import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AccountApiModel } from '../../models/accounting/account';
import { Result } from '../../models/wrappers/Result';

@Injectable({
  providedIn: 'root'
})
export class PaymentApiService {

  baseUrl = environment.apiUrl + 'accounting/accounts/payments/';

  constructor(private http: HttpClient) {
  }
  create(customerId: string, amount: number) {
    return this.http.post<Result<AccountApiModel>>(this.baseUrl, { "customerId": customerId, "amount": amount });
  }
}
