import { Injectable } from '@angular/core';
import { of, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Address, User } from '../shared/models/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUserSource$ = this.currentUserSource.asObservable();

  constructor(private http:HttpClient, private router:Router) { }

  loadCurrentUser(token:string | null){
    if(token === null){
      this.currentUserSource.next(null)
      return of(null)
    }

    let headers = new HttpHeaders
    headers = headers.set('Authorization',`Bearer ${token}`)

    return this.http
               .get<User| null>(this.baseUrl + 'account', {headers})
               .pipe(tap(user =>{
                if(user){
                  localStorage.setItem('token', user.token)
                  this.currentUserSource.next(user)
                  return user
                }else{
                  return null
                }
                  
             }))
  }

  login(values:any){
    return this.http
               .post<User>(this.baseUrl + 'account/login', values)
               .pipe(tap(user =>{
                  localStorage.setItem('token', user.token)
                  this.currentUserSource.next(user)
               }))
  }

  register(values:any){
    return this.http
               .post<User>(this.baseUrl + 'account/register', values)
               .pipe(tap(user =>{
                  localStorage.setItem('token', user.token)
                  this.currentUserSource.next(user)
                  return user
               }))
  }

  logout(){
    localStorage.removeItem('token')
    this.currentUserSource.next(null)
    this.router.navigateByUrl('/')
  }

  checkEmailExists(email:string){
    return this.http.get<boolean>(this.baseUrl + 'account/checkEmailExistsAsync?email='+ email)
  }

  getUserAddress(){
    return this.http.get<Address>(this.baseUrl + 'account/address');
  }

  updateUserAddress(address:Address){
    return this.http.put(this.baseUrl + 'account/address', address)
  }

}
