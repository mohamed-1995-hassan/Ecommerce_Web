import {  NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
  {path:'', component:HomeComponent, data:{breadcrumb:'Home'}},
  {path:'test-error', component:TestErrorComponent, data:{breadcrumb:'Test Errors'}},
  {path:'not-found', component:NotFoundComponent, data:{breadcrumb:'Not Found'}},
  {path:'server-error', component:ServerErrorComponent, data:{breadcrumb:'Server Error'}},
  {path:'shop', loadChildren: ()=> import('./shop/shop.module').then(m => m.ShopModule), data:{breadcrumb:'Shop'}},
  {path:'cart', loadChildren: ()=> import('./cart/cart.module').then(m => m.CartModule), data:{breadcrumb:'cart'}},
  {
    path:'checkout',
    canActivate:[AuthGuard],
    loadChildren: ()=> import('./checkout/checkout.module').then(m => m.CheckoutModule),
    data:{breadcrumb:'checkout'}},
  {path:'account', loadChildren: ()=> import('./account/account.module').then(m => m.AccountModule), data:{breadcrumb:'account'}},
  {path:'**', redirectTo:'', pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
