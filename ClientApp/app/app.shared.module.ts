import { PhotoService } from './services/photo.service';
import * as Raven from 'raven-js';
import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule, BrowserXhr } from '@angular/http';
import { RouterModule } from '@angular/router';
import { ToastyModule } from 'ng2-toasty';
import { ChartModule } from 'angular2-chartjs';

import { AppComponent } from './components/app/app.component';
import { AdminComponent } from './components/admin/admin.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { VehicleService } from './services/vehicle.service';
import { AppErrorHandler } from './app.error-handler';
import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component';
import { PaginationComponent } from './components/shared/pagination.component';
import { ViewVehicleComponent } from './components/view-vehicle/view-vehicle.component';
import { BrowserXhrWithProgress, ProgressService } from './services/progress.service';
import { AuthService } from './services/auth';
import { AuthGuard } from './services/auth-guard.service';
import { AdminAuthGuard } from './services/admin-auth-guard.service';
import { CallbackComponent } from './components/callback/callback.component';
import { AUTH_PROVIDERS } from 'angular2-jwt';

Raven
  .config('https://ffbdf487dbe54268a1ce9140ae59c890@sentry.io/1204095')
  .install();

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        VehicleFormComponent,
        VehicleListComponent,
        PaginationComponent,
        ViewVehicleComponent,
        AdminComponent,
        CallbackComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        ChartModule,
        FormsModule,
        ToastyModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
            { path: 'vehicles/new', component: VehicleFormComponent, canActivate: [AuthGuard] },
            { path: 'callback', component: CallbackComponent },
            { path: 'admin', component: AdminComponent, canActivate: [AdminAuthGuard] },
            { path: 'vehicles/edit/:id', component: VehicleFormComponent, canActivate: [AuthGuard] },
            { path: 'vehicles/:id', component: ViewVehicleComponent },
            { path: 'vehicles', component: VehicleListComponent },
            { path: '**', redirectTo: 'vehicles' }
        ])
    ],
    providers: [
        {
            provide: ErrorHandler,
            useClass: AppErrorHandler
        },
        AuthService,
        AUTH_PROVIDERS,
        AuthGuard,
        AdminAuthGuard,
        VehicleService,
        PhotoService
    ]
})
export class AppModuleShared {
}
