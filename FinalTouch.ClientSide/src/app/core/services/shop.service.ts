import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Product } from '../../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  types: string[] = [];
  brands: string[] = [];

  getProducts(types?: string[],brands?: string[]) {
    let params = new HttpParams();

    if (brands && brands.length > 0) {
      params = params.append('brand', brands.join(','));
    }
    if (types && types.length > 0) {
      params = params.append('type', types.join(','));
    }

    return this.http.get<Product[]>(this.baseUrl + 'Product', {params});//product[] need to be changed to Pagination<Product>
  }

  getBrands() {
    if (this.brands.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + 'Product/brand').subscribe({
      next: response => this.brands = response
    });
  }

  getTypes() {
    if (this.types.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + 'Product/type').subscribe({
      next: response => this.types = response
    });
  }

}
