import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Product } from '../../shared/models/product';
import { ShopParams } from '../../shared/models/shopparams';
import { Pagination } from '../../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  types: string[] = [];
  brands: string[] = [];

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.brand.length > 0) {
      params = params.append('brands', shopParams.brand.join(','));
    }
    if (shopParams.type.length > 0) {
      params = params.append('types', shopParams.type.join(','));
    }
    if (shopParams.sort) {
      params = params.append('sort', shopParams.sort);
    }
    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }
    params = params.append('pageSize', shopParams.pageSize);
    params = params.append('pageIndex', shopParams.pageNumber);

    return this.http.get<Pagination<Product>>(this.baseUrl + 'Product', {params});//product[] need to be changed to Pagination<Product>
  }

  getProduct(id: number) {
    return this.http.get<Product>(this.baseUrl + 'Product/' + id)

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

  addProduct(product: Product) {
    return this.http.post<Product>(this.baseUrl + 'Product', product);
  }

  updateProduct(id: number, product: Product) {
    return this.http.put<Product>(this.baseUrl + 'Product/' + id, product);
  }

  deleteProduct(id: number) {
    return this.http.delete(this.baseUrl + 'Product/' + id);
  }

}
