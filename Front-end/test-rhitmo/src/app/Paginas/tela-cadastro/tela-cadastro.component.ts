import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CadastroModel } from 'src/app/Interfaces/cadastro/cadastro-model';
import { BaseService } from 'src/app/Servicos/base/base.service';
import { CadastroService } from 'src/app/Servicos/cadastro/cadastro.service';

@Component({
  selector: 'app-tela-cadastro',
  templateUrl: './tela-cadastro.component.html',
  styleUrls: ['./tela-cadastro.component.scss']
})
export class TelaCadastroComponent implements OnInit {

  cadastroForm!: FormGroup;
  email!: string;
  senha!: string;
  confirmacaoSenha!: string;
  exibeSenha!: boolean;

  constructor(private formBuilder: FormBuilder,
    private cadastroService: CadastroService,
    private router: Router,
    private toastrService: ToastrService,
    public baseService: BaseService<CadastroModel>) {}

  ngOnInit(): void {
    this.cadastroForm = this.formBuilder.group({
      id: [''],
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required]],
      confirmacaoSenha: ['', [Validators.required]],
    });
  }

  async cadastroSubmit(): Promise<void> {
    if (this.cadastroForm.valid && !this.senhasSaoDiferentes()) {
      var requisicaoLogin = await this.cadastroService.fazerCadastro(this.email, this.senha, this.confirmacaoSenha);
      if (requisicaoLogin.sucesso) {
        this.toastrService.success(requisicaoLogin.mensagem);
        this.router.navigate(['login']);
      } 
      else {
        this.toastrService.error(requisicaoLogin.mensagem);
      }
    }
    else {
      this.toastrService.warning('Campos digitados incorretamente');
    }
  }

  senhasSaoDiferentes(): boolean {
    return this.senha != this.confirmacaoSenha && !this.cadastroForm.get('confirmacaoSenha')?.errors?.required;
  }

  exibirSenha(): void {
    this.exibeSenha = !this.exibeSenha;
  }

}
