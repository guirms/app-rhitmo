import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtModule } from '@auth0/angular-jwt';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { TelaCadastroComponent } from './pages/tela-cadastro/tela-cadastro.component';
import { MainScreenComponent } from './pages/main-screen/main-screen.component';
import { CustomerRegistrationComponent } from './pages/customer-registration/customer-registration.component';
import { SearchFilterPipe } from './objects/pipes/search-filter.pipe';

export function tokenGetter() {
  return localStorage.getItem('tokenSessao');
}

@NgModule({
  declarations: [
    AppComponent,
    TelaCadastroComponent,
    MainScreenComponent,
    CustomerRegistrationComponent,
    SearchFilterPipe
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['localhost:4200'],
      },
    }),
  ],
  providers: [MatFormFieldModule],
  bootstrap: [AppComponent]
})

export class AppModule { }
