import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ChangepasswordComponent } from './../changepassword.component';
import { DetailRouterModule } from './detail.route';
import { ChangepasswordServiceApp }from './changepassword.serviceApp';

@NgModule({
    declarations: [
        ChangepasswordComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        DetailRouterModule,
    ],
    providers: [ChangepasswordServiceApp]
})
export class DetailModule { }