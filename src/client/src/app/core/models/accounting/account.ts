import { PaymentApiModel } from "./payment";

export class AccountApiModel {
    id: string;
    holderName: string;
    total: number;
    lastPayment: Date;
    productName: string;
    payments: PaymentApiModel[];
}
