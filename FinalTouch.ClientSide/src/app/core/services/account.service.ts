import { computed, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Address, User } from '../../shared/models/user';
import { map, tap } from 'rxjs';
import { SignalrService } from './signalr.service';



@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  private signalrService = inject(SignalrService);
  currentuser = signal<User | null>(null);
  isAdmin = computed(() => {
    const roles = this.currentuser()?.roles;
    return Array.isArray(roles) ? roles.includes('Admin') : roles === 'Admin';
  })


  login(values: any) {
    let params = new HttpParams();
    params = params.append('useCookies', true);
    return this.http.post<User>(this.baseUrl + 'login', values, { params }).pipe(
      tap(() => this.signalrService.createHubConnection())
    )
  }
  register(values: any) {
    return this.http.post<User>(this.baseUrl + 'Account/Register', values)
  }
  getUserInfo(){
    return this.http.get<User>(this.baseUrl + 'Account/user-info').pipe(
      map(user => {
        this.currentuser.set(user);
        return user;
      }
      )
    )
  }
  logout() {
    return this.http.post(this.baseUrl + 'account/logout', {}).pipe(
      tap(() => this.signalrService.stopHubConnection())
    )
  }

  updateAddress(address: Address){
    return this.http.post(this.baseUrl + 'Account/address', address).pipe(
      tap(() => {
        this.currentuser.update(user => {
          if (user) user.address = address;
          return user
        })
      })
    )
  }

  getAuthState() {
    return this.http.get<{ isAuthenticated: boolean }>(this.baseUrl + 'Account/auth-status')
  }
}


