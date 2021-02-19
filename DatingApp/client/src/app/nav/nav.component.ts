import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
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

  constructor(public accountService: AccountService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    // this.currentUser$ = this.accountService.currentUser$;
  }

  login() {
    this.accountService.login(this.model).subscribe(
      response => {//this is the first callback. it's for successfull actions.
        this.router.navigateByUrl('/members');
      });
  }

  logout() {//
    // this.loggedIn = false;//h
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }//

  // getCurrentUser() {
  //   this.accountService.currentUser$.subscribe(user => {
  //     this.loggedIn = !!user;//the '!!' is a shorthand for type defining the user to boolean. If the user variable is null it's falsey, otherwise it's truthy.
  //   },
  //     error => {
  //       console.log(error);
  //     });
  // }

}
