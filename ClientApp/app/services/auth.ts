import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { filter } from 'rxjs/operators';
import * as auth0 from 'auth0-js';
import { AnonymousSubscription } from 'rxjs/Subscription';
import { Http, RequestOptions, Headers } from '@angular/http';
import { JwtHelper } from 'angular2-jwt';

@Injectable()
export class AuthService {

  profile:any;
  private roles:string[] = [];

  public isInRole(roleName:any){
    if(!this.roles) return false;
    return this.roles.indexOf(roleName) > -1;
  }

  auth0 = new auth0.WebAuth({
    clientID: 'NJE8u27x7rKVib52aZ5tGsW2ymS9c7G4',
    domain: 'rodgars.auth0.com',
    responseType: 'id_token token',
    audience: 'https://api.vega.com',
    redirectUri: 'http://localhost:5000/callback',
    scope: 'openid email profile'
  });

  constructor(public router: Router, private http: Http) {
    this.loadFromStore();
  }

  public login(): void {
    this.auth0.authorize();
  }

  private getToken(): string {
    return localStorage.getItem('token') || ''; 
  }

  private loadFromStore(){
    if(localStorage.getItem('profile'))
      this.profile = JSON.parse(localStorage.getItem('profile') || '');

    var token = this.getToken()
    if(token != ''){
      var jwtHelper = new JwtHelper();
      var decodedToken = jwtHelper.decodeToken(token);
      this.roles = decodedToken['https://vega.com/roles'] || [];
    }
  }

  public handleAuthentication(): void {
    this.auth0.parseHash((err, authResult) => {
      if (authResult && authResult.accessToken && authResult.idToken) {
        window.location.hash = '';
        this.setSession(authResult);

        this.router.navigate(['/vehicles']);
      } else if (err) {
        this.router.navigate(['/home']);
        throw err;
      }
    });
  }

  private setSession(authResult:any): void {
    // Set the time that the Access Token will expire at
    const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
    localStorage.setItem('token', authResult.accessToken);
    localStorage.setItem('expires_at', expiresAt);

    this.auth0.client.userInfo(authResult.accessToken, (error, profile) => {
      if(error) throw error;
      localStorage.setItem('profile', JSON.stringify(profile));
      this.loadFromStore();
    });
  }

  public logout(): void {
    // Remove tokens and expiry time from localStorage
    localStorage.removeItem('token');
    localStorage.removeItem('expires_at');
    localStorage.removeItem('profile');
    this.profile = null;
    this.roles = [];
    // Go back to the home route
    this.router.navigate(['/']);
  }

  public isAuthenticated(): boolean {
    // Check whether the current time is past the
    // Access Token's expiry time
    const expiresAt = JSON.parse(localStorage.getItem('expires_at') || '0');
    return new Date().getTime() < expiresAt;
  }

}