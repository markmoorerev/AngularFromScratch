import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './Nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  // currentUser$: Observable<User>;

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    // this.currentUser$ = this.accountService.currentUser$;
  }

  login() {
    this.accountService.login(this.model).subscribe(
      response => {//this is the first callback. it's for successfull actions.
        console.log(response);
        // this.loggedIn = true;
      },
      error => {//the second callback function is for anything NOT a 200 response.
        console.log(error);
      });
  }

  logout() {
    // this.loggedIn = false;//h
    this.accountService.logout();
  }

  // getCurrentUser() {
  //   this.accountService.currentUser$.subscribe(user => {
  //     this.loggedIn = !!user;//the '!!' is a shorthand for type defining the user to boolean. If the user variable is null it's falsey, otherwise it's truthy.
  //   },
  //     error => {
  //       console.log(error);
  //     });
  // }

}
