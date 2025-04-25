import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { ProductItemComponent } from "./product-item/product-item.component";
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { ShopParams } from '../../shared/models/shopparams';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Pagination } from '../../shared/models/pagination';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [
    ProductItemComponent,
    MatButton,
    MatIcon,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger,
    MatPaginator,
    FormsModule,
    CommonModule
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  private dialogService = inject(MatDialog);
  private shopService = inject(ShopService);
  private breakpointObserver = inject(BreakpointObserver);
  private route = inject(ActivatedRoute);

  products?: Pagination<Product>;
  shopParams = new ShopParams();
  pageSizeOptions = [5, 10, 20, 50, 100];

  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'price' },
    { name: 'Price: High to Low', value: 'priceDesc' }
  ];

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      if (params['types']) {
        this.shopParams.type = [params['types']];
      }

      if (params['brands']) {
        this.shopParams.brand = [params['brands']];
      }

      if (params['search']) {
        this.shopParams.search = params['search'];
      }

      this.initializeShop();
    });
  }

  initializeShop() {
    this.shopService.getTypes();
    this.shopService.getBrands();
    this.getProducts();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => this.products = response,
      error: error => console.error(error)
    });
  }

  onSearchChange() {
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  handlePageEvent(event: PageEvent) {
    this.shopParams.pageNumber = event.pageIndex + 1;
    this.shopParams.pageSize = event.pageSize;
    this.getProducts();
  }

  onSortChange(event: MatSelectionListChange) {
    const selectedOption = event.options[0];
    if (selectedOption) {
      this.shopParams.sort = selectedOption.value;
      this.shopParams.pageNumber = 1;
      this.getProducts();
    }
  }

  openFilterDialog() {
    this.breakpointObserver.observe([Breakpoints.Handset, Breakpoints.Tablet]).subscribe(result => {
      const isMobileOrTablet = result.matches;

      const dialogRef = this.dialogService.open(FiltersDialogComponent, {
        width: isMobileOrTablet ? '100%' : '600px',
        maxWidth: isMobileOrTablet ? '100vw' : '80vw',
        height: isMobileOrTablet ? '100%' : 'auto',
        maxHeight: isMobileOrTablet ? '100vh' : '90vh',
        panelClass: isMobileOrTablet ? 'mobile-filter-dialog' : 'desktop-filter-dialog',
        data: {
          selectedBrands: this.shopParams.brand,
          selectedTypes: this.shopParams.type,
        }
      });

      dialogRef.afterClosed().subscribe({
        next: result => {
          if (result) {
            this.shopParams.brand = result.selectedBrands;
            this.shopParams.type = result.selectedTypes;
            this.shopParams.pageNumber = 1;
            this.getProducts();
          }
        }
      });
    });
  }

  clearSearch() {
    this.shopParams.search = '';
    this.getProducts();
  }
}
