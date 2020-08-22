import { Routes } from '@angular/router';

import { MapComponent } from './components/map/map.component';
import { CriteriasComponent } from './components/management/criterias/criterias.component';
import { CriteriasListResolver } from './resolvers/criterias.resolver';

export const appRoutes: Routes = [
    { path: '', component: MapComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        children: [
            { path: 'management/criterias', component: CriteriasComponent,
                resolve: { criterias: CriteriasListResolver } },
            // { path: 'management/reviews', component: ReviewsComponent },
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
