import { Component, ElementRef, HostListener, inject } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { MatBadge } from '@angular/material/badge';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { BusyService } from '../../core/services/busy.service';
import { MatProgressBar } from '@angular/material/progress-bar';
import { CartService } from '../../core/services/cart.service';
import { AccountService } from '../../core/services/account.service';
import { MatMenu, MatMenuItem, MatMenuTrigger } from '@angular/material/menu';
import { MatDivider } from '@angular/material/divider';
import { IsAdminDirective } from '../../shared/directives/is-admin.directive';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    MatIcon,
    MatButton,
    MatBadge,
    RouterLink,
    RouterLinkActive,
    MatProgressBar,
    MatMenuTrigger,
    MatMenu,
    MatDivider,
    MatMenuItem,
    IsAdminDirective,
    CommonModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  busyService = inject(BusyService);
  cartService = inject(CartService);
  accountService = inject(AccountService);
  private router = inject(Router);

  // Inject ElementRef to access the component's native element
  constructor(private eRef: ElementRef) {}

  showMobileMenu = false;

  toggleMobileMenu() {
    this.showMobileMenu = !this.showMobileMenu;
  }

  logout() {
    this.accountService.logout().subscribe({
      next: () => {
        this.accountService.currentuser.set(null);
        this.router.navigateByUrl('/');
      }
    });
    if (this.cartService.cart() != null) {
      this.cartService.deleteCart();
    }
  }

  // Listen for clicks anywhere in the document
  @HostListener('document:click', ['$event'])
  onClickOutside(event: MouseEvent) {
    if (this.showMobileMenu && !this.eRef.nativeElement.contains(event.target)) {
      this.showMobileMenu = false;
    }
  }

}
