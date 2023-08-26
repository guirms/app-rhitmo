import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TelaCadastroComponent } from './pages/tela-cadastro/tela-cadastro.component';
import { MainScreenComponent } from './pages/main-screen/main-screen.component';

const routes: Routes = [
  {
    path: '', 
    pathMatch: 'full',
    component: MainScreenComponent
  },
  {
    path: 'cadastro', 
    component: TelaCadastroComponent
  },
  {
    path: '', 
    component: MainScreenComponent
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
