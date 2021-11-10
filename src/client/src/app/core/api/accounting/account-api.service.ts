import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Payment } from 'src/app/modules/pos/models/payment';
import { environment } from 'src/environments/environment';
import { AccountApiModel } from '../../models/accounting/account';
import { Result } from '../../models/wrappers/Result';

@Injectable({
  providedIn: 'root'
})
export class AccountApiService {

  baseUrl = environment.apiUrl + 'accounting/accounts/';

  constructor(private http: HttpClient) {
  }
  get(customerId: string) {
    return this.http.get<Result<AccountApiModel>>(this.baseUrl + customerId);
  }
}
