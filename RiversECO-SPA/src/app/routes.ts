import { Routes } from '@angular/router';

import { MapComponent } from './components/map/map.component';
import { CriteriasComponent } from './components/management/criterias/criterias.component';
import { ReviewsComponent } from './components/management/reviews/reviews.component';

export const appRoutes: Routes = [
    { path: '', component: MapComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        children: [
            { path: 'management/criterias', component: CriteriasComponent },
            { path: 'management/reviews', component: ReviewsComponent },
            { path: 'management/reviews/:id', component: ReviewsComponent }
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
