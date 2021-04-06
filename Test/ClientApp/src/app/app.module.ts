import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoadingInterceptor } from './_services/loading.interceptor';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingPageModule, MaterialBarModule } from 'angular-loading-page';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
    ]),
    BsDatepickerModule.forRoot(),
    LoadingPageModule,
    NgxSpinnerModule,
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
