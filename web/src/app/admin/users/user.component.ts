import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { RoleViewModel } from '../../shared/customModels/role-view-model';
import { UserServiceApp } from './shared/user.serviceApp';
import { TranslateService } from '@ngx-translate/core';
import { isNullOrUndefined, error } from 'util';
import { UserModel } from '../../shared/customModels/user-model';
import { RoleServiceApp } from './shared/role.serviceApp';

@Component({
    selector: 'app-user',
    templateUrl: 'user.component.html'
})

export class UserComponent implements OnInit {

    userModel: UserModel = {} as UserModel;
    roles: RoleViewModel[] = [] as RoleViewModel[];
    passwordMismatchError: String;

    constructor(private userServiceApp: UserServiceApp, private route: ActivatedRoute,
        private router: Router, private translateService: TranslateService, private roleServiceApp: RoleServiceApp) {
    }

    ngOnInit() {
        this.intializeMethods();
    }

    intializeMethods() {
        this.getAllRole();
        this.getUserById();
    }

    getAllRole() {
        this.roleServiceApp.getAllRoles().subscribe(
            (data) => {
                this.roles = data.body;
            }
        );
    }

    showUsersList() {
        this.router.navigate(['Users']);
    }

    getUserById() {
        this.route.params.subscribe((params: Params) => {
            const userId = params['userId'];
            if (!isNullOrUndefined(userId)) {
                this.userServiceApp.getUserById(userId).subscribe(
                    (data) => {
                        this.userModel = data.body;
                        this.userModel.confirmPassword = this.userModel.password;
                    }
                );
            }
        });
    }

    onSubmit(userForm) {
        if (userForm.valid) {
            if (this.userModel.password === this.userModel.confirmPassword) {
                if (isNullOrUndefined(this.userModel.userId)) {
                    this.userServiceApp.createUser(this.userModel).subscribe(
                        (data) => {
                            this.showUsersList();
                        }
                    );
                } else {
                    this.userServiceApp.updateUser(this.userModel).subscribe(
                        (data) => {
                            this.showUsersList();
                        }
                    );
                }
            } else {
                this.translateService.get('USER.PASSWORDMISMATCH').subscribe(
                    data => {
                        this.passwordMismatchError = data;
                    });
            }
        }
    }
}

