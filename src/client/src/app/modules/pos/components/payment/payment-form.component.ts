import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { Account } from '../../models/account';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-payment-form',
  templateUrl: './payment-form.component.html',
  styleUrls: ['./payment-form.component.scss']
})
export class PaymentFormComponent implements OnInit {
  paymentForm: FormGroup;
  formTitle: string;
  constructor(@Inject(MAT_DIALOG_DATA) public data: Account, private dialogRef: MatDialog, private cartService: CartService, private toastr: ToastrService, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.paymentForm = this.fb.group({
      id: [this.data && this.data.id],
      name: [this.data && this.data.holderName, Validators.required],
      detail: [this.data && this.data.total, Validators.required]
    })
    this.formTitle = "Register Payment";
  }

  onSubmit() {
    if (this.paymentForm.valid) {
      this.cartService.addPayment(this.paymentForm.get('detail').value).subscribe(response => {
        this.toastr.success(`Payment for ${response.holderName} received, new total ${response.total}`);
        this.dialogRef.closeAll();
      });
      // if (this.paymentForm.get('id').value === "" || this.paymentForm.get('id').value == null) {
      //   this.brandService.createBrand(this.paymentForm.value).subscribe(response => {
      //     this.toastr.success(response.messages[0]);
      //     this.dialogRef.closeAll();
      //   })
      // } else {
      //   this.brandService.updateBrand(this.paymentForm.value).subscribe(response => {
      //     this.toastr.success(response.messages[0]);
      //     this.dialogRef.closeAll();
      //   })
      // }
    }
  }

}
