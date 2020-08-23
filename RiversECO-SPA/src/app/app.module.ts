import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
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
import { ReviewsComponent } from './components/management/reviews/reviews.component';
import { ReviewModalComponent } from './components/map/review-modal/review-modal.component';
import { CriteriaModalComponent } from './components/management/criterias/criteria-modal/criteria-modal.component';

import { WaterObjectService } from './services/water-object.service';
import { CriteriaService } from './services/criteria.service';
import { ReviewService } from './services/review.service';
import { UtilsService } from './services/utils.service';

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
    FormsModule,
    RouterModule.forRoot(appRoutes),
    ModalModule.forRoot()
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
