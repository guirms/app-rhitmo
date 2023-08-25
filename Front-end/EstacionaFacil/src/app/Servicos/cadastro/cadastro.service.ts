import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { CadastroModel } from 'src/app/Interfaces/cadastro/cadastro-model';
import { ResponseBaseModel } from 'src/app/Interfaces/response-base/response-base-model';
import { environment } from 'src/environments/environment';
import { BaseService } from '../base/base.service';

@Injectable({
  providedIn: 'root'
})
export class CadastroService {

  constructor(private baseService: BaseService<ResponseBaseModel<CadastroModel>>) { }

  async fazerCadastro(email: string, senha: string, confirmacaoSenha: string): Promise<ResponseBaseModel<CadastroModel>> {
    const body = {
      Email: email,
      Senha: senha,
      ConfirmacaoSenha: confirmacaoSenha
    }

    this.baseService.setarExibeLoad(true);

    let requisicaoCadastro= await lastValueFrom(this.baseService.post(`${environment.url}Usuario/CadastrarUsuario`, body))
      .catch(ex => {
        return ex.message;
      });

    if (typeof (requisicaoCadastro) === 'string') {
      requisicaoCadastro = undefined;
      requisicaoCadastro = {
        mensagem: 'Erro durante requisição HTTP'
      }
    }

    this.baseService.setarExibeLoad(false);

    return requisicaoCadastro;
  }

}
