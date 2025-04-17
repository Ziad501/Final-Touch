import { Component, inject } from '@angular/core';
import { CartService } from '../../core/services/cart.service';
import { CartItemComponent } from "./cart-item/cart-item.component";
import { OrderSummaryComponent } from "../../shared/component/order-summary/order-summary.component";
import { EmptyStateComponent } from '../../shared/component/empty-state/empty-state.component';
import { RouterLink } from '@angular/router';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-cart',
  imports: [
    CartItemComponent,
    OrderSummaryComponent,
    EmptyStateComponent,
    RouterLink,
    MatButton
  ],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent {
  cartService = inject(CartService);
}
