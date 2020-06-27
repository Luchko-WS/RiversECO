import { Routes } from '@angular/router';

import { MapComponent } from './components/map/map.component';
import { CriteriasComponent } from './components/management/criterias/criterias.component';

import { WaterObjectsListResolver } from './resolvers/water-objects-list.resolver';

export const appRoutes: Routes = [
    { path: '', component: MapComponent,
        resolve: { waterObjects: WaterObjectsListResolver } },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        children: [
            { path: 'management/criterias', component: CriteriasComponent },
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
