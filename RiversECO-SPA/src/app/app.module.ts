import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { ModalModule } from 'ngx-bootstrap/modal';

import { appRoutes } from './routes';

import { AppComponent } from './components/root/app.component';
import { NavComponent } from './components/nav/nav.component';
import { FooterComponent } from './components/footer/footer.component';
import { MapComponent } from './components/map/map.component';
import { CriteriasComponent } from './components/management/criterias/criterias.component';
import { ReviewModalComponent } from './components/review-modal/review-modal.component';

import { WaterObjectsListResolver } from './resolvers/water-objects-list.resolver';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    FooterComponent,
    MapComponent,
    CriteriasComponent,
    ReviewModalComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    RouterModule.forRoot(appRoutes),
    ModalModule.forRoot()
  ],
  providers: [
    WaterObjectsListResolver
  ],
  entryComponents: [
    ReviewModalComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
