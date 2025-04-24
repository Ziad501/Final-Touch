import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../../shared/models/product';
import { ActivatedRoute, Router } from '@angular/router';
import { ShopService } from '../../../core/services/shop.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-delete',
  imports: [CommonModule],
  templateUrl: './product-delete.component.html',
  styleUrl: './product-delete.component.scss'
})
export class ProductDeleteComponent implements OnInit {

  route = inject(ActivatedRoute);
  shopService = inject(ShopService);
  router = inject(Router);

  product!: Product;

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.shopService.getProduct(id).subscribe(product => this.product = product);
  }

  onDelete() {
    this.shopService.deleteProduct(this.product.id).subscribe(() => {
      this.router.navigate(['/shop']);
    });
  }

}
