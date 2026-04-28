import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface TransactionRecord {
  id: string;
  fromAccount: string;
  toAccount: string;
  amount: number;
  currency: string;
  timestamp: string;
  alertTriggered: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private apiUrl = 'http://localhost:5222/api';

  constructor(private http: HttpClient) {}

  getTransactions(): Observable<TransactionRecord[]> {
    return this.http.get<TransactionRecord[]>(`${this.apiUrl}/transactionrecords`);
  }

  postTransaction(transaction: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/transactions`, transaction);
  }
}