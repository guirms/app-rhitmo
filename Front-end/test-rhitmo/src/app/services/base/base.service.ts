import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, Observable, retry } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  public exibeLoad: boolean = false;

  constructor(private router: Router) { }

  navigate(url: string): void {
    this.router.navigateByUrl('cadastro');
  }

  setarExibeLoad(estadoLoad: boolean): void {
    this.exibeLoad = estadoLoad;
  }

}
