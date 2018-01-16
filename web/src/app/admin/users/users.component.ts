import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { UserViewModel } from '../../webapi/models/user-view-model';
import { UserServiceApp } from './shared/user.serviceApp';

@Component({
    selector: 'users',
    templateUrl: 'users.component.html'
})

export class UsersComponent implements OnInit {

    users: UserViewModel[] = [] as UserViewModel[];

    constructor(private userService: UserServiceApp, private router: Router) {
    }

    ngOnInit() {
       this.getAllUsers();
    }

    getAllUsers(){
        this.userService.getAllUsers().subscribe(
            (data) => {
                this.users = data.body;
            }
        )
    }

    addUser(){
        this.router.navigate(['User']);
    }

    updateUser(userId){
        this.router.navigate(['User', userId]);
    }

    deleteUser(userId){

    }
}
