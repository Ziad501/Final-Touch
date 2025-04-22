import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../../shared/models/product';
import { ShopService } from '../../../core/services/shop.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-add',
  imports: [FormsModule,CommonModule],
  templateUrl: './product-add.component.html',
  styleUrl: './product-add.component.scss'
})
export class ProductAddComponent   {

  shopService = inject(ShopService);
  router = inject(Router);
  product: Product = {
    id: 0, name: '', brand: '', type: '', price: 1,
    description: '',
    imageUrl: '',
    quantityInStock: 1,
    length: 0,
    width: 0,
    area: 0
  };
  brands: string[] = [];
  types: string[] = [];
  fileError: string = '';


  onSubmit() {
    this.shopService.addProduct(this.product).subscribe(() => {
      this.router.navigate(['/shop']);
    });
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      const allowedTypes = ['image/jpeg', 'image/png', 'image/jpg', 'image/webp'];

      if (!allowedTypes.includes(file.type)) {
        this.fileError = 'Only image files are allowed.';
        return;
      }

      this.fileError = '';
      this.product.imageUrl = `/images/products/${file.name}`;
    }
  }



}
