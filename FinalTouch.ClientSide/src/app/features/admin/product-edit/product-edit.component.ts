import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../../shared/models/product';
import { ActivatedRoute, Router } from '@angular/router';
import { ShopService } from '../../../core/services/shop.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-edit',
  imports: [FormsModule],
  templateUrl: './product-edit.component.html',
  styleUrl: './product-edit.component.scss'
})
export class ProductEditComponent implements OnInit {

  private route = inject(ActivatedRoute);
  private shopService = inject(ShopService);
  private router = inject(Router);


  product!: Product;
  brands: string[] = [];
  types: string[] = [];
  fileError: string = '';

  ngOnInit() {
    const id = +this.route.snapshot.paramMap.get('id')!;
    this.shopService.getProduct(id).subscribe(product => this.product = product);

  }

  onSubmit() {
    this.shopService.updateProduct(this.product.id, this.product).subscribe(() => {
      this.router.navigate(['/shop']);
    });
  }


  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      const allowedTypes = ['image/jpeg', 'image/png', 'image/jpg', 'image/webp','image/jfif'];

      if (!allowedTypes.includes(file.type)) {
        this.fileError = 'Only image files are allowed.';
        return;
      }

      this.fileError = '';
      this.product.imageUrl = `/images/productss/${file.name}`;
    }
  }

}
