import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { CartApiService } from 'src/app/core/api/cart/cart-api.service';
import { CartItemsApiService } from 'src/app/core/api/cart/cart-items-api.service';
import { CustomerApiService } from 'src/app/core/api/people/customer-api.service';
import { CartItemApiModel } from 'src/app/core/models/cart/cart-item';
import { Cart } from '../models/cart';
import { Customer } from '../models/customer';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItems$ = new Subject<Cart[]>();
  private cartItems: Cart[] = [];
  private currentCustomer: Customer;
  constructor(private cartApi: CartApiService, private cartItemApi: CartItemsApiService, private cartItemsApi: CartItemsApiService) { }
  add(product: Product, quantity: number = 1) {
    var foundItem = this.cartItems.find(a => a.productId == product.id);
    if (foundItem) {
      foundItem.quantity = foundItem.quantity + quantity;
    }
    else {
      this.cartItems.push(new Cart(product.id, quantity ?? 1, product.name, product.detail, product.price));
    }
    this.cartItems$.next(this.calculate(this.cartItems));
    this.cartApi.get(this.currentCustomer.id).subscribe((result) => {
      if (result) {
        if (result.data.length > 1) {
          //take first only - temporarily
          //todo : add cart selection dialog later
          const cartId = result.data[0].id;
          this.cartItemApi.create(new CartItemApiModel(cartId, product.id, quantity)).subscribe();
        }
        else {
          //create cart
        }
      }
    });

  }
  increase(productId: string, quantity: number = 1) {
    var foundItem = this.cartItems.find(a => a.productId == productId);
    if (foundItem) {
      foundItem.quantity = foundItem.quantity + quantity;
    }
    this.cartItems$.next(this.calculate(this.cartItems));
  }
  reduce(productId: string, quantity: number = 1) {
    var foundItem = this.cartItems.find(a => a.productId == productId);
    if (foundItem) {
      if (foundItem.quantity > 1) {
        foundItem.quantity = foundItem.quantity - quantity;
      }
      else {
        this.cartItems.splice(this.cartItems.indexOf(foundItem), 1)
      }
    }
    this.cartItems$.next(this.calculate(this.cartItems));
  }
  remove(productId: string) {
    var foundItem = this.cartItems.find(a => a.productId == productId);
    if (foundItem) {
      this.cartItems.splice(this.cartItems.indexOf(foundItem), 1)
    }
    this.cartItems$.next(this.cartItems);
  }
  get(): Observable<Cart[]> {
    return this.cartItems$.asObservable();
  }
  loadCurrentCart(): Cart[] {
    return this.calculate(this.cartItems);
  }
  setCurrentCustomer(customer: Customer) {
    this.currentCustomer = customer;
  }
  getCurrentCustomer() {
    return this.currentCustomer;
  }
  private calculate(cartItems: Cart[]): Cart[] {
    cartItems.forEach(function (part, index, theArray) {
      theArray[index].total = cartItems[index].quantity * cartItems[index].rate;
    });
    return cartItems;
  }
  getCustomerCart(customerId: string) {
    this.cartItems = [];
    this.cartApi.get(customerId).subscribe((result) => {
      if (result) {
        if (result.data.length > 1) {
          //take first only - temporarily
          //todo : add cart selection dialog later
          const cartId = result.data[0].id;
          this.cartItemApi.get(cartId).subscribe((data) => {
            if (data) {
              data.data.forEach(element => {
                this.cartItems.push(new Cart(element.cartId, element.quantity, element.productName, element.productDescription, element.rate));

              });

            }
          });
        }
        else {
          //create cart
        }
      }
      this.cartItems$.next(this.calculate(this.cartItems));
    });
  }
}
