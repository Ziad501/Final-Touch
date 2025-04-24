import { Component, inject , OnInit } from '@angular/core';
import { OrderService } from '../../../core/services/order.service';
import { Order } from '../../../shared/models/order';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { PaymentCardPipe } from '../../../shared/pipes/payment-card.pipe';
import { AddressPipe } from '../../../shared/pipes/address.pipe';
import { AccountService } from '../../../core/services/account.service';
import { AdminService } from '../../../core/services/admin.service';

@Component({
  selector: 'app-order-detailed',
  imports: [MatCardModule,MatButton,DatePipe,CurrencyPipe,PaymentCardPipe,AddressPipe,RouterLink],
  templateUrl: './order-detailed.component.html',
  styleUrl: './order-detailed.component.scss'
})
export class OrderDetailedComponent {
private orderService = inject(OrderService);
private activatedRoute = inject(ActivatedRoute);
private accountService = inject(AccountService);
private adminService = inject(AdminService);
private router = inject(Router);
order?:Order;
buttonText = this.accountService.isAdmin() ? 'Return to admin' : 'Return to shop';

ngOnInit(): void {
  this.loadOrder();
}
onReturnClick() {
  this.accountService.isAdmin()
      ? this.router.navigateByUrl('/admin')
      : this.router.navigateByUrl('/orders');
}
loadOrder() {
  const id = this.activatedRoute.snapshot.paramMap.get('id');
  if (!id) return;
  
  const loadOrderData = this.accountService.isAdmin() ? this.adminService.getOrder(+id) : 
  this.orderService.getOrderDetailed(+id);
  this.orderService.getOrderDetailed(+id).subscribe({
    next: order => this.order = order
      })
}
}
