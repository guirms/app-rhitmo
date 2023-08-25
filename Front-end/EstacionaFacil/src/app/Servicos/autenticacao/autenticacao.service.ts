import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AutenticacaoService {

  constructor(private jwtHelper: JwtHelperService,
    private router: Router) { }

  tokenValido(): boolean {
    const token = localStorage.getItem('tokenSessao') ?? '';
    const isTokenInvalido = this.jwtHelper.isTokenExpired(token);

    if (isTokenInvalido) {
      this.router.navigate(['login']);
      return false;
    }

    return true;
  }

}
