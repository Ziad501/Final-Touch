import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';
import { TestErrorComponent } from './features/test-error/test-error.component';
import { NotFoundComponent } from './shared/component/not-found/not-found.component';
import { ServerErrorComponent } from './shared/component/server-error/server-error.component';
import { CartComponent } from './features/cart/cart.component';
import { CheckoutComponent } from './features/checkout/checkout.component';
import { LoginComponent } from './features/account/login/login.component';
import { RegisterComponent } from './features/account/register/register.component';
import { authGuard } from './core/guards/auth.guard';
import { CheckoutSuccessComponent } from './features/checkout/checkout-success/checkout-success.component';
import { OrderComponent } from './features/orders/order/order.component';
import { OrderDetailedComponent } from './features/orders/order-detailed/order-detailed.component';
import { orderCompleteGuard } from './core/guards/order-complete.guard';
import { AdminComponent } from './features/admin/admin.component';
import { adminGuard } from './core/guards/admin.guard';
import { ProductAddComponent } from './features/admin/product-add/product-add.component';
import { ProductDeleteComponent } from './features/admin/product-delete/product-delete.component';
import { ProductEditComponent } from './features/admin/product-edit/product-edit.component';
import { CustmizeComponent } from './features/custmize/custmize.component';

export const routes: Routes = [
  {path:'', component: HomeComponent },
  {path:'shop',component:ShopComponent},
  {path:'shop/:id', component: ProductDetailsComponent },
  { path: 'Cart', component: CartComponent },
  { path: 'custmize', component: CustmizeComponent },
  {
    path: 'products/add',
    component: ProductAddComponent,canActivate: [authGuard]
  },
  {
    path: 'products/edit/:id',
    component: ProductEditComponent,canActivate: [authGuard]
  },
  {
    path: 'products/delete/:id',
    component: ProductDeleteComponent,canActivate: [authGuard]
  },
  {path:'checkout', component: CheckoutComponent, canActivate: [authGuard] },
  {path:'checkout/success', component: CheckoutSuccessComponent,
    canActivate: [authGuard,orderCompleteGuard] },
  {path:'orders', component: OrderComponent,canActivate: [authGuard] },
  {path:'orders/:id', component: OrderDetailedComponent,canActivate: [authGuard] },
  {path:'account/login', component: LoginComponent },
  {path:'account/register', component: RegisterComponent },
  {path:'test-error', component: TestErrorComponent },
  {path:'not-found',component:NotFoundComponent},
  {path:'server-error',component:ServerErrorComponent},
  {path:'admin',component:AdminComponent,canActivate: [authGuard, adminGuard] },
  {path:'**',redirectTo:'not-found',pathMatch:'full'},
];
