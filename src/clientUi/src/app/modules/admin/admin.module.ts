import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
//sub - module
import { AdminRoutingModule } from './admin-routing.module';
import { ShareModule }  from '../share/share.module';
import { MaterialModule } from '../material.module';
//
import { DashboardComponent } from './dashboard/dashboard.component';
import { MainComponent } from './main/main.component';
import { FeeComponent } from './fee/fee.component';
import { CategoryComponent } from './category/category.component';
import { InfoComponent } from './info/info.component';
import { OrderComponent } from './order/order.component';
import { ProductComponent } from './product/product.component';
import { PromotionComponent } from './promotion/promotion.component';


@NgModule({
  declarations: [
    MainComponent, 
    FeeComponent, 
    DashboardComponent, CategoryComponent, InfoComponent, OrderComponent, ProductComponent, PromotionComponent,
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    ShareModule,
    MaterialModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class AdminModule { }