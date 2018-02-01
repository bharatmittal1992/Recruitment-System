import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';

// Modules
import { OpeningRouterModule } from './opening.route';

// Components
import { OpeningComponent, OpeningsComponent} from '../index.opening';

// Services
import { OpeningServiceApp } from './opening.serviceApp';

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        TranslateModule,
        OpeningRouterModule
    ],
    exports: [],
    declarations: [
        OpeningsComponent,
        OpeningComponent
    ],
    providers: [OpeningServiceApp],
})

export class OpeningModule {
}
