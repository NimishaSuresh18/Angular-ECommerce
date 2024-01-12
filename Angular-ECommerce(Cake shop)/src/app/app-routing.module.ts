import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Main/home/home.component';
import { LoginComponent } from './Credentials/login/login.component';
import { RegisterComponent } from './Credentials/register/register.component';
import { AddeditComponent } from './Main/addedit/addedit.component';
import { ProductlistComponent } from './Main/productlist/productlist.component';
import { UpdateComponent } from './Main/update/update.component';
import { ProductsComponent } from './Main/products/products.component';
import { DetailsComponent } from './Main/details/details.component';
import { CartComponent } from './Main/cart/cart.component';
import { OrderComponent } from './Main/order/order.component';
import { AboutComponent } from './Main/about/about.component';
import { ContactComponent } from './Main/contact/contact.component';
import { ProfileComponent } from './Credentials/profile/profile.component';
import { FeedbackComponent } from './Main/feedback/feedback.component';
import { MyordersComponent } from './Main/myorders/myorders.component';
import { AuthGuard } from './Guard/auth.guard';



const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'home',component:HomeComponent},
  {path:'customcakes',component:AboutComponent},
  {path:'contact',component:ContactComponent},
  {path:'login',component:LoginComponent},
  {path:'register',component:RegisterComponent},
  {path:'profile',component:ProfileComponent},
  {path:'add',component:AddeditComponent,canActivate:[AuthGuard]},
  {path:'list',component:ProductlistComponent,canActivate:[AuthGuard]},
  {path:'edit/:id',component:UpdateComponent,canActivate:[AuthGuard]},
  {path:'items',component:ProductsComponent},
  {path:'items/:id',component:DetailsComponent},
  {path:'cart',component:CartComponent},
  {path:'order',component:OrderComponent},
  {path:'myorders',component:MyordersComponent},
  {path:'feedback',component:FeedbackComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
