import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './Credentials/login/login.component';
import { RegisterComponent } from './Credentials/register/register.component';
import { HomeComponent } from './Main/home/home.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { TokenInterceptor } from './interceptor/token.interceptor';
import { ApiService } from './Service/api.service';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProductsComponent } from './Main/products/products.component';
import { AddeditComponent } from './Main/addedit/addedit.component';
import { ProductlistComponent } from './Main/productlist/productlist.component';
import { NavbarComponent } from './side/navbar/navbar.component';
import { FooterComponent } from './side/footer/footer.component';
import { UpdateComponent } from './Main/update/update.component';
import { ProductService } from './Main/addedit/services/product.service';
import { DetailsComponent } from './Main/details/details.component';
import { OrderComponent } from './Main/order/order.component';
import { CartComponent } from './Main/cart/cart.component';
import { AboutComponent } from './Main/about/about.component';
import { ContactComponent } from './Main/contact/contact.component';
import { ProfileComponent } from './Credentials/profile/profile.component';
import { FeedbackComponent } from './Main/feedback/feedback.component';
import { SearchComponent } from './Main/search/search.component';
import { MyordersComponent } from './Main/myorders/myorders.component';
import { CarouselModule } from 'ngx-owl-carousel-o';



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AboutComponent,
    LoginComponent,
    RegisterComponent,
    AddeditComponent,
    ProductsComponent,
    ProductlistComponent,
    NavbarComponent,
    FooterComponent,
    UpdateComponent,
    ProductsComponent,
    DetailsComponent,
    OrderComponent,
    CartComponent,
    ContactComponent,
    ProfileComponent,
    FeedbackComponent,
    SearchComponent,
    MyordersComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    FormsModule,
    CarouselModule

  ],
  providers: [{
    provide : HTTP_INTERCEPTORS,
    useClass : TokenInterceptor,
    multi : true
  }, ApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }
