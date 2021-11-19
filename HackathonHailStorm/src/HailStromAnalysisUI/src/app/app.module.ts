import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';

import { Routes, RouterModule } from '@angular/router';
import { MatTabsModule } from '@angular/material/tabs';
import { AppComponent } from './app.component';
import { StateComponent } from './state/state.component';
import { MonthComponent } from './month/month.component';
import { LocationComponent } from './location/location.component';


 const routes: Routes = [
{ path: '', redirectTo: 'state', pathMatch: 'full' },
  { path: 'state', component: StateComponent },
  { path: 'month', component: MonthComponent },
  { path: 'location', component: LocationComponent }
];

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    NoopAnimationsModule,
    FormsModule,
    MatTabsModule,
    RouterModule.forRoot(routes)
  ],
  declarations: [
    AppComponent, 
    StateComponent, 
    MonthComponent, 
    LocationComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
