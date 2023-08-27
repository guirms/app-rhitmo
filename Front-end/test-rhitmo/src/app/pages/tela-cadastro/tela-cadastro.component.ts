import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { BaseService } from 'src/app/services/base/base.service';

@Component({
  selector: 'app-tela-cadastro',
  templateUrl: './tela-cadastro.component.html',
  styleUrls: ['./tela-cadastro.component.scss']
})
export class TelaCadastroComponent {

  cadastroForm!: FormGroup;
  email!: string;
  senha!: string;
  confirmacaoSenha!: string;
  exibeSenha!: boolean;

  constructor(private formBuilder: FormBuilder,
    private toastrService: ToastrService,
    public baseService: BaseService) {}

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
      // if (requisicaoLogin.sucesso) {
      //   this.toastrService.success(requisicaoLogin.mensagem);
      //   this.router.navigate(['login']);
      // } 
      // else {
      //   this.toastrService.error(requisicaoLogin.mensagem);
      // }
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
