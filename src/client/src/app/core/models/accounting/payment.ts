export class PaymentApiModel {
    id: string;
    amount: number;
    timestamp: Date;
    constructor(id: string, amount: number, timestamp: Date) {
        this.id = id;
        this.amount = amount;
        this.timestamp = timestamp;
    }
}
