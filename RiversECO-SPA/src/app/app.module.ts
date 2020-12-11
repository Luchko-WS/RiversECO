import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { ServiceWorkerModule } from '@angular/service-worker';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';

import { ModalModule } from 'ngx-bootstrap/modal';

import { appRoutes } from './routes';

import { AppComponent } from './components/root/app.component';
import { NavComponent } from './components/nav/nav.component';
import { FooterComponent } from './components/footer/footer.component';
import { MapComponent } from './components/map/map.component';
import { CriteriasComponent } from './components/management/criterias/criterias.component';
import { ReviewsComponent } from './components/management/reviews/reviews.component';
import { ReviewModalComponent } from './components/map/review-modal/review-modal.component';
import { CriteriaModalComponent } from './components/management/criterias/criteria-modal/criteria-modal.component';

import { WaterObjectService } from './services/water-object.service';
import { CriteriaService } from './services/criteria.service';
import { ReviewService } from './services/review.service';
import { UtilsService } from './services/utils.service';

import { environment } from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    FooterComponent,
    MapComponent,
    CriteriasComponent,
    ReviewsComponent,
    CriteriaModalComponent,
    ReviewModalComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatFormFieldModule,
    RouterModule.forRoot(appRoutes),
    ModalModule.forRoot(),
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
  ],
  providers: [
    WaterObjectService,
    CriteriaService,
    ReviewService,
    UtilsService
  ],
  entryComponents: [
    ReviewModalComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
