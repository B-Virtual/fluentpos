import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { map, startWith } from 'rxjs/operators';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { BusyService } from 'src/app/core/services/busy.service';
import { Brand } from '../../models/brand';
import { BrandParams } from '../../models/brandParams';
import { Product } from '../../models/product';
import { ProductParams } from '../../models/productParams';
import { CartService } from '../../services/cart.service';
import { PosService } from '../../services/pos.service';

@Component({
  selector: 'app-catalog',
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.scss']
})
export class CatalogComponent implements OnInit {
  brands: PaginatedResult<Brand>;
  brandParams = new BrandParams();
  products: PaginatedResult<Product>;
  productParams = new ProductParams();
  searchString: string;
  brandAutoComplete = new FormControl();
  showImage: boolean = false;
  hasProductsLoaded: boolean = false;
  invalidCart: boolean = true;
  constructor(private posService: PosService, private toastr: ToastrService, public cartService: CartService, public busyService: BusyService) { }

  ngOnInit(): void {
    this.productParams.brandIds = [];
    this.productParams.categoryIds = [];
    this.productParams.pageSize = 16;
    this.brandParams.pageSize = 5;
    this.getProducts();
    this.getBrands();
    this.brandAutoComplete.valueChanges.subscribe((value) => this._filterBrand(value));
  }
  getProducts(brandId?: string) {
    console.log(brandId);
    this.productParams.brandIds = [];
    if (brandId != null) this.productParams.brandIds.push(brandId);
    console.log(this.productParams);
    this.hasProductsLoaded = false;
    this.posService.getProducts(this.productParams).subscribe((res) => { this.products = res, this.hasProductsLoaded = true });
  }
  getBrands() {
    this.posService.getBrands(this.brandParams).subscribe((res) => { this.brands = res; });
  }

  private _filterBrand(value: string) {
    const filterValue = value.toLowerCase();
    this.brandParams.searchString = filterValue;
    this.getBrands();
  }

  public doFilter(): void {
    this.productParams.searchString = this.searchString.trim().toLocaleLowerCase();
    this.getProducts();
  }
  getBrandName(brandId: string) {
    if (this.brands && this.brands.data && this.brands.data.find(book => book.id === brandId)) {
      return this.brands.data.find(brand => brand.id === brandId).name;
    }
  }
  isCustomerSelected() {
    const currentCustomer = this.cartService.getCurrentCustomer();
    if (!currentCustomer) {
      this.toastr.error('Select a customer');
      return false;
    }
    return true;
  }
  addToCart(product: Product) {
    if (this.isCustomerSelected()) {
      this.cartService.add(product);
    }

  }
  toggleImage() {
    this.showImage = !this.showImage;
  }

}
