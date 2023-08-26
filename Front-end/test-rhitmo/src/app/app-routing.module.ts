import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AutenticacaoGuard } from './Guardas/autenticacao/autenticacao.guard';
import { TelaCadastroComponent } from './Paginas/tela-cadastro/tela-cadastro.component';
import { TelaLoginComponent } from './Paginas/tela-login/tela-login.component';
import { TelaPrincipalComponent } from './Paginas/tela-principal/tela-principal.component';

const routes: Routes = [
  {
    path: '', 
    pathMatch: 'full',
    component: TelaLoginComponent
  },
  {
    path: 'login',
    component: TelaLoginComponent
  },
  {
    path: 'cadastro', 
    component: TelaCadastroComponent
  },
  {
    path: 'principal', 
    component: TelaPrincipalComponent,
    canActivate: [AutenticacaoGuard]
  },
  {
    path: '**', 
    component: TelaLoginComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
