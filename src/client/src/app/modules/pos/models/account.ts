import { Payment } from "./payment";

export class Account {
  id: string;
  holderName: string;
  total: number;
  lastPayment: Date;
  payments: Payment[];

  constructor(id: string, holderName: string, total: number, lastPayment: Date) {
    this.id = id;
    this.holderName = holderName;
    this.total = total;
    this.lastPayment = lastPayment;
    this.payments = [];
  }

  addPayment(id: string, amount: number, timestamp: Date) {
    this.payments.push(new Payment(id, amount, timestamp));
  }
}
