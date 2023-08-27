import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TelaCadastroComponent } from './pages/tela-cadastro/tela-cadastro.component';
import { MainScreenComponent } from './pages/main-screen/main-screen.component';
import { CustomerRegistrationComponent } from './pages/customer-registration/customer-registration.component';

const routes: Routes = [
  {
    path: '', 
    pathMatch: 'full',
    component: MainScreenComponent
  },
  {
    path: 'teste', 
    component: TelaCadastroComponent
  },
  {
    path: '', 
    component: MainScreenComponent
  },
  {
    path: 'cadastro', 
    component: CustomerRegistrationComponent
  },
  {
    path: '**', 
    component: TelaCadastroComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
