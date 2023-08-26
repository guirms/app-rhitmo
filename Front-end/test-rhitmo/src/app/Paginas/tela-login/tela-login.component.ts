import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginModel } from 'src/app/Interfaces/login/login-model';
import { BaseService } from 'src/app/Servicos/base/base.service';
import { LoginService } from 'src/app/Servicos/login/login.service';

@Component({
  selector: 'app-tela-login',
  templateUrl: './tela-login.component.html',
  styleUrls: ['./tela-login.component.scss']
})
export class TelaLoginComponent implements OnInit {

  loginForm!: FormGroup;
  email!: string;
  senha!: string;
  lembrarSenha!: boolean;
  exibeSenha!: boolean;

  constructor(private formBuilder: FormBuilder,
    private loginService: LoginService,
    private router: Router,
    private toastrService: ToastrService,
    public baseService: BaseService<LoginModel>) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      id: [''],
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required]],
      lembrarSenha: ['', []],
    });
  }

  async loginSubmit(): Promise<void> {
    if (this.loginForm.valid && this.senha) {
      var requisicaoLogin = await this.loginService.fazerLogin(this.email, this.senha);
      if (requisicaoLogin.sucesso) {
        this.toastrService.success(requisicaoLogin.mensagem);
        this.router.navigate(['principal']);
      } 
      else {
        this.toastrService.error(requisicaoLogin.mensagem);
      }
    }
    else {
      this.toastrService.warning('Campos digitados incorretamente');
    }
  }

  exibirSenha(): void {
    this.exibeSenha = !this.exibeSenha;
  }

}
