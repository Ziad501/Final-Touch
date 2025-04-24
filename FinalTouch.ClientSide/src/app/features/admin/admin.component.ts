import { Component, inject, OnInit } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { AdminService } from '../../core/services/admin.service';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatLabel, MatSelectChange, MatSelectModule } from '@angular/material/select';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatTabsModule } from '@angular/material/tabs';
import { RouterLink } from '@angular/router';
import { OrderParams } from '../../shared/models/OrderParams';
import { DialogService } from '../../core/services/dialog.service';
import { Order } from '../../shared/models/order';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatIcon,
    MatSelectModule,
    DatePipe,
    CurrencyPipe,
    MatLabel,
    MatTooltipModule,
    MatTabsModule,
    RouterLink,
    CommonModule
  ],
  templateUrl: './admin.component.html',
  styleUrl: './admin.component.scss'
})
export class AdminComponent implements OnInit {
  displayedColumns: string[] = ['id', 'buyerEmail', 'orderDate', 'total', 'status', 'action'];
  dataSource = new MatTableDataSource<Order>([]);
  private adminService = inject(AdminService);
  private dialogService = inject(DialogService);
  private shopService = inject(ShopService);

  products: Product[] = [];
  orderParams = new OrderParams();
  totalItems = 0;
  statusOptions = ['All', 'PaymentReceived', 'PaymentMismatch', 'Refunded', 'Pending'];

  ngOnInit(): void {
    this.loadOrders();
    this.loadProducts();
  }

  loadOrders(): void {
    this.adminService.getOrders(this.orderParams).subscribe({
      next: (response) => {
        if (response.data) {
          this.dataSource.data = response.data;
          this.totalItems = response.count;
        }
      },
      error: (err) => console.error('Failed to load orders', err)
    });
  }

  loadProducts(): void {
    this.shopService.getProducts({
      brand: [],
      type: [],
      pageNumber: 1,
      pageSize: 1000, // large enough to get all
      sort: '',
      search: ''
    }).subscribe({
      next: (res) => this.products = res.data,
      error: (err) => console.error('Failed to load products', err)
    });
  }

  onPageChange(event: PageEvent): void {
    this.orderParams.pageNumber = event.pageIndex + 1;
    this.orderParams.pageSize = event.pageSize;
    this.loadOrders();
  }

  onFilterSelect(event: MatSelectChange): void {
    this.orderParams.filter = event.value;
    this.orderParams.pageNumber = 1;
    this.loadOrders();
  }

  async openConfirmDialog(id: number): Promise<void> {
    const confirmed = await this.dialogService.confirm(
      "Confirm Refund",
      "Are you sure you want to refund this order?"
    );

    if (confirmed) {
      this.refundOrder(id);
    }
  }

  refundOrder(id: number): void {
    this.adminService.refundOrder(id).subscribe({
      next: (order) => {
        this.dataSource.data = this.dataSource.data.map(o => o.id === id ? order : o);
      },
      error: (err) => console.error('Refund failed', err)
    });
  }
}
