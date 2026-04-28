import { Component, OnInit } from '@angular/core';
import { TransactionService, TransactionRecord } from './services/transaction.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  transactions: TransactionRecord[] = [];
  loading = false;
  submitting = false;

  form = {
    fromAccount: '',
    toAccount: '',
    amount: 0,
    currency: 'INR'
  };

  constructor(private transactionService: TransactionService) {}

  ngOnInit() {
    this.loadTransactions();
  }

  loadTransactions() {
    this.loading = true;
    this.transactionService.getTransactions().subscribe({
      next: (data) => {
        this.transactions = data;
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      }
    });
  }

  submitTransaction() {
    this.submitting = true;
    this.transactionService.postTransaction(this.form).subscribe({
      next: () => {
        this.submitting = false;
        this.form = { fromAccount: '', toAccount: '', amount: 0, currency: 'INR' };
        setTimeout(() => this.loadTransactions(), 2000);
      },
      error: (err) => {
        console.error(err);
        this.submitting = false;
      }
    });
  }
}