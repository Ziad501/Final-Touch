import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { ProductItemComponent } from "./product-item/product-item.component";
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-shop',
  imports: [ProductItemComponent,MatButton,MatIcon],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  private dialogService = inject(MatDialog);
  private shopService = inject(ShopService);

  products: Product[] = [];
  selectedBrands: string[]=[];
  selectedTypes: string[]=[];

  title = 'FinalTouch';
  ngOnInit(): void {
    this.intializeShop();
  }

  intializeShop() {
    this.shopService.getTypes();
    this.shopService.getBrands();
    this.shopService.getProducts().subscribe({
      next: Response => this.products = Response,
      error: error => console.error(error),
    });
  }

  openFilterDialog() {
    const dialigRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands : this.selectedBrands,
        selectedTypes : this.selectedTypes
      }
    });
    dialigRef.afterClosed().subscribe({
      next: result => {
        if (result) {
          console.log(result)
          this.selectedBrands = result.selectedBrands
          this.selectedTypes = result.selectedTypes
          this.shopService.getProducts(this.selectedTypes,this.selectedBrands).subscribe({
            next: response => this.products = response,
            error: error => console.error(error),
          })
        }
      }
    })
  }
}
