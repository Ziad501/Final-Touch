import { Component, Input, OnInit, inject } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';
import { ShopService } from '../../../core/services/shop.service';
import { CartService } from '../../../core/services/cart.service';
import { Product } from '../../../shared/models/product';
import { ShopParams } from '../../../shared/models/shopparams';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatButtonModule, RouterModule,CurrencyPipe],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})
export class ProductListComponent implements OnInit {
  @Input() brand: string[] = [];


  shopService = inject(ShopService);
  cartService = inject(CartService);
  products: Product[] = [];

  ngOnInit(): void {
    const params: ShopParams = {
      brand: this.brand,
      type: [],
      sort: '',
      search: '',
      pageSize: 8,
      pageNumber: 1
    };

    this.shopService.getProducts(params)
      .subscribe(res => this.products = res.data);
  }

  addToCart(product: Product) {
    this.cartService.addItemToCart(product, 1);
  }
}
